using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using SuperSocket.SocketBase;
using AU.Common.Utility;

namespace AuWriter
{
    public partial class AuWriterForm : Form
    {
        #region 属性
        /// <summary>
        /// 选择是否文件
        /// </summary>
        private bool IsSelectFile = false;
        /// <summary>
        /// 启动发布服务
        /// </summary>
        public Nancy.Hosting.Self.NancyHost nancySelfHost = null;
        /// <summary>
        /// 更新基地址
        /// </summary>
        public string BaseUpdatePath;
        /// <summary>
        /// 是否打包
        /// </summary>
        public bool IsPackage = false;
        /// <summary>
        /// 打包临时路径
        /// </summary>
        public string PackageTempPath = Path.Combine(Application.StartupPath, "packagetemp\\");
        /// <summary>
        /// 更新路径
        /// </summary>
        public string UpdatePackagePath;
        /// <summary>
        /// 子系统
        /// </summary>
        public string SubSystem;

        /// <summary>
        /// 版本发布
        /// </summary>
        List<AU.Common.AuPublish> AuPublishs = new List<AU.Common.AuPublish>();
        /// <summary>
        /// 指令字典
        /// </summary>
        Dictionary<string, List<string>> DicCmdType = new Dictionary<string, List<string>>() {
            { "",new List<string>(){ ""} },
            {"AUVERSION",new List<string>(){ ""} },
            {"TERMINAL",new List<string>(){ "START", "STOP" } },
            {"RESOURCE",new List<string>(){"SEND_DISKS","GET_DIRECTORY_DETIAL"}},
            {"SCRIPT",new List<string>(){"select","insert","update","delete","other"}},
        };
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public AuWriterForm()
        {
            InitializeComponent();
            //自已绘制  
            this.tvTerminal.DrawMode = TreeViewDrawMode.OwnerDrawText;
            this.tvTerminal.DrawNode += new DrawTreeNodeEventHandler(tvTerminal_DrawNode);
            Console.SetOut(new Monitor.Common.ListTextWriter(this.lbLog));
        }
        #region [选择主程序]
        /// <summary>
        /// 选择主程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSrc_Click(object sender, EventArgs e)
        {
            this.ofdSrc.Filter = "程序文件(*.exe)|*.exe|所有文件(*.*)|*.*";
            this.ofdSrc.ShowDialog(this);
        }
        string PublishVersion = "0.0.0.0";
        /// <summary>
        /// 文件选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ofdSrc_FileOk(object sender, CancelEventArgs e)
        {
            this.txtSrc.Text = this.ofdSrc.FileName;
            FileVersionInfo m_fvi = FileVersionInfo.GetVersionInfo(this.txtSrc.Text);
            this.tbVersion.ReadOnly = true;
            this.tbVersion.Text = this.PublishVersion = string.Format("{0}.{1}.{2}.{3}", m_fvi.FileMajorPart, m_fvi.FileMinorPart, m_fvi.FileBuildPart, m_fvi.FilePrivatePart);
            this.txtUrl.Text = this.BaseUpdatePath;
            this.IsSelectFile = true;
        }
        /// <summary>
        /// 目录选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDir_Click(object sender, EventArgs e)
        {
            if (this.fbdSrc.ShowDialog(this) == DialogResult.OK)
            {
                string foldPath = this.fbdSrc.SelectedPath;
                this.txtSrc.Text = this.fbdSrc.SelectedPath;
                this.IsSelectFile = false;
                this.txtUrl.Text = this.BaseUpdatePath;
                this.tbVersion.ReadOnly = false;
                this.tbVersion.Text = this.PublishVersion;

                MessageBox.Show("请修改更新地址主版本号【0.0.0.0】为实际版本号");
            }

        }

        /// <summary>
        /// 保存位置选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDest_Click(object sender, EventArgs e)
        {
            this.sfdDest.ShowDialog(this);
        }
        /// <summary>
        /// 推出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        /// <summary>
        /// 包清单保存位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sfdDest_FileOk(object sender, CancelEventArgs e)
        {
            this.txtDest.Text = this.sfdDest.FileName.Substring(0, this.sfdDest.FileName.LastIndexOf(@"\")) + @"\aupackage.json";
        }
        /// <summary>
        /// 排除文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExpt_Click(object sender, EventArgs e)
        {
            this.ofdExpt.ShowDialog(this);
        }
        /// <summary>
        /// 过滤文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ofdExpt_FileOk(object sender, CancelEventArgs e)
        {
            foreach (string _filePath in this.ofdExpt.FileNames)
            {
                this.txtExpt.Text += @_filePath.ToString() + "\n\r;";
            }
        }

        /// <summary>
        /// 是否打包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            IsPackage = cbPackage.Checked;
            if (cbPackage.Checked)
            {
                this.txtDest.Text = PackageTempPath + "aupackage.json";
                this.txtDest.ReadOnly = true;
                if (System.IO.Directory.Exists(PackageTempPath))
                    System.IO.Directory.Delete(PackageTempPath, true);
            }
            else
            {
                this.txtDest.Text = "";
                this.txtDest.ReadOnly = false;

            }
        }

        #endregion [选择主程序]

        #region [主窗体加载]
        private void InitAuPublishs()
        {
            this.UpdatePackagePath = Application.StartupPath + "\\" + (System.Configuration.ConfigurationManager.AppSettings["VirtualPath"] ?? "package");
            if (System.IO.Directory.Exists(this.UpdatePackagePath))
                foreach (string f in AU.Common.Utility.ToolsHelp.GetAllFiles(this.UpdatePackagePath))
                {
                    if (f.EndsWith("aupublish.json"))
                    {
                        var pub = AU.Common.AppPublish.ReadPackage(f);
                        if (pub != null)
                        {
                            AddAuPublishs(pub);
                        }
                    }
                }
        }
        private void AddAuPublishs(AU.Common.AuPublish auPublish)
        {
            var autemp = this.AuPublishs.FirstOrDefault(d => d.PublishType == auPublish.PublishType);
            if (autemp != null)
            {
                this.AuPublishs.Remove(autemp);
            }
            this.AuPublishs.Add(auPublish);
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            cmbCmd.DataSource = DicCmdType.Keys.ToList();
            tabControlMain.TabPages.Remove(tbpOpt);
            BindingSource bs = new BindingSource();
            bs.DataSource = AU.Common.SubSystem.Dic;
            this.cbSubSystem.DataSource = bs;
            this.cbSubSystem.DisplayMember = "Value";
            this.cbSubSystem.ValueMember = "Key";
            this.cbSubSystem.SelectedIndexChanged += new System.EventHandler(this.cbSubSystem_SelectedIndexChanged);
            this.cbSubSystem_SelectedIndexChanged(cbSubSystem, EventArgs.Empty);
            this.txtUrl.Text = BaseUpdatePath;
            InitAuPublishs();

            try
            {
                Uri pub = new Uri(this.BaseUpdatePath);
                string url = "http://localhost:" + pub.Port;
                var nancySelfHost = new Nancy.Hosting.Self.NancyHost(new Uri(url), new MyBootstrapper());
                nancySelfHost.Start();
                Console.WriteLine("NancySelfHost已启动。。");
                //System.Diagnostics.Process.Start(url);
                Console.WriteLine("监听地址：" + url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show(ex.Message);
            }

            AU.Monitor.Server.ServerBootstrap.Init(Ms_NewSessionConnected, Ms_SessionClosed, Ms_NewRequestReceived);
            启动服务ToolStripMenuItem_Click(启动服务ToolStripMenuItem, EventArgs.Empty);


            imageKey = new System.Collections.Hashtable();
            System.Collections.Specialized.StringCollection keyCol = iml_ExplorerImages.Images.Keys;
            for (int i = 0; i < keyCol.Count; i++)
                if (!imageKey.Contains(keyCol[i]))
                    imageKey.Add(keyCol[i], keyCol[i]);

            IO.OpenRoot(lvLocalDisk, imageKey);
        }
        #endregion [主窗体加载]

        #region [生成文件]
        private void btnProduce_Click(object sender, EventArgs e)
        {


            try
            {
                //建立新线程
                Thread thrdProduce = new Thread(new ThreadStart(WriterAUPackage));

                if (this.btnProduce.Text == "生成(&G)")
                {
                    System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(@"\d+.\d.+\d.+\d", System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace);

                    if (rg.IsMatch(tbVersion.Text))
                    {
                        this.PublishVersion = tbVersion.Text.Trim();
                    }
                    else
                    {
                        MessageBox.Show("输入版本号格式不正确，请重新输入！");
                        this.tbVersion.Focus();
                        return;
                    }

                    #region [检测基本条件]

                    //if (!File.Exists(this.txtSrc.Text))
                    //{
                    //    MessageBox.Show(this, "请选择主入口程序!", "AutoUpdater", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    this.btnSrc_Click(sender, e);
                    //}

                    #region [请输入自动更新网址]

                    if (this.txtUrl.Text.Trim().Length == 0)
                    {
                        MessageBox.Show(this, "请输入自动更新网址!", "AutoUpdater", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.txtUrl.Focus();

                        return;
                    }


                    #endregion [请输入自动更新网址]

                    if (this.txtDest.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show(this, "请选择AutoUpdaterList保存位置!", "AutoUpdater", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.btnDest_Click(sender, e);
                    }

                    #endregion [检测基本条件]

                    #region [新线程写文件]

                    thrdProduce.IsBackground = true;
                    thrdProduce.Start();

                    #endregion [新线程写文件]
                    cbPackage.Enabled = false;
                    this.btnProduce.Text = "停止(&S)";
                }
                else
                {
                    Application.DoEvents();
                    if (MessageBox.Show(this, "是否停止文件生成更新文件?", "AutoUpdater", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //thrdProduce.Interrupt();
                        //thrdProduce.Abort();
                        if (thrdProduce.IsAlive)
                        {
                            thrdProduce.Abort();
                            thrdProduce.Join();
                        }
                        cbPackage.Enabled = true;
                        this.btnProduce.Text = "生成(&G)";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成失败，详情：" + ex.Message);
            }
        }

        #region [写AutoUpdaterList]
        void WriterAUPackage()
        {
            try
            {
                #region [WriterAUPackage]
                //包保存位置
                string strFilePath = this.txtDest.Text.Trim();

                AU.Common.AuList aulist = new AU.Common.AuList();
                //aulist.Url = this.txtUrl.Text.Trim();

                #region [application]
                string strEntryPoint = "";
                string dr = "";
                //入口函数
                if (this.IsSelectFile)
                {
                    FileVersionInfo m_fvi = FileVersionInfo.GetVersionInfo(this.txtSrc.Text);
                    aulist.Application.Version = string.Format("{0}.{1}.{2}.{3}", m_fvi.FileMajorPart, m_fvi.FileMinorPart, m_fvi.FileBuildPart, m_fvi.FilePrivatePart);
                    strEntryPoint = this.txtSrc.Text.Trim().Substring(this.txtSrc.Text.Trim().LastIndexOf(@"\") + 1, this.txtSrc.Text.Trim().Length - this.txtSrc.Text.Trim().LastIndexOf(@"\") - 1);
                    if (strEntryPoint.EndsWith(".exe", StringComparison.InvariantCultureIgnoreCase))
                    {
                        //入口点
                        aulist.Application.EntryPoint = strEntryPoint;
                        aulist.Application.StartType = 1;
                    }
                    aulist.Application.ApplicationId = strEntryPoint;
                    dr = this.txtSrc.Text.Substring(0, this.txtSrc.Text.LastIndexOf(@"\"));
                    aulist.Application.Location = ".";
                }
                else
                {
                    dr = this.txtSrc.Text;
                    aulist.Application.Version = this.PublishVersion;
                    if (SubSystem == AU.Common.SystemType.coreserver.ToString())
                    {
                        if (File.Exists(dr + "\\Manage\\OneCardSystem.SyncTokenControl.exe"))
                        {
                            aulist.Application.EntryPoint = aulist.Application.ApplicationId = "OneCardSystem.SyncTokenControl.exe";
                            aulist.Application.StartType = 1;
                            aulist.Application.StartArgs = "-m";
                            aulist.Application.CloseType = 1;
                            aulist.Application.CloseArgs = "-u";
                            aulist.Application.Location = ".\\Manage\\";
                        }
                    }
                    else if (SubSystem == AU.Common.SystemType.vmsclient.ToString())
                    {
                        aulist.Application.EntryPoint = aulist.Application.ApplicationId = "OneCardSystem.VehicleManageWPF.exe";
                        aulist.Application.StartType = 1;
                        aulist.Application.StartArgs = "";
                        aulist.Application.CloseType = 0;
                        aulist.Application.CloseArgs = "";
                        aulist.Application.Location = ".";
                    }
                }

                aulist.No = Guid.NewGuid().ToString();
                aulist.Description = tbUpdateMsg.Text;
                aulist.LastUpdateTime = DateTime.Now;

                #endregion [application]

                #region [Files]
                StringCollection strColl = AU.Common.Utility.ToolsHelp.GetAllFiles(dr);

                this.Invoke((MethodInvoker)delegate ()
                {
                    this.prbProd.Visible = true;
                    this.prbProd.Minimum = 0;
                    this.prbProd.Maximum = strColl.Count;
                });
                System.Security.Cryptography.SHA256 sha256 = new System.Security.Cryptography.SHA256Managed();
                string rootDir = dr + @"\";
                for (int i = 0; i < strColl.Count; i++)
                {
                    if (!CheckExist(strColl[i].Trim()))
                    {
                        string path = strColl[i].ToString();
                        FileVersionInfo f_fvi = FileVersionInfo.GetVersionInfo(path);
                        AU.Common.AuFile aufile = new AU.Common.AuFile();

                        //写目录
                        aufile.WritePath = aufile.No = @strColl[i].Replace(@rootDir, "");
                        if (path.EndsWith(".exe", StringComparison.InvariantCultureIgnoreCase))
                        {
                            aufile.FileType = 1;
                        }
                        else if (path.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase))
                        {
                            aufile.FileType = 0;
                        }
                        else if (path.EndsWith(".sql", StringComparison.InvariantCultureIgnoreCase))
                        {
                            aufile.FileType = 2;
                        }
                        else
                        {
                            aufile.FileType = 4;
                        }
                        //aufile.RunTime
                        aufile.SHA256 = AU.Common.Utility.ToolsHelp.ComputeSHA256(path);
                        aufile.Version = string.Format("{0}.{1}.{2}.{3}", f_fvi.FileMajorPart, f_fvi.FileMinorPart, f_fvi.FileBuildPart, f_fvi.FilePrivatePart);
                        aulist.Files.Add(aufile);
                        if (aufile.Version == "0.0.0.0")
                            aufile.Version = this.PublishVersion;
                        //打包复制临时目录
                        if (IsPackage)
                        {
                            string destpath = Path.Combine(PackageTempPath, path.Replace(dr, "").TrimStart('\\'));
                            AU.Common.Utility.ToolsHelp.CreateDirtory(destpath);
                            File.Copy(path, destpath, true);
                        }
                    }
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        this.prbProd.Value = i;
                    });
                }
                #endregion [Files]
                StreamWriter sw = new StreamWriter(strFilePath, false, System.Text.Encoding.UTF8);
                sw.Write(Newtonsoft.Json.JsonConvert.SerializeObject(aulist));
                sw.Close();
                #region 打包
                string mesg = this.txtDest.Text.Trim();
                if (IsPackage)
                {
                    string packagename = aulist.Application.Version + ".aup";
                    mesg = Path.Combine(UpdatePackagePath + "\\" + this.SubSystem + "\\", packagename);
                    AU.Common.Utility.ZipUtility.Compress(PackageTempPath, mesg);
                    string aupublish = UpdatePackagePath + "\\" + this.SubSystem + "\\aupublish.json";
                    string No = "1";
                    if (System.IO.File.Exists(aupublish))
                    {
                        No = (Convert.ToInt32(AU.Common.AppPublish.ReadPackage(aupublish).No) + 1).ToString();
                    }
                    AU.Common.AuPublish auPublish = new AU.Common.AuPublish()
                    {
                        No = No,
                        Description = "发布更新" + packagename,
                        Url = BaseUpdatePath,
                        DownPath = packagename,
                        LastUpdateTime = DateTime.Now,
                        PublishType = AU.Common.SubSystem.DicPublishType[this.SubSystem],
                        SHA256 = AU.Common.Utility.ToolsHelp.ComputeSHA256(mesg),
                        UpdateType = 0,
                        Version = aulist.Application.Version,
                    };
                    //发布包
                    StreamWriter swau = new StreamWriter(aupublish, false, System.Text.Encoding.UTF8);
                    string aujson = Newtonsoft.Json.JsonConvert.SerializeObject(auPublish);
                    swau.Write(aujson);

                    swau.Close();
                    //同时把新包加入列表
                    AddAuPublishs(auPublish);
                }
                #endregion
                #region [Notification]

                this.Invoke((MethodInvoker)delegate ()
                {
                    this.prbProd.Value = 0;
                    this.prbProd.Visible = false;
                    this.btnProduce.Text = "生成(&G)";
                    cbPackage.Enabled = true;
                    //发送客户端通知消息
                    AU.Monitor.Server.ServerBootstrap.Send("", AU.Common.CommandType.AUVERSION, Newtonsoft.Json.JsonConvert.SerializeObject(this.AuPublishs));
                    MessageBox.Show(this, "自动更新文件生成成功:" + mesg, "AutoUpdater", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (System.IO.Directory.Exists(PackageTempPath))
                        System.IO.Directory.Delete(PackageTempPath, true);
                });

                #endregion [Notification]

                #endregion [WriterAUPackage]
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    MessageBox.Show(this, ex.Message, "AutoUpdater", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
            }
        }
        #endregion [写AutoUpdaterList]

        #region [排除不需要的文件]

        private bool CheckExist(string filePath)
        {
            bool isExist = false;
            if (cbFilter.Checked)
            {
                System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(@"\.(log|config|db|dat)$|(unins000.exe)", System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace);
                isExist = rg.IsMatch(filePath);
                if (isExist)
                    return isExist;
            }
            foreach (string strCheck in this.txtExpt.Text.Split(';'))
            {
                if (filePath.Trim() == strCheck.Trim())
                {
                    isExist = true;

                    break;
                }
            }

            return isExist;
        }


        #endregion [排除不需要的文件]

        #endregion [生成文件]

        private void cbSubSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            int port;
            try
            {
                port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["UpdatePort"]);
            }
            catch
            {
                port = 52706;
            }


            string url = string.Format("http://{0}:{1}/package/", AU.Common.Utility.IpHelp.GetLocalIP(), port);
            this.BaseUpdatePath = Path.Combine(url, cbSubSystem.SelectedValue.ToString()) + "/";
            this.txtUrl.Text = this.BaseUpdatePath;
            this.SubSystem = cbSubSystem.SelectedValue.ToString();

            bool auclient = this.SubSystem == "auclient";

            this.panelAuclient.Visible = auclient;
            this.gbPublish.Visible = !auclient;
        }
        private void Ms_NewSessionConnected(AU.Monitor.Server.MonitorSession session)
        {
            this.AddTreeView(session.SessionID, session.RemoteEndPoint, session.StartTime, null);

            session.Send(AU.Common.CommandType.AUVERSION + ":" + Newtonsoft.Json.JsonConvert.SerializeObject(this.AuPublishs));

            Console.WriteLine("New Connected\tID=[" + session.SessionID + "]\tIP=" + session.RemoteEndPoint.ToString());

        }

        private void ModifyTreeView(string sessionId, System.Net.IPEndPoint endPoint, DateTime startTime, AU.Common.LoginModel model)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                if (tvTerminal.Nodes.ContainsKey(sessionId))
                {
                    tvTerminal.Nodes[sessionId].Tag = model;
                    if (!string.IsNullOrEmpty(model.ProjectName))
                    {
                        tvTerminal.Nodes[sessionId].Text = model.ProjectName + "(Ver:" + model.Version + ")";
                        tvTerminal.Nodes[sessionId].ToolTipText = string.Format("(E7:{0}) {1} {2} [{3}]", model.ProjectVer, endPoint.ToString(), startTime.ToString("yyyy/MM/dd HH:mm:ss"), model.ProjectNo);
                    }
                }
            });
        }

        private void AddTreeView(string sessionId, System.Net.IPEndPoint endpoint, DateTime startTime, List<AU.Common.SessionModel> sms)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                if (!tvTerminal.Nodes.ContainsKey(sessionId))
                {
                    tvTerminal.Nodes.Add(new TreeNode()
                    {
                        Name = sessionId,
                        Text = sessionId,
                        ToolTipText = endpoint + " " + startTime.ToString("yyyy/MM/dd HH:mm:ss"),
                        ContextMenuStrip = contextMenuStrip1,
                    });
                }
                if (sms == null)
                    return;
                tvTerminal.Nodes[sessionId].Nodes.Clear();
                foreach (var s in sms)
                {
                    tvTerminal.Nodes[sessionId].Nodes.Add(
                        new TreeNode()
                        {
                            Name = s.SessionId,
                            Text = s.Name + "(" + s.Version + ")",
                            ToolTipText = s.RemoteEndPoint.ToString(),
                            ContextMenuStrip = contextMenuStrip1,
                        });
                }
            });
        }

        private void Ms_SessionClosed(AU.Monitor.Server.MonitorSession session, SuperSocket.SocketBase.CloseReason value)
        {
            if (PartPackage.ContainsKey(session.SessionID))
                PartPackage.Remove(session.SessionID);

            this.BeginInvoke((MethodInvoker)delegate
            {
                tvTerminal.Nodes.RemoveByKey(session.SessionID);
            });

            Console.WriteLine("Session Closed\tID=[" + session.SessionID + "]\tIP=" + session.RemoteEndPoint.ToString() + "\tReason=" + value);
        }

        private System.Collections.Hashtable PartPackage = new System.Collections.Hashtable();
        private System.Collections.Hashtable FilePackage = new System.Collections.Hashtable();
        /// <summary>
        /// 上传速度
        /// </summary>
        private NetworkSpeed nsUpload = new NetworkSpeed();
        /// <summary>
        /// 下载速度
        /// </summary>
        private NetworkSpeed nsDownload = new NetworkSpeed();
        /// <summary>
        /// 消息分发
        /// </summary>
        /// <param name="session"></param>
        /// <param name="requestInfo"></param>
        private void Ms_NewRequestReceived(AU.Monitor.Server.MonitorSession session, SuperSocket.SocketBase.Protocol.StringRequestInfo requestInfo)
        {
            try
            {
                switch (requestInfo.Key)
                {
                    case "SESSION":
                        {
                            var au = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AU.Common.SessionModel>>(requestInfo.Body);
                            this.AddTreeView(session.SessionID, session.RemoteEndPoint, session.StartTime, au);
                        }
                        break;
                    case "LOGIN":
                        {
                            var mode = Newtonsoft.Json.JsonConvert.DeserializeObject<AU.Common.LoginModel>(requestInfo.Body);//
                            if (mode != null)
                                this.ModifyTreeView(session.SessionID, session.RemoteEndPoint, session.StartTime, mode);

                        }
                        break;
                    case "RESOURCE":
                        {
                            if (string.IsNullOrEmpty(requestInfo.Body))
                                break;
                            var cp = Newtonsoft.Json.JsonConvert.DeserializeObject<AU.Monitor.Server.CommandPackage>(requestInfo.Body);
                            string result = string.Empty;

                            switch (cp.Key)
                            {
                                case "SEND_DISKS":
                                    AU.Common.Codes.DisksCode diskcode = Newtonsoft.Json.JsonConvert.DeserializeObject<AU.Common.Codes.DisksCode>(cp.Body);
                                    this.BeginInvoke((MethodInvoker)delegate
                                    {
                                        IO.ShowDisks(diskcode, lvRemoteDisk, imageKey, false);
                                    });

                                    break;
                                case "GET_DIRECTORY_DETIAL":
                                    AU.Common.Codes.ExplorerCode explorer = Newtonsoft.Json.JsonConvert.DeserializeObject<AU.Common.Codes.ExplorerCode>(cp.Body);
                                    this.BeginInvoke((MethodInvoker)delegate
                                    {
                                        IO.ShowHostDirectory(explorer, lvRemoteDisk, imageKey);
                                    });

                                    break;
                                case "GET_FILE_DETIAL":
                                    this.BeginInvoke((MethodInvoker)delegate
                                    {
                                        txt_remoteexplorer.Text = cp.Body;
                                    });

                                    break;
                                case "DELETE_FILE":
                                    this.BeginInvoke((MethodInvoker)delegate
                                    {
                                        this.toolStripStatusLabel1.ForeColor = (cp.Parameters != null && "1".Equals(cp.Parameters[0])) ? Color.Green : Color.Red;
                                        this.toolStripStatusLabel1.Text = cp.Body;
                                        string rpath = lvRemoteDisk.Tag.ToString();
                                        SendMessage(AU.Common.CommandType.RESOURCE, string.IsNullOrEmpty(rpath) ? "SEND_DISKS" : "GET_DIRECTORY_DETIAL", rpath);
                                    });

                                    break;
                            }
                        }

                        break;
                    case "P":
                        if (requestInfo.Parameters == null && requestInfo.Parameters.Length < 3)
                            break;
                        int number = Convert.ToInt32(requestInfo.Parameters[1]);
                        if (PartPackage.ContainsKey(session.SessionID))
                        {
                            var t = PartPackage[session.SessionID] as System.Collections.Hashtable;
                            if (t == null)//不应发生
                            {
                                PartPackage.Remove(session.SessionID);
                                break;
                            }
                            if (t.ContainsKey(requestInfo.Parameters[0]))
                            {
                                var a = t[requestInfo.Parameters[0]] as Dictionary<int, string>;
                                if (a.ContainsKey(number))//重复消息
                                    break;
                                else
                                    a.Add(number, requestInfo.Parameters[2]);

                            }
                            else
                            {
                                t.Add(requestInfo.Parameters[0], new Dictionary<int, string>() { { number, requestInfo.Parameters[2] } });
                            }
                        }
                        else
                        {
                            System.Collections.Hashtable t = new System.Collections.Hashtable();
                            t.Add(requestInfo.Parameters[0], new Dictionary<int, string>() { { number, requestInfo.Parameters[2] } });

                            PartPackage.Add(session.SessionID, t);

                        }

                        break;
                    case "PE":
                        {
                            if (PartPackage.ContainsKey(session.SessionID))
                            {
                                var t = PartPackage[session.SessionID] as System.Collections.Hashtable;
                                if (t == null)//不应发生
                                {
                                    PartPackage.Remove(session.SessionID);
                                    break;
                                }
                                if (t.ContainsKey(requestInfo.Body))
                                {
                                    var a = t[requestInfo.Body] as Dictionary<int, string>;
                                    var key = a.Keys.ToList();
                                    key.Sort();
                                    StringBuilder sb = new StringBuilder();
                                    foreach (var k in key)
                                    {
                                        sb.Append(a[k]);
                                    }
                                    t.Remove(requestInfo.Body);
                                    var msg = sb.ToString();
                                    int i = msg.IndexOf(':');
                                    if (i > -1)
                                    {
                                        var v = msg.Substring(0, i);
                                        string body = msg.Substring(i + 1);
                                        Ms_NewRequestReceived(session, new SuperSocket.SocketBase.Protocol.StringRequestInfo(v, body, body.Split('&')));
                                    }
                                    else
                                    {
                                        Ms_NewRequestReceived(session, new SuperSocket.SocketBase.Protocol.StringRequestInfo(msg, msg, msg.Split('&')));
                                    }

                                }
                            }
                        }
                        break;
                    case "F":
                        {
                            if (!FilePackage.ContainsKey(session.SessionID))
                            {
                                string path = lvLocalDisk.Tag.ToString();
                                string dst = path == "" ? Application.StartupPath : path + "\\" + requestInfo.Parameters[1];
                                this.BeginInvoke((MethodInvoker)delegate
                                {
                                    this.lbl_Display.Text = dst;
                                    this.toolStripStatusLabel1.ForeColor = Color.Black;
                                    this.toolStripStatusLabel1.Text = "开始上传:" + requestInfo.Parameters[1];
                                    this.panelUpload.Visible = true;
                                    this.pgbUpLoad.Value = 0;
                                    this.pgbUpLoad.Maximum = Convert.ToInt32(requestInfo.Parameters[0]);
                                    this.lbUpload.Text = "0%";
                                });
                                var file = File.Open(dst, FileMode.OpenOrCreate);
                                FilePackage.Add(session.SessionID, file);
                            }

                        }
                        break;
                    case "FS":
                        {
                            if (FilePackage.ContainsKey(session.SessionID))
                            {
                                var fs = FilePackage[session.SessionID] as FileStream;
                                if (fs == null)
                                {
                                    FilePackage.Remove(session.SessionID);
                                    break;
                                }
                                //System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(() =>
                                //{
                                byte[] buff = AU.Common.Utility.ToolsHelp.HexStringToByte(requestInfo.Body);
                                fs.Write(buff, 0, buff.Length);
                                fs.Flush();
                                this.BeginInvoke((MethodInvoker)delegate
                                {
                                    nsUpload.Increment(1024);
                                    if (this.pgbUpLoad.Value < this.pgbUpLoad.Maximum)
                                        this.pgbUpLoad.Value += 1;
                                    this.lbUpload.Text = ((float)this.pgbUpLoad.Value / this.pgbUpLoad.Maximum * 100) + "%" + " 上传速度: " + nsUpload.GetSpeed();
                                });
                                //});
                                //t.Start();
                            }
                            return;
                        }
                    case "FE":
                        {
                            if (FilePackage.ContainsKey(session.SessionID))
                            {
                                var fs = FilePackage[session.SessionID] as FileStream;
                                if (fs != null)
                                {
                                    fs.Close();
                                    fs.Dispose();
                                }
                                FilePackage.Remove(session.SessionID);
                            }
                            this.BeginInvoke((MethodInvoker)delegate
                            {
                                AU.Common.Utility.IO.OpenDirectory(lvLocalDisk.Tag.ToString(), lvLocalDisk, imageKey);
                                this.panelUpload.Visible = false;
                                this.pgbUpLoad.Value = this.pgbUpLoad.Maximum;
                                this.toolStripStatusLabel1.ForeColor = requestInfo.Parameters[0].Equals("1") ? Color.Green : Color.Red;
                                this.toolStripStatusLabel1.Text = requestInfo.Parameters[1];
                                this.UploadToolStripMenuItem.Enabled = true;
                            });
                        }
                        break;
                    case "TERMINAL":
                        {
                            this.BeginInvoke((MethodInvoker)delegate
                            {
                                this.rtbTerminial.AppendText(requestInfo.Body + "\n");
                            });
                        }
                        break;
                    case "ERROR":
                        {
                            Console.WriteLine(requestInfo.Body);
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("Session Message\tID=[" + session.SessionID + "]\tIP=" + session.RemoteEndPoint.ToString() + "\tKey=" + requestInfo.Key + "\tMessage=" + requestInfo.Body);
        }

        private string GetTreeViewRoute(TreeNode nowNode, ref string route)
        {
            if (nowNode == null)
            {
                return "";
            }
            if (nowNode.Parent == null)
                return nowNode.Name;
            else
            {
                route += nowNode.Name + "\\";
                return GetTreeViewRoute(nowNode.Parent, ref route);
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            string route = string.Empty;
            string session = string.Empty;
            string msg = tbMsg.Text;
            if (tvTerminal.Tag != null)
            {
                var t = tvTerminal.Tag.ToString().Split(':');
                session = t[0];
                route = t[1];
            }
            if (cmbCmd.SelectedIndex == 0)
            {
                if (string.IsNullOrEmpty(msg))
                {
                    msg = Newtonsoft.Json.JsonConvert.SerializeObject(this.AuPublishs);
                    AU.Monitor.Server.ServerBootstrap.Send(session, AU.Common.CommandType.AUVERSION, msg);
                    Console.WriteLine("{0}:\t{1}", AU.Common.CommandType.AUVERSION, msg);
                }
                else
                {
                    AU.Monitor.Server.ServerBootstrap.Send(session, msg);
                    //原始文本
                    Console.WriteLine(msg);
                }

            }
            else
            {
                if (string.IsNullOrEmpty(msg))
                {
                    MessageBox.Show("请填写发送指令信息");
                    return;
                }
                else
                {
                    SendMessage(session, route, cmbCmd.Text, cmbCmdType.Text, tbMsg.Text, "", tbParameter.Text.Split(','));
                }
            }
        }

        private bool GetSession(out string session, out string route)
        {
            route = string.Empty;
            session = string.Empty;
            if (tvTerminal.Tag == null)
                return false;

            var t = tvTerminal.Tag.ToString().Split(':');
            session = t[0];
            route = t[1];

            return true;
        }

        private void SendMessage(string cmd, string key, string body, string attach = "", params string[] par)
        {
            string route = string.Empty;
            string session = string.Empty;

            if (!GetSession(out session, out route))
            {
                MessageBox.Show("请选择接收终端！");
                return;
            }

            SendMessage(session, route, cmd, key, body, attach, par);
        }
        private void SendMessage(string session, string route, string cmd, string msg)
        {
            var routes = route.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            if (routes == null || routes.Length == 0)
            {
                AU.Monitor.Server.ServerBootstrap.Send(session, cmd, msg);
            }
            else
            {
                AU.Monitor.Server.TransferPackage tp = new AU.Monitor.Server.TransferPackage()
                {
                    Route = routes,
                    RouteIndex = -1,
                    Cmd = cmd,
                    Message = msg,
                };
                msg = Newtonsoft.Json.JsonConvert.SerializeObject(tp);
                //msg = cp.Route.Replace('\\', '&') + cmd + ":" + msg;

                AU.Monitor.Server.ServerBootstrap.Send(session, AU.Common.CommandType.TRANSFER, msg);
            }
        }
        private void SendMessage(string session, string route, string cmd, string key, string body, string attach = "", params string[] par)
        {
            AU.Monitor.Server.CommandPackage cp = new AU.Monitor.Server.CommandPackage()
            {
                Key = key,
                Body = body,
                Parameters = par,
                Attachment = attach,
            };

            string msg = Newtonsoft.Json.JsonConvert.SerializeObject(cp);

            SendMessage(session, route, cmd, msg);
            Console.WriteLine("{0}:\t{1}", cmd, msg);
        }

        #region 菜单操作
        private void 启动服务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartResult result = AU.Monitor.Server.ServerBootstrap.Bootstrap.Start();
            Console.WriteLine("Start result: {0}!", result);
        }

        private void 停止服务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AU.Monitor.Server.ServerBootstrap.Stop();
        }
        #endregion

        private void lbLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbLog.SelectedIndex > -1)
            {
                tbpContent.Visible = true;
                tbpContent.Text = lbLog.Items[lbLog.SelectedIndex].ToString().Replace("\t", "\r\n");
            }
        }


        //在绘制节点事件中，按自已想的绘制  
        private void tvTerminal_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true; //我这里用默认颜色即可，只需要在TreeView失去焦点时选中节点仍然突显  
            return;
            //or  自定义颜色  
            if ((e.State & TreeNodeStates.Selected) != 0)
            {
                //演示为绿底白字  
                e.Graphics.FillRectangle(Brushes.DarkBlue, e.Node.Bounds);

                Font nodeFont = e.Node.NodeFont;
                if (nodeFont == null) nodeFont = ((TreeView)sender).Font;
                e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.White, Rectangle.Inflate(e.Bounds, 2, 0));
            }
            else
            {
                e.DrawDefault = true;
            }

            if ((e.State & TreeNodeStates.Focused) != 0)
            {
                using (Pen focusPen = new Pen(Color.Black))
                {
                    focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    Rectangle focusBounds = e.Node.Bounds;
                    focusBounds.Size = new Size(focusBounds.Width - 1,
                    focusBounds.Height - 1);
                    e.Graphics.DrawRectangle(focusPen, focusBounds);
                }
            }
        }

        private void cmbCmd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCmd.SelectedIndex > -1)
            {
                cmbCmdType.DataSource = DicCmdType[cmbCmd.SelectedValue.ToString()];
            }
        }

        private void SetDiskContextMenuStrip(AU.Common.Codes.FileFlag? flag, bool serverDisk)
        {
            cms_Disk.Tag = serverDisk;
            if (flag == null)
            {
                DownloadToolStripMenuItem.Visible = false;
                UploadToolStripMenuItem.Visible = false;
                DeleteToolStripMenuItem.Visible = false;
                BrowseToolStripMenuItem.Visible = false;

                return;
            }
            switch (flag.Value)
            {
                case AU.Common.Codes.FileFlag.Disk:
                    DownloadToolStripMenuItem.Visible = false;
                    UploadToolStripMenuItem.Visible = false;
                    DeleteToolStripMenuItem.Visible = false;
                    BrowseToolStripMenuItem.Visible = true;
                    break;
                case AU.Common.Codes.FileFlag.Directory:
                    DownloadToolStripMenuItem.Visible = false;
                    UploadToolStripMenuItem.Visible = false;
                    DeleteToolStripMenuItem.Visible = false;
                    BrowseToolStripMenuItem.Visible = true;
                    break;
                case AU.Common.Codes.FileFlag.File:
                    DownloadToolStripMenuItem.Visible = !serverDisk;
                    UploadToolStripMenuItem.Visible = serverDisk;
                    DeleteToolStripMenuItem.Visible = true;
                    BrowseToolStripMenuItem.Visible = false;
                    break;
            }
        }

        private void lvLocalDisk_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvLocalDisk.FocusedItem != null)
            {
                lvLocalDisk.ContextMenuStrip = cms_Disk;
                AU.Common.Codes.BaseFile basefile = lvLocalDisk.FocusedItem.Tag as AU.Common.Codes.BaseFile;
                if (basefile != null)
                {
                    SetDiskContextMenuStrip(basefile.Flag, false);
                    if (basefile.Flag == AU.Common.Codes.FileFlag.Directory)
                    {
                        txt_myexplorer.Text = basefile.Name;
                        txt_myexplorer.Tag = "";
                    }
                    else if (basefile.Flag == AU.Common.Codes.FileFlag.File)
                    {
                        txt_myexplorer.Text = AU.Common.Utility.IO.GetFileDetial(basefile.Name);
                        txt_myexplorer.Tag = basefile.Name;
                    }
                }
                else
                    SetDiskContextMenuStrip(null, false);

            }
        }
        /// <summary>
        /// 哈希表(Key=文件后缀名,value=图片列表的Key)
        /// 例如:后缀名A=exe,则imageKey[A]="exe",而imageKey[A]则是对应的文件图标Key值.
        /// </summary>
        private System.Collections.Hashtable imageKey;
        private void lvLocalDisk_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem selectItem = null;
            AU.Common.Codes.BaseFile basefile = null;
            try
            {
                selectItem = lvLocalDisk.FocusedItem;
                if (selectItem != null)
                {
                    basefile = selectItem.Tag as AU.Common.Codes.BaseFile;
                    if (basefile != null)
                        if (basefile.Flag != AU.Common.Codes.FileFlag.File)
                        {
                            string path = (basefile.Flag == AU.Common.Codes.FileFlag.Directory ? basefile.Name : basefile.Name + @"\");
                            txt_myexplorer.Text = path;
                            txt_myexplorer.Tag = "";
                            AU.Common.Utility.IO.OpenDirectory(path, lvLocalDisk, imageKey);
                            lbl_Display.Text = path;
                        }
                        else
                        {
                            txt_myexplorer.Tag = basefile.Name;
                        }
                }
                else
                {
                    MessageBox.Show(" 请选择一个文件夹！");
                }
            }
            catch
            {
            }
        }

        private void lvRemoteDisk_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvRemoteDisk.FocusedItem != null)
            {
                lvRemoteDisk.ContextMenuStrip = cms_Disk;
                AU.Common.Codes.BaseFile basefile = lvRemoteDisk.FocusedItem.Tag as AU.Common.Codes.BaseFile;
                if (basefile != null)
                {
                    SetDiskContextMenuStrip(basefile.Flag, true);
                    if (basefile.Flag == AU.Common.Codes.FileFlag.Directory)
                    {
                        txt_remoteexplorer.Tag = "";
                        txt_remoteexplorer.Text = basefile.Name;
                    }
                    else if (basefile.Flag == AU.Common.Codes.FileFlag.File)
                    {
                        txt_remoteexplorer.Tag = basefile.Name;
                        SendMessage(AU.Common.CommandType.RESOURCE, "GET_FILE_DETIAL", basefile.Name);
                    }
                }
                else
                    SetDiskContextMenuStrip(null, true);
                //AU.Common.Utility.IO.GetFileDetial(basefile.Name);
            }
        }

        private void lvRemoteDisk_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem selectItem = null;
            AU.Common.Codes.BaseFile basefile = null;
            try
            {
                selectItem = lvRemoteDisk.FocusedItem;
                if (selectItem != null)
                {
                    basefile = selectItem.Tag as AU.Common.Codes.BaseFile;
                    if (basefile != null)
                        if (basefile.Flag != AU.Common.Codes.FileFlag.File)
                        {
                            string path = (basefile.Flag == AU.Common.Codes.FileFlag.Directory ? basefile.Name : basefile.Name + @"\");
                            txt_remoteexplorer.Text = path;
                            txt_remoteexplorer.Tag = "";
                            //AU.Common.Utility.IO.OpenDirectory(path, lvLocalDisk, imageKey);
                            lbl_Display.Text = path;
                            SendMessage(AU.Common.CommandType.RESOURCE, string.IsNullOrEmpty(path) ? "SEND_DISKS" : "GET_DIRECTORY_DETIAL", path);
                        }
                        else
                        {
                            txt_remoteexplorer.Tag = basefile.Name;
                        }
                }
                else
                {
                    MessageBox.Show(" 请选择一个文件夹！");
                }
            }
            catch
            {
            }
        }

        private void tvTerminal_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvTerminal.SelectedNode = e.Node;
            string route = string.Empty;
            string session = GetTreeViewRoute(tvTerminal.SelectedNode, ref route);
            tvTerminal.Tag = session + ":" + route;
        }

        private void tsmRemoteResource_Click(object sender, EventArgs e)
        {
            if (UploadToolStripMenuItem.Enabled == true && DownloadToolStripMenuItem.Enabled == true)
            {
                this.tabControlCmd.SelectedTab = tbpRes;
                this.gbRemoteDisk.Text = "远程磁盘>>" + this.tvTerminal.SelectedNode.Text;
                SendMessage(AU.Common.CommandType.RESOURCE, "SEND_DISKS", "");
            }
            else
            {
                MessageBox.Show("正在上传/下载中文件，请等待文件传输完成！");
            }
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (txt_remoteexplorer.Tag == null || txt_remoteexplorer.Tag.ToString() == "")
            {
                MessageBox.Show("请选择远程磁盘待上传文件");
                return;
            }
            if (lvLocalDisk.Tag == null || lvLocalDisk.Tag.ToString() == "")
            {
                MessageBox.Show("请选择待本地磁盘目录");
                return;
            }

            SendMessage(AU.Common.CommandType.RESOURCE, "GET_FILE", txt_remoteexplorer.Tag.ToString());
        }

        private void 断开连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode tn = tvTerminal.SelectedNode;
            if (tn != null)
            {
                //tn.Name
            }
        }

        private void lvLocalDisk_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem item = this.lvLocalDisk.GetItemAt(e.X, e.Y);

                if (item == null)
                {
                    SetDiskContextMenuStrip(null, false);
                }
            }
        }

        private void lvRemoteDisk_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem item = this.lvRemoteDisk.GetItemAt(e.X, e.Y);

                if (item == null)
                {
                    SetDiskContextMenuStrip(null, true);
                }
            }
        }

        private void SendFile(string session, string route, string file, string path)
        {
            new System.Threading.Tasks.Task(() =>
             {
                 bool rs = false;
                 string m = string.Empty;
                 try
                 {
                     using (System.IO.FileStream f = System.IO.File.Open(file, System.IO.FileMode.Open))
                     {
                         string name = System.IO.Path.GetFileName(file);
                         this.SendMessage(session, route, "F", path + "\\" + name);

                         this.BeginInvoke((MethodInvoker)delegate
                         {
                             this.lbl_Display.Text = System.IO.Path.Combine(path + name);
                             this.toolStripStatusLabel1.ForeColor = Color.Black;
                             this.toolStripStatusLabel1.Text = "开始下载:" + name;

                             this.panelDownload.Visible = true;
                             this.pgbDownload.Value = 0;
                             this.pgbDownload.Maximum = (int)(f.Length / 1024);
                             this.lbDownload.Text = "0%";
                         });

                         int len = 1024;
                         byte[] buff = new byte[len];
                         int count = 0;
                         do
                         {
                             count = f.Read(buff, 0, len);
                             if (count == len)
                             {//分片包编号后期扩展
                                 var temp = AU.Common.Utility.ToolsHelp.ByteToHexString(buff);
                                 //发送消息
                                 this.SendMessage(session, route, "FS", temp);
                             }
                             else if (count > 0)
                             {
                                 byte[] b = new byte[count];
                                 Array.Copy(buff, 0, b, 0, count);
                                 var temp = AU.Common.Utility.ToolsHelp.ByteToHexString(b);
                                 //发送消息
                                 this.SendMessage(session, route, "FS", temp);
                             }
                             this.BeginInvoke((MethodInvoker)delegate
                             {
                                 nsDownload.Increment(1024);
                                 if (this.pgbDownload.Value < this.pgbDownload.Maximum)
                                     this.pgbDownload.Value += 1;
                                 this.lbDownload.Text = ((float)this.pgbDownload.Value / this.pgbDownload.Maximum * 100) + "%" + " 下载速度: " + nsDownload.GetSpeed();
                             });
                             System.Threading.Thread.Sleep(1);
                         } while (count != 0);
                         f.Close();
                         f.Dispose();
                         m = "1&传输完成";
                         this.SendMessage(session, route, "FE", m);
                         rs = true;
                     }
                 }
                 catch (Exception e)
                 {
                     m = "0&传输失败详情" + e.Message;
                     this.SendMessage(session, route, "FE", m);
                 }
                 finally
                 {
                     this.BeginInvoke((MethodInvoker)delegate
                     {
                         //服务器刷新
                         string rpath = lvRemoteDisk.Tag.ToString();
                         SendMessage(AU.Common.CommandType.RESOURCE, string.IsNullOrEmpty(rpath) ? "SEND_DISKS" : "GET_DIRECTORY_DETIAL", rpath);
                         this.panelDownload.Visible = false;
                         this.pgbDownload.Value = this.pgbDownload.Maximum;
                         this.toolStripStatusLabel1.ForeColor = rs ? Color.Green : Color.Red;
                         this.toolStripStatusLabel1.Text = m;
                         this.DownloadToolStripMenuItem.Enabled = true;
                     });
                 }
             }).Start();
        }


        private void DownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (false.Equals(cms_Disk.Tag))
            {
                if (txt_myexplorer.Tag == null || !System.IO.File.Exists(txt_myexplorer.Tag.ToString()))
                {
                    MessageBox.Show("请选择本地磁盘待下载文件");
                    return;
                }
                if (lvRemoteDisk.Tag == null || lvRemoteDisk.Tag.ToString() == "")
                {
                    MessageBox.Show("请选择待远程磁盘目录");
                    return;
                }
                string route = string.Empty;
                string session = string.Empty;

                if (!GetSession(out session, out route))
                {
                    MessageBox.Show("请选择接收终端！");
                    return;
                }
                //发送文件
                SendFile(session, route, txt_myexplorer.Tag.ToString(), lvRemoteDisk.Tag.ToString());

                DownloadToolStripMenuItem.Enabled = false;
            }

        }

        private void UploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (true.Equals(cms_Disk.Tag))
            {
                if (txt_remoteexplorer.Tag == null || txt_remoteexplorer.Tag.ToString() == "")
                {
                    MessageBox.Show("请选择远程磁盘待上传文件");
                    return;
                }
                if (lvLocalDisk.Tag == null || lvLocalDisk.Tag.ToString() == "")
                {
                    MessageBox.Show("请选择待本地磁盘目录");
                    return;
                }

                SendMessage(AU.Common.CommandType.RESOURCE, "GET_FILE", txt_remoteexplorer.Tag.ToString());
                UploadToolStripMenuItem.Enabled = false;
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (true.Equals(cms_Disk.Tag))
                {
                    if (MessageBox.Show("您确定要删除远程磁盘[" + txt_remoteexplorer.Tag + "]文件吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        SendMessage(AU.Common.CommandType.RESOURCE, "DELETE_FILE", txt_remoteexplorer.Tag.ToString());
                    }
                }
                else
                {
                    if (!System.IO.File.Exists(txt_myexplorer.Tag.ToString()))
                        return;
                    if (MessageBox.Show("您确定要删除本地磁盘[" + txt_myexplorer.Tag + "]文件吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        System.IO.File.Delete(txt_myexplorer.Tag.ToString());
                        RefreshToolStripMenuItem_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void BrowseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (true.Equals(cms_Disk.Tag))
            {
                lvRemoteDisk_DoubleClick(sender, e);
            }
            else
            {
                lvLocalDisk_DoubleClick(sender, e);
            }
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (true.Equals(cms_Disk.Tag))
            {
                string path = lvRemoteDisk.Tag.ToString();
                SendMessage(AU.Common.CommandType.RESOURCE, string.IsNullOrEmpty(path) ? "SEND_DISKS" : "GET_DIRECTORY_DETIAL", path);
            }
            else
                AU.Common.Utility.IO.OpenDirectory(lvLocalDisk.Tag.ToString(), lvLocalDisk, imageKey);
        }

        private void lbLog_DoubleClick(object sender, EventArgs e)
        {
            if (lbLog.Items.Count > 0)
                lbLog.Items.Clear();
        }

        private void rtbTerminial_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks >= 2)
                rtbTerminial.Text = "";
        }

        private void btnbtnAuClientSelect_Click(object sender, EventArgs e)
        {
            this.ofdSrc.Filter = "程序文件(AuClient.exe)|AuClient.exe";
            this.ofdSrc.ShowDialog(this);
        }

        private void btnAuClientPub_Click(object sender, EventArgs e)
        {
            try
            {
                if (!System.IO.File.Exists(ofdSrc.FileName) || !ofdSrc.FileName.EndsWith("AuClient.exe"))
                {
                    MessageBox.Show("请选择升级文件【AuClient.exe】");
                    return;
                }

                string packagename = "AuClient.exe";
                string mesg = Path.Combine(UpdatePackagePath + "\\" + this.SubSystem + "\\", packagename);
                ToolsHelp.CreateDirtory(mesg);
                //拷贝文件
                System.IO.File.Copy(ofdSrc.FileName, mesg, true);

                string aupublish = UpdatePackagePath + "\\" + this.SubSystem + "\\aupublish.json";
                string No = "1";
                if (System.IO.File.Exists(aupublish))
                {
                    No = (Convert.ToInt32(AU.Common.AppPublish.ReadPackage(aupublish).No) + 1).ToString();
                }
                AU.Common.AuPublish auPublish = new AU.Common.AuPublish()
                {
                    No = No,
                    Description = "发布更新" + packagename,
                    Url = BaseUpdatePath,
                    DownPath = packagename,
                    LastUpdateTime = DateTime.Now,
                    PublishType = AU.Common.SubSystem.DicPublishType[this.SubSystem],
                    SHA256 = AU.Common.Utility.ToolsHelp.ComputeSHA256(mesg),
                    UpdateType = 0,
                    Version = tbVersion.Text,
                };
                //发布包
                StreamWriter swau = new StreamWriter(aupublish, false, System.Text.Encoding.UTF8);
                string aujson = Newtonsoft.Json.JsonConvert.SerializeObject(auPublish);
                swau.Write(aujson);

                swau.Close();
                //同时把新包加入列表
                AddAuPublishs(auPublish);
                MessageBox.Show("发布【AuClient.exe】升级包编号【" + No + "】成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("发布失败，详情：" + ex.Message);
            }
        }


        private bool FormMaximized = false;
        bool flag = true;
        /// <summary>
        /// 图标显示时间
        /// </summary>
        public int ToolTipShowTime = 100;
        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            flag = false;
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormMaximized ? FormWindowState.Maximized : FormWindowState.Normal;
        }

        private void AuWriterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("退出本程序将影响系统自动更新服务，您确定退出吗？", "系统提示", buttons: MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                return;
            }
        }

        private void AuWriterForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                if (flag)
                    this.notifyIcon1.ShowBalloonTip(ToolTipShowTime);
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                FormMaximized = true;
            }
            else
            {
                FormMaximized = false;
            }
            flag = true;
        }
    }
}
