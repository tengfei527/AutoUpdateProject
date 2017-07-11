using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AU.Common
{
    public class AuPackage
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        public static readonly string PackageName = "aupackage.json";
        /// <summary>
        /// 本地配置路径
        /// </summary>
        public string LocalPath { get; private set; }
        /// <summary>
        /// 本地包路径
        /// </summary>
        public string PackagePath { get; private set; }
        /// <summary>
        /// 本地包配置
        /// </summary>
        public AuList LocalAuList { get; private set; }
        /// <summary>
        /// 初始化本地路径
        /// </summary>
        /// <param name="localPath">本地配置目录</param>
        public AuPackage(string localPath)
        {
            this.LocalPath = localPath;
            this.PackagePath = System.IO.Path.Combine(this.LocalPath, PackageName);

            this.LocalAuList = ReadPackage(this.PackagePath);
        }

        public void SetPackage(AuList au)
        {
            this.LocalAuList = au;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public AuList ReadPackage(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return null;
                using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
                {
                    string json = sr.ReadToEnd();
                    sr.Close();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<AuList>(json);
                }
            }
            catch
            {

                return null;
            }
        }
    }
}
