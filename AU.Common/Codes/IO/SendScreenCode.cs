/*----------------------------------------------------------------
        // Copyright (C) 2007 L3'Studio
        // ��Ȩ���С� 
        // �����ߣ�L3'Studio�Ŷ�
        // �ļ�����SendScreenCode.cs
        // �ļ������������漰����Ļ�����ָ�������Ļָ�
//----------------------------------------------------------------*/

using System;
namespace AU.Common.Codes
{
    /// <summary>
    /// ������Ļָ��
    /// </summary>
    [Serializable]
    public class SendScreenCode : BaseCode
    {
        private System.Drawing.Image screenImage;
        /// <summary>
        /// ��Ļ��ͼ
        /// </summary>
        public System.Drawing.Image ScreenImage
        {
            get { return screenImage; }
            set { screenImage = value; }
        }
        public SendScreenCode()
        {
            base.Head = CodeHead.SCREEN_SUCCESS;
        }
    }
}
