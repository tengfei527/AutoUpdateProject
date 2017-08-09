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
            get; set;
        }
        private string size;

        public DiskStruct(string name) : base(name)
        {
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
