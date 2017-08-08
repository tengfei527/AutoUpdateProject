namespace AuWriter
{
    partial class AuWriterForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuWriterForm));
            this.ofdExpt = new System.Windows.Forms.OpenFileDialog();
            this.ofdSrc = new System.Windows.Forms.OpenFileDialog();
            this.tbpOpt = new System.Windows.Forms.TabPage();
            this.sfdDest = new System.Windows.Forms.SaveFileDialog();
            this.cbPackage = new System.Windows.Forms.CheckBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbpBase = new System.Windows.Forms.TabPage();
            this.panelAuclient = new System.Windows.Forms.Panel();
            this.btnAuClientPub = new System.Windows.Forms.Button();
            this.btnbtnAuClientSelect = new System.Windows.Forms.Button();
            this.tbVersion = new System.Windows.Forms.TextBox();
            this.gbPublish = new System.Windows.Forms.GroupBox();
            this.txtExpt = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.cbFilter = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnExpt = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDir = new System.Windows.Forms.Button();
            this.prbProd = new System.Windows.Forms.ProgressBar();
            this.btnSrc = new System.Windows.Forms.Button();
            this.tbUpdateMsg = new System.Windows.Forms.TextBox();
            this.btnDest = new System.Windows.Forms.Button();
            this.txtDest = new System.Windows.Forms.TextBox();
            this.btnProduce = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.cbSubSystem = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSrc = new System.Windows.Forms.TextBox();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tbpControl = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvTerminal = new System.Windows.Forms.TreeView();
            this.tabControlCmd = new System.Windows.Forms.TabControl();
            this.tbpCmd = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tbMsg = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbParameter = new System.Windows.Forms.TextBox();
            this.cmbCmdType = new System.Windows.Forms.ComboBox();
            this.cmbCmd = new System.Windows.Forms.ComboBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tabControlLog = new System.Windows.Forms.TabControl();
            this.tbpLog = new System.Windows.Forms.TabPage();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.tbpLogSelect = new System.Windows.Forms.TabPage();
            this.tbpContent = new System.Windows.Forms.TextBox();
            this.tbpTerminal = new System.Windows.Forms.TabPage();
            this.rtbTerminial = new System.Windows.Forms.RichTextBox();
            this.tbpRes = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lvRemoteDisk = new System.Windows.Forms.ListView();
            this.iml_ExplorerImages = new System.Windows.Forms.ImageList(this.components);
            this.gbRemoteDisk = new System.Windows.Forms.GroupBox();
            this.panelUpload = new System.Windows.Forms.Panel();
            this.btnUploadCancle = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lbUpload = new System.Windows.Forms.Label();
            this.pgbUpLoad = new System.Windows.Forms.ProgressBar();
            this.txt_remoteexplorer = new System.Windows.Forms.TextBox();
            this.lvLocalDisk = new System.Windows.Forms.ListView();
            this.gbLocalDisk = new System.Windows.Forms.GroupBox();
            this.panelDownload = new System.Windows.Forms.Panel();
            this.btnDownloadCancle = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.lbDownload = new System.Windows.Forms.Label();
            this.pgbDownload = new System.Windows.Forms.ProgressBar();
            this.txt_myexplorer = new System.Windows.Forms.TextBox();
            this.fbdSrc = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.启动服务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止服务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.资源列表显示方式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbl_Display = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmRemoteResource = new System.Windows.Forms.ToolStripMenuItem();
            this.断开连接ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_Disk = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DownloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BrowseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbpBase.SuspendLayout();
            this.panelAuclient.SuspendLayout();
            this.gbPublish.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tbpControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlCmd.SuspendLayout();
            this.tbpCmd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControlLog.SuspendLayout();
            this.tbpLog.SuspendLayout();
            this.tbpLogSelect.SuspendLayout();
            this.tbpTerminal.SuspendLayout();
            this.tbpRes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.gbRemoteDisk.SuspendLayout();
            this.panelUpload.SuspendLayout();
            this.gbLocalDisk.SuspendLayout();
            this.panelDownload.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.cms_Disk.SuspendLayout();
            this.SuspendLayout();
            // 
            // ofdExpt
            // 
            this.ofdExpt.DefaultExt = "*.*";
            this.ofdExpt.Filter = "所有文件(*.*)|*.*";
            this.ofdExpt.Multiselect = true;
            this.ofdExpt.Title = "请选择主程序文件";
            this.ofdExpt.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdExpt_FileOk);
            // 
            // ofdSrc
            // 
            this.ofdSrc.DefaultExt = "*.exe";
            this.ofdSrc.Filter = "程序文件(*.exe)|*.exe|所有文件(*.*)|*.*";
            this.ofdSrc.Title = "请选择主程序文件";
            this.ofdSrc.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdSrc_FileOk);
            // 
            // tbpOpt
            // 
            this.tbpOpt.Location = new System.Drawing.Point(4, 22);
            this.tbpOpt.Name = "tbpOpt";
            this.tbpOpt.Padding = new System.Windows.Forms.Padding(3);
            this.tbpOpt.Size = new System.Drawing.Size(974, 497);
            this.tbpOpt.TabIndex = 1;
            this.tbpOpt.Text = "※选项";
            this.tbpOpt.UseVisualStyleBackColor = true;
            // 
            // sfdDest
            // 
            this.sfdDest.CheckPathExists = false;
            this.sfdDest.DefaultExt = "*.xml";
            this.sfdDest.FileName = "aupackage.json";
            this.sfdDest.Filter = "json文件(*.json)|*.json";
            this.sfdDest.Title = "请选择aupackage保存位置";
            this.sfdDest.FileOk += new System.ComponentModel.CancelEventHandler(this.sfdDest_FileOk);
            // 
            // cbPackage
            // 
            this.cbPackage.AutoSize = true;
            this.cbPackage.Location = new System.Drawing.Point(502, 199);
            this.cbPackage.Name = "cbPackage";
            this.cbPackage.Size = new System.Drawing.Size(48, 16);
            this.cbPackage.TabIndex = 11;
            this.cbPackage.Text = "打包";
            this.cbPackage.UseVisualStyleBackColor = true;
            this.cbPackage.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(98, 14);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.ReadOnly = true;
            this.txtUrl.Size = new System.Drawing.Size(513, 21);
            this.txtUrl.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "更新网址:";
            // 
            // tbpBase
            // 
            this.tbpBase.Controls.Add(this.panelAuclient);
            this.tbpBase.Controls.Add(this.tbVersion);
            this.tbpBase.Controls.Add(this.gbPublish);
            this.tbpBase.Controls.Add(this.panel2);
            this.tbpBase.Controls.Add(this.label6);
            this.tbpBase.Controls.Add(this.cbSubSystem);
            this.tbpBase.Controls.Add(this.txtUrl);
            this.tbpBase.Controls.Add(this.label5);
            this.tbpBase.Controls.Add(this.label2);
            this.tbpBase.Controls.Add(this.txtSrc);
            this.tbpBase.Location = new System.Drawing.Point(4, 22);
            this.tbpBase.Name = "tbpBase";
            this.tbpBase.Padding = new System.Windows.Forms.Padding(3);
            this.tbpBase.Size = new System.Drawing.Size(974, 497);
            this.tbpBase.TabIndex = 0;
            this.tbpBase.Text = "※基本信息";
            this.tbpBase.UseVisualStyleBackColor = true;
            // 
            // panelAuclient
            // 
            this.panelAuclient.Controls.Add(this.btnAuClientPub);
            this.panelAuclient.Controls.Add(this.btnbtnAuClientSelect);
            this.panelAuclient.Location = new System.Drawing.Point(438, 38);
            this.panelAuclient.Name = "panelAuclient";
            this.panelAuclient.Size = new System.Drawing.Size(173, 28);
            this.panelAuclient.TabIndex = 18;
            this.panelAuclient.Visible = false;
            // 
            // btnAuClientPub
            // 
            this.btnAuClientPub.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAuClientPub.Location = new System.Drawing.Point(90, 3);
            this.btnAuClientPub.Name = "btnAuClientPub";
            this.btnAuClientPub.Size = new System.Drawing.Size(79, 21);
            this.btnAuClientPub.TabIndex = 17;
            this.btnAuClientPub.Text = "发布";
            this.btnAuClientPub.UseVisualStyleBackColor = true;
            this.btnAuClientPub.Click += new System.EventHandler(this.btnAuClientPub_Click);
            // 
            // btnbtnAuClientSelect
            // 
            this.btnbtnAuClientSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnbtnAuClientSelect.Location = new System.Drawing.Point(6, 3);
            this.btnbtnAuClientSelect.Name = "btnbtnAuClientSelect";
            this.btnbtnAuClientSelect.Size = new System.Drawing.Size(79, 21);
            this.btnbtnAuClientSelect.TabIndex = 17;
            this.btnbtnAuClientSelect.Text = "选择程序(&S)";
            this.btnbtnAuClientSelect.UseVisualStyleBackColor = true;
            this.btnbtnAuClientSelect.Click += new System.EventHandler(this.btnbtnAuClientSelect_Click);
            // 
            // tbVersion
            // 
            this.tbVersion.Location = new System.Drawing.Point(439, 71);
            this.tbVersion.Name = "tbVersion";
            this.tbVersion.Size = new System.Drawing.Size(172, 21);
            this.tbVersion.TabIndex = 6;
            // 
            // gbPublish
            // 
            this.gbPublish.Controls.Add(this.txtExpt);
            this.gbPublish.Controls.Add(this.btnExit);
            this.gbPublish.Controls.Add(this.cbFilter);
            this.gbPublish.Controls.Add(this.cbPackage);
            this.gbPublish.Controls.Add(this.label3);
            this.gbPublish.Controls.Add(this.btnExpt);
            this.gbPublish.Controls.Add(this.label4);
            this.gbPublish.Controls.Add(this.btnDir);
            this.gbPublish.Controls.Add(this.prbProd);
            this.gbPublish.Controls.Add(this.btnSrc);
            this.gbPublish.Controls.Add(this.tbUpdateMsg);
            this.gbPublish.Controls.Add(this.btnDest);
            this.gbPublish.Controls.Add(this.txtDest);
            this.gbPublish.Controls.Add(this.btnProduce);
            this.gbPublish.Location = new System.Drawing.Point(3, 95);
            this.gbPublish.Name = "gbPublish";
            this.gbPublish.Size = new System.Drawing.Size(832, 399);
            this.gbPublish.TabIndex = 16;
            this.gbPublish.TabStop = false;
            this.gbPublish.Visible = false;
            // 
            // txtExpt
            // 
            this.txtExpt.Location = new System.Drawing.Point(95, 67);
            this.txtExpt.Multiline = true;
            this.txtExpt.Name = "txtExpt";
            this.txtExpt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExpt.Size = new System.Drawing.Size(455, 124);
            this.txtExpt.TabIndex = 8;
            // 
            // btnExit
            // 
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(544, 368);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(64, 23);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "退出(&X)";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cbFilter
            // 
            this.cbFilter.AutoSize = true;
            this.cbFilter.Checked = true;
            this.cbFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFilter.Location = new System.Drawing.Point(95, 45);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(300, 16);
            this.cbFilter.TabIndex = 7;
            this.cbFilter.Text = "过滤文件(*.log,*.config,*.db,*.dat,unins000.*)";
            this.cbFilter.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "保存位置:";
            // 
            // btnExpt
            // 
            this.btnExpt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExpt.Location = new System.Drawing.Point(556, 67);
            this.btnExpt.Name = "btnExpt";
            this.btnExpt.Size = new System.Drawing.Size(53, 21);
            this.btnExpt.TabIndex = 9;
            this.btnExpt.Text = "选择(&S)";
            this.btnExpt.UseVisualStyleBackColor = true;
            this.btnExpt.Click += new System.EventHandler(this.btnExpt_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "排除文件:";
            // 
            // btnDir
            // 
            this.btnDir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDir.Location = new System.Drawing.Point(195, 14);
            this.btnDir.Name = "btnDir";
            this.btnDir.Size = new System.Drawing.Size(79, 21);
            this.btnDir.TabIndex = 4;
            this.btnDir.Text = "选择目录(&D)";
            this.btnDir.UseVisualStyleBackColor = true;
            this.btnDir.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // prbProd
            // 
            this.prbProd.Location = new System.Drawing.Point(94, 368);
            this.prbProd.Name = "prbProd";
            this.prbProd.Size = new System.Drawing.Size(370, 23);
            this.prbProd.TabIndex = 2;
            this.prbProd.Visible = false;
            // 
            // btnSrc
            // 
            this.btnSrc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSrc.Location = new System.Drawing.Point(95, 14);
            this.btnSrc.Name = "btnSrc";
            this.btnSrc.Size = new System.Drawing.Size(79, 21);
            this.btnSrc.TabIndex = 3;
            this.btnSrc.Text = "选择程序(&S)";
            this.btnSrc.UseVisualStyleBackColor = true;
            this.btnSrc.Click += new System.EventHandler(this.btnSrc_Click);
            // 
            // tbUpdateMsg
            // 
            this.tbUpdateMsg.Location = new System.Drawing.Point(94, 219);
            this.tbUpdateMsg.Multiline = true;
            this.tbUpdateMsg.Name = "tbUpdateMsg";
            this.tbUpdateMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbUpdateMsg.Size = new System.Drawing.Size(514, 143);
            this.tbUpdateMsg.TabIndex = 13;
            this.tbUpdateMsg.Text = "更新说明：";
            // 
            // btnDest
            // 
            this.btnDest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDest.Location = new System.Drawing.Point(556, 196);
            this.btnDest.Name = "btnDest";
            this.btnDest.Size = new System.Drawing.Size(52, 21);
            this.btnDest.TabIndex = 12;
            this.btnDest.Text = "选择(&S)";
            this.btnDest.UseVisualStyleBackColor = true;
            this.btnDest.Click += new System.EventHandler(this.btnDest_Click);
            // 
            // txtDest
            // 
            this.txtDest.Location = new System.Drawing.Point(94, 192);
            this.txtDest.Name = "txtDest";
            this.txtDest.Size = new System.Drawing.Size(402, 21);
            this.txtDest.TabIndex = 10;
            // 
            // btnProduce
            // 
            this.btnProduce.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnProduce.Location = new System.Drawing.Point(478, 368);
            this.btnProduce.Name = "btnProduce";
            this.btnProduce.Size = new System.Drawing.Size(60, 23);
            this.btnProduce.TabIndex = 14;
            this.btnProduce.Text = "生成(&G)";
            this.btnProduce.UseVisualStyleBackColor = true;
            this.btnProduce.Click += new System.EventHandler(this.btnProduce_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 455);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(968, 39);
            this.panel2.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "升级类型:";
            // 
            // cbSubSystem
            // 
            this.cbSubSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubSystem.FormattingEnabled = true;
            this.cbSubSystem.Location = new System.Drawing.Point(98, 42);
            this.cbSubSystem.Name = "cbSubSystem";
            this.cbSubSystem.Size = new System.Drawing.Size(336, 20);
            this.cbSubSystem.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "主程序:";
            // 
            // txtSrc
            // 
            this.txtSrc.Location = new System.Drawing.Point(97, 71);
            this.txtSrc.Name = "txtSrc";
            this.txtSrc.Size = new System.Drawing.Size(336, 21);
            this.txtSrc.TabIndex = 5;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tbpBase);
            this.tabControlMain.Controls.Add(this.tbpOpt);
            this.tabControlMain.Controls.Add(this.tbpControl);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 25);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(982, 523);
            this.tabControlMain.TabIndex = 0;
            // 
            // tbpControl
            // 
            this.tbpControl.Controls.Add(this.splitContainer1);
            this.tbpControl.Location = new System.Drawing.Point(4, 22);
            this.tbpControl.Name = "tbpControl";
            this.tbpControl.Padding = new System.Windows.Forms.Padding(3);
            this.tbpControl.Size = new System.Drawing.Size(974, 497);
            this.tbpControl.TabIndex = 2;
            this.tbpControl.Text = "※控制";
            this.tbpControl.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvTerminal);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControlCmd);
            this.splitContainer1.Size = new System.Drawing.Size(968, 491);
            this.splitContainer1.SplitterDistance = 172;
            this.splitContainer1.TabIndex = 9;
            // 
            // tvTerminal
            // 
            this.tvTerminal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTerminal.HideSelection = false;
            this.tvTerminal.HotTracking = true;
            this.tvTerminal.Location = new System.Drawing.Point(0, 0);
            this.tvTerminal.Name = "tvTerminal";
            this.tvTerminal.ShowNodeToolTips = true;
            this.tvTerminal.Size = new System.Drawing.Size(172, 491);
            this.tvTerminal.TabIndex = 0;
            this.tvTerminal.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvTerminal_NodeMouseClick);
            // 
            // tabControlCmd
            // 
            this.tabControlCmd.Controls.Add(this.tbpCmd);
            this.tabControlCmd.Controls.Add(this.tbpRes);
            this.tabControlCmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlCmd.Location = new System.Drawing.Point(0, 0);
            this.tabControlCmd.Name = "tabControlCmd";
            this.tabControlCmd.SelectedIndex = 0;
            this.tabControlCmd.Size = new System.Drawing.Size(792, 491);
            this.tabControlCmd.TabIndex = 2;
            // 
            // tbpCmd
            // 
            this.tbpCmd.Controls.Add(this.splitContainer2);
            this.tbpCmd.Location = new System.Drawing.Point(4, 22);
            this.tbpCmd.Name = "tbpCmd";
            this.tbpCmd.Padding = new System.Windows.Forms.Padding(3);
            this.tbpCmd.Size = new System.Drawing.Size(784, 465);
            this.tbpCmd.TabIndex = 0;
            this.tbpCmd.Text = "命令交互";
            this.tbpCmd.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tbMsg);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControlLog);
            this.splitContainer2.Size = new System.Drawing.Size(778, 459);
            this.splitContainer2.SplitterDistance = 134;
            this.splitContainer2.TabIndex = 1;
            // 
            // tbMsg
            // 
            this.tbMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMsg.Location = new System.Drawing.Point(0, 64);
            this.tbMsg.Multiline = true;
            this.tbMsg.Name = "tbMsg";
            this.tbMsg.Size = new System.Drawing.Size(778, 70);
            this.tbMsg.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbParameter);
            this.groupBox2.Controls.Add(this.cmbCmdType);
            this.groupBox2.Controls.Add(this.cmbCmd);
            this.groupBox2.Controls.Add(this.btnSend);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(778, 64);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(328, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 16);
            this.label7.TabIndex = 7;
            this.label7.Text = "参数:以\",\"分割";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(165, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "类别:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "指令:";
            // 
            // tbParameter
            // 
            this.tbParameter.Location = new System.Drawing.Point(447, 12);
            this.tbParameter.Multiline = true;
            this.tbParameter.Name = "tbParameter";
            this.tbParameter.Size = new System.Drawing.Size(206, 30);
            this.tbParameter.TabIndex = 4;
            // 
            // cmbCmdType
            // 
            this.cmbCmdType.FormattingEnabled = true;
            this.cmbCmdType.Location = new System.Drawing.Point(201, 12);
            this.cmbCmdType.Name = "cmbCmdType";
            this.cmbCmdType.Size = new System.Drawing.Size(121, 20);
            this.cmbCmdType.TabIndex = 1;
            // 
            // cmbCmd
            // 
            this.cmbCmd.FormattingEnabled = true;
            this.cmbCmd.Items.AddRange(new object[] {
            "AUVERSION",
            "TRANSFER",
            "TRANSFERONE",
            "TERMINAL",
            "RESOURCE",
            "SCRIPT"});
            this.cmbCmd.Location = new System.Drawing.Point(38, 13);
            this.cmbCmd.Name = "cmbCmd";
            this.cmbCmd.Size = new System.Drawing.Size(121, 20);
            this.cmbCmd.TabIndex = 1;
            this.cmbCmd.SelectedIndexChanged += new System.EventHandler(this.cmbCmd_SelectedIndexChanged);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(237, 38);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(85, 20);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tabControlLog
            // 
            this.tabControlLog.Controls.Add(this.tbpLog);
            this.tabControlLog.Controls.Add(this.tbpLogSelect);
            this.tabControlLog.Controls.Add(this.tbpTerminal);
            this.tabControlLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlLog.Location = new System.Drawing.Point(0, 0);
            this.tabControlLog.Name = "tabControlLog";
            this.tabControlLog.SelectedIndex = 0;
            this.tabControlLog.Size = new System.Drawing.Size(778, 321);
            this.tabControlLog.TabIndex = 8;
            // 
            // tbpLog
            // 
            this.tbpLog.Controls.Add(this.lbLog);
            this.tbpLog.Location = new System.Drawing.Point(4, 22);
            this.tbpLog.Name = "tbpLog";
            this.tbpLog.Padding = new System.Windows.Forms.Padding(3);
            this.tbpLog.Size = new System.Drawing.Size(770, 295);
            this.tbpLog.TabIndex = 0;
            this.tbpLog.Text = "日志";
            this.tbpLog.UseVisualStyleBackColor = true;
            // 
            // lbLog
            // 
            this.lbLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLog.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lbLog.FormattingEnabled = true;
            this.lbLog.HorizontalScrollbar = true;
            this.lbLog.ItemHeight = 12;
            this.lbLog.Location = new System.Drawing.Point(3, 3);
            this.lbLog.Name = "lbLog";
            this.lbLog.ScrollAlwaysVisible = true;
            this.lbLog.Size = new System.Drawing.Size(764, 289);
            this.lbLog.TabIndex = 6;
            this.lbLog.SelectedIndexChanged += new System.EventHandler(this.lbLog_SelectedIndexChanged);
            this.lbLog.DoubleClick += new System.EventHandler(this.lbLog_DoubleClick);
            // 
            // tbpLogSelect
            // 
            this.tbpLogSelect.Controls.Add(this.tbpContent);
            this.tbpLogSelect.Location = new System.Drawing.Point(4, 22);
            this.tbpLogSelect.Name = "tbpLogSelect";
            this.tbpLogSelect.Padding = new System.Windows.Forms.Padding(3);
            this.tbpLogSelect.Size = new System.Drawing.Size(770, 295);
            this.tbpLogSelect.TabIndex = 1;
            this.tbpLogSelect.Text = "选择";
            this.tbpLogSelect.UseVisualStyleBackColor = true;
            // 
            // tbpContent
            // 
            this.tbpContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbpContent.Location = new System.Drawing.Point(3, 3);
            this.tbpContent.Multiline = true;
            this.tbpContent.Name = "tbpContent";
            this.tbpContent.Size = new System.Drawing.Size(764, 289);
            this.tbpContent.TabIndex = 7;
            this.tbpContent.Visible = false;
            // 
            // tbpTerminal
            // 
            this.tbpTerminal.Controls.Add(this.rtbTerminial);
            this.tbpTerminal.Location = new System.Drawing.Point(4, 22);
            this.tbpTerminal.Name = "tbpTerminal";
            this.tbpTerminal.Padding = new System.Windows.Forms.Padding(3);
            this.tbpTerminal.Size = new System.Drawing.Size(770, 295);
            this.tbpTerminal.TabIndex = 2;
            this.tbpTerminal.Text = "终端消息";
            this.tbpTerminal.UseVisualStyleBackColor = true;
            // 
            // rtbTerminial
            // 
            this.rtbTerminial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbTerminial.Location = new System.Drawing.Point(3, 3);
            this.rtbTerminial.Name = "rtbTerminial";
            this.rtbTerminial.Size = new System.Drawing.Size(764, 289);
            this.rtbTerminial.TabIndex = 1;
            this.rtbTerminial.Text = "";
            this.rtbTerminial.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rtbTerminial_MouseDown);
            // 
            // tbpRes
            // 
            this.tbpRes.Controls.Add(this.splitContainer3);
            this.tbpRes.Location = new System.Drawing.Point(4, 22);
            this.tbpRes.Name = "tbpRes";
            this.tbpRes.Padding = new System.Windows.Forms.Padding(3);
            this.tbpRes.Size = new System.Drawing.Size(784, 465);
            this.tbpRes.TabIndex = 1;
            this.tbpRes.Text = "资源交互";
            this.tbpRes.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lvRemoteDisk);
            this.splitContainer3.Panel1.Controls.Add(this.gbRemoteDisk);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.lvLocalDisk);
            this.splitContainer3.Panel2.Controls.Add(this.gbLocalDisk);
            this.splitContainer3.Size = new System.Drawing.Size(778, 459);
            this.splitContainer3.SplitterDistance = 380;
            this.splitContainer3.TabIndex = 0;
            // 
            // lvRemoteDisk
            // 
            this.lvRemoteDisk.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderRName,
            this.columnHeaderRDate,
            this.columnHeaderRType,
            this.columnHeaderRSize});
            this.lvRemoteDisk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvRemoteDisk.LargeImageList = this.iml_ExplorerImages;
            this.lvRemoteDisk.Location = new System.Drawing.Point(0, 78);
            this.lvRemoteDisk.MultiSelect = false;
            this.lvRemoteDisk.Name = "lvRemoteDisk";
            this.lvRemoteDisk.Size = new System.Drawing.Size(380, 381);
            this.lvRemoteDisk.SmallImageList = this.iml_ExplorerImages;
            this.lvRemoteDisk.StateImageList = this.iml_ExplorerImages;
            this.lvRemoteDisk.TabIndex = 2;
            this.lvRemoteDisk.UseCompatibleStateImageBehavior = false;
            this.lvRemoteDisk.SelectedIndexChanged += new System.EventHandler(this.lvRemoteDisk_SelectedIndexChanged);
            this.lvRemoteDisk.DoubleClick += new System.EventHandler(this.lvRemoteDisk_DoubleClick);
            this.lvRemoteDisk.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvRemoteDisk_MouseDown);
            // 
            // iml_ExplorerImages
            // 
            this.iml_ExplorerImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_ExplorerImages.ImageStream")));
            this.iml_ExplorerImages.TransparentColor = System.Drawing.Color.Transparent;
            this.iml_ExplorerImages.Images.SetKeyName(0, "Disk");
            this.iml_ExplorerImages.Images.SetKeyName(1, "Directory");
            this.iml_ExplorerImages.Images.SetKeyName(2, "midi");
            this.iml_ExplorerImages.Images.SetKeyName(3, "txt");
            this.iml_ExplorerImages.Images.SetKeyName(4, "css");
            this.iml_ExplorerImages.Images.SetKeyName(5, "html");
            this.iml_ExplorerImages.Images.SetKeyName(6, "jpg");
            this.iml_ExplorerImages.Images.SetKeyName(7, "asa");
            this.iml_ExplorerImages.Images.SetKeyName(8, "asax");
            this.iml_ExplorerImages.Images.SetKeyName(9, "asp");
            this.iml_ExplorerImages.Images.SetKeyName(10, "aspx");
            this.iml_ExplorerImages.Images.SetKeyName(11, "avi");
            this.iml_ExplorerImages.Images.SetKeyName(12, "bat");
            this.iml_ExplorerImages.Images.SetKeyName(13, "bmp");
            this.iml_ExplorerImages.Images.SetKeyName(14, "c");
            this.iml_ExplorerImages.Images.SetKeyName(15, "doc");
            this.iml_ExplorerImages.Images.SetKeyName(16, "chm");
            this.iml_ExplorerImages.Images.SetKeyName(17, "class");
            this.iml_ExplorerImages.Images.SetKeyName(18, "config");
            this.iml_ExplorerImages.Images.SetKeyName(19, "cpp");
            this.iml_ExplorerImages.Images.SetKeyName(20, "cs");
            this.iml_ExplorerImages.Images.SetKeyName(21, "dll");
            this.iml_ExplorerImages.Images.SetKeyName(22, "h");
            this.iml_ExplorerImages.Images.SetKeyName(23, "dsw");
            this.iml_ExplorerImages.Images.SetKeyName(24, "dvd");
            this.iml_ExplorerImages.Images.SetKeyName(25, "exe");
            this.iml_ExplorerImages.Images.SetKeyName(26, "fon");
            this.iml_ExplorerImages.Images.SetKeyName(27, "gif");
            this.iml_ExplorerImages.Images.SetKeyName(28, "htm");
            this.iml_ExplorerImages.Images.SetKeyName(29, "hlp");
            this.iml_ExplorerImages.Images.SetKeyName(30, "hpp");
            this.iml_ExplorerImages.Images.SetKeyName(31, "ico");
            this.iml_ExplorerImages.Images.SetKeyName(32, "ima");
            this.iml_ExplorerImages.Images.SetKeyName(33, "iso");
            this.iml_ExplorerImages.Images.SetKeyName(34, "java");
            this.iml_ExplorerImages.Images.SetKeyName(35, "png");
            this.iml_ExplorerImages.Images.SetKeyName(36, "jpg");
            this.iml_ExplorerImages.Images.SetKeyName(37, "js");
            this.iml_ExplorerImages.Images.SetKeyName(38, "jsl");
            this.iml_ExplorerImages.Images.SetKeyName(39, "lnk");
            this.iml_ExplorerImages.Images.SetKeyName(40, "mdb");
            this.iml_ExplorerImages.Images.SetKeyName(41, "mdf");
            this.iml_ExplorerImages.Images.SetKeyName(42, "mht");
            this.iml_ExplorerImages.Images.SetKeyName(43, "mkv");
            this.iml_ExplorerImages.Images.SetKeyName(44, "mov");
            this.iml_ExplorerImages.Images.SetKeyName(45, "mp3");
            this.iml_ExplorerImages.Images.SetKeyName(46, "mp4");
            this.iml_ExplorerImages.Images.SetKeyName(47, "mpg");
            this.iml_ExplorerImages.Images.SetKeyName(48, "obj");
            this.iml_ExplorerImages.Images.SetKeyName(49, "ogm");
            this.iml_ExplorerImages.Images.SetKeyName(50, "png");
            this.iml_ExplorerImages.Images.SetKeyName(51, "user");
            this.iml_ExplorerImages.Images.SetKeyName(52, "ppt");
            this.iml_ExplorerImages.Images.SetKeyName(53, "psd");
            this.iml_ExplorerImages.Images.SetKeyName(54, "rar");
            this.iml_ExplorerImages.Images.SetKeyName(55, "rc");
            this.iml_ExplorerImages.Images.SetKeyName(56, "reg");
            this.iml_ExplorerImages.Images.SetKeyName(57, "res");
            this.iml_ExplorerImages.Images.SetKeyName(58, "rm");
            this.iml_ExplorerImages.Images.SetKeyName(59, "sln");
            this.iml_ExplorerImages.Images.SetKeyName(60, "sql");
            this.iml_ExplorerImages.Images.SetKeyName(61, "swf");
            this.iml_ExplorerImages.Images.SetKeyName(62, "tif");
            this.iml_ExplorerImages.Images.SetKeyName(63, "ttf");
            this.iml_ExplorerImages.Images.SetKeyName(64, "txt");
            this.iml_ExplorerImages.Images.SetKeyName(65, "url");
            this.iml_ExplorerImages.Images.SetKeyName(66, "user");
            this.iml_ExplorerImages.Images.SetKeyName(67, "vb");
            this.iml_ExplorerImages.Images.SetKeyName(68, "vbs");
            this.iml_ExplorerImages.Images.SetKeyName(69, "wav");
            this.iml_ExplorerImages.Images.SetKeyName(70, "wma");
            this.iml_ExplorerImages.Images.SetKeyName(71, "wmv");
            this.iml_ExplorerImages.Images.SetKeyName(72, "LastPath");
            this.iml_ExplorerImages.Images.SetKeyName(73, "xls");
            this.iml_ExplorerImages.Images.SetKeyName(74, "xml");
            this.iml_ExplorerImages.Images.SetKeyName(75, "Unknown");
            this.iml_ExplorerImages.Images.SetKeyName(76, "zip");
            // 
            // gbRemoteDisk
            // 
            this.gbRemoteDisk.Controls.Add(this.panelUpload);
            this.gbRemoteDisk.Controls.Add(this.txt_remoteexplorer);
            this.gbRemoteDisk.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbRemoteDisk.Location = new System.Drawing.Point(0, 0);
            this.gbRemoteDisk.Name = "gbRemoteDisk";
            this.gbRemoteDisk.Size = new System.Drawing.Size(380, 78);
            this.gbRemoteDisk.TabIndex = 0;
            this.gbRemoteDisk.TabStop = false;
            this.gbRemoteDisk.Text = "远程磁盘";
            // 
            // panelUpload
            // 
            this.panelUpload.Controls.Add(this.btnUploadCancle);
            this.panelUpload.Controls.Add(this.label8);
            this.panelUpload.Controls.Add(this.lbUpload);
            this.panelUpload.Controls.Add(this.pgbUpLoad);
            this.panelUpload.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelUpload.Location = new System.Drawing.Point(3, 26);
            this.panelUpload.Name = "panelUpload";
            this.panelUpload.Size = new System.Drawing.Size(374, 28);
            this.panelUpload.TabIndex = 2;
            this.panelUpload.Visible = false;
            // 
            // btnUploadCancle
            // 
            this.btnUploadCancle.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUploadCancle.Location = new System.Drawing.Point(327, 0);
            this.btnUploadCancle.Name = "btnUploadCancle";
            this.btnUploadCancle.Size = new System.Drawing.Size(47, 28);
            this.btnUploadCancle.TabIndex = 5;
            this.btnUploadCancle.Text = "取消";
            this.btnUploadCancle.UseVisualStyleBackColor = true;
            this.btnUploadCancle.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "上传进度:";
            // 
            // lbUpload
            // 
            this.lbUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbUpload.AutoSize = true;
            this.lbUpload.Location = new System.Drawing.Point(269, 9);
            this.lbUpload.Name = "lbUpload";
            this.lbUpload.Size = new System.Drawing.Size(0, 12);
            this.lbUpload.TabIndex = 3;
            // 
            // pgbUpLoad
            // 
            this.pgbUpLoad.Location = new System.Drawing.Point(64, 2);
            this.pgbUpLoad.Name = "pgbUpLoad";
            this.pgbUpLoad.Size = new System.Drawing.Size(197, 23);
            this.pgbUpLoad.TabIndex = 2;
            // 
            // txt_remoteexplorer
            // 
            this.txt_remoteexplorer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txt_remoteexplorer.Location = new System.Drawing.Point(3, 54);
            this.txt_remoteexplorer.Name = "txt_remoteexplorer";
            this.txt_remoteexplorer.Size = new System.Drawing.Size(374, 21);
            this.txt_remoteexplorer.TabIndex = 0;
            // 
            // lvLocalDisk
            // 
            this.lvLocalDisk.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderDate,
            this.columnHeaderType,
            this.columnHeaderSize});
            this.lvLocalDisk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLocalDisk.LargeImageList = this.iml_ExplorerImages;
            this.lvLocalDisk.Location = new System.Drawing.Point(0, 78);
            this.lvLocalDisk.MultiSelect = false;
            this.lvLocalDisk.Name = "lvLocalDisk";
            this.lvLocalDisk.Size = new System.Drawing.Size(394, 381);
            this.lvLocalDisk.SmallImageList = this.iml_ExplorerImages;
            this.lvLocalDisk.StateImageList = this.iml_ExplorerImages;
            this.lvLocalDisk.TabIndex = 2;
            this.lvLocalDisk.UseCompatibleStateImageBehavior = false;
            this.lvLocalDisk.SelectedIndexChanged += new System.EventHandler(this.lvLocalDisk_SelectedIndexChanged);
            this.lvLocalDisk.DoubleClick += new System.EventHandler(this.lvLocalDisk_DoubleClick);
            this.lvLocalDisk.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvLocalDisk_MouseDown);
            // 
            // gbLocalDisk
            // 
            this.gbLocalDisk.Controls.Add(this.panelDownload);
            this.gbLocalDisk.Controls.Add(this.txt_myexplorer);
            this.gbLocalDisk.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbLocalDisk.Location = new System.Drawing.Point(0, 0);
            this.gbLocalDisk.Name = "gbLocalDisk";
            this.gbLocalDisk.Size = new System.Drawing.Size(394, 78);
            this.gbLocalDisk.TabIndex = 1;
            this.gbLocalDisk.TabStop = false;
            this.gbLocalDisk.Text = "本地磁盘";
            // 
            // panelDownload
            // 
            this.panelDownload.Controls.Add(this.btnDownloadCancle);
            this.panelDownload.Controls.Add(this.label10);
            this.panelDownload.Controls.Add(this.lbDownload);
            this.panelDownload.Controls.Add(this.pgbDownload);
            this.panelDownload.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelDownload.Location = new System.Drawing.Point(3, 26);
            this.panelDownload.Name = "panelDownload";
            this.panelDownload.Size = new System.Drawing.Size(388, 28);
            this.panelDownload.TabIndex = 3;
            this.panelDownload.Visible = false;
            // 
            // btnDownloadCancle
            // 
            this.btnDownloadCancle.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDownloadCancle.Location = new System.Drawing.Point(341, 0);
            this.btnDownloadCancle.Name = "btnDownloadCancle";
            this.btnDownloadCancle.Size = new System.Drawing.Size(47, 28);
            this.btnDownloadCancle.TabIndex = 6;
            this.btnDownloadCancle.Text = "取消";
            this.btnDownloadCancle.UseVisualStyleBackColor = true;
            this.btnDownloadCancle.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 4;
            this.label10.Text = "下载进度:";
            // 
            // lbDownload
            // 
            this.lbDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDownload.AutoSize = true;
            this.lbDownload.Location = new System.Drawing.Point(272, 10);
            this.lbDownload.Name = "lbDownload";
            this.lbDownload.Size = new System.Drawing.Size(0, 12);
            this.lbDownload.TabIndex = 3;
            // 
            // pgbDownload
            // 
            this.pgbDownload.Location = new System.Drawing.Point(69, 3);
            this.pgbDownload.Name = "pgbDownload";
            this.pgbDownload.Size = new System.Drawing.Size(197, 23);
            this.pgbDownload.TabIndex = 2;
            // 
            // txt_myexplorer
            // 
            this.txt_myexplorer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txt_myexplorer.Location = new System.Drawing.Point(3, 54);
            this.txt_myexplorer.Name = "txt_myexplorer";
            this.txt_myexplorer.Size = new System.Drawing.Size(388, 21);
            this.txt_myexplorer.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统ToolStripMenuItem,
            this.视图ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(982, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 系统ToolStripMenuItem
            // 
            this.系统ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.启动服务ToolStripMenuItem,
            this.停止服务ToolStripMenuItem});
            this.系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            this.系统ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.系统ToolStripMenuItem.Text = "系统";
            // 
            // 启动服务ToolStripMenuItem
            // 
            this.启动服务ToolStripMenuItem.Name = "启动服务ToolStripMenuItem";
            this.启动服务ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.启动服务ToolStripMenuItem.Text = "启动服务";
            this.启动服务ToolStripMenuItem.Click += new System.EventHandler(this.启动服务ToolStripMenuItem_Click);
            // 
            // 停止服务ToolStripMenuItem
            // 
            this.停止服务ToolStripMenuItem.Name = "停止服务ToolStripMenuItem";
            this.停止服务ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.停止服务ToolStripMenuItem.Text = "停止服务";
            this.停止服务ToolStripMenuItem.Click += new System.EventHandler(this.停止服务ToolStripMenuItem_Click);
            // 
            // 视图ToolStripMenuItem
            // 
            this.视图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.资源列表显示方式ToolStripMenuItem});
            this.视图ToolStripMenuItem.Name = "视图ToolStripMenuItem";
            this.视图ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.视图ToolStripMenuItem.Text = "视图";
            // 
            // 资源列表显示方式ToolStripMenuItem
            // 
            this.资源列表显示方式ToolStripMenuItem.Name = "资源列表显示方式ToolStripMenuItem";
            this.资源列表显示方式ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.资源列表显示方式ToolStripMenuItem.Text = "资源列表显示方式";
            this.资源列表显示方式ToolStripMenuItem.Click += new System.EventHandler(this.资源列表显示方式ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_Display,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 548);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(982, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbl_Display
            // 
            this.lbl_Display.Name = "lbl_Display";
            this.lbl_Display.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmRemoteResource,
            this.断开连接ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // tsmRemoteResource
            // 
            this.tsmRemoteResource.Name = "tsmRemoteResource";
            this.tsmRemoteResource.Size = new System.Drawing.Size(124, 22);
            this.tsmRemoteResource.Text = "浏览资源";
            this.tsmRemoteResource.Click += new System.EventHandler(this.tsmRemoteResource_Click);
            // 
            // 断开连接ToolStripMenuItem
            // 
            this.断开连接ToolStripMenuItem.Name = "断开连接ToolStripMenuItem";
            this.断开连接ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.断开连接ToolStripMenuItem.Text = "断开连接";
            this.断开连接ToolStripMenuItem.Click += new System.EventHandler(this.断开连接ToolStripMenuItem_Click);
            // 
            // cms_Disk
            // 
            this.cms_Disk.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DownloadToolStripMenuItem,
            this.UploadToolStripMenuItem,
            this.DeleteToolStripMenuItem,
            this.BrowseToolStripMenuItem,
            this.RefreshToolStripMenuItem});
            this.cms_Disk.Name = "cms_Disk";
            this.cms_Disk.Size = new System.Drawing.Size(101, 114);
            // 
            // DownloadToolStripMenuItem
            // 
            this.DownloadToolStripMenuItem.Name = "DownloadToolStripMenuItem";
            this.DownloadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.DownloadToolStripMenuItem.Text = "下载";
            this.DownloadToolStripMenuItem.Click += new System.EventHandler(this.DownloadToolStripMenuItem_Click);
            // 
            // UploadToolStripMenuItem
            // 
            this.UploadToolStripMenuItem.Name = "UploadToolStripMenuItem";
            this.UploadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.UploadToolStripMenuItem.Text = "上传";
            this.UploadToolStripMenuItem.Click += new System.EventHandler(this.UploadToolStripMenuItem_Click);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.DeleteToolStripMenuItem.Text = "删除";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // BrowseToolStripMenuItem
            // 
            this.BrowseToolStripMenuItem.Name = "BrowseToolStripMenuItem";
            this.BrowseToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.BrowseToolStripMenuItem.Text = "浏览";
            this.BrowseToolStripMenuItem.Click += new System.EventHandler(this.BrowseToolStripMenuItem_Click);
            // 
            // RefreshToolStripMenuItem
            // 
            this.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem";
            this.RefreshToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.RefreshToolStripMenuItem.Text = "刷新";
            this.RefreshToolStripMenuItem.Click += new System.EventHandler(this.RefreshToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "管理界面已隐藏，可点击小图标可显示管理器控制面板！";
            this.notifyIcon1.BalloonTipTitle = "E7 升级包发布服务器";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "E7 升级包发布服务器";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "名称";
            this.columnHeaderName.Width = 300;
            // 
            // columnHeaderDate
            // 
            this.columnHeaderDate.Text = "修改日期";
            this.columnHeaderDate.Width = 130;
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "类型";
            this.columnHeaderType.Width = 100;
            // 
            // columnHeaderSize
            // 
            this.columnHeaderSize.Text = "大小";
            this.columnHeaderSize.Width = 100;
            // 
            // columnHeaderRName
            // 
            this.columnHeaderRName.Text = "名称";
            this.columnHeaderRName.Width = 300;
            // 
            // columnHeaderRDate
            // 
            this.columnHeaderRDate.Text = "修改日期";
            this.columnHeaderRDate.Width = 130;
            // 
            // columnHeaderRType
            // 
            this.columnHeaderRType.Text = "类型";
            this.columnHeaderRType.Width = 100;
            // 
            // columnHeaderRSize
            // 
            this.columnHeaderRSize.Text = "大小";
            this.columnHeaderRSize.Width = 100;
            // 
            // AuWriterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 570);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AuWriterForm";
            this.Text = "E7升级包发布服务器";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AuWriterForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.AuWriterForm_SizeChanged);
            this.tbpBase.ResumeLayout(false);
            this.tbpBase.PerformLayout();
            this.panelAuclient.ResumeLayout(false);
            this.gbPublish.ResumeLayout(false);
            this.gbPublish.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tbpControl.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControlCmd.ResumeLayout(false);
            this.tbpCmd.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControlLog.ResumeLayout(false);
            this.tbpLog.ResumeLayout(false);
            this.tbpLogSelect.ResumeLayout(false);
            this.tbpLogSelect.PerformLayout();
            this.tbpTerminal.ResumeLayout(false);
            this.tbpRes.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.gbRemoteDisk.ResumeLayout(false);
            this.gbRemoteDisk.PerformLayout();
            this.panelUpload.ResumeLayout(false);
            this.panelUpload.PerformLayout();
            this.gbLocalDisk.ResumeLayout(false);
            this.gbLocalDisk.PerformLayout();
            this.panelDownload.ResumeLayout(false);
            this.panelDownload.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.cms_Disk.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdExpt;
        private System.Windows.Forms.OpenFileDialog ofdSrc;
        private System.Windows.Forms.TabPage tbpOpt;
        private System.Windows.Forms.SaveFileDialog sfdDest;
        private System.Windows.Forms.CheckBox cbPackage;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tbpBase;
        private System.Windows.Forms.Button btnExpt;
        private System.Windows.Forms.TextBox txtExpt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDest;
        private System.Windows.Forms.TextBox txtDest;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSrc;
        private System.Windows.Forms.TextBox txtSrc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.ProgressBar prbProd;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnProduce;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnDir;
        private System.Windows.Forms.FolderBrowserDialog fbdSrc;
        private System.Windows.Forms.ComboBox cbSubSystem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbFilter;
        private System.Windows.Forms.TextBox tbVersion;
        private System.Windows.Forms.TabPage tbpControl;
        private System.Windows.Forms.TextBox tbUpdateMsg;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbMsg;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 启动服务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止服务ToolStripMenuItem;
        private System.Windows.Forms.ComboBox cmbCmd;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tbParameter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbpContent;
        private System.Windows.Forms.TabControl tabControlLog;
        private System.Windows.Forms.TabPage tbpLog;
        private System.Windows.Forms.TabPage tbpLogSelect;
        private System.Windows.Forms.TreeView tvTerminal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbCmdType;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tabControlCmd;
        private System.Windows.Forms.TabPage tbpCmd;
        private System.Windows.Forms.TabPage tbpRes;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox gbLocalDisk;
        private System.Windows.Forms.GroupBox gbRemoteDisk;
        private System.Windows.Forms.ListView lvLocalDisk;
        private System.Windows.Forms.ListView lvRemoteDisk;
        private System.Windows.Forms.TextBox txt_myexplorer;
        private System.Windows.Forms.ToolStripStatusLabel lbl_Display;
        private System.Windows.Forms.ImageList iml_ExplorerImages;
        private System.Windows.Forms.TextBox txt_remoteexplorer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmRemoteResource;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label lbUpload;
        private System.Windows.Forms.ProgressBar pgbUpLoad;
        private System.Windows.Forms.ToolStripMenuItem 断开连接ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cms_Disk;
        private System.Windows.Forms.ToolStripMenuItem DownloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BrowseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RefreshToolStripMenuItem;
        private System.Windows.Forms.Panel panelDownload;
        private System.Windows.Forms.Label lbDownload;
        private System.Windows.Forms.ProgressBar pgbDownload;
        private System.Windows.Forms.Panel panelUpload;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnUploadCancle;
        private System.Windows.Forms.Button btnDownloadCancle;
        private System.Windows.Forms.TabPage tbpTerminal;
        private System.Windows.Forms.RichTextBox rtbTerminial;
        private System.Windows.Forms.GroupBox gbPublish;
        private System.Windows.Forms.Button btnAuClientPub;
        private System.Windows.Forms.Button btnbtnAuClientSelect;
        private System.Windows.Forms.Panel panelAuclient;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem 视图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 资源列表显示方式ToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderDate;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.ColumnHeader columnHeaderSize;
        private System.Windows.Forms.ColumnHeader columnHeaderRName;
        private System.Windows.Forms.ColumnHeader columnHeaderRDate;
        private System.Windows.Forms.ColumnHeader columnHeaderRType;
        private System.Windows.Forms.ColumnHeader columnHeaderRSize;
    }
}

