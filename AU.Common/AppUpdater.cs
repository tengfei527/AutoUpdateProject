using System;
using System.Web;
using System.IO;
using System.Net;
using System.Xml;
using System.Collections;
using System.ComponentModel;
using AU.Common.Utility;
using AU.Common;
using System.Collections.Generic;
using System.Diagnostics;

namespace AU.Common
{
    /// <summary>
    /// updater 的摘要说明。
    /// </summary>
    public class AppUpdater : IDisposable
    {
        #region 成员与字段属性
        private bool disposed = false;
        private IntPtr handle;
        private Component component = new Component();
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private extern static Boolean CloseHandle(IntPtr handle);
        /// <summary>
        /// 消息通知
        /// </summary>
        public event EventHandler<NotifyMessage> Notify;
        /// <summary>
        /// 新版本包信息
        /// </summary>
        public AuPackage UpdateAuPackage { get; set; }
        /// <summary>
        /// 目标待升级包信息
        /// </summary>
        public AuPackage TargetAuPackage { get; set; }
        /// <summary>
        /// 备份路径
        /// </summary>
        public string AuBackupPath { get; set; }
        #endregion

        /// <summary>
        ///  AppUpdater构造函数
        /// </summary>
        /// <param name="targetPath">待升级包信息</param>
        /// <param name="updatePath">新版本包信息</param>
        public AppUpdater(string targetPath, string updatePath, string aubackupPath)
        {
            this.AuBackupPath = aubackupPath;
            //this.handle = handle;
            this.UpdateAuPackage = new AuPackage(updatePath);

            this.TargetAuPackage = new AuPackage(targetPath);
        }
        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                    component.Dispose();
                }
                CloseHandle(handle);
                handle = IntPtr.Zero;
            }
            disposed = true;
        }
        /// <summary>
        /// 析构函数
        /// </summary>
        ~AppUpdater()
        {
            Dispose(false);
        }

        /// <summary>
        /// 检查更新文件
        /// </summary>
        /// <param name="serverXmlFile"></param>
        /// <param name="localXmlFile"></param>
        /// <param name="updateFileList"></param>
        /// <returns></returns>
        public int CheckForUpdate(out AuPackage updatePackage)
        {
            updatePackage = null;
            if (this.UpdateAuPackage.LocalAuList == null)
                return -1;
            if (this.TargetAuPackage.LocalAuList == null)
            {
                updatePackage = this.UpdateAuPackage;
                return updatePackage.LocalAuList.Files.Count;
            }
            int k = 0;
            List<AuFile> updateFileList = new List<AuFile>();
            //新增 修改
            foreach (var m in this.UpdateAuPackage.LocalAuList.Files)
            {
                var index = this.TargetAuPackage.LocalAuList.Files.FindIndex(d => d.No == m.No);
                if (index == -1)//新增文件
                {
                    updateFileList.Add(m);
                    k++;
                }//服务器版本高更新
                else if (index > -1 && m.Version.CompareTo(this.TargetAuPackage.LocalAuList.Files[index].Version) > 0)
                {
                    updateFileList.Add(m);
                    k++;
                }
            }
            if (k > 0)
            {
                updatePackage = this.UpdateAuPackage;
                updatePackage.LocalAuList.Files = updateFileList;
            }
            return k;
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="state"></param>
        private void NotifyMessage(NotifyMessage state)
        {
            if (Notify != null)
            {
                //if (Notify.Target is System.Windows.Forms.Control)
                //{
                //    var c = Notify.Target as System.Windows.Forms.Control;
                //    try
                //    {
                //        c.Invoke((System.Windows.Forms.MethodInvoker)delegate ()
                //        {
                //            Notify(this, state);
                //        });
                //    }
                //    catch (Exception e)
                //    {
                //        //this.loge.Error("[" + this.ServiceType.ToString() + "】发送通知异常", e);
                //    }
                //}
                //else
                //{
                Notify.Invoke(this, state);
                //}
            }
        }
        /// <summary>
        /// 已升级
        /// </summary>
        public bool IsUpgrade = false;
        /// <summary>
        /// 升级
        /// </summary>
        /// <param name="obj"></param>
        public void Upgrade(object obj)
        {
            AuPackage upgradeFiles = obj as AuPackage;
            if (upgradeFiles == null || upgradeFiles.LocalAuList == null)
            {
                this.IsUpgrade = false;
                return;
            }

            NotifyMessage(new Common.NotifyMessage(NotifyType.StartDown, "开始升级文件"));

            try
            {
                if (this.TargetAuPackage.LocalAuList != null && this.TargetAuPackage.LocalAuList.Application.StartType == 1)
                {
                    Process[] allProcess = Process.GetProcesses();
                    foreach (Process p in allProcess)
                    {

                        if (p.ProcessName.ToLower() + ".exe" == this.TargetAuPackage.LocalAuList.Application.EntryPoint.ToLower())
                        {
                            for (int i = 0; i < p.Threads.Count; i++)
                                p.Threads[i].Dispose();
                            p.Kill();
                        }
                    }
                }
                //清除备份路径
                if (System.IO.Directory.Exists(this.AuBackupPath))
                {
                    System.IO.Directory.Move(this.AuBackupPath, this.AuBackupPath.TrimEnd('\\') + "_" + DateTime.Now.ToString("yyyyMMddHHmm"));
                }

                int index = 0;

                foreach (var m in upgradeFiles.LocalAuList.Files)
                {
                    try
                    {
                        NotifyMessage(new Common.NotifyMessage(NotifyType.Process, "正在处理[" + m.No + "]文件,请稍后...", 1));
                        //本地路径
                        string path = this.TargetAuPackage.LocalPath + "\\" + m.WritePath;
                        if (System.IO.File.Exists(path))
                        {
                            //备份
                            string bak = AuBackupPath + m.WritePath;
                            ToolsHelp.CreateDirtory(bak);
                            System.IO.File.Copy(path, bak, true);
                        }
                        NotifyMessage(new Common.NotifyMessage(NotifyType.UpProcess, index + ":100%", 1));
                        index++;
                    }
                    catch (WebException ex)
                    {
                        NotifyMessage(new Common.NotifyMessage(NotifyType.Error, "更新文件下载失败", ex));
                        this.IsUpgrade = false;
                        return;
                    }
                }
                //备份目标包信息
                if (System.IO.File.Exists(this.TargetAuPackage.PackagePath))
                {
                    System.IO.File.Copy(this.TargetAuPackage.PackagePath, this.AuBackupPath + "\\" + AuPackage.PackageName, true);
                }
                this.IsUpgrade = true;
            }
            catch (Exception e)
            {
                NotifyMessage(new Common.NotifyMessage(NotifyType.Error, "关闭主线程失败", e));

                this.IsUpgrade = false;
                return;
            }
            finally
            {
                NotifyMessage(new Common.NotifyMessage(NotifyType.StopDown, "更新完成"));
            }
            return;
        }

        /*
    public void DownUpdateFile(object obj)
    {
        AuPackage upgradeFiles = obj as AuPackage;
        if (upgradeFiles == null || upgradeFiles.LocalAuList == null)
        {
            IsDownLoad = false;
            return;
        }

        NotifyMessage(new Common.NotifyMessage(NotifyType.StartDown, "开始下载文件"));

        try
        {
            Process[] allProcess = Process.GetProcesses();
            foreach (Process p in allProcess)
            {

                if (p.ProcessName.ToLower() + ".exe" == MyAuPackage.LocalAuList.Application.EntryPoint.ToLower())
                {
                    for (int i = 0; i < p.Threads.Count; i++)
                        p.Threads[i].Dispose();
                    p.Kill();
                }
            }
            int index = 0;
            foreach (var m in upgradeFiles.LocalAuList.Files)
            {
                try
                {

                    string down = upgradeFiles.LocalAuList.Url.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) ? upgradeFiles.LocalAuList.Url + "/" + m.DownPath : "http://" + upgradeFiles.LocalAuList.Url + "/" + m.DownPath;
                    WebRequest webReq = WebRequest.Create(down);
                    WebResponse webRes = webReq.GetResponse();
                    long fileLength = webRes.ContentLength;
                    if (fileLength < 0)
                        continue;


                    NotifyMessage(new Common.NotifyMessage(NotifyType.Process, "正在下载[" + m.No + "]文件,请稍后...", fileLength));

                    Stream srm = webRes.GetResponseStream();
                    StreamReader srmReader = new StreamReader(srm);
                    byte[] bufferbyte = new byte[fileLength];
                    int allByte = (int)bufferbyte.Length;
                    int startByte = 0;
                    while (fileLength > 0)
                    {
                        int downByte = srm.Read(bufferbyte, startByte, allByte);
                        if (downByte == 0) { break; };
                        startByte += downByte;
                        allByte -= downByte;

                        float part = (float)startByte / 1024;
                        float total = (float)bufferbyte.Length / 1024;
                        int percent = Convert.ToInt32((part / total) * 100);


                        NotifyMessage(new Common.NotifyMessage(NotifyType.UpProcess, index + ":" + percent.ToString() + "%", downByte));
                        //this.Invoke((MethodInvoker)delegate ()
                        //{
                        //    pbDownFile.Value += downByte;
                        //    this.lvUpdateList.Items[i].SubItems[2].Text = percent.ToString() + "%";
                        //});

                    }

                    string tempPath = upgradeFiles.LocalPath + "\\" + m.WritePath;
                    ToolsHelp.CreateDirtory(tempPath);
                    FileStream fs = new FileStream(tempPath, FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(bufferbyte, 0, bufferbyte.Length);
                    srm.Close();
                    srmReader.Close();
                    fs.Close();
                    index++;
                }
                catch (WebException ex)
                {
                    NotifyMessage(new Common.NotifyMessage(NotifyType.Error, "更新文件下载失败", ex));
                    IsDownLoad = false;
                    return;
                }
            }
        }
        catch (Exception e)
        {
            NotifyMessage(new Common.NotifyMessage(NotifyType.Error, "关闭主线程失败", e));

            IsDownLoad = false;
            return;
        }
        finally
        {
            NotifyMessage(new Common.NotifyMessage(NotifyType.StopDown, "更新完成"));
            //this.Invoke((MethodInvoker)delegate ()
            //{
            //    InvalidateControl();
            //    this.Cursor = Cursors.Default;
            //});
        }
        //isDownLoad = true;
        IsDownLoad = true;
        return;
    }
    */


    }
}
