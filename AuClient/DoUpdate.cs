using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuClient
{
    public class DoUpdate
    {
        /// <summary>
        /// 应用程序位置
        /// </summary>
        public string ApplicationPath { get; protected set; }
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string ApplicationName { get; protected set; }
        /// <summary>
        /// 日志接口
        /// </summary>
        public log4net.ILog log = log4net.LogManager.GetLogger(typeof(DoUpdate));
        public DoUpdate()
        {
            ApplicationName = System.IO.Path.GetFileNameWithoutExtension(AuClient.Properties.Resources.ApplicationService);
            ApplicationPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), AuClient.Properties.Resources.ApplicationService);
        }
        /// <summary>
        /// 启动 升级
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        public bool Start(string filepath, string runpath)
        {
            try
            {
                log.Info("准备启动 AuClient 升级……");
                var process = System.Diagnostics.Process.GetProcessesByName(this.ApplicationName);
                if (process.Length == 0)
                {
                    System.Diagnostics.ProcessStartInfo processInfo = new System.Diagnostics.ProcessStartInfo();
                    processInfo.FileName = this.ApplicationPath;
                    processInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(this.ApplicationPath);
                    processInfo.UseShellExecute = false;
                    processInfo.Arguments = string.Format("-u \"{0}\" \"{1}\"", filepath, runpath);
                    processInfo.CreateNoWindow = true;
                    processInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    var proc = System.Diagnostics.Process.Start(processInfo);
                    log.Info("成功启动 AuClient 升级");
                }
                else
                {
                    log.Info("已存在 AuClient 升级进程");
                }

                return true;
            }
            catch (Exception e)
            {
                log.Error("启动 AuClient 升级失败", e);
                Console.WriteLine(e);
            }

            return false;
        }
    }
}
