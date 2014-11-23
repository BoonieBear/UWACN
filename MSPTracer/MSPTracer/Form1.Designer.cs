namespace MSPTracer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OpenSerial = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.Serialbaud = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem15 = new DevComponents.Editors.ComboItem();
            this.comboItem16 = new DevComponents.Editors.ComboItem();
            this.comboItem17 = new DevComponents.Editors.ComboItem();
            this.SerialPort = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.comboItem7 = new DevComponents.Editors.ComboItem();
            this.comboItem8 = new DevComponents.Editors.ComboItem();
            this.comboItem9 = new DevComponents.Editors.ComboItem();
            this.comboItem10 = new DevComponents.Editors.ComboItem();
            this.comboItem11 = new DevComponents.Editors.ComboItem();
            this.comboItem12 = new DevComponents.Editors.ComboItem();
            this.comboItem13 = new DevComponents.Editors.ComboItem();
            this.comboItem14 = new DevComponents.Editors.ComboItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.CircleCheck = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.CircleTime = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem18 = new DevComponents.Editors.ComboItem();
            this.comboItem19 = new DevComponents.Editors.ComboItem();
            this.comboItem20 = new DevComponents.Editors.ComboItem();
            this.comboItem21 = new DevComponents.Editors.ComboItem();
            this.comboItem22 = new DevComponents.Editors.ComboItem();
            this.comboItem23 = new DevComponents.Editors.ComboItem();
            this.comboItem24 = new DevComponents.Editors.ComboItem();
            this.comboItem25 = new DevComponents.Editors.ComboItem();
            this.comboItem26 = new DevComponents.Editors.ComboItem();
            this.comboItem27 = new DevComponents.Editors.ComboItem();
            this.SaveBtn = new DevComponents.DotNetBar.ButtonX();
            this.LoadBtn = new DevComponents.DotNetBar.ButtonX();
            this.CommandBox = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.Commandlist = new System.Windows.Forms.ListBox();
            this.GpsLog = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.numberlabel = new DevComponents.DotNetBar.LabelX();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // skinEngine1
            // 
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = "F:\\学习\\计算机语言\\C#.net\\C#\\60种皮肤界面ssk文件\\ssk皮肤\\RealOne\\RealOne.ssk";
            this.skinEngine1.SkinStreamMain = ((System.IO.Stream)(resources.GetObject("skinEngine1.SkinStreamMain")));
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OpenSerial);
            this.groupBox1.Controls.Add(this.Serialbaud);
            this.groupBox1.Controls.Add(this.SerialPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(534, 63);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "通信设置";
            // 
            // OpenSerial
            // 
            this.OpenSerial.Location = new System.Drawing.Point(417, 19);
            this.OpenSerial.Name = "OpenSerial";
            this.OpenSerial.Size = new System.Drawing.Size(75, 23);
            this.OpenSerial.TabIndex = 5;
            this.OpenSerial.Text = "打开串口";
            this.OpenSerial.CheckedChanged += new System.EventHandler(this.OpenSerial_CheckedChanged);
            // 
            // Serialbaud
            // 
            this.Serialbaud.DisplayMember = "Text";
            this.Serialbaud.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Serialbaud.FormattingEnabled = true;
            this.Serialbaud.ItemHeight = 15;
            this.Serialbaud.Items.AddRange(new object[] {
            this.comboItem15,
            this.comboItem16,
            this.comboItem17});
            this.Serialbaud.Location = new System.Drawing.Point(268, 20);
            this.Serialbaud.Name = "Serialbaud";
            this.Serialbaud.Size = new System.Drawing.Size(121, 21);
            this.Serialbaud.TabIndex = 4;
            // 
            // comboItem15
            // 
            this.comboItem15.Text = "4800";
            // 
            // comboItem16
            // 
            this.comboItem16.Text = "9600";
            // 
            // comboItem17
            // 
            this.comboItem17.Text = "115200";
            // 
            // SerialPort
            // 
            this.SerialPort.DisplayMember = "Text";
            this.SerialPort.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.SerialPort.FormattingEnabled = true;
            this.SerialPort.ItemHeight = 15;
            this.SerialPort.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4,
            this.comboItem5,
            this.comboItem6,
            this.comboItem7,
            this.comboItem8,
            this.comboItem9,
            this.comboItem10,
            this.comboItem11,
            this.comboItem12,
            this.comboItem13,
            this.comboItem14});
            this.SerialPort.Location = new System.Drawing.Point(83, 20);
            this.SerialPort.Name = "SerialPort";
            this.SerialPort.Size = new System.Drawing.Size(121, 21);
            this.SerialPort.TabIndex = 3;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "COM1";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "COM2";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "COM3";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "COM4";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "COM5";
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "COM6";
            // 
            // comboItem7
            // 
            this.comboItem7.Text = "COM7";
            // 
            // comboItem8
            // 
            this.comboItem8.Text = "COM8";
            // 
            // comboItem9
            // 
            this.comboItem9.Text = "COM9";
            // 
            // comboItem10
            // 
            this.comboItem10.Text = "COM10";
            // 
            // comboItem11
            // 
            this.comboItem11.Text = "COM11";
            // 
            // comboItem12
            // 
            this.comboItem12.Text = "COM12";
            // 
            // comboItem13
            // 
            this.comboItem13.Text = "COM13";
            // 
            // comboItem14
            // 
            this.comboItem14.Text = "COM14";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "波特率";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "串口号";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numberlabel);
            this.groupBox2.Controls.Add(this.buttonX1);
            this.groupBox2.Controls.Add(this.CircleCheck);
            this.groupBox2.Controls.Add(this.CircleTime);
            this.groupBox2.Controls.Add(this.SaveBtn);
            this.groupBox2.Controls.Add(this.LoadBtn);
            this.groupBox2.Controls.Add(this.CommandBox);
            this.groupBox2.Controls.Add(this.Commandlist);
            this.groupBox2.Location = new System.Drawing.Point(12, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(534, 237);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "命令打包";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Enabled = false;
            this.buttonX1.Location = new System.Drawing.Point(442, 206);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Shape = new DevComponents.DotNetBar.EllipticalShapeDescriptor();
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.TabIndex = 15;
            this.buttonX1.Text = "发送";
            this.buttonX1.Tooltip = "下发数据";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // CircleCheck
            // 
            this.CircleCheck.Location = new System.Drawing.Point(382, 168);
            this.CircleCheck.Name = "CircleCheck";
            this.CircleCheck.Size = new System.Drawing.Size(55, 23);
            this.CircleCheck.TabIndex = 14;
            this.CircleCheck.Text = "循环";
            this.CircleCheck.CheckedChanged += new System.EventHandler(this.CircleCheck_CheckedChanged);
            // 
            // CircleTime
            // 
            this.CircleTime.DisplayMember = "Text";
            this.CircleTime.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CircleTime.Enabled = false;
            this.CircleTime.FormattingEnabled = true;
            this.CircleTime.ItemHeight = 15;
            this.CircleTime.Items.AddRange(new object[] {
            this.comboItem18,
            this.comboItem19,
            this.comboItem20,
            this.comboItem21,
            this.comboItem22,
            this.comboItem23,
            this.comboItem24,
            this.comboItem25,
            this.comboItem26,
            this.comboItem27});
            this.CircleTime.Location = new System.Drawing.Point(442, 168);
            this.CircleTime.Name = "CircleTime";
            this.CircleTime.Size = new System.Drawing.Size(83, 21);
            this.CircleTime.TabIndex = 13;
            this.CircleTime.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty;
            this.CircleTime.WatermarkText = "循环发送时间";
            // 
            // comboItem18
            // 
            this.comboItem18.Text = "1";
            // 
            // comboItem19
            // 
            this.comboItem19.Text = "3";
            // 
            // comboItem20
            // 
            this.comboItem20.Text = "5";
            // 
            // comboItem21
            // 
            this.comboItem21.Text = "10";
            // 
            // comboItem22
            // 
            this.comboItem22.Text = "20";
            // 
            // comboItem23
            // 
            this.comboItem23.Text = "30";
            // 
            // comboItem24
            // 
            this.comboItem24.Text = "60";
            // 
            // comboItem25
            // 
            this.comboItem25.Text = "120";
            // 
            // comboItem26
            // 
            this.comboItem26.Text = "150";
            // 
            // comboItem27
            // 
            this.comboItem27.Text = "300";
            // 
            // SaveBtn
            // 
            this.SaveBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.SaveBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.SaveBtn.Image = ((System.Drawing.Image)(resources.GetObject("SaveBtn.Image")));
            this.SaveBtn.Location = new System.Drawing.Point(301, 168);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(27, 23);
            this.SaveBtn.TabIndex = 12;
            this.SaveBtn.Tooltip = "保存当前数据";
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // LoadBtn
            // 
            this.LoadBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.LoadBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.LoadBtn.Image = ((System.Drawing.Image)(resources.GetObject("LoadBtn.Image")));
            this.LoadBtn.Location = new System.Drawing.Point(268, 168);
            this.LoadBtn.Name = "LoadBtn";
            this.LoadBtn.Size = new System.Drawing.Size(27, 23);
            this.LoadBtn.TabIndex = 11;
            this.LoadBtn.Tooltip = "载入文件";
            this.LoadBtn.Click += new System.EventHandler(this.LoadBtn_Click);
            // 
            // CommandBox
            // 
            // 
            // 
            // 
            this.CommandBox.Border.Class = "TextBoxBorder";
            this.CommandBox.Location = new System.Drawing.Point(268, 33);
            this.CommandBox.Multiline = true;
            this.CommandBox.Name = "CommandBox";
            this.CommandBox.Size = new System.Drawing.Size(257, 125);
            this.CommandBox.TabIndex = 10;
            this.CommandBox.WatermarkText = "将要发送的数据";
            // 
            // Commandlist
            // 
            this.Commandlist.FormattingEnabled = true;
            this.Commandlist.HorizontalScrollbar = true;
            this.Commandlist.ItemHeight = 12;
            this.Commandlist.Location = new System.Drawing.Point(14, 33);
            this.Commandlist.Name = "Commandlist";
            this.Commandlist.Size = new System.Drawing.Size(247, 160);
            this.Commandlist.TabIndex = 3;
            this.Commandlist.SelectedIndexChanged += new System.EventHandler(this.Commandlist_SelectedIndexChanged);
            // 
            // GpsLog
            // 
            // 
            // 
            // 
            this.GpsLog.Border.Class = "TextBoxBorder";
            this.GpsLog.Location = new System.Drawing.Point(12, 335);
            this.GpsLog.Multiline = true;
            this.GpsLog.Name = "GpsLog";
            this.GpsLog.ReadOnly = true;
            this.GpsLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.GpsLog.Size = new System.Drawing.Size(534, 198);
            this.GpsLog.TabIndex = 8;
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // numberlabel
            // 
            this.numberlabel.Location = new System.Drawing.Point(14, 206);
            this.numberlabel.Name = "numberlabel";
            this.numberlabel.Size = new System.Drawing.Size(247, 23);
            this.numberlabel.TabIndex = 17;
            this.numberlabel.Text = "上位机校验错误次数：0 双击清零";
            this.numberlabel.DoubleClick += new System.EventHandler(this.numberlabel_DoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 543);
            this.Controls.Add(this.GpsLog);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Msp430简单调试程序v1.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.CheckBoxX OpenSerial;
        private DevComponents.DotNetBar.Controls.ComboBoxEx Serialbaud;
        private DevComponents.DotNetBar.Controls.ComboBoxEx SerialPort;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.Editors.ComboItem comboItem6;
        private DevComponents.Editors.ComboItem comboItem7;
        private DevComponents.Editors.ComboItem comboItem8;
        private DevComponents.Editors.ComboItem comboItem9;
        private DevComponents.Editors.ComboItem comboItem10;
        private DevComponents.Editors.ComboItem comboItem11;
        private DevComponents.Editors.ComboItem comboItem12;
        private DevComponents.Editors.ComboItem comboItem13;
        private DevComponents.Editors.ComboItem comboItem14;
        private DevComponents.Editors.ComboItem comboItem15;
        private DevComponents.Editors.ComboItem comboItem16;
        private DevComponents.Editors.ComboItem comboItem17;
        private DevComponents.DotNetBar.Controls.TextBoxX GpsLog;
        private System.Windows.Forms.ListBox Commandlist;
        private DevComponents.DotNetBar.Controls.TextBoxX CommandBox;
        private DevComponents.DotNetBar.ButtonX SaveBtn;
        private DevComponents.DotNetBar.ButtonX LoadBtn;
        private DevComponents.DotNetBar.Controls.ComboBoxEx CircleTime;
        private DevComponents.DotNetBar.Controls.CheckBoxX CircleCheck;
        private DevComponents.Editors.ComboItem comboItem18;
        private DevComponents.Editors.ComboItem comboItem19;
        private DevComponents.Editors.ComboItem comboItem20;
        private DevComponents.Editors.ComboItem comboItem21;
        private DevComponents.Editors.ComboItem comboItem22;
        private DevComponents.Editors.ComboItem comboItem23;
        private DevComponents.Editors.ComboItem comboItem24;
        private DevComponents.Editors.ComboItem comboItem25;
        private DevComponents.Editors.ComboItem comboItem26;
        private DevComponents.Editors.ComboItem comboItem27;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Timer timer;
        private DevComponents.DotNetBar.LabelX numberlabel;
    }
}

