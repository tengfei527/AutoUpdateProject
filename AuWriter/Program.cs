using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace AuWriter
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
                            AU.Common.Utility.ToolsHelp.CloseApplication("AuWriter.exe", System.Diagnostics.Process.GetCurrentProcess().Id);

                        }
                        catch
                        {

                        }
                        return;
                    }
                default:
                    break;
            }
            string proc = Process.GetCurrentProcess().ProcessName;
            Process[] processes = Process.GetProcessesByName(proc);
            if (processes.Length >= 2)
            {
                MessageBox.Show("系统中已经有一个程序进程在运行, 您不能同时运行多个实例.", "提示:");
                return;
            }
            else
            {
                //bool isRuned;
                //System.Threading.Mutex mutex = new System.Threading.Mutex(true, typeof(AuWriterForm).FullName, out isRuned);
                //if (isRuned)
                //{
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new AuWriterForm());
            }
        }
    }
}
