namespace LY.Remote.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public class ConnectedEventArgs : EventArgs
    {
        public ConnectedEventArgs(LoginCommand user)
        {
            this.User = user;
        }

        public LoginCommand User { get; set; }
    }
}

