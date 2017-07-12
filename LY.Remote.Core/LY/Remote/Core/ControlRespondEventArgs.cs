namespace LY.Remote.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public class ControlRespondEventArgs : EventArgs
    {
        public ControlRespondEventArgs(ControlRespondCommand controlRespond)
        {
            this.ControlRespond = controlRespond;
        }

        public ControlRespondCommand ControlRespond { get; set; }
    }
}

