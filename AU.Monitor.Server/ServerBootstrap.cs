using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AU.Monitor.Server
{
    public class ServerBootstrap
    {
        private static Lazy<IBootstrap> lazyBootstrap = new Lazy<IBootstrap>(() =>
        {
            var laz = BootstrapFactory.CreateBootstrap();
            laz.Initialize();
            return laz;
        });
        public static IBootstrap Bootstrap
        {
            get
            {
                return lazyBootstrap.Value;
            }
        }
        public static void Send(string Message)
        {
            foreach (AU.Monitor.Server.MonitorServer d in Bootstrap.AppServers)
            {
                foreach (var s in d.GetAllSessions())
                {
                    s.Send(Message.Replace("\r\n", ""));
                }
            }
        }
        public static void Send(string key, string body)
        {
            string message = key + ":" + body;
            Send(message);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="SessionConnectedEvent"></param>
        /// <param name="SessionClosedEvent"></param>
        public static void Init(SessionHandler<MonitorSession> SessionConnectedEvent, SessionHandler<MonitorSession, CloseReason> SessionClosedEvent, RequestHandler<MonitorSession, SuperSocket.SocketBase.Protocol.StringRequestInfo> NewRequestReceived)
        {
            foreach (AU.Monitor.Server.MonitorServer d in Bootstrap.AppServers)
            {
                string listen = "Listen address: ";
                Array.ForEach(d.Listeners, l => listen += l.EndPoint.ToString());
                Console.WriteLine(listen);
                if (SessionConnectedEvent != null)
                    d.NewSessionConnected += SessionConnectedEvent;
                if (SessionClosedEvent != null)
                    d.SessionClosed += SessionClosedEvent;
                if (NewRequestReceived != null)
                    d.NewRequestReceived += NewRequestReceived;
            }
        }

        /// <summary>
        /// 启动
        /// </summary>
        public static StartResult Start()
        {
            return Bootstrap.Start();
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        public static void Stop()
        {
            Bootstrap.Stop();
        }
    }
}
