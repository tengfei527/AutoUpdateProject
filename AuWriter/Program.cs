using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
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
                MessageBox.Show("程序已经在运行中, 您不能同时运行多个升级包发布服务！", "提示:");
                return;
            }
            else
            {
                Application.ThreadException += new ThreadExceptionEventHandler(MainUIThreadExceptionHandler);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(MainUIUnhandledExceptionHandler);
                //bool isRuned;
                //System.Threading.Mutex mutex = new System.Threading.Mutex(true, typeof(AuWriterForm).FullName, out isRuned);
                //if (isRuned)
                //{
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new AuWriterForm());
            }
        }

        public static void MainUIThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message + "\r\n" + e.Exception.StackTrace, "线程异常:", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void MainUIUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ExceptionObject.ToString(), "未处理的异常:", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
