namespace LY.Remote.Core
{
    using System;
    using System.Collections.Generic;

    public class DecodeCommand
    {
        public static CommonCommandType DecodeCommonCommand(string value)
        {
            // This item is obfuscated and can not be translated.
        }

        public static ControlRequestCommand DecodeControlRequestCommand(string value)
        {
            // This item is obfuscated and can not be translated.
        }

        public static ControlRespondCommand DecodeControlRespondCommand(string value)
        {
            // This item is obfuscated and can not be translated.
            string[] strArray;
            ControlRespondCommand command2;
            bool flag;
            int num2 = 2;
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
                    if (flag)
                    {
                        command2 = null;
                        num = 5;
                    }
                    else
                    {
                        num = 6;
                    }
                    goto Label_0019;

                case 1:
                case 3:
                    flag = strArray.Length <= 0;
                    num = 0;
                    goto Label_0019;

                case 2:
                    if (strArray == null)
                    {
                        num = 3;
                        goto Label_0019;
                    }
                    goto Label_0068;

                case 4:
                    return command2;

                case 5:
                    return command2;

                case 6:
                {
                    ControlRespondCommand command = new ControlRespondCommand();
                    string[] strArray2 = strArray[0].Split(new char[] { '|' });
                    command.Controler = strArray2[1];
                    command.Controled = strArray2[2];
                    command.RespondType = (ControlRespondType) int.Parse(strArray2[3]);
                    command2 = command;
                    num = 4;
                    goto Label_0019;
                }
                case 7:
                    num = 1;
                    goto Label_0019;
            }
        Label_0040:
            strArray = GetMatches(value, SMouseEventArgs.b("臜ﯞ폠뿢駤짦싨퓪뇬鏮\udff0\ud8f2쫴꯶藸ꟺ駼퓾㸀异礄嬆圈", num2));
            num = 2;
            goto Label_0019;
            if (1 != 0)
            {
            }
            num = 7;
            goto Label_0019;
        }

        public static List<string> DecodeCursorCommand(string value)
        {
            // This item is obfuscated and can not be translated.
        }

        public static List<KeyCommand> DecodeKeyCommand(string value)
        {
            // This item is obfuscated and can not be translated.
        }

        public static LoginCommand DecodeLoginCommand(string value)
        {
            // This item is obfuscated and can not be translated.
        }

        public static LoginRespondCommand DecodeLoginRespondCommand(string value)
        {
            // This item is obfuscated and can not be translated.
        }

        public static List<MouseCommand> DecodeMouseCommand(string value)
        {
            List<MouseCommand> list;
            string[] matches;
            List<MouseCommand> list2;
            bool flag;
            string[] strArray3;
            int num;
            int num3 = 7;
            int num2 = 0;
            switch (num2)
            {
                default:
                    goto Label_0040;
            }
        Label_0019:
            switch (num2)
            {
                case 0:
                    return list2;

                case 1:
                    if (flag)
                    {
                        string str = strArray3[num];
                        MouseCommand item = new MouseCommand();
                        string[] strArray2 = str.Split(new char[] { '|' });
                        item.X = int.Parse(strArray2[1]);
                        item.Y = int.Parse(strArray2[2]);
                        item.MouseEventFlag = (MouseEventFlag) int.Parse(strArray2[3]);
                        item.Delta = int.Parse(strArray2[4]);
                        item.Width = int.Parse(strArray2[5]);
                        list.Add(item);
                        num++;
                        num2 = 7;
                    }
                    else
                    {
                        num2 = 4;
                    }
                    goto Label_0019;

                case 2:
                case 7:
                    flag = num < strArray3.Length;
                    num2 = 1;
                    goto Label_0019;

                case 3:
                    goto Label_00D5;

                case 4:
                    num2 = 3;
                    goto Label_0019;

                case 5:
                    strArray3 = matches;
                    num = 0;
                    num2 = 2;
                    goto Label_0019;

                case 6:
                {
                    if (flag)
                    {
                        goto Label_00D5;
                    }
                    int expressionStack_78_0 = 1;
                    if (expressionStack_78_0 == 0)
                    {
                    }
                    num2 = 5;
                    goto Label_0019;
                }
            }
        Label_0040:
            list = new List<MouseCommand>();
            matches = GetMatches(value, SMouseEventArgs.b("뻡샣뫥铧뛩裫역쿯껱裳ꫵ鳷퇹쏻ꋽ糿币怃ⴅ㜇嘉瀋唍䰏㼑䠓爕䔗ㄙ⌛䈝尟縡䀣ഥᜧ瘩倫爭港", num3));
            flag = matches == null;
            num2 = 6;
            goto Label_0019;
        Label_00D5:
            list2 = list;
            num2 = 0;
            goto Label_0019;
        }

        public static PictureCompressType DecodePictureCompressCommand(string value)
        {
            // This item is obfuscated and can not be translated.
        }

        public static string[] GetMatches(string value, string regx)
        {
            // This item is obfuscated and can not be translated.
        }
    }
}

