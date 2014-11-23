namespace webnode.Forms
{
    partial class NewNodeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewNodeForm));
            this.ComfirmBtn = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.DescBox = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.LngBox = new DevComponents.Editors.DoubleInput();
            this.LatBox = new DevComponents.Editors.DoubleInput();
            this.NodeNameBox = new DevComponents.Editors.IntegerInput();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.ipaddress = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.GetGps = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.LngBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LatBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NodeNameBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ComfirmBtn
            // 
            this.ComfirmBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ComfirmBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ComfirmBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ComfirmBtn.Enabled = false;
            this.ComfirmBtn.Location = new System.Drawing.Point(301, 66);
            this.ComfirmBtn.Name = "ComfirmBtn";
            this.ComfirmBtn.Size = new System.Drawing.Size(75, 23);
            this.ComfirmBtn.TabIndex = 0;
            this.ComfirmBtn.Text = "添加";
            this.ComfirmBtn.Click += new System.EventHandler(this.ComfirmBtn_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonX1.Location = new System.Drawing.Point(382, 66);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.TabIndex = 1;
            this.buttonX1.Text = "取消";
            // 
            // DescBox
            // 
            // 
            // 
            // 
            this.DescBox.Border.Class = "TextBoxBorder";
            this.DescBox.Location = new System.Drawing.Point(79, 40);
            this.DescBox.Name = "DescBox";
            this.DescBox.Size = new System.Drawing.Size(378, 21);
            this.DescBox.TabIndex = 3;
            this.DescBox.Text = "IACAS";
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(61, 23);
            this.labelX1.TabIndex = 7;
            this.labelX1.Text = "节点编号";
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(12, 39);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(61, 23);
            this.labelX2.TabIndex = 8;
            this.labelX2.Text = "节点描述";
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(12, 66);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(61, 23);
            this.labelX3.TabIndex = 9;
            this.labelX3.Text = "节点位置";
            // 
            // LngBox
            // 
            // 
            // 
            // 
            this.LngBox.BackgroundStyle.Class = "DateTimeInputBackground";
            this.LngBox.ButtonFreeText.Checked = true;
            this.LngBox.DisplayFormat = "F08";
            this.LngBox.FieldNavigation = DevComponents.Editors.eInputFieldNavigation.Enter;
            this.LngBox.FreeTextEntryMode = true;
            this.LngBox.Increment = 1D;
            this.LngBox.Location = new System.Drawing.Point(79, 66);
            this.LngBox.MaxValue = 180D;
            this.LngBox.MinValue = -180D;
            this.LngBox.Name = "LngBox";
            this.LngBox.Size = new System.Drawing.Size(97, 21);
            this.LngBox.TabIndex = 11;
            this.LngBox.WatermarkText = "经度(西经-)";
            // 
            // LatBox
            // 
            // 
            // 
            // 
            this.LatBox.BackgroundStyle.Class = "DateTimeInputBackground";
            this.LatBox.ButtonFreeText.Checked = true;
            this.LatBox.DisplayFormat = "F08";
            this.LatBox.FieldNavigation = DevComponents.Editors.eInputFieldNavigation.Enter;
            this.LatBox.FreeTextEntryMode = true;
            this.LatBox.Increment = 1D;
            this.LatBox.Location = new System.Drawing.Point(189, 66);
            this.LatBox.MaxValue = 90D;
            this.LatBox.MinValue = 0D;
            this.LatBox.Name = "LatBox";
            this.LatBox.Size = new System.Drawing.Size(80, 21);
            this.LatBox.TabIndex = 12;
            this.LatBox.WatermarkText = "纬度(南纬-)";
            // 
            // NodeNameBox
            // 
            // 
            // 
            // 
            this.NodeNameBox.BackgroundStyle.Class = "DateTimeInputBackground";
            this.NodeNameBox.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.NodeNameBox.Location = new System.Drawing.Point(80, 12);
            this.NodeNameBox.MaxValue = 63;
            this.NodeNameBox.MinValue = 0;
            this.NodeNameBox.Name = "NodeNameBox";
            this.NodeNameBox.Size = new System.Drawing.Size(80, 21);
            this.NodeNameBox.TabIndex = 13;
            this.NodeNameBox.WatermarkText = "节点编号";
            this.NodeNameBox.ValueChanged += new System.EventHandler(this.NodeNameBox_ValueChanged);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Image = ((System.Drawing.Image)(resources.GetObject("buttonX2.Image")));
            this.buttonX2.Location = new System.Drawing.Point(275, 67);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Shape = new DevComponents.DotNetBar.EllipticalShapeDescriptor();
            this.buttonX2.Size = new System.Drawing.Size(20, 20);
            this.buttonX2.TabIndex = 10;
            this.buttonX2.Tooltip = "在地图上选取位置";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(189, 12);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(53, 23);
            this.labelX4.TabIndex = 14;
            this.labelX4.Text = "节点IP";
            // 
            // ipaddress
            // 
            // 
            // 
            // 
            this.ipaddress.Border.Class = "TextBoxBorder";
            this.ipaddress.Location = new System.Drawing.Point(237, 12);
            this.ipaddress.Name = "ipaddress";
            this.ipaddress.Size = new System.Drawing.Size(118, 21);
            this.ipaddress.TabIndex = 15;
            // 
            // GetGps
            // 
            this.GetGps.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.GetGps.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.GetGps.Location = new System.Drawing.Point(382, 12);
            this.GetGps.Name = "GetGps";
            this.GetGps.Size = new System.Drawing.Size(75, 23);
            this.GetGps.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.GetGps.TabIndex = 16;
            this.GetGps.Text = "使用GPS数据";
            this.GetGps.Tooltip = "使用当前GPS位置数据";
            this.GetGps.Click += new System.EventHandler(this.GetGps_Click);
            // 
            // NewNodeForm
            // 
            this.AcceptButton = this.ComfirmBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonX1;
            this.ClientSize = new System.Drawing.Size(462, 100);
            this.ControlBox = false;
            this.Controls.Add(this.GetGps);
            this.Controls.Add(this.ipaddress);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.NodeNameBox);
            this.Controls.Add(this.LatBox);
            this.Controls.Add(this.LngBox);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.DescBox);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.ComfirmBtn);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewNodeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "增加节点";
            ((System.ComponentModel.ISupportInitialize)(this.LngBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LatBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NodeNameBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        public DevComponents.Editors.DoubleInput LngBox;
        public DevComponents.Editors.DoubleInput LatBox;
        public DevComponents.DotNetBar.Controls.TextBoxX DescBox;
        public DevComponents.DotNetBar.ButtonX buttonX2;
        public DevComponents.Editors.IntegerInput NodeNameBox;
        public DevComponents.DotNetBar.ButtonX ComfirmBtn;
        private DevComponents.DotNetBar.LabelX labelX4;
        public DevComponents.DotNetBar.Controls.TextBoxX ipaddress;
        private DevComponents.DotNetBar.ButtonX GetGps;
    }
}