namespace LY.Remote.Core
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Forms;
    using System.Windows.Input;

    public class ClientControl
    {
        private object _lock;
        private static Action<Action> _SafeRun = null;
        public int BorderWidth;
        public int CaptionHeight;
        private LoginCommand clientUser;
        private Socket CommandSocket;
        private Thread CommandThread;
        private ControlRequestEventHandler _ControlRequest;
        private ControlRespondEventHandler _ControlRespond;
        private bool DisconnectEventTrigger;
        private long encodeValue;
        public EventHandlerList Events;
        private GlobalHook Hook;
        private bool HookStart;
        private ManualResetEvent ImageBeginReset;
        private AutoResetEvent ImageSendReset;
        private Socket ImageSocket;
        private Thread ImageThread;
        private bool isControled;
        private bool isControler;
        private AutoResetEvent LoginOKReset;
        private DateTime MouseMoveTime;
        private System.Windows.Controls.Image screenControl;
        private Thread SendScreenThread;
        private string ServerIP;
        private int? ServerPort;
        private SetImageEventHandler _ShowScreenView;
        private ManualResetEvent TimeoutReset;
        private LoginRespondEventHandler UserLoginRespond;
        private LoginoutRespondEventHandler UserLogoutRespond;
        private AutoResetEvent WindowInitReset;

        public event ControlRequestEventHandler ControlRequest
        {
            add
            {
                ControlRequestEventHandler handler2;
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
                ControlRequestEventHandler controlRequest = this._ControlRequest;
                num = 2;
                goto Label_0010;
            Label_003D:
                handler2 = controlRequest;
                ControlRequestEventHandler handler3 = (ControlRequestEventHandler) Delegate.Combine(handler2, value);
                controlRequest = Interlocked.CompareExchange<ControlRequestEventHandler>(ref this._ControlRequest, handler3, handler2);
                flag = controlRequest != handler2;
                num = 0;
                goto Label_0010;
            }
            remove
            {
                ControlRequestEventHandler handler;
                bool flag;
            Label_0023:
                handler = this._ControlRequest;
                int num = 1;
            Label_0010:
                switch (num)
                {
                    case 0:
                        if ((flag ? 0 : 1) != 0)
                        {
                        }
                        num = 2;
                        goto Label_0010;

                    case 1:
                    {
                        ControlRequestEventHandler source = handler;
                        ControlRequestEventHandler handler3 = (ControlRequestEventHandler) Delegate.Remove(source, value);
                        handler = Interlocked.CompareExchange<ControlRequestEventHandler>(ref this._ControlRequest, handler3, source);
                        flag = handler != source;
                        num = 0;
                        goto Label_0010;
                    }
                    case 2:
                        return;
                }
                goto Label_0023;
            }
        }

        public event ControlRespondEventHandler ControlRespond
        {
            add
            {
                ControlRespondEventHandler handler2;
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
                ControlRespondEventHandler controlRespond = this.ControlRespond;
                num = 2;
                goto Label_0010;
            Label_003D:
                handler2 = controlRespond;
                ControlRespondEventHandler handler3 = (ControlRespondEventHandler) Delegate.Combine(handler2, value);
                controlRespond = Interlocked.CompareExchange<ControlRespondEventHandler>(ref this.ControlRespond, handler3, handler2);
                flag = controlRespond != handler2;
                num = 0;
                goto Label_0010;
            }
            remove
            {
                // This item is obfuscated and can not be translated.
            }
        }

        public event SetImageEventHandler ShowScreenView
        {
            add
            {
                // This item is obfuscated and can not be translated.
            }
            remove
            {
                SetImageEventHandler handler2;
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
                SetImageEventHandler showScreenView = this._ShowScreenView;
                num = 1;
                goto Label_0010;
            Label_003D:
                handler2 = showScreenView;
                SetImageEventHandler handler3 = (SetImageEventHandler) Delegate.Remove(handler2, value);
                showScreenView = Interlocked.CompareExchange<SetImageEventHandler>(ref this._ShowScreenView, handler3, handler2);
                flag = showScreenView != handler2;
                num = 2;
                goto Label_0010;
            }
        }

        public event LoginRespondEventHandler UserLoginRespond
        {
            add
            {
                LoginRespondEventHandler handler2;
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
                        goto Label_003D;

                    case 1:
                        return;

                    case 2:
                        if (flag)
                        {
                            goto Label_003D;
                        }
                        num = 1;
                        goto Label_0010;
                }
            Label_0023:
                expressionStack_29_0 = 1;
                if (expressionStack_29_0 == 0)
                {
                }
                LoginRespondEventHandler userLoginRespond = this.UserLoginRespond;
                num = 0;
                goto Label_0010;
            Label_003D:
                handler2 = userLoginRespond;
                LoginRespondEventHandler handler3 = (LoginRespondEventHandler) Delegate.Combine(handler2, value);
                userLoginRespond = Interlocked.CompareExchange<LoginRespondEventHandler>(ref this.UserLoginRespond, handler3, handler2);
                flag = userLoginRespond != handler2;
                num = 2;
                goto Label_0010;
            }
            remove
            {
                LoginRespondEventHandler handler2;
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
                LoginRespondEventHandler userLoginRespond = this.UserLoginRespond;
                num = 2;
                goto Label_0010;
            Label_003D:
                handler2 = userLoginRespond;
                LoginRespondEventHandler handler3 = (LoginRespondEventHandler) Delegate.Remove(handler2, value);
                userLoginRespond = Interlocked.CompareExchange<LoginRespondEventHandler>(ref this.UserLoginRespond, handler3, handler2);
                flag = userLoginRespond != handler2;
                num = 1;
                goto Label_0010;
            }
        }

        public event LoginoutRespondEventHandler UserLogoutRespond
        {
            add
            {
                LoginoutRespondEventHandler handler;
                bool flag;
            Label_0023:
                handler = this.UserLogoutRespond;
                int num = 1;
            Label_0010:
                switch (num)
                {
                    case 0:
                        return;

                    case 1:
                    {
                        LoginoutRespondEventHandler a = handler;
                        LoginoutRespondEventHandler handler3 = (LoginoutRespondEventHandler) Delegate.Combine(a, value);
                        handler = Interlocked.CompareExchange<LoginoutRespondEventHandler>(ref this.UserLogoutRespond, handler3, a);
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
                LoginoutRespondEventHandler handler;
                bool flag;
            Label_0023:
                handler = this.UserLogoutRespond;
                int num = 0;
            Label_0010:
                switch (num)
                {
                    case 0:
                    {
                        LoginoutRespondEventHandler source = handler;
                        LoginoutRespondEventHandler handler3 = (LoginoutRespondEventHandler) Delegate.Remove(source, value);
                        handler = Interlocked.CompareExchange<LoginoutRespondEventHandler>(ref this.UserLogoutRespond, handler3, source);
                        flag = handler != source;
                        num = 1;
                        goto Label_0010;
                    }
                    case 1:
                        if ((flag ? 0 : 1) != 0)
                        {
                        }
                        num = 2;
                        goto Label_0010;

                    case 2:
                        return;
                }
                goto Label_0023;
            }
        }

        public ClientControl()
        {
            int num = 1;
            this.UserLoginRespond = null;
            this.UserLogoutRespond = null;
            this.ShowScreenView = null;
            this.ControlRespond = null;
            this.ControlRequest = null;
            this.screenControl = null;
            this.clientUser = null;
            this.ImageSendReset = new AutoResetEvent(false);
            this.ImageBeginReset = new ManualResetEvent(false);
            this.TimeoutReset = new ManualResetEvent(false);
            this.WindowInitReset = new AutoResetEvent(false);
            this.LoginOKReset = new AutoResetEvent(false);
            this.CommandSocket = null;
            this.ImageSocket = null;
            this.SendScreenThread = null;
            this.MouseMoveTime = DateTime.Now;
            this._lock = new object();
            this.isControled = false;
            this.isControler = false;
            this.encodeValue = 60L;
            this.BorderWidth = 0;
            this.CaptionHeight = 0;
            this.ServerIP = SMouseEventArgs.b("ퟟ쳡퓣죥\ud8e7\uc4e9\uddeb", num);
            this.ServerPort = 0x1771;
            this.HookStart = false;
            this.Events = new EventHandlerList();
            this.DisconnectEventTrigger = false;
            if (this.Hook == null)
            {
                this.Hook = new GlobalHook();
                this.Hook.KeyDown += new System.Windows.Forms.KeyEventHandler(this.hook_KeyDown);
                this.Hook.KeyUp += new System.Windows.Forms.KeyEventHandler(this.hook_KeyUp);
                this.Hook.OnMouseActivity += new System.Windows.Forms.MouseEventHandler(this.hook_OnMouseActivity);
            }
        }

        private void BeginWindow()
        {
            SafeRun(delegate {
                int expressionStack_6_0 = 1;
                if (expressionStack_6_0 == 0)
                {
                }
                new ScreenWindow(this).Show();
                this.WindowInitReset.Set();
            });
        }

        public void CloseConnect()
        {
            new Thread(new ThreadStart(this.CloseConnectMethod)).Start();
        }

        private void CloseConnectMethod()
        {
            // This item is obfuscated and can not be translated.
        }

        private void CommandSocketConnect(object user)
        {
            // This item is obfuscated and can not be translated.
        }

        private string GetCursorStyle()
        {
            // This item is obfuscated and can not be translated.
        }

        private System.Drawing.Point GetPoint(int x, int y, System.Windows.Controls.Image child)
        {
            int expressionStack_6_0 = 1;
            if (expressionStack_6_0 == 0)
            {
            }
            Window ancestor = Window.GetWindow(child);
            System.Windows.Point point = child.TransformToAncestor(ancestor).Transform(new System.Windows.Point(0.0, 0.0));
            int num = x;
            int num2 = y;
            num -= Convert.ToInt32((double) ((point.X + ancestor.Left) + this.BorderWidth));
            return new System.Drawing.Point(num, num2 - Convert.ToInt32((double) (((point.Y + ancestor.Top) + this.CaptionHeight) + this.BorderWidth)));
        }

        private void hook_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.SendKeyCommand(e, KeyEventFlag.KEYEVENTF_KEYDOWN);
        }

        private void hook_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.SendKeyCommand(e, KeyEventFlag.KEYEVENTF_KEYUP);
        }

        private void hook_OnMouseActivity(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            bool flag;
        Label_0014:
            flag = this.screenControl == null;
            int num = 0;
        Label_0002:
            switch (num)
            {
                case 0:
                {
                    int expressionStack_2D_0 = 1;
                    if (expressionStack_2D_0 == 0)
                    {
                    }
                    if (!flag)
                    {
                        num = 1;
                        goto Label_0002;
                    }
                    break;
                }
                case 1:
                {
                    System.Drawing.Point lpPoint = new System.Drawing.Point();
                    WinAPI.GetCursorPos(out lpPoint);
                    System.Drawing.Point point2 = this.GetPoint(lpPoint.X, lpPoint.Y, this.screenControl);
                    this.SendMouseCommand(e, point2.X, point2.Y);
                    num = 2;
                    goto Label_0002;
                }
                case 2:
                    break;

                default:
                    goto Label_0014;
            }
        }

        private void ImageSocketConnect(object user)
        {
            // This item is obfuscated and can not be translated.
        }

        public void Login(LoginCommand user)
        {
            bool flag;
            int num;
            goto Label_0024;
        Label_0002:
            switch (num)
            {
                case 0:
                    if (flag)
                    {
                        goto Label_0110;
                    }
                    num = 2;
                    goto Label_0002;

                case 1:
                case 4:
                    return;

                case 2:
                {
                    int expressionStack_50_0 = 1;
                    if (expressionStack_50_0 == 0)
                    {
                    }
                    LoginRespondCommand loginResultCmd = new LoginRespondCommand {
                        UserName = user.UserName,
                        RespondType = LoginRespondType.Logined
                    };
                    new Thread(delegate {
                        int expressionStack_6_0 = 1;
                        if (expressionStack_6_0 == 0)
                        {
                        }
                        this.UserLoginRespond(this, new LoginRespondEventArgs(loginResultCmd));
                    }).Start();
                    num = 3;
                    goto Label_0002;
                }
                case 3:
                    goto Label_0110;

                case 5:
                    if (flag)
                    {
                        flag = this.UserLoginRespond == null;
                        num = 0;
                    }
                    else
                    {
                        num = 6;
                    }
                    goto Label_0002;

                case 6:
                    this.CommandThread = new Thread(new ParameterizedThreadStart(this.CommandSocketConnect));
                    this.CommandThread.IsBackground = true;
                    this.CommandThread.Start(user);
                    num = 4;
                    goto Label_0002;
            }
        Label_0024:
            flag = this.ClientUser != null;
            num = 5;
            goto Label_0002;
        Label_0110:
            num = 1;
            goto Label_0002;
        }

        public void LoginImg()
        {
            int expressionStack_6_0 = 1;
            if (expressionStack_6_0 == 0)
            {
            }
            this.ImageThread = new Thread(new ParameterizedThreadStart(this.ImageSocketConnect));
            this.ImageThread.IsBackground = true;
            this.ImageThread.Start(this.ClientUser);
        }

        private bool MoveInRemoteWindow()
        {
            // This item is obfuscated and can not be translated.
            bool flag2;
            bool hookStart;
            int num;
            goto Label_0030;
            if (1 != 0)
            {
            }
            switch (num)
            {
                case 0:
                {
                    bool flag = this.Hook.Start();
                    this.HookStart = true;
                    flag2 = flag;
                    num = 1;
                    goto Label_0008;
                }
                case 1:
                    return flag2;

                case 2:
                case 4:
                    hookStart = this.HookStart;
                    num = 6;
                    goto Label_0008;

                case 3:
                    return flag2;

                case 5:
                    if (!this.IsControler)
                    {
                        num = 4;
                    }
                    else
                    {
                        num = 7;
                    }
                    goto Label_0008;

                case 6:
                    if (hookStart)
                    {
                        flag2 = false;
                        num = 3;
                    }
                    else
                    {
                        num = 0;
                    }
                    goto Label_0008;

                case 7:
                    num = 2;
                    goto Label_0008;
            }
        Label_0030:
            num = 5;
            goto Label_0008;
        }

        private void MoveOutRemoteWindow()
        {
            // This item is obfuscated and can not be translated.
        }

        private void OpenTask()
        {
        }

        private void screen_MouseEnter(object sender, EventArgs e)
        {
            this.MoveInRemoteWindow();
        }

        private void screen_MouseLeave(object sender, EventArgs e)
        {
            this.MoveOutRemoteWindow();
        }

        private void screenView_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.MoveInRemoteWindow();
        }

        private void screenView_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.MoveOutRemoteWindow();
        }

        public void SendCommonCommand(CommonCommandType type)
        {
            // This item is obfuscated and can not be translated.
        }

        public void SendControlConfirm(ControlRequestCommand controlRequest, ControlConfirmType confirmType)
        {
            // This item is obfuscated and can not be translated.
            byte[] bytes;
            string str;
            bool flag;
            int num2 = 11;
            int num = 0;
            switch (num)
            {
                default:
                    goto Label_0040;
            }
        Label_0019:
            switch (num)
            {
                case 0:
                    str = string.Format(SMouseEventArgs.b("싥\ud9e7\u96e9韫\udeed\u8def軱迳쟵藷蛹蟻쳽緿縁缃㔅甇瘉刋", num2), new object[] { controlRequest.Controler, controlRequest.Controled, (int) controlRequest.RequestType, 3 });
                    controlRequest.ConfirmType = ControlConfirmType.OK;
                    bytes = Encoding.UTF8.GetBytes(str);
                    this.CommandSocket.Send(bytes, bytes.Length, SocketFlags.None);
                    num = 7;
                    goto Label_0019;

                case 1:
                case 2:
                    flag = confirmType != ControlConfirmType.Agree;
                    num = 6;
                    goto Label_0019;

                case 3:
                    num = 1;
                    goto Label_0019;

                case 4:
                case 7:
                    return;

                case 5:
                    if (controlRequest.RequestType != ControlRequestType.CanYouControlMe)
                    {
                        num = 2;
                        goto Label_0019;
                    }
                    goto Label_006D;

                case 6:
                    if (flag)
                    {
                        str = string.Format(SMouseEventArgs.b("싥\ud9e7\u96e9韫\udeed\u8def軱迳쟵藷蛹蟻쳽緿縁缃㔅甇瘉刋", num2), new object[] { controlRequest.Controler, controlRequest.Controled, (int) controlRequest.RequestType, (int) confirmType });
                        controlRequest.ConfirmType = confirmType;
                        bytes = Encoding.UTF8.GetBytes(str);
                        this.CommandSocket.Send(bytes, bytes.Length, SocketFlags.None);
                        num = 4;
                    }
                    else
                    {
                        num = 0;
                    }
                    goto Label_0019;
            }
        Label_0040:
            bytes = new byte[0x100];
            str = string.Empty;
            num = 5;
            goto Label_0019;
            if (1 != 0)
            {
            }
            num = 3;
            goto Label_0019;
        }

        public void SendControlRequest(string remoteUser, ControlRequestType controType)
        {
            // This item is obfuscated and can not be translated.
        }

        public void SendCursorStyle(string shape)
        {
            // This item is obfuscated and can not be translated.
        }

        private void SendKeyCommand(System.Windows.Forms.KeyEventArgs e, KeyEventFlag flag)
        {
            int num2 = 8;
            int expressionStack_F_0 = 1;
            if (expressionStack_F_0 == 0)
            {
            }
            int num = 0;
            try
            {
                string str;
                byte[] bytes;
                bool flag2;
                bool expressionStack_F0_0;
                goto Label_004B;
            Label_0024:
                switch (num)
                {
                    case 0:
                        if (this.CommandSocket == null)
                        {
                            goto Label_00E1;
                        }
                        num = 1;
                        goto Label_0024;

                    case 1:
                        num = 2;
                        goto Label_0024;

                    case 2:
                        expressionStack_F0_0 = !this.CommandSocket.Connected;
                        goto Label_00F0;

                    case 3:
                        return;

                    case 4:
                        this.CommandSocket.Send(bytes, bytes.Length, SocketFlags.None);
                        num = 7;
                        goto Label_0024;

                    case 5:
                        expressionStack_F0_0 = true;
                        goto Label_00F0;

                    case 6:
                        if (flag2)
                        {
                            goto Label_010F;
                        }
                        num = 4;
                        goto Label_0024;

                    case 7:
                        goto Label_010F;
                }
            Label_004B:
                str = string.Format(SMouseEventArgs.b("쟢駤鳦\ud9e8\u96ea釬铮샰軲觴꧶", num2), e.KeyValue, (int) flag);
                bytes = new byte[0x100];
                bytes = Encoding.UTF8.GetBytes(str);
                num = 0;
                goto Label_0024;
            Label_00E1:
                num = 5;
                goto Label_0024;
            Label_00F0:
                flag2 = expressionStack_F0_0;
                num = 6;
                goto Label_0024;
            Label_010F:
                num = 3;
                goto Label_0024;
            }
            catch (Exception exception)
            {
                CommandLog.LogAction(string.Format(SMouseEventArgs.b("飢헤髦쓨웪샬싮\udcf0\u88f2쓴諶", num2), SMouseEventArgs.b("냢胤触跨쯪ꛬ諮裰폲뛴飶铸雺鳼釾攀⌂䀄甆笈搊缌", num2), exception.Message));
            }
        }

        private void SendMouseCommand(System.Windows.Forms.MouseEventArgs e, int screenX, int screenY)
        {
            // This item is obfuscated and can not be translated.
        }

        public void SendPictureCompress(PictureCompressType type)
        {
            int num = 5;
            int expressionStack_F_0 = 1;
            if (expressionStack_F_0 == 0)
            {
            }
            try
            {
                byte[] bytes = new byte[0x100];
                string s = string.Empty;
                s = string.Format(SMouseEventArgs.b("쓟퇡飣鷥\ud8e7\u97e9郫냭", num), (int) type);
                bytes = Encoding.UTF8.GetBytes(s);
                this.CommandSocket.Send(bytes, bytes.Length, SocketFlags.None);
            }
            catch (SocketException exception)
            {
                CommandLog.LogAction(string.Format(SMouseEventArgs.b("鯟틡駣쯥엧쟩쇫쏭误쏱觳", num), SMouseEventArgs.b("ꏟ跡解该觧蓩裫뷭鿯釱鿳鏵賷\udaf9\uaffb鯽瓿刁洃攅簇缉縋欍匏紑礓昕樗缙漛洝\x001f無䬣䔥䌧伩堫欭䠯儱儳䘵䰷匹医倽", num), exception.Message));
                this.CloseConnect();
            }
            catch (Exception exception2)
            {
                CommandLog.LogAction(string.Format(SMouseEventArgs.b("鯟틡駣쯥엧쟩쇫쏭误쏱觳", num), SMouseEventArgs.b("ꏟ跡解该觧蓩裫뷭鿯釱鿳鏵賷\udaf9\uaffb鯽瓿刁洃攅簇缉縋欍匏紑礓昕樗缙漛洝\x001f朡尣䔥䴧娩堫䜭弯就", num), exception2.Message));
            }
        }

        private void SendScreen()
        {
            byte[] buffer;
            bool flag;
            int expressionStack_36_0;
            int num2 = 6;
            int num = 0;
            switch (num)
            {
                default:
                    goto Label_0030;
            }
        Label_0019:
            switch (num)
            {
                case 0:
                case 3:
                    goto Label_01FD;

                case 1:
                    try
                    {
                        int expressionStack_C1_0;
                        goto Label_007F;
                    Label_0054:
                        switch (num)
                        {
                            case 0:
                                num = 4;
                                goto Label_0054;

                            case 1:
                                goto Label_0233;

                            case 2:
                                if (flag)
                                {
                                    goto Label_0131;
                                }
                                num = 7;
                                goto Label_0054;

                            case 3:
                                expressionStack_C1_0 = 1;
                                goto Label_00C1;

                            case 4:
                                expressionStack_C1_0 = (int) !this.ImageSocket.Connected;
                                goto Label_00C1;

                            case 5:
                                goto Label_0223;

                            case 6:
                                num = 5;
                                goto Label_0054;

                            case 7:
                            {
                                MemoryStream stream = ScreenCapture.Capture(this.EncodeValue);
                                buffer = stream.GetBuffer();
                                SocketHelper.SendDataWithSize(this.ImageSocket, buffer);
                                stream.Close();
                                stream.Dispose();
                                this.ImageSendReset.WaitOne();
                                Thread.Sleep(10);
                                num = 6;
                                goto Label_0054;
                            }
                            case 8:
                                if (this.ImageSocket == null)
                                {
                                    goto Label_00B5;
                                }
                                num = 0;
                                goto Label_0054;
                        }
                    Label_007F:
                        num = 8;
                        goto Label_0054;
                    Label_00B5:
                        num = 3;
                        goto Label_0054;
                    Label_00C1:
                        flag = (bool) expressionStack_C1_0;
                        num = 2;
                        goto Label_0054;
                    Label_0131:
                        num = 1;
                        goto Label_0054;
                    }
                    catch (SocketException exception)
                    {
                        CommandLog.LogAction(string.Format(SMouseEventArgs.b("髠폢飤쫦쓨웪샬싮諰싲裴", num2), SMouseEventArgs.b("닠蛢诤菦뫨裪鿬諮铰鷲헴ꓶ雸飺雼髾甀䘂約搆氈笊礌明縐紒", num2), exception.Message));
                        goto Label_0233;
                    }
                    catch (ThreadAbortException exception2)
                    {
                        CommandLog.LogAction(string.Format(SMouseEventArgs.b("髠폢飤쫦쓨웪샬싮諰싲裴", num2), SMouseEventArgs.b("닠蛢诤菦뫨裪鿬諮铰鷲헴ꏶ釸觺飼黾攀䈂朄栆笈缊䠌眎爐瘒攔挖瀘琚猜", num2), exception2.Message));
                        goto Label_0233;
                    }
                    catch (Exception exception3)
                    {
                        CommandLog.LogAction(string.Format(SMouseEventArgs.b("髠폢飤쫦쓨웪샬싮諰싲裴", num2), SMouseEventArgs.b("닠蛢诤菦뫨裪鿬諮铰鷲헴닶臸飺飼迾甀樂樄椆", num2), exception3.Message));
                        goto Label_0223;
                    }
                    goto Label_01FD;

                case 2:
                    num = 1;
                    goto Label_0019;
            }
        Label_0030:
            expressionStack_36_0 = 1;
            if (expressionStack_36_0 == 0)
            {
            }
            buffer = new byte[0xfa000];
            num = 3;
            goto Label_0019;
        Label_01FD:
            flag = true;
            num = 2;
            goto Label_0019;
        Label_0223:
            num = 0;
            goto Label_0019;
        Label_0233:
            this.CloseConnect();
        }

        private void SetCursorStyle(string style)
        {
            int expressionStack_6_0 = 1;
            if (expressionStack_6_0 == 0)
            {
            }
            this.ScreenControl.Dispatcher.Invoke(delegate {
                // This item is obfuscated and can not be translated.
            });
        }

        public bool SetServerAddress(string ip, int port)
        {
            // This item is obfuscated and can not be translated.
        }

        public void TestSocket()
        {
            System.Windows.MessageBox.Show(this.CommandSocket.Connected.ToString());
        }

        public LoginCommand ClientUser
        {
            get => 
                this.clientUser;
            set
            {
                this.clientUser = value;
            }
        }

        public long EncodeValue
        {
            get => 
                this.encodeValue;
            set
            {
                this.encodeValue = value;
            }
        }

        protected bool IsControled
        {
            get => 
                this.isControled;
            set
            {
                bool flag;
            Label_0018:
                flag = !value;
                int num = 2;
            Label_0002:
                switch (num)
                {
                    case 0:
                        this.ImageBeginReset.Set();
                        num = 3;
                        goto Label_0002;

                    case 1:
                    case 3:
                        this.isControled = value;
                        return;

                    case 2:
                    {
                        int expressionStack_2C_0 = 1;
                        if (expressionStack_2C_0 == 0)
                        {
                        }
                        if (!flag)
                        {
                            num = 0;
                        }
                        else
                        {
                            this.ImageBeginReset.Reset();
                            num = 1;
                        }
                        goto Label_0002;
                    }
                }
                goto Label_0018;
            }
        }

        protected bool IsControler
        {
            get => 
                this.isControler;
            set
            {
                bool flag;
            Label_0018:
                flag = !value;
                int expressionStack_24_0 = 1;
                if (expressionStack_24_0 == 0)
                {
                }
                int num = 1;
            Label_0002:
                switch (num)
                {
                    case 0:
                        this.ImageBeginReset.Set();
                        num = 2;
                        goto Label_0002;

                    case 1:
                        if (flag)
                        {
                            this.ImageBeginReset.Reset();
                            num = 3;
                        }
                        else
                        {
                            num = 0;
                        }
                        goto Label_0002;

                    case 2:
                    case 3:
                        this.isControler = value;
                        return;
                }
                goto Label_0018;
            }
        }

        public static Action<Action> SafeRun
        {
            get => 
                _SafeRun;
            set
            {
                _SafeRun = value;
            }
        }

        public System.Windows.Controls.Image ScreenControl
        {
            get => 
                this.screenControl;
            set
            {
                int expressionStack_6_0 = 1;
                if (expressionStack_6_0 == 0)
                {
                }
                this.screenControl = value;
                this.screenControl.MouseEnter += new System.Windows.Input.MouseEventHandler(this.screenView_MouseEnter);
                this.screenControl.MouseLeave += new System.Windows.Input.MouseEventHandler(this.screenView_MouseLeave);
            }
        }
    }
}

