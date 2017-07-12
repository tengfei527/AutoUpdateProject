namespace LY.Remote.Core
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    [Serializable]
    public class LoginCommand : ICloneable
    {
        public object Clone()
        {
            int expressionStack_6_0 = 1;
            if (expressionStack_6_0 == 0)
            {
            }
            MemoryStream serializationStream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(serializationStream, this);
            serializationStream.Position = 0L;
            LoginCommand command = (LoginCommand) formatter.Deserialize(serializationStream);
            serializationStream.Close();
            return command;
        }

        public LY.Remote.Core.ChanelType ChanelType { get; set; }

        public string IP { get; set; }

        public DateTime LoginTime { get; set; }

        public string Password { get; set; }

        public string Port { get; set; }

        public string UserName { get; set; }
    }
}

