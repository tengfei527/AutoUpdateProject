using System;
using System.IO;
/// <summary>
/// 文件功能描述：涉及到文件管理的指令－磁盘结构类。
/// </summary>
namespace AU.Common.Codes
{
    /// <summary>
    /// 磁盘结构(作为序列化指令在网络上传输)
    /// </summary>
    [Serializable]
    public class DiskStruct : FileStruct
    {
        /// <summary>
        /// 磁盘标志
        /// </summary>
        public override FileFlag Flag
        {
            get { return FileFlag.Disk; }
        }
        public DriveType Drive
        {
            get; protected set;
        }
        private string size;

        public DiskStruct(string name, long TotalFreeSpace, long TotalSize, DriveType drive = DriveType.Unknown) : base(name)
        {
            try
            {
                this.Drive = drive;
                if (TotalFreeSpace < (1024 * 1024))
                {
                    this.size = TotalFreeSpace / 1024 + " kb 可用";
                }
                else if (TotalFreeSpace < (1024 * 1024 * 1024))
                {
                    this.size = TotalFreeSpace / (1024 * 1024) + " MB 可用";
                }
                else
                {
                    this.size = TotalFreeSpace / (1024 * 1024 * 1024) + " GB 可用";
                }
                if (TotalSize < (1024 * 1024))
                {
                    this.size += ",共 " + TotalSize / 1024 + " KB";
                }
                else if (TotalSize < (1024 * 1024 * 1024))
                {
                    this.size += ",共 " + TotalSize / (1024 * 1024) + " MB";
                }
                else
                {
                    this.size += ",共 " + TotalSize / (1024 * 1024 * 1024) + " GB";
                }
            }
            catch
            {
                this.size = "未知";
                this.Drive = DriveType.Unknown;
            }
        }
        private string lastUpdateTime;
        public override string LastUpdateTime
        {
            get
            {
                return lastUpdateTime;
            }
            set
            {
                lastUpdateTime = value;
            }
        }

        public override string Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }
    }
}
