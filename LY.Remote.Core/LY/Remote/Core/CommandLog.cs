namespace LY.Remote.Core
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;

    public class CommandLog
    {
        public static object _lock = new object();
        private static Action<string> _LogAction = null;

        public static void WriteClientLog(string detail)
        {
            // This item is obfuscated and can not be translated.
        }

        public static void WriteServerLog(string log)
        {
            object obj2;
            int num2 = 0x11;
            int num = 0;
            int expressionStack_1D_0 = 1;
            if (expressionStack_1D_0 == 0)
            {
            }
            bool lockTaken = false;
            try
            {
                Monitor.Enter(obj2 = _lock, ref lockTaken);
                try
                {
                    FileStream stream = new FileStream(Application.StartupPath + SMouseEventArgs.b("냫鷭闯胱苳鏵諷퓹裻蛽瓿", num2), FileMode.Append);
                    StreamWriter writer = new StreamWriter(stream);
                    string str = string.Format(SMouseEventArgs.b("韫\udeed\u8def\udff1\ud9f3\udbf5\ud5f7ퟹ퇻藽ㇿ缁⤃⬅┇✉ℋ⌍", num2), DateTime.Now.ToString(SMouseEventArgs.b("闫韭觯诱\ud9f3\ubbf5뗷ퟹ飻髽⃿䨁䰃㰅攇有㘋納挏⠑爓瀕縗", num2)), log);
                    writer.WriteLine(str);
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }
                catch (Exception)
                {
                }
            }
            finally
            {
                bool flag2;
                goto Label_00CE;
            Label_00BB:
                switch (num)
                {
                    case 0:
                        if (flag2)
                        {
                            goto Label_0100;
                        }
                        num = 2;
                        goto Label_00BB;

                    case 1:
                        goto Label_0100;

                    case 2:
                        Monitor.Exit(obj2);
                        num = 1;
                        goto Label_00BB;
                }
            Label_00CE:
                flag2 = !lockTaken;
                num = 0;
                goto Label_00BB;
            Label_0100:;
            }
        }

        public static void WriteServerLog(string title, string detail)
        {
            // This item is obfuscated and can not be translated.
        }

        public static Action<string> LogAction
        {
            get
            {
                Action<string> action;
                bool flag;
            Label_0018:
                flag = _LogAction != null;
                int expressionStack_2B_0 = 1;
                if (expressionStack_2B_0 == 0)
                {
                }
                int num = 1;
            Label_0002:
                switch (num)
                {
                    case 0:
                        return action;

                    case 1:
                        if (flag)
                        {
                            action = _LogAction;
                            num = 3;
                        }
                        else
                        {
                            num = 2;
                        }
                        goto Label_0002;

                    case 2:
                        action = new Action<string>(CommandLog.WriteClientLog);
                        num = 0;
                        goto Label_0002;

                    case 3:
                        return action;
                }
                goto Label_0018;
            }
            set
            {
                _LogAction = value;
            }
        }
    }
}

