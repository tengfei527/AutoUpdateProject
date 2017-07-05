using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AU.Monitor.Server.Command
{
    public class ECHO : CommandBase<MonitorSession, StringRequestInfo>
    {
        public override void ExecuteCommand(MonitorSession session, StringRequestInfo requestInfo)
        {
            session.Send(requestInfo.Body);
        }
    }
}
