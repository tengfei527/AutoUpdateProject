/*----------------------------------------------------------------
        // Copyright (C) 2007 L3'Studio
        // ��Ȩ���С� 
        // �����ߣ�L3'Studio�Ŷ�
        // �ļ�����ThreeCode.cs
        // �ļ������������̳���DoubleCode��
//----------------------------------------------------------------*/

using System;

namespace AU.Common.Codes
{
    /// <summary>
    /// ��ָ��(���������������ļ������ϴ���ָ��)
    /// </summary>
    [Serializable]
    public class ThreeCode : DoubleCode
    {
        private string foot;
        /// <summary>
        /// ָ��β��
        /// </summary>
        public string Foot
        {
            get { return foot; }
            set { foot = value; }
        }

        public override string ToString()
        {
            return base.ToString() + ",Foot=" + foot;
        }
    }
}
