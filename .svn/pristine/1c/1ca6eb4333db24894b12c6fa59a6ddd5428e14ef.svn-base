# define DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace AU.Monitor.Server
{
    /// <summary>
    /// 会话
    /// </summary>
    public class MonitorSession : AppSession<MonitorSession>
    {
        protected override void OnSessionStarted()
        {
#if DEBUG
            this.Send("This is Monitor Server");
#endif
            base.OnSessionStarted();
        }

        protected override void HandleUnknownRequest(StringRequestInfo requestInfo)
        {
#if DEBUG
            this.Send("Unknow request >> " + requestInfo.Key + " " + requestInfo.Body);
#endif
            base.HandleUnknownRequest(requestInfo);
        }

        protected override void HandleException(Exception e)
        {
#if DEBUG
            this.Send("Application error >> {0}", e.Message);
#endif
            base.HandleException(e);
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);
        }
    }
}
