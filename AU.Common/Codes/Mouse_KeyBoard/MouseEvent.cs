using System;
/// <summary>
/// �ļ������������漰����Ļ�����ָ�������ָ�
/// </summary>
namespace AU.Common.Codes
{
    /// <summary>
    /// ����¼�����
    /// </summary>
    [Serializable]
    public enum MouseEventType
    {
        MouseMove,
        MouseLeftDown,
        MouseLeftUp,
        MouseRightDown,
        MouseRightUp,
        MouseClick,
        MouseDoubleClick
    }

    /// <summary>
    /// ����¼��ṹ
    /// </summary>
    [Serializable]
    public class MouseEvent : BaseCode
    {
        private Byte[] type;
        private Byte[] x;
        private Byte[] y;

        /// <summary>
        /// ��������¼���ʵ��
        /// </summary>
        /// <param name="Type">����</param>
        /// <param name="X">���ָ���X����</param>
        /// <param name="Y">���ָ���Y����</param>
        public MouseEvent(MouseEventType Type, int X, int Y)
        {
            this.type = BitConverter.GetBytes((int)Type);
            this.x = BitConverter.GetBytes(X);
            this.y = BitConverter.GetBytes(Y);
        }

        public MouseEvent(Byte[] Type, Byte[] X, Byte[] Y)
        {
            this.type = Type;
            this.x = X;
            this.y = Y;
        }

        public MouseEvent(Byte[] Content)
        {
            type = new byte[4];
            x = new byte[4];
            y = new byte[4];
            for (int i = 0; i < Content.Length; i++)
            {
                if (i >= 0 && i < 4)
                    type[i] = Content[i];
                if (i >= 4 && i < 8)
                    x[i - 4] = Content[i];
                if (i >= 8 && i < 12)
                    y[i - 8] = Content[i];
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public MouseEventType Type
        {
            get { return (MouseEventType)BitConverter.ToInt32(type, 0); }
        }
        /// <summary>
        /// ���ָ���X����
        /// </summary>
        public int X
        {
            get { return BitConverter.ToInt32(x, 0); }
        }
        /// <summary>
        /// ���ָ���Y����
        /// </summary>
        public int Y
        {
            get { return BitConverter.ToInt32(y, 0); }
        }

        public Byte[] ToBytes()
        {
            Byte[] Bytes = new Byte[12];
            type.CopyTo(Bytes, 0);
            x.CopyTo(Bytes, 4);
            y.CopyTo(Bytes, 8);
            return Bytes;
        }
    }
}