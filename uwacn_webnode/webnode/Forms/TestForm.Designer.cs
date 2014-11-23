namespace webnode.Forms
{
    partial class TestForm
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
            this.TaskBox = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.OpenTestFile = new System.Windows.Forms.OpenFileDialog();
            this.testlog = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.teststart = new DevComponents.DotNetBar.ButtonX();
            this.label = new DevComponents.DotNetBar.LabelX();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.StopTest = new DevComponents.DotNetBar.ButtonX();
            this.testwait = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // TaskBox
            // 
            // 
            // 
            // 
            this.TaskBox.Border.Class = "TextBoxBorder";
            this.TaskBox.Location = new System.Drawing.Point(12, 12);
            this.TaskBox.Multiline = true;
            this.TaskBox.Name = "TaskBox";
            this.TaskBox.ReadOnly = true;
            this.TaskBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TaskBox.Size = new System.Drawing.Size(263, 236);
            this.TaskBox.TabIndex = 0;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(12, 254);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(80, 23);
            this.buttonX1.TabIndex = 1;
            this.buttonX1.Text = "选择测试脚本";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // OpenTestFile
            // 
            this.OpenTestFile.Title = "选择测试脚本";
            // 
            // testlog
            // 
            // 
            // 
            // 
            this.testlog.Border.Class = "TextBoxBorder";
            this.testlog.Location = new System.Drawing.Point(287, 12);
            this.testlog.Multiline = true;
            this.testlog.Name = "testlog";
            this.testlog.ReadOnly = true;
            this.testlog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.testlog.Size = new System.Drawing.Size(263, 236);
            this.testlog.TabIndex = 2;
            // 
            // teststart
            // 
            this.teststart.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.teststart.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.teststart.Location = new System.Drawing.Point(287, 254);
            this.teststart.Name = "teststart";
            this.teststart.Size = new System.Drawing.Size(75, 23);
            this.teststart.TabIndex = 5;
            this.teststart.Text = "开始测试";
            this.teststart.Click += new System.EventHandler(this.teststart_Click);
            // 
            // label
            // 
            this.label.Location = new System.Drawing.Point(98, 254);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(177, 23);
            this.label.TabIndex = 6;
            this.label.Text = "---";
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // StopTest
            // 
            this.StopTest.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.StopTest.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.StopTest.Enabled = false;
            this.StopTest.Location = new System.Drawing.Point(475, 254);
            this.StopTest.Name = "StopTest";
            this.StopTest.Size = new System.Drawing.Size(75, 23);
            this.StopTest.TabIndex = 7;
            this.StopTest.Text = "停止测试";
            this.StopTest.Click += new System.EventHandler(this.StopTest_Click);
            // 
            // testwait
            // 
            this.testwait.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.testwait.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.testwait.Enabled = false;
            this.testwait.Location = new System.Drawing.Point(382, 254);
            this.testwait.Name = "testwait";
            this.testwait.Size = new System.Drawing.Size(75, 23);
            this.testwait.TabIndex = 8;
            this.testwait.Text = "暂停测试";
            this.testwait.Click += new System.EventHandler(this.testwait_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 286);
            this.Controls.Add(this.testwait);
            this.Controls.Add(this.StopTest);
            this.Controls.Add(this.label);
            this.Controls.Add(this.teststart);
            this.Controls.Add(this.testlog);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.TaskBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自动测试";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX TaskBox;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.OpenFileDialog OpenTestFile;
        private DevComponents.DotNetBar.Controls.TextBoxX testlog;
        private DevComponents.DotNetBar.ButtonX teststart;
        private DevComponents.DotNetBar.LabelX label;
        private System.Windows.Forms.Timer timer;
        private DevComponents.DotNetBar.ButtonX StopTest;
        private DevComponents.DotNetBar.ButtonX testwait;
    }
}