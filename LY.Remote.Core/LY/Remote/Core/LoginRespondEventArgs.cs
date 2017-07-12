namespace LY.Remote.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public class LoginRespondEventArgs : EventArgs
    {
        public LoginRespondEventArgs(LoginRespondCommand loginRespond)
        {
            this.LoginRespond = loginRespond;
        }

        public LoginRespondCommand LoginRespond { get; set; }
    }
}

