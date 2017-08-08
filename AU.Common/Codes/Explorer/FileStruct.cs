using System;
using System.IO;
/// <summary>
/// 文件功能描述：涉及到文件管理的指令－文件结构类。
/// </summary>
namespace AU.Common.Codes
{
    /// <summary>
    /// 文件结构(作为序列化指令在网络上传输)
    /// </summary>
    [Serializable]
    public class FileStruct : BaseFile
    {
        private string name;
        /// <summary>
        /// 文件标志
        /// </summary>
        public override FileFlag Flag
        {
            get
            {
                return FileFlag.File;
            }
        }
        /// <summary>
        /// 全名
        /// </summary>
        public override string Name
        {
            get { return name; }
        }
        public FileStruct(string name)
        {
            this.name = name;

        }

        public override string ToString()
        {
            return name;
        }
        private string size;
        public override string Size
        {
            get
            {
                return this.size;
            }
            set
            {
                try
                {
                    this.size = AU.Common.Utility.IO.GetFileSize(this.name);
                }
                catch
                {
                    this.size = "未知";
                }
            }

        }
        private string lastUpdateTime;
        public override string LastUpdateTime
        {
            get
            {
                return this.lastUpdateTime;
            }
            set
            {
                try
                {
                    lastUpdateTime = File.GetLastWriteTime(name).ToString();
                }
                catch
                {
                    lastUpdateTime = "未知";
                }
            }
        }
    }
}
