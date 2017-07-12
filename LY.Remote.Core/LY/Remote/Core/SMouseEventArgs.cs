namespace LY.Remote.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class SMouseEventArgs : MouseEventArgs
    {
        public SMouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta, int flag) : base(button, clicks, x, y, delta)
        {
            this.Flag = flag;
        }

        internal static string b(string A_0, int A_1)
        {
            int num1;
            char ch1;
            int num2;
            char[] chArray = A_0.ToCharArray();
            int num = 0x144abdda + A_1;
            if (0 < 1)
            {
                goto Label_0047;
            }
        Label_0014:
            ch1 = chArray[num2];
            byte num3 = (byte) ((ch1 & '\x00ff') ^ num++);
            byte num4 = (byte) ((ch1 >> 8) ^ num++);
            num4 = num3;
            num3 = num4;
            chArray[num2] = (char) ((num4 << 8) | num3);
        Label_0047:
            num1 = (num2 = 0) + 1;
            if (num1 < chArray.Length)
            {
                goto Label_0014;
            }
            return string.Intern(new string(chArray));
        }

        public int Flag { get; set; }
    }
}

