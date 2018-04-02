using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;

namespace AU.Monitor.Server
{
    public class MonitorServer : AppServer<MonitorSession>
    {
        public MonitorServer() : base(new SuperSocket.SocketBase.Protocol.CommandLineReceiveFilterFactory(Encoding.UTF8, new BasicRequestInfoParser(":", "&")))
        { }
        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            return base.Setup(rootConfig, config);
        }

        protected override void OnStarted()
        {
            base.OnStarted();
        }

        protected override void OnStopped()
        {
            base.OnStopped();
        }
    }
}
