namespace LY.Remote.Core
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public class MouseLLHookStruct
    {
        public POINT pt;
        public int mouseData;
        public int flags;
        public int time;
        public int dwExtraInfo;
    }
}

