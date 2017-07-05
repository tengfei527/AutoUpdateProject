using AU.Common;
using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AuClient
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 更新文件个数
        /// </summary>
        private int AvailableUpdate = 0;
        /// <summary>
        /// 更新辅助类
        /// </summary>
        private AppUpdater auUpdater = null;
        /// <summary>
        /// 获取更新文件列表
        /// </summary>
        private AuPackage htUpdateFile = null;
        /// <summary>
        /// 发布包信息
        /// </summary>
        AuPublishHelp auPublishHelp = null;
        /// <summary>
        /// 初始化系统参数
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
            this.auPublishHelp = new AuPublishHelp(this);
            linkLabel1.Text = AppConfig.Current.LinkUrl;
        }
        /// <summary>
        ///检查是否显示UI
        /// </summary>
        public void Check()
        {
            if (System.IO.Directory.Exists(AppConfig.Current.UpdateTempPath) && (this.WindowState == FormWindowState.Minimized || !this.Visible))
            {
                this.ShowUpdate("");
            }
        }
        /// <summary>
        /// 显示更新
        /// </summary>
        public void ShowUpdate(string path)
        {
            if (this.WindowState != FormWindowState.Minimized && this.Visible)
                return;
            this.InvalidateControl(false);

            try
            {
                if (System.IO.File.Exists(path) && !System.IO.Directory.Exists(AppConfig.Current.UpdateTempPath))
                {
                    AU.Common.Utility.ZipUtility.Decompress(path, AppConfig.Current.UpdateTempPath);
                }
                else
                {
                    //验证是否最新
                }

                auUpdater = new AppUpdater(AppConfig.Current.SystemPath, AppConfig.Current.UpdateTempPath, AppConfig.Current.AuBackupPath, AppConfig.Current.SystemPath);
                auUpdater.Notify += Au_Notify;
            }
            catch
            {
                MessageBox.Show("配置文件出错!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //与服务器连接,下载更新配置文件
            try
            {
                AvailableUpdate = auUpdater.CheckForUpdate(out htUpdateFile);
                if (AvailableUpdate > 0)
                {
                    tbUpdateMsg.Text = htUpdateFile.LocalAuList.Description;
                    lvUpdateList.Items.Clear();
                    htUpdateFile.LocalAuList.Files.ForEach(d => lvUpdateList.Items.Add(new ListViewItem(
                        new string[]
                        {
                            d.No,d.Version,"",d.WritePath,d.SHA256
                        })));
                    //有更新
                    if (this.WindowState == FormWindowState.Minimized)
                        this.WindowState = FormWindowState.Normal;
                    this.Show();
                }
                else
                {
                    return;
                }
            }
            catch
            {
                MessageBox.Show("与服务器连接失败,操作超时!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private SuperSocket.ClientEngine.EasyClient easyClient = new SuperSocket.ClientEngine.EasyClient();
        /// <summary>
        /// 系统加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            auPublishHelp.Start();
            //Server
            StartResult result = AU.Monitor.Server.ServerBootstrap.Start(Ms_NewSessionConnected);
            Console.WriteLine("Start result: {0}!", result);
            //Client
            easyClient.Initialize(new AU.Monitor.Client.FakeReceiveFilter(System.Text.Encoding.Default), (p =>
            {
                string body = p.Body;
                string key = p.Key;
                string[] par = p.Parameters;
                if (key != body)
                    Console.WriteLine("{0}:{1}", key, body);
                else
                    Console.WriteLine(key);
            }));
            //var ips = AppConfig.Current.SocketServer.Split(':');
            //System.Threading.Tasks.Task<bool> result = easyClient.ConnectAsync(new System.Net.IPEndPoint(ips[0],ips[1]);
            //System.Threading.Tasks.Task.WaitAll(result);
        }
        /// <summary>
        /// 新客户端连接
        /// </summary>
        /// <param name="session"></param>
        private void Ms_NewSessionConnected(AU.Monitor.Server.MonitorSession session)
        {
            session.Send("Welcome to AuClient Socket Server");
        }
        /// <summary>
        /// 消息通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Au_Notify(object sender, NotifyMessage e)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                switch (e.NotifyType)
                {
                    case NotifyType.Error:
                        MessageBox.Show(e.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case NotifyType.StartDown:
                        this.Cursor = Cursors.WaitCursor;
                        break;
                    case NotifyType.Process:
                        pbDownFile.Minimum = 0;
                        lbState.Text = e.Message;
                        pbDownFile.Maximum = Convert.ToInt32(e.Attachment);
                        break;
                    case NotifyType.UpProcess:
                        int v = pbDownFile.Value + Convert.ToInt32(e.Attachment);
                        string[] arg = e.Message.Split(':');
                        lvUpdateList.Items[Convert.ToInt32(arg[0])].SubItems[2].Text = arg[1];
                        pbDownFile.Value = v > pbDownFile.Maximum ? pbDownFile.Maximum : v;
                        break;
                    case NotifyType.StopDown:
                        this.InvalidateControl();
                        this.Cursor = Cursors.Default;
                        break;
                }
            });

        }
        /// <summary>
        /// 执行更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (tabPageMain.SelectedTab == tabPageMsg)
            {
                tabPageMain.TabPages.Clear();
                tabPageMain.TabPages.Add(tabPageList);

                return;
            }
            if (AvailableUpdate > 0)
            {
                Thread threadDown = new Thread(new ParameterizedThreadStart(auUpdater.Upgrade));
                threadDown.IsBackground = true;
                threadDown.Start(htUpdateFile);
            }
            else
            {
                MessageBox.Show("没有可用的更新!", "自动更新", MessageBoxButtons.OK, MessageBoxIcon.Information);

                tabPageMain.TabPages.Clear();
                tabPageMain.TabPages.Add(tabPageSucess);

                return;
            }
        }
        /// <summary>
        /// 取消更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            string path = string.Empty;
            if (!IsMainAppRun(out path))
            {
                if (System.IO.File.Exists(path))
                {
                    System.Diagnostics.Process.Start(path);
                }
            }
        }


        /// <summary>
        /// 执行链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //打开首页
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        /// <summary>
        /// 重新绘制窗体部分控件属性
        /// </summary>
        /// <param name="sucess">sucess 完成=true 下一步=false</param>
        private void InvalidateControl(bool sucess = true)
        {
            if (sucess)
            {
                tabPageMain.TabPages.Clear();
                tabPageMain.TabPages.Add(tabPageSucess);
                btnNext.Visible = false;
                btnCancel.Text = "完成";
            }
            else
            {
                tabPageMain.TabPages.Clear();
                tabPageMain.TabPages.Add(tabPageMsg);
                lbState.Text = "点击“下一步”开始更新文件";
                pbDownFile.Value = 0;
                btnNext.Visible = true;
                btnCancel.Text = "取消";
            }
        }
        /// <summary>
        /// 判断主应用程序是否正在运行
        /// </summary>
        /// <returns></returns>
        private bool IsMainAppRun(out string applicationPath)
        {
            string name = string.Empty;
            applicationPath = string.Empty;

            if (auUpdater.IsUpgrade)
            {
                if (htUpdateFile.LocalAuList.Application.StartType != 1)
                {
                    return true;
                }
                name = htUpdateFile.LocalAuList.Application.EntryPoint.ToLower();
                applicationPath = AppConfig.Current.SystemPath + "\\" + htUpdateFile.LocalAuList.Application.Location + "\\" + htUpdateFile.LocalAuList.Application.EntryPoint;
            }
            else
            {
                if (auUpdater.TargetAuPackage == null || auUpdater.TargetAuPackage.LocalAuList == null || auUpdater.TargetAuPackage.LocalAuList.Application.StartType != 1)
                    return true;
                else
                {
                    name = auUpdater.TargetAuPackage.LocalAuList.Application.EntryPoint.ToLower();
                    applicationPath = auUpdater.TargetAuPackage.LocalAuList.Application.Location + "\\" + auUpdater.TargetAuPackage.LocalAuList.Application.EntryPoint;
                }
            }
            bool isRun = false;
            Process[] allProcess = Process.GetProcesses();
            foreach (Process p in allProcess)
            {
                if (p.ProcessName.ToLower() + ".exe" == name)
                {
                    isRun = true;
                    //break;
                }
            }

            return isRun;
        }
    }
}
