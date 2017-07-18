using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AU.Common.Utility
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class ToolsHelp
    {
        /// <summary>
        /// 从指定网络地址下载文件到指定目录
        /// </summary>
        /// <param name="downpath">下载地址</param>
        /// <param name="writepath">保存地址</param>
        /// <param name="filename">文件名称</param>
        /// <exception cref="System.IO.IOException">写目录失败</exception>
        /// <exception cref="System.Net.WebException">下载文件失败</exception>
        /// <returns>返回下载地址</returns>
        public static string DownAutoUpdateFile(string downpath, string writepath, string filename)
        {
            //System.Net.WebRequest req = System.Net.WebRequest.Create(downpath);
            //System.Net.WebResponse res = req.GetResponse();
            //if (res.ContentLength > 0)
            string path = string.Empty;
            try
            {
                if (!System.IO.Directory.Exists(writepath))
                    System.IO.Directory.CreateDirectory(writepath);
                path = writepath + "/" + filename;

                using (System.Net.WebClient wClient = new System.Net.WebClient())
                {
                    wClient.DownloadFile(downpath.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) ? downpath : "http://" + downpath, path);
                }
            }
            catch
            {
            }

            return path;
        }
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path">目录</param>
        public static void CreateDirtory(string path)
        {
            if (!File.Exists(path))
            {
                string[] dirArray = path.Split('\\');
                string temp = string.Empty;
                for (int i = 0; i < dirArray.Length - 1; i++)
                {
                    temp += dirArray[i].Trim() + "\\";
                    if (!Directory.Exists(temp))
                        Directory.CreateDirectory(temp);
                }
            }
        }


        /// <summary>
        /// 复制文件，递归拷贝
        /// </summary>
        /// <param name="sourcePath">源路径</param>
        /// <param name="objPath">目标路径</param>
        public static void CopyFile(string sourcePath, string objPath)
        {
            if (!Directory.Exists(objPath))
            {
                Directory.CreateDirectory(objPath);
            }
            string[] files = Directory.GetFiles(sourcePath);
            for (int i = 0; i < files.Length; i++)
            {
                string[] childfile = files[i].Split('\\');
                File.Copy(files[i], objPath + @"\" + childfile[childfile.Length - 1], true);
            }
            string[] dirs = Directory.GetDirectories(sourcePath);
            for (int i = 0; i < dirs.Length; i++)
            {
                string[] childdir = dirs[i].Split('\\');
                CopyFile(dirs[i], objPath + @"\" + childdir[childdir.Length - 1]);
            }
        }


        /// <summary>
        ///  计算指定文件的SHA1值
        /// </summary>
        /// <param name="fileName">指定文件的完全限定名称</param>
        /// <returns>返回值的字符串形式</returns>
        public static String ComputeSHA1(String fileName)
        {
            String hashSHA1 = String.Empty;

            //检查文件是否存在，如果文件存在则进行计算，否则返回空值
            if (System.IO.File.Exists(fileName))
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    //计算文件的SHA1值
                    System.Security.Cryptography.SHA1 calculator = System.Security.Cryptography.SHA1.Create();
                    Byte[] buffer = calculator.ComputeHash(fs);
                    calculator.Clear();
                    //将字节数组转换成十六进制的字符串形式
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        stringBuilder.Append(buffer[i].ToString("x2"));
                    }

                    hashSHA1 = stringBuilder.ToString();

                }//关闭文件流
            }

            return hashSHA1;
        }//ComputeSHA1

        /// <summary>
        ///  计算指定文件的SHA1值
        /// </summary>
        /// <param name="fileName">指定文件的完全限定名称</param>
        /// <returns>返回值的字符串形式</returns>
        public static String ComputeSHA256(String fileName)
        {
            String hashSHA256 = String.Empty;

            //检查文件是否存在，如果文件存在则进行计算，否则返回空值
            if (System.IO.File.Exists(fileName))
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    //计算文件的SHA1值
                    System.Security.Cryptography.SHA256 calculator = System.Security.Cryptography.SHA256.Create();
                    Byte[] buffer = calculator.ComputeHash(fs);
                    calculator.Clear();
                    //将字节数组转换成十六进制的字符串形式
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        stringBuilder.Append(buffer[i].ToString("x2"));
                    }

                    hashSHA256 = stringBuilder.ToString();

                }//关闭文件流
            }

            return hashSHA256;
        }//ComputeSHA1

        /// <summary>
        /// 关闭主应用程序
        /// </summary>
        /// <param name="applicationName"></param>
        public static void CloseApplication(string applicationName)
        {
            System.Diagnostics.Process[] allProcess = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process p in allProcess)
            {

                if (p.ProcessName.ToLower() + ".exe" == applicationName)
                {
                    for (int i = 0; i < p.Threads.Count; i++)
                        p.Threads[i].Dispose();
                    p.Kill();
                }
            }
        }

        /// <summary>
        /// 获取指定目录下所有文件
        /// </summary>
        /// <param name="rootdir">文件夹</param>
        /// <param name="recursion">是否递归子文件夹</param>
        /// <returns></returns>

        public static System.Collections.Specialized.StringCollection GetAllFiles(string rootdir, bool recursion = true)
        {
            System.Collections.Specialized.StringCollection result = new System.Collections.Specialized.StringCollection();
            if (recursion)
                GetAllFiles(rootdir, result);
            else
            {
                string[] file = Directory.GetFiles(rootdir);
                for (int i = 0; i < file.Length; i++)
                    result.Add(file[i]);
            }

            return result;
        }
        /// <summary>
        /// 递归获取文件
        /// </summary>
        /// <param name="parentDir">父文件夹</param>
        /// <param name="result">结果</param>
        public static void GetAllFiles(string parentDir, System.Collections.Specialized.StringCollection result)
        {
            string[] dir = Directory.GetDirectories(parentDir);
            for (int i = 0; i < dir.Length; i++)
                GetAllFiles(dir[i], result);
            string[] file = Directory.GetFiles(parentDir);
            for (int i = 0; i < file.Length; i++)
                result.Add(file[i]);
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="rootdir"></param>
        /// <param name="recursion"></param>
        /// <param name="namefileter"></param>
        /// <returns></returns>
        public static bool DeleteFile(string rootdir, bool recursion = true, params string[] namefileter)
        {
            try
            {
                System.Collections.Specialized.StringCollection file = GetAllFiles(rootdir, recursion);
                foreach (var f in file)
                {
                    if (namefileter != null && namefileter.Length > 0)
                    {
                        bool filter = false;
                        foreach (var n in namefileter)
                        {
                            if (f.EndsWith(n, StringComparison.InvariantCultureIgnoreCase))
                            {
                                filter = true;
                                break;
                            }
                        }
                        if (filter)
                            continue;
                    }
                    System.IO.File.Delete(f);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="rootdir">根目录</param>
        /// <param name="namefileter">过滤目录</param>
        /// <returns></returns>
        public static bool DeleteDirectory(string rootdir, params string[] namefileter)
        {
            try
            {
                string[] dir = Directory.GetDirectories(rootdir);
                foreach (var d in dir)
                {
                    if (namefileter != null && namefileter.Length > 0)
                    {
                        bool filter = false;
                        foreach (var n in namefileter)
                        {
                            if (d.IndexOf(n, StringComparison.InvariantCultureIgnoreCase) > -1)
                            {
                                filter = true;
                                break;
                            }
                        }
                        if (filter)
                            continue;
                    }
                    System.IO.Directory.Delete(d, true);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name=”bytes”></param>
        /// <returns></returns>
        public static string ByteToHexString(byte[] bytes)
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
        public static byte[] HexStringToByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
    }
}
