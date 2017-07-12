namespace LY.Remote.Core
{
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    public class ServerListener
    {
        private object _lock = new object();
        private Dictionary<string, Socket> CmdSockets = new Dictionary<string, Socket>();
        private Dictionary<string, string> ControlPairs = new Dictionary<string, string>();
        private Control Form = null;
        private Dictionary<string, Socket> ImgSockets = new Dictionary<string, Socket>();
        private bool IsServerStarted = false;
        private Socket ListenSocket;
        private Thread ListenThread;
        private ConnectedEventHandler ServerConnected = null;
        private ConnectedEventHandler ServerDisconnected = null;
        private EventHandler ServerStarted = null;
        private List<LoginCommand> Users = new List<LoginCommand>();
        private LoginVerifyEventHandler UserVerify = null;

        public event ConnectedEventHandler ServerConnected
        {
            add
            {
                ConnectedEventHandler handler;
                bool flag;
            Label_0023:
                handler = this.ServerConnected;
                int num = 1;
            Label_0010:
                switch (num)
                {
                    case 0:
                        return;

                    case 1:
                    {
                        ConnectedEventHandler a = handler;
                        ConnectedEventHandler handler3 = (ConnectedEventHandler) Delegate.Combine(a, value);
                        handler = Interlocked.CompareExchange<ConnectedEventHandler>(ref this.ServerConnected, handler3, a);
                        flag = handler != a;
                        num = 2;
                        goto Label_0010;
                    }
                    case 2:
                        if ((flag ? 0 : 1) != 0)
                        {
                        }
                        num = 0;
                        goto Label_0010;
                }
                goto Label_0023;
            }
            remove
            {
                ConnectedEventHandler handler;
                bool flag;
            Label_0023:
                handler = this.ServerConnected;
                int num = 0;
            Label_0010:
                switch (num)
                {
                    case 0:
                    {
                        ConnectedEventHandler source = handler;
                        ConnectedEventHandler handler3 = (ConnectedEventHandler) Delegate.Remove(source, value);
                        handler = Interlocked.CompareExchange<ConnectedEventHandler>(ref this.ServerConnected, handler3, source);
                        flag = handler != source;
                        num = 2;
                        goto Label_0010;
                    }
                    case 1:
                        return;

                    case 2:
                        if ((flag ? 0 : 1) != 0)
                        {
                        }
                        num = 1;
                        goto Label_0010;
                }
                goto Label_0023;
            }
        }

        public event ConnectedEventHandler ServerDisconnected
        {
            add
            {
                // This item is obfuscated and can not be translated.
            }
            remove
            {
                ConnectedEventHandler handler2;
                bool flag;
                int expressionStack_29_0;
                int num = 0;
                switch (num)
                {
                    default:
                        goto Label_0023;
                }
            Label_0010:
                switch (num)
                {
                    case 0:
                        return;

                    case 1:
                        goto Label_003D;

                    case 2:
                        if (flag)
                        {
                            goto Label_003D;
                        }
                        num = 0;
                        goto Label_0010;
                }
            Label_0023:
                expressionStack_29_0 = 1;
                if (expressionStack_29_0 == 0)
                {
                }
                ConnectedEventHandler serverDisconnected = this.ServerDisconnected;
                num = 1;
                goto Label_0010;
            Label_003D:
                handler2 = serverDisconnected;
                ConnectedEventHandler handler3 = (ConnectedEventHandler) Delegate.Remove(handler2, value);
                serverDisconnected = Interlocked.CompareExchange<ConnectedEventHandler>(ref this.ServerDisconnected, handler3, handler2);
                flag = serverDisconnected != handler2;
                num = 2;
                goto Label_0010;
            }
        }

        public event EventHandler ServerStarted
        {
            add
            {
                EventHandler handler2;
                bool flag;
                int expressionStack_29_0;
                int num = 0;
                switch (num)
                {
                    default:
                        goto Label_0023;
                }
            Label_0010:
                switch (num)
                {
                    case 0:
                        if (flag)
                        {
                            goto Label_003D;
                        }
                        num = 1;
                        goto Label_0010;

                    case 1:
                        return;

                    case 2:
                        goto Label_003D;
                }
            Label_0023:
                expressionStack_29_0 = 1;
                if (expressionStack_29_0 == 0)
                {
                }
                EventHandler serverStarted = this.ServerStarted;
                num = 2;
                goto Label_0010;
            Label_003D:
                handler2 = serverStarted;
                EventHandler handler3 = (EventHandler) Delegate.Combine(handler2, value);
                serverStarted = Interlocked.CompareExchange<EventHandler>(ref this.ServerStarted, handler3, handler2);
                flag = serverStarted != handler2;
                num = 0;
                goto Label_0010;
            }
            remove
            {
                EventHandler handler2;
                bool flag;
                int expressionStack_29_0;
                int num = 0;
                switch (num)
                {
                    default:
                        goto Label_0023;
                }
            Label_0010:
                switch (num)
                {
                    case 0:
                        return;

                    case 1:
                        if (flag)
                        {
                            goto Label_003D;
                        }
                        num = 0;
                        goto Label_0010;

                    case 2:
                        goto Label_003D;
                }
            Label_0023:
                expressionStack_29_0 = 1;
                if (expressionStack_29_0 == 0)
                {
                }
                EventHandler serverStarted = this.ServerStarted;
                num = 2;
                goto Label_0010;
            Label_003D:
                handler2 = serverStarted;
                EventHandler handler3 = (EventHandler) Delegate.Remove(handler2, value);
                serverStarted = Interlocked.CompareExchange<EventHandler>(ref this.ServerStarted, handler3, handler2);
                flag = serverStarted != handler2;
                num = 1;
                goto Label_0010;
            }
        }

        public event LoginVerifyEventHandler UserVerify
        {
            add
            {
                LoginVerifyEventHandler handler2;
                bool flag;
                int expressionStack_29_0;
                int num = 0;
                switch (num)
                {
                    default:
                        goto Label_0023;
                }
            Label_0010:
                switch (num)
                {
                    case 0:
                        return;

                    case 1:
                        if (flag)
                        {
                            goto Label_003D;
                        }
                        num = 0;
                        goto Label_0010;

                    case 2:
                        goto Label_003D;
                }
            Label_0023:
                expressionStack_29_0 = 1;
                if (expressionStack_29_0 == 0)
                {
                }
                LoginVerifyEventHandler userVerify = this.UserVerify;
                num = 2;
                goto Label_0010;
            Label_003D:
                handler2 = userVerify;
                LoginVerifyEventHandler handler3 = (LoginVerifyEventHandler) Delegate.Combine(handler2, value);
                userVerify = Interlocked.CompareExchange<LoginVerifyEventHandler>(ref this.UserVerify, handler3, handler2);
                flag = userVerify != handler2;
                num = 1;
                goto Label_0010;
            }
            remove
            {
                // This item is obfuscated and can not be translated.
            }
        }

        public ServerListener(Control c)
        {
            this.Form = c;
        }

        private void CancelControl(string user)
        {
            bool flag;
        Label_0014:
            flag = !this.ControlPairs.ContainsKey(user);
            int expressionStack_2B_0 = 1;
            if (expressionStack_2B_0 == 0)
            {
            }
            int num = 1;
        Label_0002:
            switch (num)
            {
                case 0:
                {
                    string remoteUser = this.ControlPairs[user];
                    this.SendDisconnectCommand(user, remoteUser);
                    this.ControlPairs.Remove(user);
                    this.ControlPairs.Remove(remoteUser);
                    this.CloseSocket(remoteUser);
                    num = 2;
                    goto Label_0002;
                }
                case 1:
                    if (flag)
                    {
                        break;
                    }
                    num = 0;
                    goto Label_0002;

                case 2:
                    break;

                default:
                    goto Label_0014;
            }
        }

        public void ClearDisconnectSocket()
        {
            string str;
            bool flag;
            bool flag2;
            string[] strArray2;
            int num;
            bool expressionStack_18D_0;
            int num2 = 0;
            switch (num2)
            {
                default:
                    goto Label_005F;
            }
        Label_0010:
            switch (num2)
            {
                case 0:
                    this.CloseSocket(str);
                    num2 = 2;
                    goto Label_0010;

                case 1:
                    expressionStack_18D_0 = this.CmdSockets[str] == null;
                    goto Label_018D;

                case 2:
                    goto Label_00D0;

                case 3:
                    if (!this.CmdSockets.ContainsKey(str))
                    {
                        num2 = 12;
                    }
                    else
                    {
                        num2 = 14;
                    }
                    goto Label_0010;

                case 4:
                case 6:
                    return;

                case 5:
                    num2 = 4;
                    goto Label_0010;

                case 7:
                    if (flag2)
                    {
                        goto Label_0175;
                    }
                    num2 = 8;
                    goto Label_0010;

                case 8:
                    flag2 = this.IsSocketConnected(this.CmdSockets[str]);
                    num2 = 9;
                    goto Label_0010;

                case 9:
                    if (flag2)
                    {
                        goto Label_00D0;
                    }
                    num2 = 0;
                    goto Label_0010;

                case 10:
                case 0x10:
                    flag2 = num < strArray2.Length;
                    num2 = 11;
                    goto Label_0010;

                case 11:
                    if (flag2)
                    {
                        str = strArray2[num];
                        flag2 = !string.IsNullOrEmpty(str);
                        num2 = 13;
                    }
                    else
                    {
                        num2 = 6;
                    }
                    goto Label_0010;

                case 12:
                    expressionStack_18D_0 = true;
                    goto Label_018D;

                case 13:
                    if (flag2)
                    {
                        goto Label_0249;
                    }
                    num2 = 5;
                    goto Label_0010;

                case 14:
                    num2 = 1;
                    goto Label_0010;

                case 15:
                    goto Label_0175;

                case 0x11:
                    object obj2;
                    try
                    {
                        Monitor.Enter(obj2 = this._lock, ref flag);
                        string[] array = new string[this.CmdSockets.Keys.Count];
                        this.CmdSockets.Keys.CopyTo(array, 0);
                        int expressionStack_B8_0 = 1;
                        if (expressionStack_B8_0 == 0)
                        {
                        }
                        strArray2 = array;
                        num = 0;
                        num2 = 0x10;
                        goto Label_0010;
                    }
                    finally
                    {
                        goto Label_0218;
                    Label_0205:
                        switch (num2)
                        {
                            case 0:
                                Monitor.Exit(obj2);
                                num2 = 1;
                                goto Label_0205;

                            case 1:
                                goto Label_0248;

                            case 2:
                                if (flag2)
                                {
                                    goto Label_0248;
                                }
                                num2 = 0;
                                goto Label_0205;
                        }
                    Label_0218:
                        flag2 = !flag;
                        num2 = 2;
                        goto Label_0205;
                    Label_0248:;
                    }
                    goto Label_0249;
            }
        Label_005F:
            flag = false;
            num2 = 0x11;
            goto Label_0010;
        Label_00D0:
            num2 = 15;
            goto Label_0010;
        Label_0175:
            num++;
            num2 = 10;
            goto Label_0010;
        Label_018D:
            flag2 = expressionStack_18D_0;
            num2 = 7;
            goto Label_0010;
        Label_0249:
            num2 = 3;
            goto Label_0010;
        }

        private void CloseSocket(Socket socket)
        {
            // This item is obfuscated and can not be translated.
            bool flag;
            int num;
            goto Label_0030;
            if (1 != 0)
            {
            }
            switch (num)
            {
                case 0:
                    try
                    {
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                    }
                    catch (Exception)
                    {
                    }
                    num = 3;
                    goto Label_0008;

                case 1:
                case 4:
                    flag = !socket.Connected;
                    num = 7;
                    goto Label_0008;

                case 2:
                    if (socket == null)
                    {
                        num = 1;
                    }
                    else
                    {
                        num = 6;
                    }
                    goto Label_0008;

                case 3:
                    return;

                case 5:
                    num = 0;
                    goto Label_0008;

                case 6:
                    num = 4;
                    goto Label_0008;

                case 7:
                    if (flag)
                    {
                        return;
                    }
                    num = 5;
                    goto Label_0008;
            }
        Label_0030:
            num = 2;
            goto Label_0008;
        }

        private void CloseSocket(string userName)
        {
            // This item is obfuscated and can not be translated.
        }

        private bool IsSocketConnected(Socket socket)
        {
            int num = 2;
            try
            {
                int expressionStack_12_0 = 1;
                if (expressionStack_12_0 == 0)
                {
                }
                byte[] bytes = new byte[10];
                string s = SMouseEventArgs.b("隆뿠", num);
                bytes = Encoding.UTF8.GetBytes(s);
                socket.Send(bytes);
                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }

        private void ReceiveCommand(Socket socket, LoginCommand user)
        {
            // This item is obfuscated and can not be translated.
        }

        private void ReceiveImage(Socket socket, LoginCommand user)
        {
            // This item is obfuscated and can not be translated.
        }

        private void ReceiveLogin(object socket)
        {
            // This item is obfuscated and can not be translated.
        }

        private void SendDisconnectCommand(string user, string remoteUser)
        {
            int num2 = 10;
            int expressionStack_F_0 = 1;
            if (expressionStack_F_0 == 0)
            {
            }
            int num = 0;
            try
            {
                bool flag;
                bool expressionStack_130_0;
                goto Label_0057;
            Label_0024:
                switch (num)
                {
                    case 0:
                        goto Label_0161;

                    case 1:
                        if (flag)
                        {
                            goto Label_00A4;
                        }
                        num = 2;
                        goto Label_0024;

                    case 2:
                    {
                        string s = string.Format(SMouseEventArgs.b("쇤헦门郪\uddec\u92ee跰裲쓴諶藸胺쿼苾紀崂", num2), user, remoteUser, 4);
                        byte[] bytes = Encoding.UTF8.GetBytes(s);
                        this.CmdSockets[remoteUser].Send(bytes);
                        num = 6;
                        goto Label_0024;
                    }
                    case 3:
                        num = 4;
                        goto Label_0024;

                    case 4:
                        expressionStack_130_0 = !this.CmdSockets[remoteUser].Connected;
                        goto Label_0130;

                    case 5:
                        return;

                    case 6:
                        goto Label_00A4;

                    case 7:
                        if (this.CmdSockets[remoteUser] == null)
                        {
                            goto Label_0152;
                        }
                        num = 3;
                        goto Label_0024;

                    case 8:
                        expressionStack_130_0 = true;
                        goto Label_0130;

                    case 9:
                        goto Label_00B6;

                    case 10:
                        if (flag)
                        {
                            goto Label_0161;
                        }
                        num = 9;
                        goto Label_0024;
                }
            Label_0057:
                flag = !this.CmdSockets.ContainsKey(remoteUser);
                num = 10;
                goto Label_0024;
            Label_00A4:
                num = 0;
                goto Label_0024;
            Label_00B6:
                num = 7;
                goto Label_0024;
            Label_0130:
                flag = expressionStack_130_0;
                num = 1;
                goto Label_0024;
            Label_0152:
                num = 8;
                goto Label_0024;
            Label_0161:
                num = 5;
                goto Label_0024;
            }
            catch (Exception exception)
            {
                CommandLog.WriteServerLog(SMouseEventArgs.b("뛤苦蟨迪췬꯮飰胲雴飶韸闺飼鳾甀⌂䘄栆搈昊氌愎甐㌒倔漖稘縚洜欞䠠䰢䬤", num2), exception.Message);
            }
        }

        private void StartListen()
        {
            // This item is obfuscated and can not be translated.
        }

        public void StartListenThread()
        {
            int expressionStack_27_0;
            int num2 = 13;
        Label_0021:
            expressionStack_27_0 = 1;
            if (expressionStack_27_0 == 0)
            {
            }
            bool isServerStarted = this.IsServerStarted;
            int num = 3;
        Label_000B:
            switch (num)
            {
                case 0:
                case 1:
                    return;

                case 2:
                    this.ListenThread = new Thread(new ThreadStart(this.StartListen));
                    this.ListenThread.Start();
                    this.ListenThread.IsBackground = true;
                    num = 0;
                    goto Label_000B;

                case 3:
                    if (isServerStarted)
                    {
                        CommandLog.WriteServerLog(SMouseEventArgs.b("㦑욽便ʲ㶏\udba7\u5ea7", num2));
                        num = 1;
                    }
                    else
                    {
                        num = 2;
                    }
                    goto Label_000B;
            }
            goto Label_0021;
        }
    }
}

