using AU.Common;
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
        private string updateUrl = string.Empty;
        private int availableUpdate = 0;
        /// <summary>
        /// 更新辅助类
        /// </summary>
        private AppUpdater au = null;
        //获取更新文件列表
        private AuPackage htUpdateFile = null;
        /// <summary>
        /// 更新地址
        /// </summary>
        private string UpdatePath = string.Empty;
        /// <summary>
        /// 系统路径
        /// </summary>
        public string SystemPath = string.Empty;
        /// <summary>
        /// 备份路径
        /// </summary>
        public string AuBackupPath = string.Empty;
        /// <summary>
        /// 发布包信息
        /// </summary>
        AuPublishHelp aph = null;
        public Point DefaultPanelPoint;
        public Size DefaultPanelSize;
        public Point DefaultBtnPoint;
        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
            this.aph = new AuPublishHelp(this);
            this.UpdatePath = System.IO.Path.Combine(Application.StartupPath, this.aph.SubSystem + "\\autemp\\");
            this.AuBackupPath = System.IO.Path.Combine(Application.StartupPath, this.aph.SubSystem + "\\aubackup\\");
            this.SystemPath = System.IO.Path.Combine(Application.StartupPath, System.Configuration.ConfigurationManager.AppSettings["SystemPath"]);
        }

        public void Check()
        {
            if (System.IO.Directory.Exists(this.UpdatePath) && (this.WindowState == FormWindowState.Minimized || !this.Visible))
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
                if (System.IO.File.Exists(path) && !System.IO.Directory.Exists(this.UpdatePath))
                {
                    AU.Common.Utility.ZipUtility.Decompress(path, this.UpdatePath);
                }
                else
                {
                    //验证是否最新
                }

                au = new AppUpdater(this.SystemPath, this.UpdatePath, this.AuBackupPath);
                au.Notify += Au_Notify;
            }
            catch
            {
                MessageBox.Show("配置文件出错!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //与服务器连接,下载更新配置文件
            try
            {
                availableUpdate = au.CheckForUpdate(out htUpdateFile);
                if (availableUpdate > 0)
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
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.btnFinish.Visible = false;
            this.DefaultPanelPoint = this.panel1.Location;
            this.DefaultPanelSize = this.panel1.Size;
            this.DefaultBtnPoint = this.btnCancel.Location;
            aph.Start();
        }

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

        private void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                if (au.IsUpgrade)
                {
                    AU.Common.Utility.ToolsHelp.CopyFile(htUpdateFile.LocalPath, SystemPath);
                    System.IO.Directory.Delete(htUpdateFile.LocalPath, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            if (!IsMainAppRun())
            {
                string path = SystemPath + "\\" + htUpdateFile.LocalAuList.Application.Location + "\\" + htUpdateFile.LocalAuList.Application.EntryPoint;
                if (System.IO.File.Exists(path))
                {
                    System.Diagnostics.Process.Start(path);
                }
            }
            this.Hide();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (tabPageMain.SelectedTab == tabPageMsg)
            {
                tabPageMain.TabPages.Clear();
                tabPageMain.TabPages.Add(tabPageList);

                return;
            }
            if (availableUpdate > 0)
            {
                Thread threadDown = new Thread(new ParameterizedThreadStart(au.Upgrade));
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (!IsMainAppRun())
            {
                string path = au.TargetAuPackage.LocalAuList.Application.Location + "\\" + au.TargetAuPackage.LocalAuList.Application.EntryPoint;
                if (System.IO.File.Exists(path))
                {
                    System.Diagnostics.Process.Start(path);
                }
            }

        }



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
                btnCancel.Visible = false;
                btnFinish.Location = this.DefaultBtnPoint;
                btnFinish.Visible = true;
            }
            else
            {
                tabPageMain.TabPages.Clear();
                tabPageMain.TabPages.Add(tabPageMsg);
                lbState.Text = "点击“下一步”开始更新文件";
                pbDownFile.Value = 0;
                btnNext.Visible = true;
                btnCancel.Visible = true;
                btnCancel.Location = this.DefaultBtnPoint;
                btnFinish.Visible = false;
            }
        }
        /// <summary>
        /// 判断主应用程序是否正在运行
        /// </summary>
        /// <returns></returns>
        private bool IsMainAppRun()
        {
            string name = string.Empty;
            if (au.IsUpgrade)
            {
                if (htUpdateFile.LocalAuList.Application.StartType != 1)
                {
                    return true;
                }
                name = htUpdateFile.LocalAuList.Application.EntryPoint.ToLower();
            }
            else
            {
                if (au.TargetAuPackage == null || au.TargetAuPackage.LocalAuList == null || au.TargetAuPackage.LocalAuList.Application.StartType != 1)
                    return true;
                else
                    name = au.TargetAuPackage.LocalAuList.Application.EntryPoint.ToLower();
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
