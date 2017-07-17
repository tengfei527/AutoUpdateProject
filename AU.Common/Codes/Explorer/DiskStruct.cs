/*----------------------------------------------------------------
        // Copyright (C) 2007 L3'Studio
        // ��Ȩ���С� 
        // �����ߣ�L3'Studio�Ŷ�
        // �ļ�����DiskStruct.cs
        // �ļ������������漰���ļ������ָ����̽ṹ�ࡣ
//----------------------------------------------------------------*/

using System;

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
        public DiskStruct(string name) : base(name) { }
    }
}
