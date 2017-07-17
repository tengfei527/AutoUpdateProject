/*----------------------------------------------------------------
        // Copyright (C) 2007 L3'Studio
        // ��Ȩ���С� 
        // �����ߣ�L3'Studio�Ŷ�
        // �ļ�����BaseServer.cs
        // �ļ�����������ָ���ࣨ������˺Ϳͻ���֮������Լ�õ�һ���涨����Ʃ��ͻ��˷��͵�ͨѶ���ݰ���ָ��SHUTDOWN��
        // ��ô������յ����������ݺ�������ػ�����Ӧ��
//----------------------------------------------------------------*/

using System;

namespace AU.Common.Codes
{  
    /// <summary>
    /// ָ��
    /// </summary>
    [Serializable]
    public abstract class Code
    {
        /// <summary>
        /// ָ��ͷ��
        /// </summary>
        public abstract CodeHead Head
        {
            get;
            set;
        }
    }
    
    /// <summary>
    /// ��ָ��
    /// </summary>
    [Serializable]
    public class BaseCode:Code
    {
        private CodeHead head;
        /// <summary>
        /// ָ��ͷ��
        /// </summary>
        public override CodeHead Head
        {
            get { return head; }
            set { head = value; }
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Head=" + head;
        }
    }

}
