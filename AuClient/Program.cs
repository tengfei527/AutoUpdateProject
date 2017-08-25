using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace AuClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            switch (args?.Length > 0 ? args[0].ToLower() : "")
            {
                case "-u":
                case "/u":
                    {
                        try
                        {
                            AU.Common.Utility.ToolsHelp.CloseApplication("AuClient.exe", System.Diagnostics.Process.GetCurrentProcess().Id);

                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        return;
                    }
                case "-r":
                case "/r":
                    {
                        do
                        {
                            try
                            {
                                AU.Common.Utility.ToolsHelp.CloseApplication("AuClient.exe", System.Diagnostics.Process.GetCurrentProcess().Id);

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }

                            System.Threading.Thread.Sleep(2000);
                        } while (AU.Common.Utility.ToolsHelp.IsRunApplication("AuClient.exe", System.Diagnostics.Process.GetCurrentProcess().Id));
                    }
                    break;
                default:
                    break;
            }
            string proc = Process.GetCurrentProcess().ProcessName;
            Process[] processes = Process.GetProcessesByName(proc);
            if (processes.Length >= 2)
            {
                MessageBox.Show("程序已经在运行中, 您不能同时运行多个升级服务！", "提示:");
                return;
            }
            else
            {
                //    bool isRuned;

                //System.Threading.Mutex mutex = new System.Threading.Mutex(true, typeof(MainForm).FullName, out isRuned);
                //if (isRuned)
                //{
                try
                {
                    string path = System.IO.Path.Combine(Application.StartupPath, "AuClient.exe.config");
                    if (!System.IO.File.Exists(path))
                    {
                        byte[] bu = System.Text.Encoding.UTF8.GetBytes(AuClient.Properties.Resources.App);
                        using (System.IO.FileStream fs = System.IO.File.Create(path))
                        {
                            fs.Write(bu, 0, bu.Length);
                            fs.Flush();
                            fs.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                try
                {
                    byte[] bu = AuClient.Properties.Resources.AuWizard;
                    string path = System.IO.Path.Combine(Application.StartupPath, AuClient.Properties.Resources.ApplicationConfig);
                    AU.Common.Utility.ToolsHelp.CloseApplication(System.IO.Path.GetFileNameWithoutExtension(AuClient.Properties.Resources.ApplicationConfig));

                    using (System.IO.FileStream fs = System.IO.File.Create(path))
                    {
                        fs.Write(bu, 0, bu.Length);
                        fs.Flush();
                        fs.Close();
                    }
                    while (!AU.Common.Utility.IpHelp.IsValidIPEndPoint(System.Configuration.ConfigurationManager.AppSettings
                        ["SocketServer"] ?? ""))
                    {

                        //启动配置
                        AU.Common.Utility.ToolsHelp.CreateApplication(path, true);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                try
                {
                    byte[] bu = AuClient.Properties.Resources.AuUpdate;
                    string path = System.IO.Path.Combine(Application.StartupPath, AuClient.Properties.Resources.ApplicationService);
                    AU.Common.Utility.ToolsHelp.CloseApplication(System.IO.Path.GetFileNameWithoutExtension(AuClient.Properties.Resources.ApplicationService));

                    using (System.IO.FileStream fs = System.IO.File.Create(path))
                    {
                        fs.Write(bu, 0, bu.Length);
                        fs.Flush();
                        fs.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            //        break;
            //}


        }
    }
}
