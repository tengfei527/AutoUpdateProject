namespace LY.Remote.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public class ControlRequestEventArgs : EventArgs
    {
        public ControlRequestEventArgs(ControlRequestCommand controlRequest)
        {
            this.ControlRequest = controlRequest;
        }

        public ControlRequestCommand ControlRequest { get; set; }
    }
}

