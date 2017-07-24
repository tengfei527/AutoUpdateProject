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
            switch (args?.Length > 0 ? args[0].ToLower() : "")
            {
                case "-u":
                case "/u":
                    {
                        MyServiceControll mc = new MyServiceControll();
                        mc.UninstallService();
                    }
                    break;
                default:
                    bool isRuned;
                    System.Threading.Mutex mutex = new System.Threading.Mutex(true, typeof(MainForm).FullName, out isRuned);
                    if (isRuned)
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new MainForm());
                    }
                    break;
            }


        }
    }
}
