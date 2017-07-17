/*----------------------------------------------------------------
        // Copyright (C) 2007 L3'Studio
        // ��Ȩ���С� 
        // �����ߣ�L3'Studio�Ŷ�
        // �ļ�����HostCode.cs
        // �ļ�������������ȡ������Ϣ��ָ�
//----------------------------------------------------------------*/

using System;

namespace AU.Common.Codes
{
    /// <summary>
    /// ������Ϣ�ṹ
    /// </summary>
    [Serializable]
    public class HostCode : BaseCode
    {
        private string ip;
        private string name;

        /// <summary>
        /// ������IP��ַ
        /// </summary>
        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// ����ToString�������
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return IP + "(" + Name + ")";
        }
    }
}
