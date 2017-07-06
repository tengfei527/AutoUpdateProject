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
            this.ofdExpt = new System.Windows.Forms.OpenFileDialog();
            this.ofdSrc = new System.Windows.Forms.OpenFileDialog();
            this.tbpOpt = new System.Windows.Forms.TabPage();
            this.sfdDest = new System.Windows.Forms.SaveFileDialog();
            this.cbPackage = new System.Windows.Forms.CheckBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbpBase = new System.Windows.Forms.TabPage();
            this.tbUpdateMsg = new System.Windows.Forms.TextBox();
            this.tbVersion = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.prbProd = new System.Windows.Forms.ProgressBar();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnProduce = new System.Windows.Forms.Button();
            this.cbFilter = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbSubSystem = new System.Windows.Forms.ComboBox();
            this.btnExpt = new System.Windows.Forms.Button();
            this.txtExpt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDest = new System.Windows.Forms.Button();
            this.txtDest = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDir = new System.Windows.Forms.Button();
            this.btnSrc = new System.Windows.Forms.Button();
            this.txtSrc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbpControl = new System.Windows.Forms.TabPage();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbMsg = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.fbdSrc = new System.Windows.Forms.FolderBrowserDialog();
            this.btnStart = new System.Windows.Forms.Button();
            this.tbpBase.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbpControl.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.tbpOpt.Size = new System.Drawing.Size(521, 371);
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
            this.cbPackage.Location = new System.Drawing.Point(396, 238);
            this.cbPackage.Name = "cbPackage";
            this.cbPackage.Size = new System.Drawing.Size(48, 16);
            this.cbPackage.TabIndex = 11;
            this.cbPackage.Text = "打包";
            this.cbPackage.UseVisualStyleBackColor = true;
            this.cbPackage.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(70, 9);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.ReadOnly = true;
            this.txtUrl.Size = new System.Drawing.Size(433, 21);
            this.txtUrl.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "更新网址:";
            // 
            // tbpBase
            // 
            this.tbpBase.Controls.Add(this.tbUpdateMsg);
            this.tbpBase.Controls.Add(this.tbVersion);
            this.tbpBase.Controls.Add(this.panel2);
            this.tbpBase.Controls.Add(this.cbFilter);
            this.tbpBase.Controls.Add(this.label6);
            this.tbpBase.Controls.Add(this.cbSubSystem);
            this.tbpBase.Controls.Add(this.cbPackage);
            this.tbpBase.Controls.Add(this.txtUrl);
            this.tbpBase.Controls.Add(this.label5);
            this.tbpBase.Controls.Add(this.btnExpt);
            this.tbpBase.Controls.Add(this.txtExpt);
            this.tbpBase.Controls.Add(this.label4);
            this.tbpBase.Controls.Add(this.btnDest);
            this.tbpBase.Controls.Add(this.txtDest);
            this.tbpBase.Controls.Add(this.label3);
            this.tbpBase.Controls.Add(this.btnDir);
            this.tbpBase.Controls.Add(this.btnSrc);
            this.tbpBase.Controls.Add(this.txtSrc);
            this.tbpBase.Controls.Add(this.label2);
            this.tbpBase.Location = new System.Drawing.Point(4, 22);
            this.tbpBase.Name = "tbpBase";
            this.tbpBase.Padding = new System.Windows.Forms.Padding(3);
            this.tbpBase.Size = new System.Drawing.Size(521, 371);
            this.tbpBase.TabIndex = 0;
            this.tbpBase.Text = "※基本信息";
            this.tbpBase.UseVisualStyleBackColor = true;
            // 
            // tbUpdateMsg
            // 
            this.tbUpdateMsg.Location = new System.Drawing.Point(69, 263);
            this.tbUpdateMsg.Multiline = true;
            this.tbUpdateMsg.Name = "tbUpdateMsg";
            this.tbUpdateMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbUpdateMsg.Size = new System.Drawing.Size(375, 59);
            this.tbUpdateMsg.TabIndex = 18;
            this.tbUpdateMsg.Text = "更新说明：";
            // 
            // tbVersion
            // 
            this.tbVersion.Location = new System.Drawing.Point(424, 65);
            this.tbVersion.Name = "tbVersion";
            this.tbVersion.Size = new System.Drawing.Size(79, 21);
            this.tbVersion.TabIndex = 15;
            this.tbVersion.TextChanged += new System.EventHandler(this.tbVersion_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.prbProd);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnProduce);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 328);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(515, 40);
            this.panel2.TabIndex = 4;
            // 
            // prbProd
            // 
            this.prbProd.Location = new System.Drawing.Point(7, 9);
            this.prbProd.Name = "prbProd";
            this.prbProd.Size = new System.Drawing.Size(370, 23);
            this.prbProd.TabIndex = 2;
            this.prbProd.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(446, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(64, 23);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "退出(&X)";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnProduce
            // 
            this.btnProduce.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnProduce.Location = new System.Drawing.Point(380, 10);
            this.btnProduce.Name = "btnProduce";
            this.btnProduce.Size = new System.Drawing.Size(60, 23);
            this.btnProduce.TabIndex = 0;
            this.btnProduce.Text = "生成(&G)";
            this.btnProduce.UseVisualStyleBackColor = true;
            this.btnProduce.Click += new System.EventHandler(this.btnProduce_Click);
            // 
            // cbFilter
            // 
            this.cbFilter.AutoSize = true;
            this.cbFilter.Checked = true;
            this.cbFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFilter.Location = new System.Drawing.Point(70, 89);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(300, 16);
            this.cbFilter.TabIndex = 14;
            this.cbFilter.Text = "过滤文件(*.log,*.config,*.db,*.dat,unins000.*)";
            this.cbFilter.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "升级类型:";
            // 
            // cbSubSystem
            // 
            this.cbSubSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubSystem.FormattingEnabled = true;
            this.cbSubSystem.Location = new System.Drawing.Point(70, 37);
            this.cbSubSystem.Name = "cbSubSystem";
            this.cbSubSystem.Size = new System.Drawing.Size(263, 20);
            this.cbSubSystem.TabIndex = 12;
            // 
            // btnExpt
            // 
            this.btnExpt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExpt.Location = new System.Drawing.Point(450, 209);
            this.btnExpt.Name = "btnExpt";
            this.btnExpt.Size = new System.Drawing.Size(53, 21);
            this.btnExpt.TabIndex = 8;
            this.btnExpt.Text = "选择(&S)";
            this.btnExpt.UseVisualStyleBackColor = true;
            this.btnExpt.Click += new System.EventHandler(this.btnExpt_Click);
            // 
            // txtExpt
            // 
            this.txtExpt.Location = new System.Drawing.Point(70, 111);
            this.txtExpt.Multiline = true;
            this.txtExpt.Name = "txtExpt";
            this.txtExpt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExpt.Size = new System.Drawing.Size(374, 119);
            this.txtExpt.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "排除文件:";
            // 
            // btnDest
            // 
            this.btnDest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDest.Location = new System.Drawing.Point(450, 235);
            this.btnDest.Name = "btnDest";
            this.btnDest.Size = new System.Drawing.Size(52, 21);
            this.btnDest.TabIndex = 5;
            this.btnDest.Text = "选择(&S)";
            this.btnDest.UseVisualStyleBackColor = true;
            this.btnDest.Click += new System.EventHandler(this.btnDest_Click);
            // 
            // txtDest
            // 
            this.txtDest.Location = new System.Drawing.Point(69, 236);
            this.txtDest.Name = "txtDest";
            this.txtDest.Size = new System.Drawing.Size(321, 21);
            this.txtDest.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "保存位置:";
            // 
            // btnDir
            // 
            this.btnDir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDir.Location = new System.Drawing.Point(424, 36);
            this.btnDir.Name = "btnDir";
            this.btnDir.Size = new System.Drawing.Size(79, 21);
            this.btnDir.TabIndex = 2;
            this.btnDir.Text = "选择目录(&D)";
            this.btnDir.UseVisualStyleBackColor = true;
            this.btnDir.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // btnSrc
            // 
            this.btnSrc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSrc.Location = new System.Drawing.Point(339, 36);
            this.btnSrc.Name = "btnSrc";
            this.btnSrc.Size = new System.Drawing.Size(79, 21);
            this.btnSrc.TabIndex = 2;
            this.btnSrc.Text = "选择程序(&S)";
            this.btnSrc.UseVisualStyleBackColor = true;
            this.btnSrc.Click += new System.EventHandler(this.btnSrc_Click);
            // 
            // txtSrc
            // 
            this.txtSrc.Location = new System.Drawing.Point(70, 65);
            this.txtSrc.Name = "txtSrc";
            this.txtSrc.Size = new System.Drawing.Size(347, 21);
            this.txtSrc.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "主程序:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(535, 417);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbpBase);
            this.tabControl1.Controls.Add(this.tbpOpt);
            this.tabControl1.Controls.Add(this.tbpControl);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 17);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(529, 397);
            this.tabControl1.TabIndex = 0;
            // 
            // tbpControl
            // 
            this.tbpControl.Controls.Add(this.lbLog);
            this.tbpControl.Controls.Add(this.groupBox2);
            this.tbpControl.Location = new System.Drawing.Point(4, 22);
            this.tbpControl.Name = "tbpControl";
            this.tbpControl.Padding = new System.Windows.Forms.Padding(3);
            this.tbpControl.Size = new System.Drawing.Size(521, 371);
            this.tbpControl.TabIndex = 2;
            this.tbpControl.Text = "※控制";
            this.tbpControl.UseVisualStyleBackColor = true;
            // 
            // lbLog
            // 
            this.lbLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLog.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lbLog.FormattingEnabled = true;
            this.lbLog.HorizontalScrollbar = true;
            this.lbLog.ItemHeight = 12;
            this.lbLog.Location = new System.Drawing.Point(3, 89);
            this.lbLog.Name = "lbLog";
            this.lbLog.ScrollAlwaysVisible = true;
            this.lbLog.Size = new System.Drawing.Size(515, 279);
            this.lbLog.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbMsg);
            this.groupBox2.Controls.Add(this.btnStart);
            this.groupBox2.Controls.Add(this.btnSend);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(515, 86);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // tbMsg
            // 
            this.tbMsg.Location = new System.Drawing.Point(6, 10);
            this.tbMsg.Multiline = true;
            this.tbMsg.Name = "tbMsg";
            this.tbMsg.Size = new System.Drawing.Size(424, 69);
            this.tbMsg.TabIndex = 5;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(436, 56);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(64, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(436, 10);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(64, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "启动服务";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // AuWriterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 417);
            this.Controls.Add(this.groupBox1);
            this.Name = "AuWriterForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tbpBase.ResumeLayout(false);
            this.tbpBase.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tbpControl.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
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
        private System.Windows.Forms.Button btnStart;
    }
}

