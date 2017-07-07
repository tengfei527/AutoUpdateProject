using AU.Common.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;

namespace AU.Common
{
    public class AppRemotePublish : IDisposable
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
        /// 配置名称
        /// </summary>
        public static readonly string PackageName = "aupublish.json";
        /// <summary>
        /// 发布地址
        /// </summary>
        public string PublishAddress { get; set; }
        /// <summary>
        /// 本地包地址
        /// </summary>
        public string LocalPath { get; set; }
        #endregion
        public AppRemotePublish(string publishaddress, string localPath)
        {
            this.LocalPath = localPath;
            this.PublishAddress = publishaddress + "/package";
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
        ~AppRemotePublish()
        {
            Dispose(false);
        }
        /// <summary>
        /// 检查更新文件
        /// </summary>
        /// <param name="updatePackage"></param>
        /// <returns></returns>
        public int CheckForUpdate(string subsystem, AuPublish remote)
        {
            var local = AppPublish.ReadPackage(this.LocalPath + "\\" + subsystem + "\\" + PackageName);
            //本地空
            if (local == null)
                return 1;
            if (remote == null)
                return -1;
            //有更新
            if (remote.No.CompareTo(local.No) > 0)
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
        /// 下载文件
        /// </summary>
        /// <param name="upgradeFiles"></param>
        /// <returns></returns>
        public string DownUpdateFile(string subSystem, AuPublish upgradeFiles, bool allowPublish = false)
        {
            if (upgradeFiles == null)
            {
                return string.Empty;
            }

            try
            {
                int index = 0;
                string down = string.Format("{0}{1}/{2}", upgradeFiles.Url.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) ? "" : "http://", upgradeFiles.Url, upgradeFiles.DownPath);
                NotifyMessage(new Common.NotifyMessage(NotifyType.StartDown, "开始下载文件"));

                string tempPath = Environment.GetEnvironmentVariable("Temp") + "\\" + "_" + upgradeFiles.SHA256 + "\\" + upgradeFiles.DownPath;
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
                if (allowPublish)
                    return this.Publis(subSystem, upgradeFiles, tempPath);

                return tempPath;
            }
            catch (WebException ex)
            {
                NotifyMessage(new Common.NotifyMessage(NotifyType.Error, "更新文件下载失败", ex));
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
        public string Publis(string subSystem, AuPublish auPublish, string filePath)
        {
            //发布地址转换]
            auPublish.Url = this.PublishAddress + "/" + subSystem;
            string targetPath = this.LocalPath + "\\package\\" + subSystem + "\\" + auPublish.DownPath;
            ToolsHelp.CreateDirtory(targetPath);
            File.Copy(filePath, targetPath, true);
            //写发布
            StreamWriter swau = new StreamWriter(this.LocalPath + "\\package\\" + subSystem + "\\" + AppPublish.PackageName, false, System.Text.Encoding.UTF8);
            swau.Write(Newtonsoft.Json.JsonConvert.SerializeObject(auPublish));
            swau.Close();
            //删除临时目录
            System.IO.Directory.Delete(System.IO.Path.GetDirectoryName(filePath), true);

            return targetPath;
        }
    }
}
