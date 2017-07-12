namespace LY.Remote.Core
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class KeyCommand
    {
        public string Head { get; set; }

        public LY.Remote.Core.KeyEventFlag KeyEventFlag { get; set; }

        public byte KeyValue { get; set; }
    }
}

