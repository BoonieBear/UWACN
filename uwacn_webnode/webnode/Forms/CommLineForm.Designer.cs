namespace webnode.Forms
{
    partial class CommLineForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommLineForm));
            this.Menubar = new DevComponents.DotNetBar.Bar();
            this.AlwaysOnFront = new System.Windows.Forms.CheckBox();
            this.ConnNodeBtn = new DevComponents.DotNetBar.ButtonX();
            this.label4 = new System.Windows.Forms.Label();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.CommandLineBox = new System.Windows.Forms.RichTextBox();
            this.BoxMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ClearCommandLine = new System.Windows.Forms.ToolStripMenuItem();
            this.NodeLinker = new System.ComponentModel.BackgroundWorker();
            this.NodeReceiver = new System.ComponentModel.BackgroundWorker();
            this.DLoadDataWorker = new System.ComponentModel.BackgroundWorker();
            this.CommAnsReceiver = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Menubar)).BeginInit();
            this.Menubar.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.BoxMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // Menubar
            // 
            this.Menubar.Controls.Add(this.AlwaysOnFront);
            this.Menubar.Controls.Add(this.ConnNodeBtn);
            this.Menubar.Controls.Add(this.label4);
            this.Menubar.Dock = System.Windows.Forms.DockStyle.Top;
            this.Menubar.DockedBorderStyle = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.Menubar.Location = new System.Drawing.Point(0, 0);
            this.Menubar.Name = "Menubar";
            this.Menubar.Size = new System.Drawing.Size(606, 31);
            this.Menubar.Stretch = true;
            this.Menubar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.Menubar.TabIndex = 3;
            this.Menubar.TabStop = false;
            this.Menubar.Text = "bar1";
            // 
            // AlwaysOnFront
            // 
            this.AlwaysOnFront.AutoSize = true;
            this.AlwaysOnFront.Location = new System.Drawing.Point(529, 9);
            this.AlwaysOnFront.Name = "AlwaysOnFront";
            this.AlwaysOnFront.Size = new System.Drawing.Size(72, 16);
            this.AlwaysOnFront.TabIndex = 6;
            this.AlwaysOnFront.Text = "总在最前";
            this.AlwaysOnFront.UseVisualStyleBackColor = true;
            this.AlwaysOnFront.CheckedChanged += new System.EventHandler(this.AlwaysOnFront_CheckedChanged);
            // 
            // ConnNodeBtn
            // 
            this.ConnNodeBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ConnNodeBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ConnNodeBtn.Location = new System.Drawing.Point(12, 5);
            this.ConnNodeBtn.Name = "ConnNodeBtn";
            this.ConnNodeBtn.Size = new System.Drawing.Size(75, 23);
            this.ConnNodeBtn.TabIndex = 5;
            this.ConnNodeBtn.Text = "连接节点";
            this.ConnNodeBtn.Click += new System.EventHandler(this.ConnNodeBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(523, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 12);
            this.label4.TabIndex = 4;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.Controls.Add(this.CommandLineBox);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 31);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(606, 428);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 4;
            this.panelEx1.Text = "panelEx1";
            // 
            // CommandLineBox
            // 
            this.CommandLineBox.AutoWordSelection = true;
            this.CommandLineBox.ContextMenuStrip = this.BoxMenuStrip;
            this.CommandLineBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CommandLineBox.Location = new System.Drawing.Point(0, 0);
            this.CommandLineBox.Name = "CommandLineBox";
            this.CommandLineBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.CommandLineBox.Size = new System.Drawing.Size(606, 428);
            this.CommandLineBox.TabIndex = 0;
            this.CommandLineBox.Text = "";
            this.CommandLineBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommandLineBox_KeyDown);
            this.CommandLineBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CommandLineBox_KeyPress);
            // 
            // BoxMenuStrip
            // 
            this.BoxMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearCommandLine});
            this.BoxMenuStrip.Name = "BoxMenuStrip";
            this.BoxMenuStrip.Size = new System.Drawing.Size(131, 26);
            // 
            // ClearCommandLine
            // 
            this.ClearCommandLine.Name = "ClearCommandLine";
            this.ClearCommandLine.Size = new System.Drawing.Size(130, 22);
            this.ClearCommandLine.Text = "清空命令行";
            this.ClearCommandLine.Click += new System.EventHandler(this.ClearCommandLine_Click);
            // 
            // NodeLinker
            // 
            this.NodeLinker.WorkerSupportsCancellation = true;
            this.NodeLinker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.NodeLinker_DoWork);
            this.NodeLinker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.NodeLinker_RunWorkerCompleted);
            // 
            // NodeReceiver
            // 
            this.NodeReceiver.WorkerSupportsCancellation = true;
            this.NodeReceiver.DoWork += new System.ComponentModel.DoWorkEventHandler(this.NodeReceiver_DoWork);
            this.NodeReceiver.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.NodeReceiver_RunWorkerCompleted);
            // 
            // DLoadDataWorker
            // 
            this.DLoadDataWorker.WorkerReportsProgress = true;
            this.DLoadDataWorker.WorkerSupportsCancellation = true;
            this.DLoadDataWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DLoadDataWorker_DoWork);
            this.DLoadDataWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.DLoadDataWorker_RunWorkerCompleted);
            this.DLoadDataWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.DLoadDataWorker_ProgressChanged);
            // 
            // CommAnsReceiver
            // 
            this.CommAnsReceiver.WorkerSupportsCancellation = true;
            this.CommAnsReceiver.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CommAnsReceiver_DoWork);
            this.CommAnsReceiver.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CommAnsReceiver_RunWorkerCompleted);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CommLineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 459);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.Menubar);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CommLineForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "命令行";
            this.Load += new System.EventHandler(this.CommLineForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CommLineForm_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CommLineForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.Menubar)).EndInit();
            this.Menubar.ResumeLayout(false);
            this.Menubar.PerformLayout();
            this.panelEx1.ResumeLayout(false);
            this.BoxMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar Menubar;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.RichTextBox CommandLineBox;
        public System.ComponentModel.BackgroundWorker NodeLinker;
        private System.ComponentModel.BackgroundWorker NodeReceiver;
        private System.Windows.Forms.ContextMenuStrip BoxMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ClearCommandLine;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker CommAnsReceiver;
        public System.ComponentModel.BackgroundWorker DLoadDataWorker;
        private System.Windows.Forms.Timer timer1;
        public DevComponents.DotNetBar.ButtonX ConnNodeBtn;
        private System.Windows.Forms.CheckBox AlwaysOnFront;
    }
}