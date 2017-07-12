namespace LY.Remote.Core
{
    using System;

    [Serializable]
    public enum MouseEventFlag
    {
        Absolute = 0x8000,
        LeftDown = 2,
        LeftUp = 4,
        MiddleDown = 0x20,
        MiddleUp = 0x40,
        Move = 1,
        RightDown = 8,
        RightUp = 0x10,
        VirtualDesk = 0x4000,
        Wheel = 0x800,
        XDown = 0x80,
        XUp = 0x100
    }
}

