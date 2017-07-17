/*----------------------------------------------------------------
        // Copyright (C) 2007 L3'Studio
        // ��Ȩ���С� 
        // �����ߣ�L3'Studio�Ŷ�
        // �ļ�����DoubleCode.cs
        // �ļ������������̳���BaseCode��
//----------------------------------------------------------------*/

using System;

namespace AU.Common.Codes
{
    /// <summary>
    /// ˫ָ��
    /// </summary>
    [Serializable]
    public class DoubleCode : BaseCode
    {
        private string body;
        /// <summary>
        /// ָ������
        /// </summary>
        public string Body
        {
            get { return body; }
            set { body = value; }
        }

        public override string ToString()
        {
            return body;
        }
    }
}
