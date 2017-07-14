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
        private int AvailableUpdate = -1;
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
        /// 主窗体是否显示
        /// </summary>
        public bool IsShow
        {
            get
            {
                bool show = this.WindowState != FormWindowState.Minimized && this.Visible;

                return show;
            }
        }

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
        /// 静默更新
        /// </summary>
        /// <param name="path"></param>
        /// <param name="subsystem"></param>
        /// <param name="systemPath"></param>
        /// <returns></returns>
        private bool DoUpgrade(string path, string subsystem, string systemPath)
        {
            try
            {
                this.AvailableUpdate = Check(path, subsystem, systemPath);
                if (AvailableUpdate > 0)
                {
                    iisOperate(subsystem, false);
                    //升级
                    this.auUpdater.Upgrade(htUpdateFile);
                    this.MainAppRun();
                    this.AvailableUpdate = 0;

                    return true;
                }
                else
                {
                    this.MainAppRun();
                    //删除临时目录
                    if (string.IsNullOrWhiteSpace(path) && System.IO.Directory.Exists(AppConfig.GetUpdateTempPath(subsystem)))
                        System.IO.Directory.Delete(AppConfig.GetUpdateTempPath(subsystem), true);
                }
            }
            catch
            {
                //log
                MessageBox.Show("与服务器连接失败,操作超时!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                iisOperate(subsystem, true);
            }

            return false;
        }

        private int Check(string path, string subsystem, string systemPath)
        {
            string updateTempPath = AppConfig.GetUpdateTempPath(subsystem);
            //更新
            auUpdater = new AppUpdater(systemPath, updateTempPath, AppConfig.GetAuBackupPath(subsystem), systemPath, subsystem);
            if (auUpdater.UpdateAuPackage.LocalAuList == null && System.IO.File.Exists(path))
            {
                //解压一个文件
                string temp = AU.Common.Utility.ZipUtility.DecompressFile(path, AuPackage.PackageName, System.Text.Encoding.UTF8);
                AuList a = Newtonsoft.Json.JsonConvert.DeserializeObject<AuList>(temp);
                auUpdater.UpdateAuPackage.SetPackage(a, subsystem);
            }
            auUpdater.Notify += Au_Notify;
            int up = auUpdater.CheckForUpdate(subsystem, out htUpdateFile);
            if (up > 0)
            {
                //解压临时包
                if (!System.IO.Directory.Exists(updateTempPath))
                {
                    AU.Common.Utility.ZipUtility.Decompress(path, updateTempPath);
                }
                else
                {
                    //验证是否最新
                }
            }

            return up;
        }
        private void iisOperate(string subsystem, bool start = true)
        {
            try
            {
                if (subsystem == SystemType.coreserver.ToString() || subsystem == SystemType.managerserver.ToString() || subsystem == SystemType.handsetserver.ToString() || subsystem == SystemType.imageserver.ToString())
                {
                    if (start)
                        Process.Start("iisreset", "/start");
                    else
                        Process.Start("iisreset", "/stop");
                }
            }
            catch (Exception e)
            {
                //log
            }
        }
        /// <summary>
        /// 显示更新
        /// </summary>
        public bool ShowUpdate(string path, string subsystem, string systemPath)
        {
            bool result = false;
            try
            {
                if (this.IsShow)
                    return result;

                this.InvalidateControl(false);

                AvailableUpdate = Check(path, subsystem, systemPath);
                if (AvailableUpdate > 0)
                {
                    btnNext.Tag = subsystem;
                    tbUpdateMsg.Text = htUpdateFile.LocalAuList.Description;
                    lvUpdateList.Items.Clear();
                    htUpdateFile.LocalAuList.Files.ForEach(d => lvUpdateList.Items.Add(new ListViewItem(
                        new string[]
                        {
                            d.No,d.Version,"",d.WritePath,d.SHA256
                        })));
                    //有更新
                    this.Text = "【" + SubSystem.Dic[subsystem] + "】自动更新";
                    if (this.WindowState == FormWindowState.Minimized)
                        this.WindowState = FormWindowState.Normal;
                    this.Show();
                    result = true;
                }
                else
                {
                    //删除临时目录
                    if (string.IsNullOrWhiteSpace(path) && System.IO.Directory.Exists(AppConfig.GetUpdateTempPath(subsystem)))
                        System.IO.Directory.Delete(AppConfig.GetUpdateTempPath(subsystem), true);
                }
            }
            catch
            {
                MessageBox.Show("与服务器连接失败,操作超时!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
            }

            return result;
        }
        /// <summary>
        /// 系统加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            auPublishHelp.Start();
            bgw.RunWorkerAsync();
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
                        lbState.Text = e.Message;
                        break;
                    case NotifyType.Normal:
                        lbState.Text = e.Message;
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
                        lbState.Text = e.Message;
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
                iisOperate(btnNext.Tag.ToString(), false);
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
            iisOperate(btnNext.Tag.ToString(), true);
            this.Hide();
            this.MainAppRun();
            this.AvailableUpdate = 0;
            if (!this.bgw.IsBusy)
                bgw.RunWorkerAsync();
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

        private void MainAppRun()
        {
            string path = string.Empty;
            string args = string.Empty;
            if (!IsMainAppRun(out path, out args))
            {
                if (System.IO.File.Exists(path))
                {
                    System.Diagnostics.Process.Start(path, args);
                }
            }
        }
        /// <summary>
        /// 判断主应用程序是否正在运行
        /// </summary>
        /// <returns></returns>
        private bool IsMainAppRun(out string applicationPath, out string args)
        {
            string name = args = applicationPath = string.Empty;

            if (auUpdater.IsUpgrade)
            {
                if (htUpdateFile.LocalAuList.Application.StartType != 1)
                {
                    return true;
                }
                name = htUpdateFile.LocalAuList.Application.EntryPoint.ToLower();
                applicationPath = auUpdater.SystemPath + "\\" + htUpdateFile.LocalAuList.Application.Location + "\\" + htUpdateFile.LocalAuList.Application.EntryPoint;
                args = htUpdateFile.LocalAuList.Application.StartArgs;
            }
            else
            {
                if (auUpdater.TargetAuPackage == null)
                    return true;
                else if (auUpdater.TargetAuPackage.LocalAuList == null)
                {
                    if (htUpdateFile != null && htUpdateFile.LocalAuList != null && htUpdateFile.LocalAuList.Application.StartType != 0)
                    {
                        name = htUpdateFile.LocalAuList.Application.EntryPoint.ToLower();
                        applicationPath = auUpdater.SystemPath + "\\" + htUpdateFile.LocalAuList.Application.Location + "\\" + htUpdateFile.LocalAuList.Application.EntryPoint;
                        args = htUpdateFile.LocalAuList.Application.StartArgs;
                    }
                    return true;
                }
                else if (auUpdater.TargetAuPackage.LocalAuList.Application.StartType != 1)
                {
                    return true;
                }
                else
                {
                    name = auUpdater.TargetAuPackage.LocalAuList.Application.EntryPoint.ToLower();
                    applicationPath = auUpdater.SystemPath + "\\" + auUpdater.TargetAuPackage.LocalAuList.Application.Location + "\\" + auUpdater.TargetAuPackage.LocalAuList.Application.EntryPoint;

                    args = auUpdater.TargetAuPackage.LocalAuList.Application.StartArgs;
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

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            //去通知消息
            while (true)
            {
                if (this.IsShow)
                    return;

                UpgradeMessage um;
                if (!this.auPublishHelp.UpgradeMessageQueue.TryDequeue(out um))
                {
                    foreach (var d in this.auPublishHelp.SubSystemDic)
                    {
                        if (System.IO.Directory.Exists(AppConfig.GetUpdateTempPath(d.Key)) && !this.IsShow)
                        {
                            um = new UpgradeMessage()
                            {
                                UpdatePackFile = "",
                                SubSystem = d.Key,
                                UpgradePath = d.Value
                            };

                            break;
                        }
                    }
                }
                if (um != null)
                {
                    if (AppConfig.Current.AllowUI)
                    {
                        this.BeginInvoke((MethodInvoker)delegate ()
                       {
                           this.ShowUpdate(um.UpdatePackFile, um.SubSystem, um.UpgradePath);
                       });
                    }
                    else
                        this.DoUpgrade(um.UpdatePackFile, um.SubSystem, um.UpgradePath);
                }

                System.Threading.Thread.Sleep(AppConfig.Current.Interval);
            }
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //没有显示UI
            if (!this.IsShow)
            {
                System.Threading.Thread.Sleep(AppConfig.Current.Interval);
                bgw.RunWorkerAsync();
            }
        }
    }
}
