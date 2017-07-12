namespace LY.Remote.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public class LoginRespondCommand
    {
        public LoginRespondType RespondType { get; set; }

        public string UserName { get; set; }
    }
}

