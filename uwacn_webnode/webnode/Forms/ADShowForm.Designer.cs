namespace webnode.Forms
{
    partial class ADShowForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ADShowForm));
            this.ADwaveBox = new WaveBox.WaveBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.通道选择ToolStripMenuItem = new System.Windows.Forms.ToolStripComboBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ADwaveBox
            // 
            this.ADwaveBox.AudioFrameSize = 1024;
            this.ADwaveBox.BitsPerSample = 16;
            this.ADwaveBox.Channel = 1;
            this.ADwaveBox.ContextMenuStrip = this.contextMenuStrip1;
            this.ADwaveBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ADwaveBox.isPlaying = false;
            this.ADwaveBox.Location = new System.Drawing.Point(0, 0);
            this.ADwaveBox.MaxAmpShow = 32767;
            this.ADwaveBox.MaxFrequecyShow = 16000;
            this.ADwaveBox.Name = "ADwaveBox";
            this.ADwaveBox.SamlesPerSecond = 64000;
            this.ADwaveBox.Size = new System.Drawing.Size(888, 576);
            this.ADwaveBox.TabIndex = 0;
            this.ADwaveBox.TimeWindowSamples = 131072;
            this.ADwaveBox.Title = "波形显示";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.通道选择ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(213, 55);
            // 
            // 通道选择ToolStripMenuItem
            // 
            this.通道选择ToolStripMenuItem.Items.AddRange(new object[] {
            "通道1",
            "通道2",
            "通道3",
            "通道4"});
            this.通道选择ToolStripMenuItem.Name = "通道选择ToolStripMenuItem";
            this.通道选择ToolStripMenuItem.Size = new System.Drawing.Size(152, 25);
            this.通道选择ToolStripMenuItem.Text = "通道选择";
            this.通道选择ToolStripMenuItem.ToolTipText = "选择AD波形通道号";
            this.通道选择ToolStripMenuItem.SelectedIndexChanged += new System.EventHandler(this.通道选择ToolStripMenuItem_SelectedIndexChanged);
            // 
            // ADShowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 576);
            this.Controls.Add(this.ADwaveBox);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ADShowForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AD回传波形";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ADShowForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ADShowForm_FormClosed);
            this.Load += new System.EventHandler(this.ADShowForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WaveBox.WaveBox ADwaveBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripComboBox 通道选择ToolStripMenuItem;

    }
}