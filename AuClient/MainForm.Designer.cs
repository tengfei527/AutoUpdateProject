namespace AuClient
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnFinish = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPageMain = new System.Windows.Forms.TabControl();
            this.tabPageList = new System.Windows.Forms.TabPage();
            this.tabPageMsg = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvUpdateList = new System.Windows.Forms.ListView();
            this.No = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Version = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Progress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WritePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SHA256 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pbDownFile = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbUpdateMsg = new System.Windows.Forms.TextBox();
            this.lbState = new System.Windows.Forms.Label();
            this.tabPageSucess = new System.Windows.Forms.TabPage();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPageMain.SuspendLayout();
            this.tabPageList.SuspendLayout();
            this.tabPageMsg.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPageSucess.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(224, 323);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(80, 24);
            this.btnFinish.TabIndex = 8;
            this.btnFinish.Text = "完成(&F)";
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 273);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(388, 2);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox2";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(60, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "欢迎以后继续关注我们的产品。";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(59, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(232, 48);
            this.label2.TabIndex = 10;
            this.label2.Text = "     程序更新完成,如果程序更新期间被关闭,点击\"完成\"自动更新程序会自动重新启动系统。";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(6, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(238, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "感谢使用在线升级";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(253, 220);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "深圳富士智能科技有限公司";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Location = new System.Drawing.Point(245, 244);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(128, 16);
            this.linkLabel1.TabIndex = 12;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://www.ftf.cn";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.linkLabel1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(388, 275);
            this.panel2.TabIndex = 11;
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(6, 32);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(377, 10);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(312, 323);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(80, 24);
            this.btnNext.TabIndex = 9;
            this.btnNext.Text = "下一步(&N)>";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(400, 323);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 24);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(112, 307);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.tabPageMsg);
            this.tabPageMain.Controls.Add(this.tabPageList);
            this.tabPageMain.Controls.Add(this.tabPageSucess);
            this.tabPageMain.Location = new System.Drawing.Point(120, 9);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.SelectedIndex = 0;
            this.tabPageMain.Size = new System.Drawing.Size(402, 307);
            this.tabPageMain.TabIndex = 12;
            // 
            // tabPageList
            // 
            this.tabPageList.Controls.Add(this.panel1);
            this.tabPageList.Location = new System.Drawing.Point(4, 22);
            this.tabPageList.Name = "tabPageList";
            this.tabPageList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageList.Size = new System.Drawing.Size(394, 281);
            this.tabPageList.TabIndex = 0;
            this.tabPageList.Text = "以下为更新文件列表";
            this.tabPageList.UseVisualStyleBackColor = true;
            // 
            // tabPageMsg
            // 
            this.tabPageMsg.Controls.Add(this.tbUpdateMsg);
            this.tabPageMsg.Location = new System.Drawing.Point(4, 22);
            this.tabPageMsg.Name = "tabPageMsg";
            this.tabPageMsg.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMsg.Size = new System.Drawing.Size(394, 281);
            this.tabPageMsg.TabIndex = 1;
            this.tabPageMsg.Text = "更新说明";
            this.tabPageMsg.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbState);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.pbDownFile);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.lvUpdateList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(388, 275);
            this.panel1.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 250);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(388, 2);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // lvUpdateList
            // 
            this.lvUpdateList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.No,
            this.Version,
            this.Progress,
            this.WritePath,
            this.SHA256});
            this.lvUpdateList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvUpdateList.Location = new System.Drawing.Point(0, 0);
            this.lvUpdateList.Name = "lvUpdateList";
            this.lvUpdateList.Size = new System.Drawing.Size(388, 275);
            this.lvUpdateList.TabIndex = 6;
            this.lvUpdateList.UseCompatibleStateImageBehavior = false;
            this.lvUpdateList.View = System.Windows.Forms.View.Details;
            // 
            // No
            // 
            this.No.Text = "组件名称";
            this.No.Width = 124;
            // 
            // Version
            // 
            this.Version.Text = "版本号";
            this.Version.Width = 67;
            // 
            // Progress
            // 
            this.Progress.Text = "进度";
            this.Progress.Width = 49;
            // 
            // WritePath
            // 
            this.WritePath.Text = "下载地址";
            this.WritePath.Width = 66;
            // 
            // SHA256
            // 
            this.SHA256.Text = "SHA256";
            // 
            // pbDownFile
            // 
            this.pbDownFile.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbDownFile.Location = new System.Drawing.Point(0, 252);
            this.pbDownFile.Name = "pbDownFile";
            this.pbDownFile.Size = new System.Drawing.Size(388, 23);
            this.pbDownFile.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(388, 8);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // tbUpdateMsg
            // 
            this.tbUpdateMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbUpdateMsg.Location = new System.Drawing.Point(3, 3);
            this.tbUpdateMsg.Multiline = true;
            this.tbUpdateMsg.Name = "tbUpdateMsg";
            this.tbUpdateMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbUpdateMsg.Size = new System.Drawing.Size(388, 275);
            this.tbUpdateMsg.TabIndex = 18;
            // 
            // lbState
            // 
            this.lbState.AutoSize = true;
            this.lbState.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbState.Location = new System.Drawing.Point(0, 238);
            this.lbState.Name = "lbState";
            this.lbState.Size = new System.Drawing.Size(137, 12);
            this.lbState.TabIndex = 8;
            this.lbState.Text = "点击“下一步”开始更新";
            // 
            // tabPageSucess
            // 
            this.tabPageSucess.Controls.Add(this.panel2);
            this.tabPageSucess.Location = new System.Drawing.Point(4, 22);
            this.tabPageSucess.Name = "tabPageSucess";
            this.tabPageSucess.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSucess.Size = new System.Drawing.Size(394, 281);
            this.tabPageSucess.TabIndex = 2;
            this.tabPageSucess.Text = "更新完成";
            this.tabPageSucess.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 362);
            this.ControlBox = false;
            this.Controls.Add(this.tabPageMain);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自动更新";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPageMain.ResumeLayout(false);
            this.tabPageList.ResumeLayout(false);
            this.tabPageMsg.ResumeLayout(false);
            this.tabPageMsg.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPageSucess.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabControl tabPageMain;
        private System.Windows.Forms.TabPage tabPageList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvUpdateList;
        private System.Windows.Forms.ColumnHeader No;
        private System.Windows.Forms.ColumnHeader Version;
        private System.Windows.Forms.ColumnHeader Progress;
        private System.Windows.Forms.ColumnHeader WritePath;
        private System.Windows.Forms.ColumnHeader SHA256;
        private System.Windows.Forms.ProgressBar pbDownFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabPageMsg;
        private System.Windows.Forms.TextBox tbUpdateMsg;
        private System.Windows.Forms.Label lbState;
        private System.Windows.Forms.TabPage tabPageSucess;
    }
}