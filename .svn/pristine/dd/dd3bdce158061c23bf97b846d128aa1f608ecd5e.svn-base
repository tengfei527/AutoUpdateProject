using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace Au.Service
{
    partial class AuGuardService : ServiceBase
    {
        public AuGuardService()
        {
            InitializeComponent();
        }

        private bool IsRun = true;
        private System.Threading.Thread task = null;
        protected override void OnStart(string[] args)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true);
            try
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "Start.");
                sw.WriteLine("par:" + string.Join(",", args));
                string runpath = string.Empty;
                //string lo = System.Reflection.Assembly.GetCallingAssembly().Location;
                //sw.WriteLine(lo);
                switch (args?.Length > 0 ? args[0].ToLower() : "")
                {
                    case "-u":
                    case "/u":
                        {
                            //升级
                            string filepath = args?[1];
                            if (!System.IO.File.Exists(filepath))
                            {
                                sw.WriteLine(filepath + "不存在");
                                break;
                            }
                            runpath = args?[2];
                            if (!System.IO.File.Exists(runpath))
                            {
                                sw.WriteLine(runpath + "不存在");
                                break;
                            }
                            string err = string.Empty;
                            if (!CloseAuClient(out err))
                            {
                                sw.WriteLine("关闭进程[" + Au.Service.Properties.Resources.Core + "]失败," + err);
                                break;
                            }
                            try
                            {
                                System.IO.File.Copy(filepath, runpath, true);
                                //启动服务
                            }
                            catch (Exception e)
                            {
                                sw.WriteLine(e);
                            }
                            //关闭主进程
                        }
                        break;
                    case "/s":
                    case "-s":
                        {
                            //已在运行
                            if (IsRun && task != null && task.ThreadState == System.Threading.ThreadState.Running)
                            {
                                return;
                            }

                            runpath = args?[1];
                        }
                        break;
                }

                if (string.IsNullOrEmpty(runpath) && !System.IO.File.Exists(runpath))
                    return;

                task = new System.Threading.Thread(() =>
                 {
                     this.IsRun = true;

                     while (IsRun)
                     {
                         //判断是否存在Au.Client
                         CreateAuClient(runpath);
                         System.Threading.Thread.Sleep(1000);
                     }

                 });

                task.Start();

            }
            catch (Exception e)
            {
                sw.WriteLine(e);
                //sw.WriteLine("par:" + string.Join(",", args));
            }
            finally
            {
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        protected override void OnStop()
        {
            StopTask();
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "Stop.");
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }
        private void StopTask()
        {
            this.IsRun = false;

            if (task != null)
            {
                task.Join(2000);
            }
            if (task != null && task.ThreadState == System.Threading.ThreadState.Running)
            {
                try
                {
                    task.Abort();

                }
                catch
                {

                }
            }
            task = null;
        }

        private bool CloseAuClient(out string msg)
        {
            msg = string.Empty;
            try
            {
                StopTask();
                var process = System.Diagnostics.Process.GetProcessesByName(Au.Service.Properties.Resources.Core);
                if (process != null)
                    foreach (var p in process)
                    {
                        for (int i = 0; i < p.Threads.Count; i++)
                            p.Threads[i].Dispose();
                        p.Kill();
                    }
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
        private bool CreateAuClient(string path)
        {

            var process = System.Diagnostics.Process.GetProcessesByName(Au.Service.Properties.Resources.Core);
            if (process.Length == 0)
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
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
