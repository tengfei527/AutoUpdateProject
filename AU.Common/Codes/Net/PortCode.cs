/*----------------------------------------------------------------
        // Copyright (C) 2007 L3'Studio
        // ��Ȩ���С� 
        // �����ߣ�L3'Studio�Ŷ�
        // �ļ�����PortCode.cs
        // �ļ�����������������ȡ����˺Ϳͻ����ļ�/��Ļ��ͨѶ�˿ڡ�
//----------------------------------------------------------------*/

using System;

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
