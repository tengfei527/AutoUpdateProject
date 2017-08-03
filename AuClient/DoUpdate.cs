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
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return false;
        }
    }
}
