/*----------------------------------------------------------------
        // Copyright (C) 2007 L3'Studio
        // ��Ȩ���С� 
        // �����ߣ�L3'Studio�Ŷ�
        // �ļ�����KeyBoardEvent.cs
        // �ļ������������漰����Ļ�����ָ����̿���ָ�
//----------------------------------------------------------------*/

using System;
using System.Text;

namespace AU.Common.Codes
{
    /// <summary>
    /// �����¼�����
    /// </summary>
    [Serializable]
    public enum KeyBoardType
    {
        /// <summary>
        /// ���°���
        /// </summary>
        Key_Down,
        /// <summary>
        /// �ͷŰ���
        /// </summary>
        Key_Up,
        /// <summary>
        /// ���²��ͷŰ���
        /// </summary>
        Key_Press,
    }

    /// <summary>
    /// �����¼��ṹ
    /// </summary>
    [Serializable]
    public class KeyBoardEvent : BaseCode
    {
        /// <summary>
        /// �����¼�����
        /// </summary>
        private KeyBoardType type;

        /// <summary>
        /// ������
        /// </summary>
        private System.Windows.Forms.Keys keyCode;

        /// <summary>
        /// �����¼�����
        /// </summary>
        public KeyBoardType Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public System.Windows.Forms.Keys KeyCode
        {
            get { return keyCode; }
            set { keyCode = value; }
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        public KeyBoardEvent()
        {
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyCode"></param>
        public KeyBoardEvent(KeyBoardType type, System.Windows.Forms.Keys keyCode)
        {
            this.type = type;
            this.keyCode = keyCode;
        }
    } 
}
