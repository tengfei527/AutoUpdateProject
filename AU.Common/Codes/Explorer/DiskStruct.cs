using System;
using System.IO;
/// <summary>
/// �ļ������������漰���ļ������ָ����̽ṹ�ࡣ
/// </summary>
namespace AU.Common.Codes
{
    /// <summary>
    /// ���̽ṹ(��Ϊ���л�ָ���������ϴ���)
    /// </summary>
    [Serializable]
    public class DiskStruct : FileStruct
    {
        /// <summary>
        /// ���̱�־
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
                    this.size = TotalFreeSpace / 1024 + " kb ����";
                }
                else if (TotalFreeSpace < (1024 * 1024 * 1024))
                {
                    this.size = TotalFreeSpace / (1024 * 1024) + " MB ����";
                }
                else
                {
                    this.size = TotalFreeSpace / (1024 * 1024 * 1024) + " GB ����";
                }
                if (TotalSize < (1024 * 1024))
                {
                    this.size += ",�� " + TotalSize / 1024 + " KB";
                }
                else if (TotalSize < (1024 * 1024 * 1024))
                {
                    this.size += ",�� " + TotalSize / (1024 * 1024) + " MB";
                }
                else
                {
                    this.size += ",�� " + TotalSize / (1024 * 1024 * 1024) + " GB";
                }
            }
            catch
            {
                this.size = "δ֪";
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
