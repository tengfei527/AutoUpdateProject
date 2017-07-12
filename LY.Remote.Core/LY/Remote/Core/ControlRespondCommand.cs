namespace LY.Remote.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public class ControlRespondCommand
    {
        public string Controled { get; set; }

        public string Controler { get; set; }

        public ControlRespondType RespondType { get; set; }
    }
}

