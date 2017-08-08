using System;
using System.IO;
/// <summary>
/// �ļ������������漰���ļ������ָ��ļ��ṹ�ࡣ
/// </summary>
namespace AU.Common.Codes
{
    /// <summary>
    /// �ļ��ṹ(��Ϊ���л�ָ���������ϴ���)
    /// </summary>
    [Serializable]
    public class FileStruct : BaseFile
    {
        private string name;
        /// <summary>
        /// �ļ���־
        /// </summary>
        public override FileFlag Flag
        {
            get
            {
                return FileFlag.File;
            }
        }
        /// <summary>
        /// ȫ��
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
                    this.size = "δ֪";
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
                    lastUpdateTime = "δ֪";
                }
            }
        }
    }
}
