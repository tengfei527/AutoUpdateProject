using System;
using System.Collections.Generic;
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
            //switch (args?.Length > 0 ? args[0].ToLower() : "")
            //{
            //    case "-u":
            //    case "/u":
            //        {
            //            //MyServiceControll mc = new MyServiceControll();
            //            //mc.UninstallService();
            //        }
            //        break;
            //    default:
            bool isRuned;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, typeof(MainForm).FullName, out isRuned);
            if (isRuned)
            {
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
