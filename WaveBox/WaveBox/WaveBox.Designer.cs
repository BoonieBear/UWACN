namespace WaveBox
{
    partial class WaveBox
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.YSection = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.FrequencyDomainBox = new System.Windows.Forms.PictureBox();
            this.TimeDomainBox = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.YSection)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FrequencyDomainBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeDomainBox)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // YSection
            // 
            this.YSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YSection.Location = new System.Drawing.Point(802, 0);
            this.YSection.Margin = new System.Windows.Forms.Padding(0);
            this.YSection.Name = "YSection";
            this.tableLayoutPanel1.SetRowSpan(this.YSection, 2);
            this.YSection.Size = new System.Drawing.Size(50, 582);
            this.YSection.TabIndex = 2;
            this.YSection.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.FrequencyDomainBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TimeDomainBox);
            this.tableLayoutPanel1.SetRowSpan(this.splitContainer1, 2);
            this.splitContainer1.Size = new System.Drawing.Size(796, 576);
            this.splitContainer1.SplitterDistance = 286;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 3;
            // 
            // FrequencyDomainBox
            // 
            this.FrequencyDomainBox.BackColor = System.Drawing.SystemColors.ControlText;
            this.FrequencyDomainBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FrequencyDomainBox.Location = new System.Drawing.Point(0, 0);
            this.FrequencyDomainBox.Name = "FrequencyDomainBox";
            this.FrequencyDomainBox.Size = new System.Drawing.Size(796, 286);
            this.FrequencyDomainBox.TabIndex = 0;
            this.FrequencyDomainBox.TabStop = false;
            // 
            // TimeDomainBox
            // 
            this.TimeDomainBox.BackColor = System.Drawing.SystemColors.ControlText;
            this.TimeDomainBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TimeDomainBox.Location = new System.Drawing.Point(0, 0);
            this.TimeDomainBox.Name = "TimeDomainBox";
            this.TimeDomainBox.Size = new System.Drawing.Size(796, 289);
            this.TimeDomainBox.TabIndex = 0;
            this.TimeDomainBox.TabStop = false;
            // 
            // timer
            // 
            this.timer.Interval = 80;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timer1
            // 
            this.timer1.Interval = 80;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 80;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.YSection, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(852, 582);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // WaveBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "WaveBox";
            this.Size = new System.Drawing.Size(852, 582);
            this.Load += new System.EventHandler(this.WaveBox_Load);
            this.Resize += new System.EventHandler(this.WaveBox_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.YSection)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FrequencyDomainBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeDomainBox)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox YSection;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox FrequencyDomainBox;
        private System.Windows.Forms.PictureBox TimeDomainBox;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
