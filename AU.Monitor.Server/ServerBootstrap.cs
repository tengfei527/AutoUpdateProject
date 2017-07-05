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
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="SessionConnectedEvent">NewSessionConnected</param>
        /// <returns></returns>
        public static StartResult Start(SessionHandler<MonitorSession> SessionConnectedEvent)
        {
            foreach (AU.Monitor.Server.MonitorServer d in Bootstrap.AppServers)
            {
                string listen = "Listen address: ";
                Array.ForEach(d.Listeners, l => listen += l.EndPoint.ToString());
                Console.WriteLine(listen);
                if (SessionConnectedEvent != null)
                    d.NewSessionConnected += SessionConnectedEvent;
            }
            return Bootstrap.Start();
        }
    }
}
