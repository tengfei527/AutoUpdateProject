namespace LY.Remote.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public class ControlRequestCommand
    {
        public ControlConfirmType ConfirmType { get; set; }

        public string Controled { get; set; }

        public string Controler { get; set; }

        public ControlRequestType RequestType { get; set; }
    }
}

