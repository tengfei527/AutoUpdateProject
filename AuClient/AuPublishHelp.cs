using AU.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using System.Collections.Concurrent;

namespace AuClient
{
    /// <summary>
    /// 包发布
    /// </summary>
    public class AuPublishHelp
    {
        /// <summary>
        /// 监听发布
        /// </summary>
        Nancy.Hosting.Self.NancyHost nancySelfHost = null;
        /// <summary>
        /// 取消控制变量
        /// </summary>
        private System.Threading.CancellationTokenSource cts;

        public Dictionary<string, string> SubSystemDic { get; private set; }

        private SuperSocket.ClientEngine.EasyClient easyClient = new SuperSocket.ClientEngine.EasyClient();
        private ConcurrentQueue<AuPublish> AuPublishQueue = new ConcurrentQueue<AuPublish>();

        public MainForm UI = null;
        /// <summary>
        /// 升级消息
        /// </summary>
        public ConcurrentQueue<UpgradeMessage> UpgradeMessageQueue = new ConcurrentQueue<UpgradeMessage>();

        AppRemotePublish AppRemotePublishConten { get; set; }
        /// <summary>
        /// 本地路径
        /// </summary>
        public string LocalPath { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public AuPublishHelp(MainForm ui)
        {
            this.LocalPath = AppConfig.Current.UpdateConfigPath + "\\package\\";
            this.AppRemotePublishConten = new AppRemotePublish(AppConfig.Current.PublishAddress, this.LocalPath);
            this.UI = ui;
            if (AppConfig.Current.AllowPublish)
            {
                nancySelfHost = new Nancy.Hosting.Self.NancyHost(new Uri(AppConfig.Current.PublishAddress), new MyBootstrapper());
                //Server
                AU.Monitor.Server.ServerBootstrap.Init(Ms_NewSessionConnected, Ms_SessionClosed);
                StartResult result = AU.Monitor.Server.ServerBootstrap.Start();
                Console.WriteLine("Start result: {0}!", result);
            }

            InitSocketClient();
        }

        private void InitSocketClient()
        {
            easyClient.Initialize(new AU.Monitor.Client.FakeReceiveFilter(System.Text.Encoding.UTF8), (p =>
            {
                string body = p.Body;
                string key = p.Key;
                string[] par = p.Parameters;
                if (key != body)
                    Console.WriteLine("{0}:{1}", key, body);
                else
                    Console.WriteLine(key);
                try
                {
                    switch (key)
                    {
                        case "AUVERSION":
                            List<AuPublish> aulist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AuPublish>>(p.Body);
                            if (aulist != null)
                            {
                                foreach (var a in aulist)
                                    AuPublishQueue.Enqueue(a);
                            }
                            break;
                    }
                }
                catch (Exception e)
                {
                    //log
                    Console.WriteLine(e);
                }
            }));

            //Client            
            var ips = AppConfig.Current.SocketServer.Split(':');
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(ips[0]);
            int port = Convert.ToInt32(ips[1]);
            System.Threading.Tasks.Task client = new System.Threading.Tasks.Task(() =>
            {
                while (true)
                {
                    try
                    {
                        if (!easyClient.IsConnected)
                        {
                            var res = easyClient.ConnectAsync(new System.Net.IPEndPoint(ip, port));
                            System.Threading.Tasks.Task.WaitAll(res);
                        }
                    }
                    catch (Exception ex)
                    {
                        //log
                    }

                    System.Threading.Thread.Sleep(AppConfig.Current.Interval);
                }
            });
            client.Start();
        }
        /// <summary>
        /// 新客户端连接
        /// </summary>
        /// <param name="session"></param>
        private void Ms_NewSessionConnected(AU.Monitor.Server.MonitorSession session)
        {
            Console.WriteLine("New Connected ID=[" + session.SessionID + "] IP=" + session.RemoteEndPoint.ToString());
            session.Send("Welcome to AuClient Socket Server");
        }
        private static void Ms_SessionClosed(AU.Monitor.Server.MonitorSession session, SuperSocket.SocketBase.CloseReason value)
        {
            Console.WriteLine("Session Closed ID=[" + session.SessionID + "] IP=" + session.RemoteEndPoint.ToString() + " Reason=" + value);
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <returns>启动结果</returns>
        public bool Start()
        {
            //读注册表
            SubSystemDic = AU.Common.Utility.RegistryHelper.GetRegistrySubs(Microsoft.Win32.Registry.LocalMachine, "SYSTEM\\E7");
            //cts = new System.Threading.CancellationTokenSource();
            //Task engineTask = new Task(() => Engine(cts.Token), cts.Token);
            //engineTask.Start();
            cts = new System.Threading.CancellationTokenSource();
            Task engineTask = new Task(() => UpdateProcess(cts.Token), cts.Token);
            engineTask.Start();
            this.StartUpdateCheck();
            if (AppConfig.Current.AllowPublish)
                nancySelfHost.Start();
            return true;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public void Stop()
        {
            this.IsUpdateCheckRun = false;
            if (cts != null)
                cts.Cancel();
            if (AppConfig.Current.AllowPublish)
                nancySelfHost.Stop();
        }

        public void StartUpdateCheck()
        {
            this.IsUpdateCheckRun = true;
        }

        private bool IsUpdateCheckRun { get; set; }

        /// <summary>
        /// 更新消息到达
        /// </summary>
        public void UpdateProcess(System.Threading.CancellationToken ct)
        {
            AuPublish a = null;
            while (true)
            {
                if (ct.IsCancellationRequested)
                    break;
                if (!this.IsUpdateCheckRun)
                {
                    System.Threading.Thread.Sleep(AppConfig.Current.Interval);
                    continue;
                }
                //通知消息
                if (!AuPublishQueue.TryDequeue(out a))
                {
                    System.Threading.Thread.Sleep(AppConfig.Current.Interval);
                    continue;
                }
                //识别子系统
                if (!Enum.IsDefined(typeof(SystemType), a.PublishType))
                {
                    System.Threading.Thread.Sleep(AppConfig.Current.Interval);
                    continue;
                }

                string sub = ((SystemType)a.PublishType).ToString();
                //管理及发布？
                if (!this.SubSystemDic.ContainsKey(sub) && !AppConfig.Current.AllowPublish)
                {
                    System.Threading.Thread.Sleep(AppConfig.Current.Interval);
                    continue;
                }
                AuPublish notify = null;
                //检查升级包
                if (this.AppRemotePublishConten.CheckForUpdate(sub, a) > 0)
                {
                    //获取升级包
                    string file = this.AppRemotePublishConten.DownUpdateFile(sub, a, out notify, AppConfig.Current.AllowPublish);
                    //显示升级服务
                    if (this.SubSystemDic.ContainsKey(sub) && System.IO.File.Exists(file))
                    {
                        //通知消息
                        UpgradeMessageQueue.Enqueue(new UpgradeMessage()
                        {
                            SubSystem = sub,
                            UpdatePackFile = file,
                            UpgradePath = this.SubSystemDic[sub]
                        });
                    }
                }
                else
                {
                    notify = AppPublish.ReadPackage(this.LocalPath + "\\" + sub + "\\" + AppPublish.PackageName);
                    //检查服务器和包是否一直
                    if (notify != null)
                        UpgradeMessageQueue.Enqueue(new UpgradeMessage()
                        {
                            SubSystem = sub,
                            UpdatePackFile = this.LocalPath + "\\" + sub + "\\" + notify.DownPath,
                            UpgradePath = this.SubSystemDic[sub]
                        });
                }
                //通知客户端消息
                if (notify != null)
                    AU.Monitor.Server.ServerBootstrap.Send("AUVERSION:" + Newtonsoft.Json.JsonConvert.SerializeObject(notify));
                if (ct.IsCancellationRequested)
                    break;
                System.Threading.Thread.Sleep(AppConfig.Current.Interval);
            }
            this.IsUpdateCheckRun = false;
        }
    }
}
