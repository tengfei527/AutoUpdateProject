using System;
/// <summary>
/// �ļ�����������������ȡ����˺Ϳͻ����ļ�/��Ļ��ͨѶ�˿ڡ�
/// </summary>
namespace AU.Common.Codes
{
    /// <summary>
    /// ������ȡ����˺Ϳͻ����ļ�/��Ļ��ͨѶ�˿�
    /// </summary>
    [Serializable]
    public class PortCode : BaseCode
    {
        private int port;
        /// <summary>
        /// ͨѶ�˿�
        /// </summary>
        public int Port
        {
            get { return port; }
            set { port = value; }
        }
    }
}
