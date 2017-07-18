using System;
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
        public FileStruct(string name) { this.name = name; }

        public override string ToString()
        {
            return name;
        }

    }
}
