using System;
/// <summary>
/// �ļ������������漰���ļ������ָ�"���д���"ָ���ࡣ
/// </summary>
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
