using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuUpdate
{
    public class AuUpdateHelp
    {
        public string ApplicationName { get; protected set; }
        public AuUpdateHelp(string applicationName)
        {
            this.ApplicationName = applicationName;
        }
        /// <summary>
        /// 关闭客户端
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool CloseAuClient(out string msg)
        {
            msg = string.Empty;
            try
            {
                var process = System.Diagnostics.Process.GetProcessesByName(this.ApplicationName);
                if (process != null)
                    foreach (var p in process)
                    {
                        for (int i = 0; i < p.Threads.Count; i++)
                            p.Threads[i].Dispose();
                        p.Kill();
                    }

                System.Threading.Thread.Sleep(3000);
            }
            catch (Exception e)
            {
                msg = e.Message;
                return false;
            }

            return true;
        }

        /// <summary>
        /// 创建AuClient(
        /// </summary>
        /// <param name="path"></param>
        public bool CreateAuClient(string path)
        {

            var process = System.Diagnostics.Process.GetProcessesByName(this.ApplicationName);
            if (process.Length == 0 && System.IO.File.Exists(path))
            {
                try
                {
                    System.Diagnostics.ProcessStartInfo processInfo = new System.Diagnostics.ProcessStartInfo();
                    processInfo.FileName = path;
                    //processInfo.Verb = "runas";
                    processInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(path);
                    processInfo.UseShellExecute = false;
                    //processInfo.CreateNoWindow = true;
                    var proc = System.Diagnostics.Process.Start(processInfo);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(DateTime.Now.ToString() + ":" + e);
                }
            }
            else
            {
                Console.WriteLine(DateTime.Now.ToString() + ":" + this.ApplicationName + "进程已存在!");
            }

            return false;
        }

        public bool Upgrade(string filepath, string runpath)
        {
            string err = string.Empty;
            if (!CloseAuClient(out err))
            {
                Console.WriteLine(DateTime.Now.ToString() + ":" + "关闭进程[" + this.ApplicationName + "]失败," + err);

                return false;
            }
            try
            {
                System.IO.File.Copy(filepath, runpath, true);
                //启动服务
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now.ToString() + ":" + e);

                return false;
            }
        }
    }
}
