namespace ADplayer
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
            this.components = new System.ComponentModel.Container();
            this.play = new DevComponents.DotNetBar.ButtonX();
            this.SampleSet = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.BitLen = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SampleLen = new System.Windows.Forms.Label();
            this.FrameLen = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.waveBox = new WaveBox.WaveBox();
            this.StopBtn = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // play
            // 
            this.play.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.play.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.play.Location = new System.Drawing.Point(984, 573);
            this.play.Name = "play";
            this.play.Size = new System.Drawing.Size(75, 23);
            this.play.TabIndex = 0;
            this.play.Text = "循环播放";
            this.play.Click += new System.EventHandler(this.play_Click);
            // 
            // SampleSet
            // 
            // 
            // 
            // 
            this.SampleSet.Border.Class = "TextBoxBorder";
            this.SampleSet.Location = new System.Drawing.Point(984, 487);
            this.SampleSet.Name = "SampleSet";
            this.SampleSet.Size = new System.Drawing.Size(100, 21);
            this.SampleSet.TabIndex = 1;
            // 
            // BitLen
            // 
            // 
            // 
            // 
            this.BitLen.Border.Class = "TextBoxBorder";
            this.BitLen.Location = new System.Drawing.Point(984, 514);
            this.BitLen.Name = "BitLen";
            this.BitLen.Size = new System.Drawing.Size(100, 21);
            this.BitLen.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(937, 493);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "采样率";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(937, 517);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "bit宽度";
            // 
            // SampleLen
            // 
            this.SampleLen.AutoSize = true;
            this.SampleLen.Location = new System.Drawing.Point(931, 543);
            this.SampleLen.Name = "SampleLen";
            this.SampleLen.Size = new System.Drawing.Size(53, 12);
            this.SampleLen.TabIndex = 5;
            this.SampleLen.Text = "采样长度";
            // 
            // FrameLen
            // 
            // 
            // 
            // 
            this.FrameLen.Border.Class = "TextBoxBorder";
            this.FrameLen.Location = new System.Drawing.Point(984, 541);
            this.FrameLen.Name = "FrameLen";
            this.FrameLen.Size = new System.Drawing.Size(100, 21);
            this.FrameLen.TabIndex = 6;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // timer
            // 
            this.timer.Interval = 64;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // waveBox
            // 
            this.waveBox.AudioFrameSize = 1024;
            this.waveBox.BitsPerSample = 16;
            this.waveBox.Channel = 1;
            this.waveBox.isPlaying = false;
            this.waveBox.Location = new System.Drawing.Point(12, 35);
            this.waveBox.Name = "waveBox";
            this.waveBox.SamlesPerSecond = 64000;
            this.waveBox.Size = new System.Drawing.Size(852, 582);
            this.waveBox.TabIndex = 7;
            this.waveBox.TimeWindowSamples = 131072;
            this.waveBox.Title = "波形显示";
            // 
            // StopBtn
            // 
            this.StopBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.StopBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.StopBtn.Location = new System.Drawing.Point(984, 602);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(75, 23);
            this.StopBtn.TabIndex = 8;
            this.StopBtn.Text = "停止";
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 643);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.waveBox);
            this.Controls.Add(this.FrameLen);
            this.Controls.Add(this.SampleLen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BitLen);
            this.Controls.Add(this.SampleSet);
            this.Controls.Add(this.play);
            this.Name = "Form1";
            this.Text = "播放AD";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX play;
        private DevComponents.DotNetBar.Controls.TextBoxX SampleSet;
        private DevComponents.DotNetBar.Controls.TextBoxX BitLen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label SampleLen;
        private DevComponents.DotNetBar.Controls.TextBoxX FrameLen;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Timer timer;
        private WaveBox.WaveBox waveBox;
        private DevComponents.DotNetBar.ButtonX StopBtn;
    }
}

