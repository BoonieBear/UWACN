namespace webnode.Forms
{
    partial class ConfForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfForm));
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.BuoyName = new DevComponents.DotNetBar.Controls.MaskedTextBoxAdv();
            this.langInput = new DevComponents.Editors.DoubleInput();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.latinput = new DevComponents.Editors.DoubleInput();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.COMM2Set = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.COMM3Set = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.EmitTypeBox = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.ConfBtn = new DevComponents.DotNetBar.ButtonX();
            this.Cancel = new DevComponents.DotNetBar.ButtonX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.NetCheck = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.net_off = new DevComponents.Editors.ComboItem();
            this.net_on = new DevComponents.Editors.ComboItem();
            this.EmitNum = new DevComponents.Editors.IntegerInput();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.NodeName = new DevComponents.DotNetBar.Controls.MaskedTextBoxAdv();
            this.GetGps = new DevComponents.DotNetBar.ButtonX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.NodeType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.AccessMode = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            ((System.ComponentModel.ISupportInitialize)(this.langInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.latinput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmitNum)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(13, 24);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(48, 19);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "浮标号";
            // 
            // BuoyName
            // 
            this.BuoyName.AsciiOnly = true;
            this.BuoyName.ButtonClear.Visible = true;
            this.BuoyName.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.BuoyName.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.BuoyName.Location = new System.Drawing.Point(67, 23);
            this.BuoyName.Mask = "99";
            this.BuoyName.Name = "BuoyName";
            this.BuoyName.Size = new System.Drawing.Size(38, 21);
            this.BuoyName.TabIndex = 1;
            this.BuoyName.Text = "00";
            this.BuoyName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.BuoyName.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // langInput
            // 
            // 
            // 
            // 
            this.langInput.BackgroundStyle.Class = "DateTimeInputBackground";
            this.langInput.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.langInput.DisplayFormat = "F06";
            this.langInput.Increment = 1D;
            this.langInput.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left;
            this.langInput.Location = new System.Drawing.Point(87, 64);
            this.langInput.MaxValue = 180D;
            this.langInput.MinValue = -180D;
            this.langInput.Name = "langInput";
            this.langInput.Size = new System.Drawing.Size(90, 21);
            this.langInput.TabIndex = 2;
            this.langInput.WatermarkText = "经度(西经-)";
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(13, 64);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(38, 21);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "经度";
            // 
            // latinput
            // 
            // 
            // 
            // 
            this.latinput.BackgroundStyle.Class = "DateTimeInputBackground";
            this.latinput.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.latinput.DisplayFormat = "F06";
            this.latinput.Increment = 1D;
            this.latinput.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left;
            this.latinput.Location = new System.Drawing.Point(278, 64);
            this.latinput.MaxValue = 180D;
            this.latinput.MinValue = -180D;
            this.latinput.Name = "latinput";
            this.latinput.Size = new System.Drawing.Size(96, 21);
            this.latinput.TabIndex = 2;
            this.latinput.WatermarkText = "纬度(南纬-)";
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(205, 64);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(45, 23);
            this.labelX3.TabIndex = 3;
            this.labelX3.Text = "纬度";
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(13, 106);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(64, 23);
            this.labelX4.TabIndex = 4;
            this.labelX4.Text = "串口2设备";
            // 
            // COMM2Set
            // 
            this.COMM2Set.DisplayMember = "Text";
            this.COMM2Set.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.COMM2Set.FormattingEnabled = true;
            this.COMM2Set.ItemHeight = 15;
            this.COMM2Set.Location = new System.Drawing.Point(87, 106);
            this.COMM2Set.Name = "COMM2Set";
            this.COMM2Set.Size = new System.Drawing.Size(90, 21);
            this.COMM2Set.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.COMM2Set.TabIndex = 5;
            this.COMM2Set.Text = "无设备";
            // 
            // labelX5
            // 
            this.labelX5.Location = new System.Drawing.Point(205, 106);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(67, 23);
            this.labelX5.TabIndex = 4;
            this.labelX5.Text = "串口3设备";
            // 
            // COMM3Set
            // 
            this.COMM3Set.DisplayMember = "Text";
            this.COMM3Set.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.COMM3Set.FormattingEnabled = true;
            this.COMM3Set.ItemHeight = 15;
            this.COMM3Set.Location = new System.Drawing.Point(278, 106);
            this.COMM3Set.Name = "COMM3Set";
            this.COMM3Set.Size = new System.Drawing.Size(96, 21);
            this.COMM3Set.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.COMM3Set.TabIndex = 5;
            this.COMM3Set.Text = "SBE39_TD";
            // 
            // EmitTypeBox
            // 
            this.EmitTypeBox.DisplayMember = "Text";
            this.EmitTypeBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.EmitTypeBox.FormattingEnabled = true;
            this.EmitTypeBox.ItemHeight = 15;
            this.EmitTypeBox.Location = new System.Drawing.Point(87, 146);
            this.EmitTypeBox.Name = "EmitTypeBox";
            this.EmitTypeBox.Size = new System.Drawing.Size(90, 21);
            this.EmitTypeBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.EmitTypeBox.TabIndex = 6;
            this.EmitTypeBox.Text = "PWM发射机";
            // 
            // labelX6
            // 
            this.labelX6.Location = new System.Drawing.Point(13, 144);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(72, 23);
            this.labelX6.TabIndex = 7;
            this.labelX6.Text = "发射机类型";
            // 
            // ConfBtn
            // 
            this.ConfBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ConfBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ConfBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ConfBtn.Location = new System.Drawing.Point(380, 146);
            this.ConfBtn.Name = "ConfBtn";
            this.ConfBtn.Size = new System.Drawing.Size(75, 23);
            this.ConfBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.ConfBtn.TabIndex = 8;
            this.ConfBtn.Text = "确定";

            // 
            // Cancel
            // 
            this.Cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(379, 175);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.Cancel.TabIndex = 9;
            this.Cancel.Text = "取消";
            // 
            // labelX7
            // 
            this.labelX7.Location = new System.Drawing.Point(205, 146);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(72, 23);
            this.labelX7.TabIndex = 10;
            this.labelX7.Text = "网络开关";
            // 
            // labelX8
            // 
            this.labelX8.Location = new System.Drawing.Point(13, 180);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(72, 23);
            this.labelX8.TabIndex = 11;
            this.labelX8.Text = "换能器个数";
            // 
            // NetCheck
            // 
            this.NetCheck.DisplayMember = "Text";
            this.NetCheck.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.NetCheck.FormattingEnabled = true;
            this.NetCheck.ItemHeight = 15;
            this.NetCheck.Items.AddRange(new object[] {
            this.net_off,
            this.net_on});
            this.NetCheck.Location = new System.Drawing.Point(278, 146);
            this.NetCheck.Name = "NetCheck";
            this.NetCheck.Size = new System.Drawing.Size(96, 21);
            this.NetCheck.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.NetCheck.TabIndex = 12;
            this.NetCheck.Text = "关";
            // 
            // net_off
            // 
            this.net_off.Text = "关";
            // 
            // net_on
            // 
            this.net_on.Text = "开";
            // 
            // EmitNum
            // 
            // 
            // 
            // 
            this.EmitNum.BackgroundStyle.Class = "DateTimeInputBackground";
            this.EmitNum.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.EmitNum.Location = new System.Drawing.Point(87, 182);
            this.EmitNum.MaxValue = 8;
            this.EmitNum.MinValue = 1;
            this.EmitNum.Name = "EmitNum";
            this.EmitNum.ShowUpDown = true;
            this.EmitNum.Size = new System.Drawing.Size(90, 21);
            this.EmitNum.TabIndex = 13;
            this.EmitNum.Value = 1;
            // 
            // labelX9
            // 
            this.labelX9.Location = new System.Drawing.Point(111, 22);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(56, 23);
            this.labelX9.TabIndex = 14;
            this.labelX9.Text = "节点编号";
            // 
            // NodeName
            // 
            this.NodeName.AsciiOnly = true;
            // 
            // 
            // 
            this.NodeName.BackgroundStyle.Class = "TextBoxBorder";
            this.NodeName.ButtonClear.Visible = true;
            this.NodeName.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.NodeName.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.NodeName.Location = new System.Drawing.Point(173, 22);
            this.NodeName.Mask = "99";
            this.NodeName.Name = "NodeName";
            this.NodeName.Size = new System.Drawing.Size(42, 21);
            this.NodeName.TabIndex = 15;
            this.NodeName.Text = "";
            this.NodeName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.NodeName.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // GetGps
            // 
            this.GetGps.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.GetGps.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.GetGps.Image = ((System.Drawing.Image)(resources.GetObject("GetGps.Image")));
            this.GetGps.Location = new System.Drawing.Point(380, 62);
            this.GetGps.Name = "GetGps";
            this.GetGps.Size = new System.Drawing.Size(38, 23);
            this.GetGps.TabIndex = 16;
            this.GetGps.Tooltip = "使用当前获取的GPS实时数据填充经纬度";
            this.GetGps.Click += new System.EventHandler(this.GetGps_Click);
            // 
            // labelX10
            // 
            this.labelX10.Location = new System.Drawing.Point(221, 22);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(56, 23);
            this.labelX10.TabIndex = 17;
            this.labelX10.Text = "节点类型";
            // 
            // NodeType
            // 
            this.NodeType.DisplayMember = "Text";
            this.NodeType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.NodeType.FormattingEnabled = true;
            this.NodeType.ItemHeight = 15;
            this.NodeType.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.NodeType.Location = new System.Drawing.Point(278, 22);
            this.NodeType.Name = "NodeType";
            this.NodeType.Size = new System.Drawing.Size(72, 21);
            this.NodeType.TabIndex = 18;
            this.NodeType.Text = "静态节点";
            this.NodeType.SelectedIndexChanged += new System.EventHandler(this.NodeType_SelectedIndexChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "静态节点";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "动态节点";
            // 
            // labelX11
            // 
            this.labelX11.Location = new System.Drawing.Point(205, 180);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(56, 23);
            this.labelX11.TabIndex = 19;
            this.labelX11.Text = "接入模式";
            // 
            // AccessMode
            // 
            this.AccessMode.DisplayMember = "Text";
            this.AccessMode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.AccessMode.Enabled = false;
            this.AccessMode.FormattingEnabled = true;
            this.AccessMode.ItemHeight = 15;
            this.AccessMode.Items.AddRange(new object[] {
            this.comboItem3,
            this.comboItem4});
            this.AccessMode.Location = new System.Drawing.Point(278, 180);
            this.AccessMode.Name = "AccessMode";
            this.AccessMode.Size = new System.Drawing.Size(46, 21);
            this.AccessMode.TabIndex = 20;
            this.AccessMode.Text = "0";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "0";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "1";
            // 
            // ConfForm
            // 
            this.AcceptButton = this.ConfBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(463, 218);
            this.ControlBox = false;
            this.Controls.Add(this.AccessMode);
            this.Controls.Add(this.labelX11);
            this.Controls.Add(this.NodeType);
            this.Controls.Add(this.labelX10);
            this.Controls.Add(this.GetGps);
            this.Controls.Add(this.NodeName);
            this.Controls.Add(this.labelX9);
            this.Controls.Add(this.EmitNum);
            this.Controls.Add(this.NetCheck);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.ConfBtn);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.EmitTypeBox);
            this.Controls.Add(this.COMM3Set);
            this.Controls.Add(this.COMM2Set);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.latinput);
            this.Controls.Add(this.langInput);
            this.Controls.Add(this.BuoyName);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "配置";
            this.Load += new System.EventHandler(this.ConfForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.langInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.latinput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmitNum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        public DevComponents.Editors.DoubleInput langInput;
        public DevComponents.Editors.DoubleInput latinput;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.ButtonX ConfBtn;
        private DevComponents.DotNetBar.ButtonX Cancel;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX8;
        public DevComponents.DotNetBar.Controls.MaskedTextBoxAdv BuoyName;
        private DevComponents.DotNetBar.LabelX labelX9;
        public DevComponents.DotNetBar.Controls.MaskedTextBoxAdv NodeName;
        private DevComponents.Editors.ComboItem net_off;
        private DevComponents.Editors.ComboItem net_on;
        public DevComponents.DotNetBar.Controls.ComboBoxEx COMM2Set;
        public DevComponents.DotNetBar.Controls.ComboBoxEx COMM3Set;
        public DevComponents.DotNetBar.Controls.ComboBoxEx EmitTypeBox;
        public DevComponents.DotNetBar.Controls.ComboBoxEx NetCheck;
        public DevComponents.Editors.IntegerInput EmitNum;
        private DevComponents.DotNetBar.ButtonX GetGps;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        public DevComponents.DotNetBar.Controls.ComboBoxEx NodeType;
        private DevComponents.DotNetBar.LabelX labelX11;
        public DevComponents.DotNetBar.Controls.ComboBoxEx AccessMode;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
    }
}