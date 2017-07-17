/*----------------------------------------------------------------
        // Copyright (C) 2007 L3'Studio
        // ��Ȩ���С� 
        // �����ߣ�L3'Studio�Ŷ�
        // �ļ�����DisksCode.cs
        // �ļ������������漰���ļ������ָ�"���д���"ָ���ࡣ
//----------------------------------------------------------------*/

using System;

namespace AU.Common.Codes
{
    /// <summary>
    /// "���д���"ָ����(��Ϊ���л�ָ���������ϴ���)
    /// </summary>
    [Serializable]
    public class DisksCode : BaseCode
    {
        private DiskStruct[] disks;
        /// <summary>
        /// ��������
        /// </summary>
        public DiskStruct[] Disks
        {
            get { return disks; }
            set { disks = value; }
        }

        public DisksCode() { base.Head = CodeHead.SEND_DISKS; }
    }
}
