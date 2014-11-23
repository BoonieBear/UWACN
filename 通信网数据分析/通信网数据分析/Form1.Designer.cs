namespace 通信网数据分析
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.opensinglefile = new DevComponents.DotNetBar.ButtonItem();
            this.opennetfile = new DevComponents.DotNetBar.ButtonItem();
            this.opendirectory = new DevComponents.DotNetBar.ButtonItem();
            this.opennetdirectory = new DevComponents.DotNetBar.ButtonItem();
            this.closeallWindows = new DevComponents.DotNetBar.ButtonItem();
            this.quit = new DevComponents.DotNetBar.ButtonItem();
            this.about = new DevComponents.DotNetBar.ButtonItem();
            this.Translate = new DevComponents.DotNetBar.ButtonItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.FileProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tabStrip1 = new DevComponents.DotNetBar.TabStrip();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.FilesWorker = new System.ComponentModel.BackgroundWorker();
            this.TranslateWorker = new System.ComponentModel.BackgroundWorker();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.AccessibleDescription = "DotNetBar Bar (bar1)";
            this.bar1.AccessibleName = "DotNetBar Bar";
            this.bar1.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.bar1.AutoHide = true;
            this.bar1.BackColor = System.Drawing.Color.Silver;
            this.bar1.BarType = DevComponents.DotNetBar.eBarType.MenuBar;
            this.bar1.DisplayMoreItemsOnMenu = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1,
            this.about,
            this.Translate});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.MenuBar = true;
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(960, 26);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.bar1.TabIndex = 2;
            this.bar1.TabNavigation = true;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // buttonItem1
            // 
            this.buttonItem1.AutoExpandOnClick = true;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.opensinglefile,
            this.opennetfile,
            this.opendirectory,
            this.opennetdirectory,
            this.closeallWindows,
            this.quit});
            this.buttonItem1.Text = "开始";
            // 
            // opensinglefile
            // 
            this.opensinglefile.Name = "opensinglefile";
            this.opensinglefile.Text = "打开串口文件";
            this.opensinglefile.Tooltip = "查看单个串口文件";
            this.opensinglefile.Click += new System.EventHandler(this.opensinglefile_Click);
            // 
            // opennetfile
            // 
            this.opennetfile.Name = "opennetfile";
            this.opennetfile.Text = "打开网络文件";
            this.opennetfile.Tooltip = "查看单个网络文件";
            this.opennetfile.Click += new System.EventHandler(this.opennetfile_Click);
            // 
            // opendirectory
            // 
            this.opendirectory.Name = "opendirectory";
            this.opendirectory.Text = "打开串口文件夹";
            this.opendirectory.Click += new System.EventHandler(this.opendirectory_Click);
            // 
            // opennetdirectory
            // 
            this.opennetdirectory.Name = "opennetdirectory";
            this.opennetdirectory.Text = "打开网络文件夹";
            this.opennetdirectory.Click += new System.EventHandler(this.opennetdirectory_Click);
            // 
            // closeallWindows
            // 
            this.closeallWindows.Name = "closeallWindows";
            this.closeallWindows.Text = "关闭所有窗口";
            this.closeallWindows.Click += new System.EventHandler(this.closeallWindows_Click);
            // 
            // quit
            // 
            this.quit.Name = "quit";
            this.quit.Text = "退出";
            this.quit.Click += new System.EventHandler(this.quit_Click);
            // 
            // about
            // 
            this.about.Name = "about";
            this.about.Text = "关于";
            this.about.Click += new System.EventHandler(this.about_Click);
            // 
            // Translate
            // 
            this.Translate.Name = "Translate";
            this.Translate.Text = "批量转换";
            this.Translate.Tooltip = "将网络数据批量转换为串口数据";
            this.Translate.Click += new System.EventHandler(this.Translate_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.FileProgress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 589);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(960, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(945, 17);
            this.StatusLabel.Spring = true;
            this.StatusLabel.Text = "---";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FileProgress
            // 
            this.FileProgress.AutoToolTip = true;
            this.FileProgress.MarqueeAnimationSpeed = 10;
            this.FileProgress.Name = "FileProgress";
            this.FileProgress.Size = new System.Drawing.Size(400, 16);
            this.FileProgress.Step = 1;
            this.FileProgress.Visible = false;
            // 
            // tabStrip1
            // 
            this.tabStrip1.AutoSelectAttachedControl = true;
            this.tabStrip1.CanReorderTabs = true;
            this.tabStrip1.CloseButtonOnTabsVisible = true;
            this.tabStrip1.CloseButtonVisible = true;
            this.tabStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabStrip1.Location = new System.Drawing.Point(0, 563);
            this.tabStrip1.MdiAutoHide = false;
            this.tabStrip1.MdiForm = this;
            this.tabStrip1.MdiTabbedDocuments = true;
            this.tabStrip1.Name = "tabStrip1";
            this.tabStrip1.SelectedTab = null;
            this.tabStrip1.Size = new System.Drawing.Size(960, 26);
            this.tabStrip1.Style = DevComponents.DotNetBar.eTabStripStyle.Office2007Dock;
            this.tabStrip1.TabIndex = 4;
            this.tabStrip1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.MultilineNoNavigationBox;
            this.tabStrip1.Text = "tabStrip1";
            // 
            // FilesWorker
            // 
            this.FilesWorker.WorkerReportsProgress = true;
            this.FilesWorker.WorkerSupportsCancellation = true;
            this.FilesWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.FilesWorker_DoWork);
            this.FilesWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.FilesWorker_ProgressChanged);
            this.FilesWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.FilesWorker_RunWorkerCompleted);
            // 
            // TranslateWorker
            // 
            this.TranslateWorker.WorkerReportsProgress = true;
            this.TranslateWorker.WorkerSupportsCancellation = true;
            this.TranslateWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.TranslateWorker_DoWork);
            this.TranslateWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.TranslateWorker_ProgressChanged);
            this.TranslateWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.TranslateWorker_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 611);
            this.Controls.Add(this.bar1);
            this.Controls.Add(this.tabStrip1);
            this.Controls.Add(this.statusStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通信网设备数据分析( 新协议)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Bar bar1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem about;
        private DevComponents.DotNetBar.ButtonItem opensinglefile;
        private DevComponents.DotNetBar.ButtonItem opendirectory;
        private DevComponents.DotNetBar.ButtonItem closeallWindows;
        private DevComponents.DotNetBar.ButtonItem quit;
        private DevComponents.DotNetBar.TabStrip tabStrip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.ComponentModel.BackgroundWorker FilesWorker;
        private System.Windows.Forms.ToolStripProgressBar FileProgress;
        private DevComponents.DotNetBar.ButtonItem opennetfile;
        private DevComponents.DotNetBar.ButtonItem opennetdirectory;
        private DevComponents.DotNetBar.ButtonItem Translate;
        private System.ComponentModel.BackgroundWorker TranslateWorker;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;

    }
}

