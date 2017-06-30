using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace AU.Common.Utility
{
    public class ZipUtility
    {
        /// <summary>
        /// 缓冲区
        /// </summary>
        public const int BUFFER_SIZE = 2048;

        #region CompressLevel

        // 0 - store only to 9 - means best compression
        public enum CompressLevel
        {
            Store = 0,
            Level1,
            Level2,
            Level3,
            Level4,
            Level5,
            Level6,
            Level7,
            Level8,
            Best
        }

        #endregion
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="tartgetfile">目标</param>
        /// <param name="level">压缩级别</param>
        /// <param name="password">密码</param>
        /// <returns>文件数</returns>
        public static int Compress(string source, string tartgetfile, CompressLevel level = CompressLevel.Level6, string password = "")
        {
            int count = 0;
            Directory.CreateDirectory(Path.GetDirectoryName(tartgetfile));
            using (ZipOutputStream s = new ZipOutputStream(File.Create(tartgetfile)))
            {
                //Specify Password
                if (password != null && password.Trim().Length > 0)
                {
                    s.Password = password;
                }
                s.SetLevel(Convert.ToInt32(level));
                count += Compress(source, s, source);
                s.Finish();
                s.Close();
            }
            return count;
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="source">源目录</param>
        /// <param name="s">ZipOutputStream对象</param>
        /// <param name="basepath">根目录</param>
        /// <returns>文件数</returns>
        public static int Compress(string source, ZipOutputStream s, string basepath = "")
        {
            int count = 0;
            string[] filenames = Directory.GetFileSystemEntries(source);
            foreach (string file in filenames)
            {
                if (Directory.Exists(file))
                {
                    count += Compress(file, s, basepath);  //递归压缩子文件夹
                }
                else
                {
                    using (FileStream fs = File.OpenRead(file))
                    {
                        byte[] buffer = new byte[2 * BUFFER_SIZE];
                        //ZipEntry entry = new ZipEntry(file.Replace(Path.GetPathRoot(file), ""));     //此处去掉盘符，如D:\123\1.txt 去掉D:
                        ZipEntry entry = new ZipEntry(file.Replace(basepath, "").TrimStart(new char[] { '\\', '/' }));
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);

                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }

                count++;
            }

            return count;
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="sourceFile">源文件</param>
        /// <param name="targetPath">目标</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static bool Decompress(string sourceFile, string targetPath, string password = "")
        {
            if (!File.Exists(sourceFile))
            {
                throw new FileNotFoundException(string.Format("未能找到文件 '{0}' ", sourceFile));
            }
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            using (var s = new ZipInputStream(File.OpenRead(sourceFile)))
            {
                //Specify Password
                if (password != null && password.Trim().Length > 0)
                {
                    s.Password = password;
                }
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    if (theEntry.IsDirectory) continue;

                    string directorName = Path.Combine(targetPath, Path.GetDirectoryName(theEntry.Name));
                    string fileName = Path.Combine(directorName, Path.GetFileName(theEntry.Name));

                    if (!Directory.Exists(directorName))
                    {
                        Directory.CreateDirectory(directorName);
                    }
                    if (!String.IsNullOrEmpty(fileName))
                    {
                        using (FileStream streamWriter = File.Create(fileName))
                        {
                            int size = BUFFER_SIZE;
                            byte[] data = new byte[size];
                            while (size > 0)
                            {
                                size = s.Read(data, 0, data.Length);
                                streamWriter.Write(data, 0, size);
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}