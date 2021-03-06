﻿using AU.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using System.Collections.Concurrent;
using System.Collections;
using AU.Common.Utility;
using System.IO;

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
        /// 客户端
        /// </summary>
        public Hashtable SessionTable { get; set; }
        /// <summary>
        /// 本地路径
        /// </summary>
        public string LocalPath { get; set; }
        /// <summary>
        /// 终端
        /// </summary>
        private Dictionary<string, CmdUtility> DicTerminal = new Dictionary<string, CmdUtility>();
        /// <summary>
        /// 文件哈希
        /// </summary>
        public string Hash256 { get; private set; }
        /// <summary>
        /// 日志接口
        /// </summary>
        public log4net.ILog log = log4net.LogManager.GetLogger(typeof(AuPublishHelp));
        /// <summary>
        /// 服务器管理
        /// </summary>
        //MyServiceControll mc = new MyServiceControll();

        //public AU.Monitor.Server.MonitorServerHelp ms { get; private set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public AuPublishHelp(MainForm ui)
        {
            this.Hash256 = AU.Common.Utility.ToolsHelp.ComputeSHA256(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            this.LocalPath = AppConfig.Current.UpdateConfigPath + "\\package\\";
            this.AppRemotePublishConten = new AppRemotePublish(AppConfig.Current.PublishAddress, this.LocalPath);
            this.UI = ui;
            if (AppConfig.Current.AllowPublish)
            {
                try
                {
                    nancySelfHost = new Nancy.Hosting.Self.NancyHost(new Uri("http://localhost:" + AppConfig.Current.PublishPort), new MyBootstrapper());
                }
                catch (Exception e)
                {
                    log.Error(e);
                    //Console.WriteLine(e);
                }
                //Server
                SessionTable = new Hashtable();
                AU.Monitor.Server.ServerBootstrap.Init(Ms_NewSessionConnected, Ms_SessionClosed, Ms_NewRequestReceived);
                //InitSocketServer();
            }

            easyClient.Initialize(new AU.Monitor.Client.FakeReceiveFilter(System.Text.Encoding.UTF8), clienthandler);

            //mc.Start(System.Reflection.Assembly.GetCallingAssembly().Location);
        }
        private System.Collections.Hashtable FilePackage = new System.Collections.Hashtable();
        private FileStream Fs;
        public void clienthandler(SuperSocket.ProtoBase.StringPackageInfo p)
        {
            string body = p.Body;
            string key = p.Key;
            string[] par = p.Parameters;
            if (key != body)
                log.InfoFormat("{0}:{1}", key, body);
            //Console.WriteLine("{0}:{1}", key, body);
            else
                log.Info(key);
            //Console.WriteLine(key);
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
                    case "SCRIPT":
                        {
                            if (string.IsNullOrEmpty(p.Body) && !this.SubSystemDic.ContainsKey(SystemType.coreserver.ToString()))
                                break;
                            string config = this.SubSystemDic[SystemType.coreserver.ToString()] + "\\Core\\Web.config";
                            if (System.IO.File.Exists(config))
                            {
                                string con = AU.Common.Utility.ConfigUtility.GetApiDbConnect(config);
                                if (!string.IsNullOrEmpty(con))
                                {
                                    var cp = Newtonsoft.Json.JsonConvert.DeserializeObject<AU.Monitor.Server.CommandPackage>(p.Body);
                                    string result = AuDataBase.RunScriptString(con, cp.Key, cp.Body, cp.Parameters);

                                    AU.Monitor.Server.CommandPackage cpackage = new AU.Monitor.Server.CommandPackage()
                                    {
                                        Key = cp.Key,
                                        Body = result,
                                    };

                                    this.Send(CommandType.SCRIPT, Newtonsoft.Json.JsonConvert.SerializeObject(cpackage));

                                }
                            }
                        }
                        break;
                    case "TRANSFER"://通知当前客户端
                        {
                            this.Transfer(p);
                        }
                        break;
                    case "TRANSFERONE"://通知指定客户端
                        {
                            this.Transfer(p);

                        }
                        break;
                    case "RESOURCE":
                        {
                            string[] Parameters = null;
                            if (string.IsNullOrEmpty(p.Body))
                                break;
                            var cp = Newtonsoft.Json.JsonConvert.DeserializeObject<AU.Monitor.Server.CommandPackage>(p.Body);
                            string result = string.Empty;

                            switch (cp.Key)
                            {
                                case "SEND_DISKS":
                                    AU.Common.Codes.DisksCode diskcode = IO.GetDisks();
                                    result = Newtonsoft.Json.JsonConvert.SerializeObject(diskcode);
                                    break;
                                case "GET_DIRECTORY_DETIAL":
                                    AU.Common.Codes.ExplorerCode explorer = new AU.Common.Codes.ExplorerCode();
                                    explorer.Enter(cp.Body);
                                    result = Newtonsoft.Json.JsonConvert.SerializeObject(explorer);
                                    break;
                                case "GET_FILE_DETIAL":
                                    result = AU.Common.Utility.IO.GetFileDetial(cp.Body);
                                    break;
                                case "GET_FILE":
                                    SendFile(cp.Body);
                                    return;
                                case "DELETE_FILE":
                                    try
                                    {
                                        if (System.IO.File.Exists(cp.Body))
                                            System.IO.File.Delete(cp.Body);
                                        Parameters = new string[] { "1" };
                                        result = "已删除";
                                    }
                                    catch (Exception e)
                                    {
                                        result = e.Message;
                                        Parameters = new string[] { "0" };
                                    }
                                    break;
                            }

                            AU.Monitor.Server.CommandPackage cpackage = new AU.Monitor.Server.CommandPackage()
                            {
                                Key = cp.Key,
                                Body = result,
                                Parameters = Parameters,
                            };

                            this.Send(CommandType.RESOURCE, Newtonsoft.Json.JsonConvert.SerializeObject(cpackage));
                        }

                        break;
                    case "F":
                        {
                            if (Fs == null)
                            {
                                string path = p.Body;
                                Fs = File.Open(path, FileMode.OpenOrCreate);
                                //发送通知消息
                            }
                        }
                        break;
                    case "FS":
                        {
                            if (Fs != null)
                            {
                                byte[] buff = AU.Common.Utility.ToolsHelp.HexStringToByte(p.Body);
                                Fs.Write(buff, 0, buff.Length);
                                Fs.Flush();
                            }
                            //发送通知消息

                        }
                        return;
                    case "FE":
                        {

                            if (Fs != null)
                            {
                                Fs.Close();
                                Fs.Dispose();
                                Fs = null;
                            }

                        }

                        break;
                    case "TERMINAL":
                        {
                            if (string.IsNullOrEmpty(p.Body))
                                break;
                            var cp = Newtonsoft.Json.JsonConvert.DeserializeObject<AU.Monitor.Server.CommandPackage>(p.Body);
                            string result = string.Empty;

                            switch (cp.Key)
                            {
                                case "START":
                                    if (!DicTerminal.ContainsKey(cp.Body))
                                    {
                                        var cmd = new CmdUtility(cp.Body, cp.Parameters);
                                        cmd.MyProcess.OutputDataReceived += MyProcess_OutputDataReceived;
                                        cmd.MyProcess.ErrorDataReceived += MyProcess_ErrorDataReceived;
                                        DicTerminal.Add(cp.Body, cmd);
                                    }
                                    break;
                                case "STOP":
                                    if (DicTerminal.ContainsKey(cp.Body))
                                    {
                                        DicTerminal[cp.Body].Close();
                                        DicTerminal.Remove(cp.Body);
                                    }
                                    break;
                                default:
                                    if (DicTerminal.ContainsKey(cp.Key))
                                        try
                                        {
                                            DicTerminal[cp.Key].Run(cp.Body);
                                        }
                                        catch
                                        {
                                            DicTerminal[cp.Key].Close();
                                            DicTerminal.Remove(cp.Key);
                                        }
                                    break;
                            }
                        }
                        break;
                    case "CONTROLL":
                        {
                            if (string.IsNullOrEmpty(p.Body))
                                break;
                            var cp = Newtonsoft.Json.JsonConvert.DeserializeObject<AU.Monitor.Server.CommandPackage>(p.Body);
                            string result = string.Empty;
                            switch (cp.Key)
                            {
                                case "CLOSE":
                                    {
                                        AU.Monitor.Server.ServerBootstrap.Close(cp.Body);
                                    }
                                    break;
                            }

                        }
                        break;
                }
            }
            catch (Exception e)
            {
                this.Send(CommandType.ERROR, e.Message);
                //log
                log.Error(e);
                //Console.WriteLine(e);
            }
        }

        private void Transfer(SuperSocket.ProtoBase.StringPackageInfo p)
        {
            var cp = Newtonsoft.Json.JsonConvert.DeserializeObject<AU.Monitor.Server.TransferPackage>(p.Body);
            //中转
            if (cp.Route != null && cp.Route.Length > 0 && cp.RouteIndex < cp.Route.Length - 1)
            {
                cp.RouteIndex++;
                //ms.Send(cp.Route[cp.RouteIndex], p.Key, Newtonsoft.Json.JsonConvert.SerializeObject(cp));
                AU.Monitor.Server.ServerBootstrap.Send(cp.Route[cp.RouteIndex], p.Key, Newtonsoft.Json.JsonConvert.SerializeObject(cp));
            }
            else
            {
                clienthandler(new SuperSocket.ProtoBase.StringPackageInfo(cp.Cmd, cp.Message, cp.Message.Split('&')));
            }
        }


        public string cmdSpilts = "\r\n";
        //单线程操作
        private void SendFile(string path)
        {
            new System.Threading.Tasks.Task(() =>
            {
                try
                {
                    if (!System.IO.File.Exists(path))
                        return;
                    byte[] sp = System.Text.Encoding.UTF8.GetBytes(cmdSpilts);
                    using (FileStream f = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        easyClient.Send(System.Text.Encoding.UTF8.GetBytes("F:" + f.Length / 1024 + "&" + System.IO.Path.GetFileName(path) + cmdSpilts));

                        byte[] head = System.Text.Encoding.UTF8.GetBytes("FS:");
                        int len = 1024;
                        byte[] buff = new byte[len];
                        int count = 0;
                        do
                        {
                            count = f.Read(buff, 0, len);
                            if (count == len)
                            {//分片包编号后期扩展
                                var temp = Encoding.UTF8.GetBytes(AU.Common.Utility.ToolsHelp.ByteToHexString(buff));
                                byte[] send = new byte[head.Length + temp.Length + sp.Length];
                                //head
                                Array.Copy(head, 0, send, 0, head.Length);
                                //msg
                                Array.Copy(temp, 0, send, head.Length, temp.Length);
                                //sp
                                Array.Copy(sp, 0, send, head.Length + temp.Length, sp.Length);
                                //发送消息
                                easyClient.Send(send);
                            }
                            else if (count > 0)
                            {
                                //ArraySegment<byte> b = new ArraySegment<byte>(buff, 0, count);
                                byte[] b = new byte[count];
                                Array.Copy(buff, 0, b, 0, count);
                                var temp = Encoding.UTF8.GetBytes(AU.Common.Utility.ToolsHelp.ByteToHexString(b));
                                byte[] send = new byte[head.Length + temp.Length + sp.Length];
                                //head
                                Array.Copy(head, 0, send, 0, head.Length);
                                //msg
                                Array.Copy(temp, 0, send, head.Length, temp.Length);
                                //sp
                                Array.Copy(sp, 0, send, head.Length + temp.Length, sp.Length);
                                //发送消息
                                easyClient.Send(send);
                            }
                            System.Threading.Thread.Sleep(1);
                        } while (count != 0);
                        f.Close();
                        f.Dispose();
                        easyClient.Send(System.Text.Encoding.UTF8.GetBytes("FE:1&传输完成" + cmdSpilts));
                    }
                }
                catch (Exception e)
                {
                    easyClient.Send(System.Text.Encoding.UTF8.GetBytes("FE:0&传输失败详情," + e.Message + cmdSpilts));
                }

            }).Start();
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="key">命令</param>
        /// <param name="body">消息</param>
        private void Send(string key, string body)
        {
            try
            {
                byte[] sp = System.Text.Encoding.UTF8.GetBytes(cmdSpilts);
                string id = Guid.NewGuid().ToString();
                string par = "P:" + id + "&";
                string message = (key + ":" + body).Replace(cmdSpilts, "");
                byte[] b = System.Text.Encoding.UTF8.GetBytes(message);
                int t = b.Length / 1024;
                if (t > 0)
                {
                    byte[] buff;
                    //easyClient.Send(System.Text.Encoding.UTF8.GetBytes(("PS:" + id + cmdSpilts)));
                    for (int i = 0; i <= t; i++)
                    {
                        byte[] head = System.Text.Encoding.UTF8.GetBytes(par + i.ToString() + "&");
                        if (i == t)
                        {
                            buff = new byte[head.Length + b.Length - i * 1024 + sp.Length];
                            //head
                            Array.Copy(head, 0, buff, 0, head.Length);
                            //消息体
                            Array.Copy(b, i * 1024, buff, head.Length, buff.Length - sp.Length - head.Length);
                            Array.Copy(sp, 0, buff, buff.Length - sp.Length, sp.Length);
                        }
                        else
                        {
                            buff = new byte[head.Length + 1024 + sp.Length];
                            Array.Copy(head, 0, buff, 0, head.Length);
                            Array.Copy(b, i * 1024, buff, head.Length, 1024);
                            Array.Copy(sp, 0, buff, buff.Length - sp.Length, sp.Length);
                        }

                        easyClient.Send(buff);
                    }
                    //发送分包结束
                    easyClient.Send(System.Text.Encoding.UTF8.GetBytes(("PE:" + id + cmdSpilts)));
                }
                else
                    easyClient.Send(System.Text.Encoding.UTF8.GetBytes(message + cmdSpilts));
            }
            catch (Exception e)
            {
                log.Error(e);
                //Console.WriteLine(e);
            }
        }
        private void InitSocketServer()
        {
            //ms = new AU.Monitor.Server.MonitorServerHelp();
            //var serverConfig = new SuperSocket.SocketBase.Config.ServerConfig
            //{
            //    Port = AppConfig.Current.MsPort,
            //    TextEncoding = AppConfig.Current.TextEncoding,
            //    ReceiveBufferSize = AppConfig.Current.ReceiveBufferSize,
            //    MaxConnectionNumber = AppConfig.Current.MaxConnectionNumber,
            //    SendBufferSize = AppConfig.Current.SendBufferSize,
            //    MaxRequestLength = AppConfig.Current.MaxRequestLength
            //    //set the listening port
            //};

            //ms.Init(serverConfig, Ms_NewSessionConnected, Ms_SessionClosed, Ms_NewRequestReceived);

            SessionTable = new Hashtable();
            //ms.ms.Start();

            AU.Monitor.Server.ServerBootstrap.Init(Ms_NewSessionConnected, Ms_SessionClosed, Ms_NewRequestReceived);
        }
        public System.Net.IPAddress ServerIp { get; protected set; }
        private void StartSocketClient()
        {
            var ips = AppConfig.Current.SocketServer.Split(':');
            ServerIp = System.Net.IPAddress.Parse(ips[0]);
            int port = Convert.ToInt32(ips[1]);
            System.Threading.Tasks.Task client = new System.Threading.Tasks.Task(() =>
            {
                int count = 60;
                while (this.IsUpdateCheckRun)
                {
                    try
                    {
                        count--;
                        if (!easyClient.IsConnected)
                        {
                            var res = easyClient.ConnectAsync(new System.Net.IPEndPoint(ServerIp, port));
                            System.Threading.Tasks.Task.WaitAll(res);

                            //推送指令
                            if (res.Result)
                            {
                                //登陆
                                //通知服务器
                                AU.Common.LoginModel lm = new LoginModel();
                                if (this.SubSystemDic.ContainsKey("coreserver"))
                                {
                                    lm = PeculiarHelp.GetProject();
                                }
                                if (this.SubSystemDic.ContainsKey("vmsclient"))
                                {
                                    //获取车场版本
                                    lm.Park = PeculiarHelp.GetVersion(this.SubSystemDic["vmsclient"]);
                                }
                                lm.Version = Application.ProductVersion.ToString();
                                lm.Password = "ftf";
                                lm.UserName = "客户端";

                                this.Send(AU.Common.CommandType.LOGIN, Newtonsoft.Json.JsonConvert.SerializeObject(lm));

                                System.Threading.Thread.Sleep(10);
                                //通知当前客户端版本
                                SessionOper(-1, null);
                            }
                        }
                        else if (count % 20 == 0)
                        {
                            //发送心跳包
                            this.Send(AU.Common.CommandType.HEART, "");
                        }
                        if (count <= 0)
                        {
                            count = 60;
                            //清理注册表错误数据
                            AU.Common.Utility.RegistryHelper.DeleteRegist(Microsoft.Win32.Registry.LocalMachine, "SYSTEM\\E7\\", "AuError");
                        }
                    }
                    catch (Exception ex)
                    {
                        //log
                        log.Error(ex);
                        //Console.WriteLine(ex);
                    }

                    System.Threading.Thread.Sleep(AppConfig.Current.Interval);
                }
            });

            client.Start();
        }
        /// <summary>
        /// 新消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyProcess_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                //发送消息
                this.Send(CommandType.TERMINAL, e.Data);
            }
        }
        private void MyProcess_ErrorDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                //发送消息
                this.Send(CommandType.TERMINAL, e.Data);
            }
        }

        private void SessionOper(int oper, AU.Monitor.Server.MonitorSession session, AU.Common.SessionModel sm = null)
        {
            if (oper == 0)
            {
                if (SessionTable.ContainsKey(session.SessionID))
                    SessionTable.Remove(session.SessionID);
            }
            else if (oper == 1)
            {
                if (SessionTable.ContainsKey(session.SessionID))
                    SessionTable[session.SessionID] = sm;
                else
                    SessionTable.Add(session.SessionID, sm);
            }
            //通知服务器
            this.Send(AU.Common.CommandType.SESSION, Newtonsoft.Json.JsonConvert.SerializeObject(SessionTable.Values));
        }
        /// <summary>
        /// 新客户端连接
        /// </summary>
        /// <param name="session"></param>
        private void Ms_NewSessionConnected(AU.Monitor.Server.MonitorSession session)
        {
            log.Info("New Connected ID=[" + session.SessionID + "] IP=" + session.RemoteEndPoint.ToString());
            //Console.WriteLine("New Connected ID=[" + session.SessionID + "] IP=" + session.RemoteEndPoint.ToString());
            //this.SessionOper(1, session, new SessionModel()
            //{
            //    SessionId = session.SessionID,
            //    RemoteEndPoint = session.RemoteEndPoint.ToString(),
            //    Name = "客户端",
            //    Version = "0.0.0.0",
            //});

            session.Send("Welcome to AuClient Socket Server");
        }
        private void Ms_SessionClosed(AU.Monitor.Server.MonitorSession session, SuperSocket.SocketBase.CloseReason value)
        {
            this.SessionOper(0, session);
            log.Info("Session Closed ID=[" + session.SessionID + "] IP=" + session.RemoteEndPoint.ToString() + " Reason=" + value);
            //Console.WriteLine("Session Closed ID=[" + session.SessionID + "] IP=" + session.RemoteEndPoint.ToString() + " Reason=" + value);
        }
        private void Ms_NewRequestReceived(AU.Monitor.Server.MonitorSession session, SuperSocket.SocketBase.Protocol.StringRequestInfo requestInfo)
        {
            //中转
            //发送分包结束
            switch (requestInfo.Key)
            {
                case "HEART":
                case "SESSION":
                    break;
                case "LOGIN":
                    {
                        var mode = Newtonsoft.Json.JsonConvert.DeserializeObject<AU.Common.LoginModel>(requestInfo.Body);//
                        this.SessionOper(1, session, new SessionModel()
                        {
                            SessionId = session.SessionID,
                            RemoteEndPoint = session.RemoteEndPoint.ToString(),
                            Name = mode == null ? "客户端" : mode.UserName + ":" + mode.Park,
                            Version = mode == null ? "0.0.0.0" : mode.Version,
                        });

                    }
                    break;
                case "P":
                case "F":
                case "FE":
                case "FS":
                    easyClient.Send(System.Text.Encoding.UTF8.GetBytes(requestInfo.Key + ":" + requestInfo.Body + cmdSpilts));
                    break;
                default:
                    this.Send(requestInfo.Key, requestInfo.Body);
                    break;
            }
            //Console.WriteLine("Session Message ID=[" + session.SessionID + "] IP=" + session.RemoteEndPoint.ToString() + "Key= " + requestInfo.Key + " Message=" + requestInfo.Body);
        }
        /// <summary>
        /// 最后读注册表时间
        /// </summary>
        public static DateTime ReadRegistryTime;
        /// <summary>
        /// 子系统锁
        /// </summary>
        private object lockSubSystemDic = new object();
        /// <summary>
        /// 读取注册表
        /// </summary>
        public void ReadRegistry()
        {
            try
            {
                int s = (AppConfig.Current.Interval / 1000) + 1;
                if (ReadRegistryTime.AddMinutes(s) < DateTime.Now)
                {
                    ReadRegistryTime = DateTime.Now;
                    //读注册表
                    lock (lockSubSystemDic)
                    {
                        SubSystemDic = AU.Common.Utility.RegistryHelper.GetRegistrySubs(Microsoft.Win32.Registry.LocalMachine, "SYSTEM\\E7");
                        if (!SubSystemDic.ContainsKey("auclient"))
                            SubSystemDic.Add(SystemType.auclient.ToString(), System.Reflection.Assembly.GetCallingAssembly().Location);
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e);
                //Console.WriteLine(e);
            }
        }
        /// <summary>
        /// 启动
        /// </summary>
        /// <returns>启动结果</returns>
        public bool Start()
        {
            //读注册表
            this.ReadRegistry();
            //cts = new System.Threading.CancellationTokenSource();
            //Task engineTask = new Task(() => Engine(cts.Token), cts.Token);
            //engineTask.Start();
            cts = new System.Threading.CancellationTokenSource();
            Task engineTask = new Task(() => UpdateProcess(cts.Token), cts.Token);
            engineTask.Start();
            this.StartUpdateCheck();
            if (AppConfig.Current.AllowPublish)
            {
                StartResult result = AU.Monitor.Server.ServerBootstrap.Start();
                log.InfoFormat("Start result: {0}!", result);
                //Console.WriteLine("Start result: {0}!", result);
                nancySelfHost?.Start();
            }

            StartSocketClient();

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
            {
                AU.Monitor.Server.ServerBootstrap.Stop();
                log.Info("Stop Au Monitor Server");
                //Console.WriteLine("Stop Au Monitor Server");
                nancySelfHost?.Stop();
            }

            easyClient.Close();
            log.Info("Stop EasyClient");
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
                try
                {
                    if (ct.IsCancellationRequested)
                        break;
                    //读取注册表
                    this.ReadRegistry();
                    //检查UI服务是否运行
                    this.UI.BeginInvoke((MethodInvoker)delegate ()
                    {
                        this.UI.CheckRun();
                    });

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
                    AuPublish notify = null;
                    ////客户端升级
                    //if ((int)SystemType.auclient == a.PublishType && !a.SHA256.Equals(this.Hash256, StringComparison.InvariantCultureIgnoreCase))
                    //{
                    //    //自升级
                    //    //获取升级包
                    //    string file = this.AppRemotePublishConten.DownUpdateFile(SystemType.auclient.ToString(), a, out notify, true);
                    //    if (System.IO.File.Exists(file))
                    //    {
                    //        //启动自升级
                    //        this.Stop();
                    //        if (du.Start(file, System.Reflection.Assembly.GetCallingAssembly().Location))
                    //            return;
                    //    }
                    //}

                    string sub = ((SystemType)a.PublishType).ToString();
                    //管理及发布？
                    if (!this.SubSystemDic.ContainsKey(sub) && !AppConfig.Current.AllowPublish)
                    {
                        System.Threading.Thread.Sleep(AppConfig.Current.Interval);
                        continue;
                    }
                    //检查升级包
                    if ((sub == "auclient" && this.Hash256.ToLower() != a.SHA256.ToLower()) || (sub != "auclient" && this.AppRemotePublishConten.CheckForUpdate(sub, a) > 0))
                    {
                        //获取升级包
                        string file = this.AppRemotePublishConten.DownUpdateFile(sub, ServerIp?.ToString(), a, out notify, AppConfig.Current.AllowPublish);
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
                        if (sub != "auclient" && this.SubSystemDic.ContainsKey(sub) && notify != null)
                            UpgradeMessageQueue.Enqueue(new UpgradeMessage()
                            {
                                SubSystem = sub,
                                UpdatePackFile = this.LocalPath + "\\" + sub + "\\" + notify.DownPath,
                                UpgradePath = this.SubSystemDic[sub]
                            });
                    }
                    //通知客户端消息
                    if (notify != null && AppConfig.Current.AllowPublish)
                    {
                        //ms.Send("", AU.Common.CommandType.AUVERSION, Newtonsoft.Json.JsonConvert.SerializeObject(new List<AuPublish>() { notify }));
                        AU.Monitor.Server.ServerBootstrap.Send("", AU.Common.CommandType.AUVERSION, Newtonsoft.Json.JsonConvert.SerializeObject(new List<AuPublish>() { notify }));
                    }
                    if (ct.IsCancellationRequested)
                        break;
                }
                catch (Exception e)
                {
                    log.Info(e);
                    //Console.WriteLine(e.Message);
                }

                System.Threading.Thread.Sleep(AppConfig.Current.Interval);
            }
            this.IsUpdateCheckRun = false;
        }
    }
}
