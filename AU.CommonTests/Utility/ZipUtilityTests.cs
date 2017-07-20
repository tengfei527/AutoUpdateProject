using Microsoft.VisualStudio.TestTools.UnitTesting;
using AU.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace AU.Common.Utility.Tests
{
    [TestClass()]
    public class ZipUtilityTests
    {
        public string password = "123";
        [TestMethod()]
        public void ComTest()
        {
            int count = ZipUtility.Compress(@"C:\Users\ftf\Desktop\F6", @"C:\Users\ftf\Desktop\F6.zip", ZipUtility.CompressLevel.Level6, password: password);
            Assert.IsTrue(count > 0);
        }


        [TestMethod()]
        public void DecompressTest()
        {

            bool result = ZipUtility.Decompress(@"C:\Users\ftf\Desktop\F6.zip", @"C:\Users\ftf\Desktop\F62", password);
            Assert.IsTrue(result);
        }
        [TestMethod()]
        public void FileTest()
        {
            var f = File.Open(@"C:\Users\ftf\Desktop\test.txt", FileMode.Open);
            var f2 = File.Open(@"C:\Users\ftf\Desktop\test2.txt", FileMode.OpenOrCreate);
            byte[] buff = new byte[1024];
            int count = 0;
            do
            {
                count = f.Read(buff, 0, 1024);
                if (count == 1024)
                {
                    string b = byteToHexStr(buff);
                    var t = Encoding.UTF8.GetBytes(b);
                    var s = Encoding.UTF8.GetString(t);
                    byte[] temp1 = strToToHexByte(s);
                    f2.Write(temp1, 0, temp1.Length);
                    f2.Flush();
                }
                else if (count > 0)
                {
                    byte[] temp = new byte[count];
                    Array.Copy(buff, 0, temp, 0, count);
                    string b = byteToHexStr(temp);
                    var t = Encoding.UTF8.GetBytes(b);

                    var s = Encoding.UTF8.GetString(t);
                    byte[] temp1 = strToToHexByte(s);
                    f2.Write(temp1, 0, temp1.Length);
                    f2.Flush();
                }

            } while (count != 0);
            f.Close();
            f2.Close();
        }

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name=”bytes”></param>
        /// <returns></returns>
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("x2");
                }
            }
            return returnStr;
        }

        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name=”hexString”></param>
        /// <returns></returns>
        public static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        [TestMethod]
        public void TestCmd()
        {
            Cmd c = new Cmd();

            string r = c.Run("ping 192.168.1.1");
            string v = c.Run("dir");
            string d = c.Run("ping -t 192.168.1.1");

        }
    }

    public class Cmd
    {
        public System.Diagnostics.Process MyProcess { get; private set; }
        public Cmd()
        {
            MyProcess = new System.Diagnostics.Process();
            //设定程序名
            MyProcess.StartInfo.FileName = "cmd.exe";
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
            MyProcess.OutputDataReceived += MyProcess_OutputDataReceived;
            //执行VER命令
            MyProcess.Start();
            // Start the asynchronous read of the sort output stream.
            MyProcess.BeginOutputReadLine();
        }

        private void MyProcess_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        public string Run(string cmd)
        {
            MyProcess.StandardInput.WriteLine(cmd);
            return "";
        }
    }
}