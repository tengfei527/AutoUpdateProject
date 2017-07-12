namespace LY.Remote.Core
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    public class GlobalHook
    {
        private static int _hKeyboardHook = 0;
        private static int _hMouseHook = 0;
        private GlobalHookProc KeyboardHookProcedure;
        private KeyEventHandler KeyDown;
        private KeyPressEventHandler KeyPress;
        private KeyEventHandler KeyUp;
        private GlobalHookProc MouseHookProcedure;
        private MouseEventHandler OnMouseActivity;
        public const int WH_KEYBOARD_LL = 13;
        public const int WH_MOUSE_LL = 14;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_LBUTTONDBLCLK = 0x203;
        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_LBUTTONUP = 0x202;
        private const int WM_MBUTTONDBLCLK = 0x209;
        private const int WM_MBUTTONDOWN = 0x207;
        private const int WM_MBUTTONUP = 520;
        private const int WM_MOUSEMOVE = 0x200;
        private const int WM_MOUSEWHEEL = 0x20a;
        private const int WM_RBUTTONDBLCLK = 0x206;
        private const int WM_RBUTTONDOWN = 0x204;
        private const int WM_RBUTTONUP = 0x205;
        private const int WM_SYSKEYDOWN = 260;
        private const int WM_SYSKEYUP = 0x105;

        public event KeyEventHandler KeyDown
        {
            add
            {
                // This item is obfuscated and can not be translated.
            }
            remove
            {
                KeyEventHandler handler2;
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
                        if (flag)
                        {
                            goto Label_003D;
                        }
                        num = 2;
                        goto Label_0010;

                    case 2:
                        return;
                }
            Label_0023:
                expressionStack_29_0 = 1;
                if (expressionStack_29_0 == 0)
                {
                }
                KeyEventHandler keyDown = this.KeyDown;
                num = 0;
                goto Label_0010;
            Label_003D:
                handler2 = keyDown;
                KeyEventHandler handler3 = (KeyEventHandler) Delegate.Remove(handler2, value);
                keyDown = Interlocked.CompareExchange<KeyEventHandler>(ref this.KeyDown, handler3, handler2);
                flag = keyDown != handler2;
                num = 1;
                goto Label_0010;
            }
        }

        public event KeyPressEventHandler KeyPress
        {
            add
            {
                KeyPressEventHandler handler2;
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
                KeyPressEventHandler keyPress = this.KeyPress;
                num = 2;
                goto Label_0010;
            Label_003D:
                handler2 = keyPress;
                KeyPressEventHandler handler3 = (KeyPressEventHandler) Delegate.Combine(handler2, value);
                keyPress = Interlocked.CompareExchange<KeyPressEventHandler>(ref this.KeyPress, handler3, handler2);
                flag = keyPress != handler2;
                num = 0;
                goto Label_0010;
            }
            remove
            {
                // This item is obfuscated and can not be translated.
            }
        }

        public event KeyEventHandler KeyUp
        {
            add
            {
                KeyEventHandler handler2;
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
                KeyEventHandler keyUp = this.KeyUp;
                num = 0;
                goto Label_0010;
            Label_003D:
                handler2 = keyUp;
                KeyEventHandler handler3 = (KeyEventHandler) Delegate.Combine(handler2, value);
                keyUp = Interlocked.CompareExchange<KeyEventHandler>(ref this.KeyUp, handler3, handler2);
                flag = keyUp != handler2;
                num = 2;
                goto Label_0010;
            }
            remove
            {
                KeyEventHandler handler;
                bool flag;
            Label_0023:
                handler = this.KeyUp;
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
                        KeyEventHandler source = handler;
                        KeyEventHandler handler3 = (KeyEventHandler) Delegate.Remove(source, value);
                        handler = Interlocked.CompareExchange<KeyEventHandler>(ref this.KeyUp, handler3, source);
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

        public event MouseEventHandler OnMouseActivity
        {
            add
            {
                MouseEventHandler handler;
                bool flag;
            Label_0023:
                handler = this.OnMouseActivity;
                int num = 0;
            Label_0010:
                switch (num)
                {
                    case 0:
                    {
                        MouseEventHandler a = handler;
                        MouseEventHandler handler3 = (MouseEventHandler) Delegate.Combine(a, value);
                        handler = Interlocked.CompareExchange<MouseEventHandler>(ref this.OnMouseActivity, handler3, a);
                        flag = handler != a;
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
            remove
            {
                MouseEventHandler handler;
                bool flag;
            Label_0023:
                handler = this.OnMouseActivity;
                int num = 1;
            Label_0010:
                switch (num)
                {
                    case 0:
                        return;

                    case 1:
                    {
                        MouseEventHandler source = handler;
                        MouseEventHandler handler3 = (MouseEventHandler) Delegate.Remove(source, value);
                        handler = Interlocked.CompareExchange<MouseEventHandler>(ref this.OnMouseActivity, handler3, source);
                        flag = handler != source;
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
        }

        [DllImport("user32.dll", CallingConvention=CallingConvention.StdCall, CharSet=CharSet.Auto)]
        public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);
        ~GlobalHook()
        {
            int expressionStack_6_0 = 1;
            if (expressionStack_6_0 == 0)
            {
            }
            this.Stop();
        }

        [DllImport("user32")]
        public static extern int GetKeyboardState(byte[] pbKeyState);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);
        private int KeyboardHookProc(int nCode, int wParam, IntPtr lParam)
        {
            // This item is obfuscated and can not be translated.
        }

        private int MouseHookProc(int nCode, int wParam, IntPtr lParam)
        {
            // This item is obfuscated and can not be translated.
            MouseLLHookStruct struct2;
            int num;
            SMouseEventArgs args;
            int num2;
            bool flag;
            int num3 = 0;
            switch (num3)
            {
                default:
                    goto Label_0043;
            }
        Label_0010:
            switch (num3)
            {
                case 0:
                    return num2;

                case 1:
                    goto Label_00DB;

                case 2:
                    goto Label_010E;

                case 3:
                    if (flag)
                    {
                        goto Label_010E;
                    }
                    num3 = 5;
                    goto Label_0010;

                case 4:
                    if (nCode < 0)
                    {
                        num3 = 8;
                    }
                    else
                    {
                        num3 = 10;
                    }
                    goto Label_0010;

                case 5:
                    num = (short) ((struct2.mouseData >> 0x10) & 0xffff);
                    goto Label_0078;

                case 6:
                    struct2 = (MouseLLHookStruct) Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));
                    num = 0;
                    flag = wParam != 0x20a;
                    num3 = 3;
                    goto Label_0010;

                case 7:
                case 8:
                    flag = this.OnMouseActivity == null;
                    num3 = 9;
                    goto Label_0010;

                case 9:
                    if (flag)
                    {
                        goto Label_00DB;
                    }
                    num3 = 6;
                    goto Label_0010;

                case 10:
                    num3 = 7;
                    goto Label_0010;
            }
        Label_0043:
            num3 = 4;
            goto Label_0010;
            if (1 != 0)
            {
            }
            num3 = 2;
            goto Label_0010;
        Label_00DB:
            num2 = CallNextHookEx(_hMouseHook, nCode, wParam, lParam);
            num3 = 0;
            goto Label_0010;
        Label_010E:
            args = new SMouseEventArgs(MouseButtons.None, 0, struct2.pt.x, struct2.pt.y, num, wParam);
            this.OnMouseActivity(this, args);
            num3 = 1;
            goto Label_0010;
        }

        [DllImport("user32.dll", CallingConvention=CallingConvention.StdCall, CharSet=CharSet.Auto)]
        public static extern int SetWindowsHookEx(int idHook, GlobalHookProc lpfn, IntPtr hInstance, int threadId);
        public bool Start()
        {
            bool flag;
            bool flag2;
            int expressionStack_59_0;
            int num = 0;
            switch (num)
            {
                default:
                    goto Label_0053;
            }
        Label_0010:
            switch (num)
            {
                case 0:
                    this.KeyboardHookProcedure = new GlobalHookProc(this.KeyboardHookProc);
                    num = 11;
                    goto Label_0010;

                case 1:
                    if (flag2)
                    {
                        goto Label_0131;
                    }
                    num = 4;
                    goto Label_0010;

                case 2:
                    if (flag2)
                    {
                        goto Label_011E;
                    }
                    num = 0;
                    goto Label_0010;

                case 3:
                    if (flag2)
                    {
                        num = 5;
                    }
                    else
                    {
                        num = 12;
                    }
                    goto Label_0010;

                case 4:
                    this.MouseHookProcedure = new GlobalHookProc(this.MouseHookProc);
                    num = 14;
                    goto Label_0010;

                case 5:
                    goto Label_011E;

                case 6:
                    if (flag2)
                    {
                        num = 13;
                    }
                    else
                    {
                        num = 8;
                    }
                    goto Label_0010;

                case 7:
                    return flag;

                case 8:
                    this.Stop();
                    flag = false;
                    num = 7;
                    goto Label_0010;

                case 9:
                    return flag;

                case 10:
                    return flag;

                case 11:
                    try
                    {
                        _hKeyboardHook = SetWindowsHookEx(13, this.KeyboardHookProcedure, GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);
                    }
                    catch (Exception)
                    {
                    }
                    flag2 = _hKeyboardHook != 0;
                    num = 3;
                    goto Label_0010;

                case 12:
                    this.Stop();
                    flag = false;
                    num = 10;
                    goto Label_0010;

                case 13:
                    goto Label_0131;

                case 14:
                    try
                    {
                        _hMouseHook = SetWindowsHookEx(14, this.MouseHookProcedure, GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);
                    }
                    catch (Exception)
                    {
                    }
                    flag2 = _hMouseHook != 0;
                    num = 6;
                    goto Label_0010;
            }
        Label_0053:
            expressionStack_59_0 = 1;
            if (expressionStack_59_0 == 0)
            {
            }
            flag2 = _hMouseHook != 0;
            num = 1;
            goto Label_0010;
        Label_011E:
            flag = true;
            num = 9;
            goto Label_0010;
        Label_0131:
            flag2 = _hKeyboardHook != 0;
            num = 2;
            goto Label_0010;
        }

        public void Stop()
        {
            // This item is obfuscated and can not be translated.
        }

        public void Stop(int hMouseHook, int hKeyboardHook)
        {
            bool flag;
            int num;
            goto Label_0020;
        Label_0002:
            switch (num)
            {
                case 0:
                    return;

                case 1:
                {
                    int expressionStack_34_0 = 1;
                    if (expressionStack_34_0 == 0)
                    {
                    }
                    if (!flag)
                    {
                        num = 4;
                        goto Label_0002;
                    }
                    goto Label_0056;
                }
                case 2:
                    if (flag)
                    {
                        return;
                    }
                    num = 5;
                    goto Label_0002;

                case 3:
                    goto Label_0056;

                case 4:
                    UnhookWindowsHookEx(hMouseHook);
                    num = 3;
                    goto Label_0002;

                case 5:
                    UnhookWindowsHookEx(hKeyboardHook);
                    num = 0;
                    goto Label_0002;
            }
        Label_0020:
            flag = hMouseHook == 0;
            num = 1;
            goto Label_0002;
        Label_0056:
            flag = hKeyboardHook == 0;
            num = 2;
            goto Label_0002;
        }

        [DllImport("user32")]
        public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);
        [DllImport("user32.dll", CallingConvention=CallingConvention.StdCall, CharSet=CharSet.Auto)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        public int HKeyboardHook =>
            _hKeyboardHook;

        public int HMouseHook =>
            _hMouseHook;

        public delegate int GlobalHookProc(int nCode, int wParam, IntPtr lParam);

        public delegate int HookProc(int nCode, int wParam, IntPtr lParam);
    }
}

