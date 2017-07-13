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
        /// 
        /// </summary>
        List<AU.Common.AuPublish> AuPublishs = new List<AU.Common.AuPublish>();
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public AuWriterForm()
        {
            InitializeComponent();

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
            BindingSource bs = new BindingSource();
            bs.DataSource = AU.Common.SubSystem.Dic;
            this.cbSubSystem.DataSource = bs;
            this.cbSubSystem.DisplayMember = "Value";
            this.cbSubSystem.ValueMember = "Key";
            this.cbSubSystem.SelectedIndexChanged += new System.EventHandler(this.cbSubSystem_SelectedIndexChanged);
            this.cbSubSystem_SelectedIndexChanged(cbSubSystem, EventArgs.Empty);
            this.txtUrl.Text = BaseUpdatePath;
            InitAuPublishs();
            string url = "http://localhost:12345";
            var nancySelfHost = new Nancy.Hosting.Self.NancyHost(new Uri(url), new MyBootstrapper());
            try
            {
                nancySelfHost.Start();
                Console.WriteLine("NancySelfHost已启动。。");
                //System.Diagnostics.Process.Start(url);
                Console.WriteLine("监听地址：" + url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            AU.Monitor.Server.ServerBootstrap.Init(Ms_NewSessionConnected, Ms_SessionClosed, Ms_NewRequestReceived);
            启动服务ToolStripMenuItem_Click(启动服务ToolStripMenuItem, EventArgs.Empty);
        }
        #endregion [主窗体加载]

        #region [生成文件]
        private void btnProduce_Click(object sender, EventArgs e)
        {
            //建立新线程
            Thread thrdProduce = new Thread(new ThreadStart(WriterAUPackage));

            if (this.btnProduce.Text == "生成(&G)")
            {

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

        #region [写AutoUpdaterList]
        void WriterAUPackage()
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
                        File.Copy(path, destpath);
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
                AU.Monitor.Server.ServerBootstrap.Send(AU.Common.CommandType.AUVERSION, Newtonsoft.Json.JsonConvert.SerializeObject(this.AuPublishs));
                MessageBox.Show(this, "自动更新文件生成成功:" + mesg, "AutoUpdater", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (System.IO.Directory.Exists(PackageTempPath))
                    System.IO.Directory.Delete(PackageTempPath, true);
            });

            #endregion [Notification]

            #endregion [WriterAUPackage]
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
            this.BaseUpdatePath = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["UpdateUrl"] ?? "", cbSubSystem.SelectedValue.ToString()) + "/";
            this.txtUrl.Text = this.BaseUpdatePath;
            this.SubSystem = cbSubSystem.SelectedValue.ToString();
        }

        private void tbVersion_TextChanged(object sender, EventArgs e)
        {
            System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(@"\d+.\d.+\d.+\d", System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace);

            if (rg.IsMatch(tbVersion.Text))
            {
                this.PublishVersion = tbVersion.Text.Trim();
            }
            else
            {
                MessageBox.Show("输入版本号格式不正确，请重新输入！");
            }
        }
        private void Ms_NewSessionConnected(AU.Monitor.Server.MonitorSession session)
        {
            AU.Monitor.Server.ServerBootstrap.Send(AU.Common.CommandType.AUVERSION, Newtonsoft.Json.JsonConvert.SerializeObject(this.AuPublishs));
            Console.WriteLine("New Connected\tID=[" + session.SessionID + "]\tIP=" + session.RemoteEndPoint.ToString());

        }
        private static void Ms_SessionClosed(AU.Monitor.Server.MonitorSession session, SuperSocket.SocketBase.CloseReason value)
        {
            Console.WriteLine("Session Closed\tID=[" + session.SessionID + "]\tIP=" + session.RemoteEndPoint.ToString() + "\tReason=" + value);
        }
        private static void Ms_NewRequestReceived(AU.Monitor.Server.MonitorSession session, SuperSocket.SocketBase.Protocol.StringRequestInfo requestInfo)
        {
            Console.WriteLine("Session Message\tID=[" + session.SessionID + "]\tIP=" + session.RemoteEndPoint.ToString() + "\tKey=" + requestInfo.Key + "\tMessage=" + requestInfo.Body);
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (cmbCmd.SelectedItem == null)
            {
                if (string.IsNullOrEmpty(tbMsg.Text))
                {
                    AU.Monitor.Server.ServerBootstrap.Send(AU.Common.CommandType.AUVERSION, Newtonsoft.Json.JsonConvert.SerializeObject(this.AuPublishs));
                }
            }
            else
            {
                if (string.IsNullOrEmpty(tbMsg.Text))
                {
                    MessageBox.Show("请填写发送指令信息");
                    return;
                }
                else
                {
                    AU.Monitor.Server.CommandPackage cp = new AU.Monitor.Server.CommandPackage()
                    {
                        Key = tbKey.Text.Trim(),
                        Body = tbMsg.Text,
                        Parameters = tbParameter.Text.Split(','),
                        Attachment = "",
                    };

                    AU.Monitor.Server.ServerBootstrap.Send(cmbCmd.SelectedItem.ToString(), Newtonsoft.Json.JsonConvert.SerializeObject(cp));
                    return;
                }
            }
            //原始文本
            AU.Monitor.Server.ServerBootstrap.Send(tbMsg.Text);
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
                tbContent.Visible = true;
                tbContent.Text = lbLog.Items[lbLog.SelectedIndex].ToString().Replace("\t", "\r\n");
            }
        }
    }
}
