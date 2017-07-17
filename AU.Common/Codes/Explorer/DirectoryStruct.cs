/*----------------------------------------------------------------
        // Copyright (C) 2007 L3'Studio
        // ��Ȩ���С� 
        // �����ߣ�L3'Studio�Ŷ�
        // �ļ�����DirectoryStruct.cs
        // �ļ������������漰���ļ������ָ��ļ��нṹ�ࡣ
//----------------------------------------------------------------*/
using System;

namespace AU.Common.Codes
{
    /// <summary>
    /// �ļ��нṹ(��Ϊ���л�ָ���������ϴ���)
    /// </summary>
    [Serializable]
    public class DirectoryStruct : FileStruct
    {
        /// <summary>
        /// �ļ��б�־
        /// </summary>
        public override FileFlag Flag
        {
            get { return FileFlag.Directory; }
        }
        public DirectoryStruct(string name) : base(name) { }
    }
}
