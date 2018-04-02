using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AU.Monitor.Server
{
    public class MonitorServerHelp
    {
        public MonitorServer ms { get; private set; }

        public void Init(SuperSocket.SocketBase.Config.ServerConfig sc, SessionHandler<MonitorSession> SessionConnectedEvent, SessionHandler<MonitorSession, CloseReason> SessionClosedEvent, RequestHandler<MonitorSession, SuperSocket.SocketBase.Protocol.StringRequestInfo> NewRequestReceived)
        {
            ms = new MonitorServer();

            if (SessionConnectedEvent != null)
                ms.NewSessionConnected += SessionConnectedEvent;
            if (SessionClosedEvent != null)
                ms.SessionClosed += SessionClosedEvent;
            if (NewRequestReceived != null)
                ms.NewRequestReceived += NewRequestReceived;

            ms.Setup(sc);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sessionid">会话编号</param>
        /// <param name="Message">消息体</param>
        public void Send(string sessionid, string Message)
        {
            try
            {
                if (string.IsNullOrEmpty(sessionid))
                    foreach (var s in ms.GetAllSessions())
                    {
                        s.Send(Message.Replace("\r\n", ""));
                    }
                else
                {
                    var s = ms.GetSessionByID(sessionid);
                    if (s != null)
                        s.Send(Message.Replace("\r\n", ""));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sessionid">会话编号</param>
        /// <param name="key">命令</param>
        /// <param name="body">命令内容</param>
        public void Send(string sessionid, string key, string body)
        {
            string message = key + ":" + body;
            Send(sessionid, message);
        }
    }
}
