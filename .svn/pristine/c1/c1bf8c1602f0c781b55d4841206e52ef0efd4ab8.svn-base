using System;
using System.Collections.Generic;
using System.Text;

namespace AU.Common.Utility
{
    public class CmdUtility
    {
        /// <summary>
        /// 进程
        /// </summary>
        public System.Diagnostics.Process MyProcess { get; private set; }
        /// <summary>
        /// 应用程序名称
        /// </summary>
        /// <param name="application"></param>
        public CmdUtility(string application = "cmd.exe", params string[] args)
        {
            MyProcess = new System.Diagnostics.Process();
            //设定程序名
            MyProcess.StartInfo.FileName = application;
            MyProcess.StartInfo.Arguments = args?.Length > 0 ? args[0] : "";
            //关闭Shell的使用
            MyProcess.StartInfo.UseShellExecute = false;
            //重定向标准输入
            MyProcess.StartInfo.RedirectStandardInput = true;
            //重定向标准输出
            MyProcess.StartInfo.RedirectStandardOutput = true;
            //重定向错误输出
            MyProcess.StartInfo.RedirectStandardError = true;
            //设置不显示窗口
            MyProcess.StartInfo.CreateNoWindow = true;

            //执行VER命令
            MyProcess.Start();
            // Start the asynchronous read of the sort output stream.
            MyProcess.BeginOutputReadLine();
            MyProcess.BeginErrorReadLine();
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="cmd"></param>
        public void Run(string cmd)
        {
            if (MyProcess != null)
                MyProcess.StandardInput.WriteLine(cmd);
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            if (MyProcess == null)
                return;
            MyProcess.Close();
            MyProcess.Dispose();
        }
    }
}
