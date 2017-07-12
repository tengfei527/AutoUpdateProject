namespace LY.Remote.Core
{
    using System;
    using System.Net.Sockets;

    public class SocketHelper
    {
        public static byte[] ReceiveData(Socket socket, int size)
        {
            int num;
            int num3;
            byte[] buffer2;
            bool flag;
        Label_0037:
            num = 0;
            int num2 = size;
            byte[] buffer = new byte[size];
            int num4 = 4;
        Label_0010:
            switch (num4)
            {
                case 0:
                    if (flag)
                    {
                        num3 = socket.Receive(buffer, num, num2, SocketFlags.None);
                        flag = num3 != 0;
                        num4 = 1;
                    }
                    else
                    {
                        num4 = 5;
                    }
                    goto Label_0010;

                case 1:
                    if (flag)
                    {
                        num += num3;
                        num2 -= num3;
                        num4 = 7;
                    }
                    else
                    {
                        num4 = 2;
                    }
                    goto Label_0010;

                case 2:
                    buffer = null;
                    num4 = 3;
                    goto Label_0010;

                case 3:
                case 5:
                    buffer2 = buffer;
                    num4 = 6;
                    goto Label_0010;

                case 4:
                case 7:
                {
                    flag = num < size;
                    int expressionStack_5A_0 = 1;
                    if (expressionStack_5A_0 == 0)
                    {
                    }
                    num4 = 0;
                    goto Label_0010;
                }
                case 6:
                    return buffer2;
            }
            goto Label_0037;
        }

        public static byte[] ReceiveDataBySize(Socket socket)
        {
            // This item is obfuscated and can not be translated.
        }

        public static byte[] ReceiveSendDataBySize(Socket socket, Socket sendSocket)
        {
            // This item is obfuscated and can not be translated.
        }

        public static int SendData(Socket socket, byte[] data)
        {
            int num;
            int num5;
            bool flag;
        Label_002B:
            num = 0;
            int length = data.Length;
            int size = length;
            int expressionStack_3A_0 = 1;
            if (expressionStack_3A_0 == 0)
            {
            }
            int num6 = 3;
        Label_0010:
            switch (num6)
            {
                case 0:
                    return num5;

                case 1:
                case 3:
                    flag = num < length;
                    num6 = 2;
                    goto Label_0010;

                case 2:
                    if (flag)
                    {
                        int num4 = socket.Send(data, num, size, SocketFlags.None);
                        num += num4;
                        size -= num4;
                        num6 = 1;
                    }
                    else
                    {
                        num6 = 4;
                    }
                    goto Label_0010;

                case 4:
                    num5 = num;
                    num6 = 0;
                    goto Label_0010;
            }
            goto Label_002B;
        }

        public static int SendDataWithSize(Socket socket, byte[] data)
        {
            // This item is obfuscated and can not be translated.
        }
    }
}

