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
    public class AppPublish : IDisposable
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
        /// 本地包配置
        /// </summary>
        public AuPublish MyPublish { get; set; }
        /// <summary>
        /// 配置名称
        /// </summary>
        public static readonly string PackageName = "aupublish.json";
        /// <summary>
        /// 本地配置路径
        /// </summary>
        public string LocalPath { get; private set; }
        /// <summary>
        /// 本地包路径
        /// </summary>
        public string PackagePath { get; private set; }
        /// <summary>
        /// 更新地址
        /// </summary>
        public string UpdaterUrl { get; set; }
        /// <summary>
        /// 发布地址
        /// </summary>
        public string PublishAddress { get; set; }
        /// <summary>
        /// 子系统
        /// </summary>
        public string SubSystem { get; set; }
        #endregion
        public AppPublish()
        {

        }
        /// <summary>
        /// AppUpdater构造函数
        /// </summary>
        /// <param name="subsystem">子系统</param>
        /// <param name="path">配置文件的相对路径</param>
        /// <param name="publishaddress">发布地址</param>
        public AppPublish(string subsystem, string path, string publishaddress)
        {
            this.SubSystem = subsystem;
            this.PublishAddress = publishaddress;
            this.LocalPath = path;
            this.PackagePath = this.LocalPath + "\\" + PackageName;

            this.MyPublish = ReadPackage(this.PackagePath);
            if (this.MyPublish != null)
                this.UpdaterUrl = this.MyPublish.Url + "/" + PackageName;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public AuPublish ReadPackage(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return null;
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
                {
                    string json = sr.ReadToEnd();
                    sr.Close();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<AuPublish>(json);
                }
            }
            catch
            {

                return null;
            }
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
        ~AppPublish()
        {
            Dispose(false);
        }
        string TempUpdatePath = string.Empty;
        /// <summary>
        /// 检查更新文件
        /// </summary>
        /// <param name="updatePackage"></param>
        /// <returns></returns>
        public int CheckForUpdate(out AuPublish updatePackage)
        {
            updatePackage = null;
            if (this.MyPublish == null)
                return -1;
            //临时目录
            this.TempUpdatePath = Environment.GetEnvironmentVariable("Temp") + "\\" + "_" + this.MyPublish.SHA256 + "\\";
            //异常处理
            string package = ToolsHelp.DownAutoUpdateFile(this.UpdaterUrl, this.TempUpdatePath, AppPublish.PackageName);
            //this.DownAutoUpdateFile(tempUpdatePath);
            if (!File.Exists(package))
            {
                return -1;
            }

            updatePackage = ReadPackage(package);
            if (updatePackage == null)
                return -1;
            //有更新
            if (updatePackage.No.CompareTo(this.MyPublish.No) > 0)
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="state"></param>
        private void NotifyMessage(NotifyMessage state)
        {
            if (Notify != null)
            {
                Notify.Invoke(this, state);
            }
        }
        /// <summary>
        /// 是否下载
        /// </summary>
        public bool IsDownLoad = false;
        /// <summary>
        /// 是否发布
        /// </summary>
        public bool IsPublish = false;

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="upgradeFiles"></param>
        /// <returns></returns>
        public string DownUpdateFile(AuPublish upgradeFiles, bool allowPublish = false)
        {
            if (upgradeFiles == null)
            {
                IsDownLoad = false;
                return string.Empty;
            }

            try
            {
                int index = 0;
                string down = string.Format("{0}{1}/{2}", upgradeFiles.Url.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) ? "" : "http://", upgradeFiles.Url, upgradeFiles.DownPath);
                NotifyMessage(new Common.NotifyMessage(NotifyType.StartDown, "开始下载文件"));
                string tempPath = this.TempUpdatePath + "\\" + upgradeFiles.DownPath;
                ToolsHelp.CreateDirtory(tempPath);

                WebRequest webReq = WebRequest.Create(down);
                WebResponse webRes = webReq.GetResponse();

                NotifyMessage(new Common.NotifyMessage(NotifyType.Process, "正在下载[" + upgradeFiles.DownPath + "]文件,请稍后...", 100000));
                Stream srm = webRes.GetResponseStream();
                Stream outStream = System.IO.File.Create(tempPath);
                byte[] buffer = new byte[1024];
                int l;
                int startByte = 0;
                do
                {
                    l = srm.Read(buffer, 0, buffer.Length);
                    if (l > 0)
                    {
                        startByte += l;
                        outStream.Write(buffer, 0, l);
                        NotifyMessage(new Common.NotifyMessage(NotifyType.UpProcess, index + ":" + Convert.ToInt32((startByte / 100000) * 100).ToString() + "%", l));
                    }
                }
                while (l > 0);
                outStream.Flush();
                outStream.Close();
                srm.Close();
                index++;
                IsDownLoad = true;
                if (allowPublish)
                    return this.Publis(upgradeFiles, tempPath);

                return tempPath;
            }
            catch (WebException ex)
            {
                NotifyMessage(new Common.NotifyMessage(NotifyType.Error, "更新文件下载失败", ex));
                IsDownLoad = false;
                return string.Empty;
            }
            finally
            {
                NotifyMessage(new Common.NotifyMessage(NotifyType.StopDown, "更新完成"));
            }
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="auPublish"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string Publis(AuPublish auPublish, string filePath)
        {
            //发布地址转换
            auPublish.Url = this.PublishAddress + "/" + this.SubSystem;
            string targetPath = this.LocalPath + "\\package\\" + this.SubSystem + "\\" + auPublish.DownPath;
            ToolsHelp.CreateDirtory(targetPath);
            File.Copy(filePath, targetPath, true);
            //写发布
            StreamWriter swau = new StreamWriter(this.LocalPath + "\\package\\" + this.SubSystem + "\\" + AppPublish.PackageName, false, System.Text.Encoding.UTF8);
            swau.Write(Newtonsoft.Json.JsonConvert.SerializeObject(auPublish));
            swau.Close();
            //更新本地包
            File.Copy(this.TempUpdatePath + AppPublish.PackageName, this.PackagePath, true);
            //删除临时目录
            System.IO.Directory.Delete(this.TempUpdatePath, true);

            return targetPath;
        }
    }
}
