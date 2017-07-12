namespace LY.Remote.Core
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class MouseCommand
    {
        public int Delta { get; set; }

        public string Head { get; set; }

        public LY.Remote.Core.MouseEventFlag MouseEventFlag { get; set; }

        public int Width { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}

