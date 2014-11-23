namespace webnode.Forms
{
    partial class NodeInfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodeInfoForm));
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.DepthInput = new DevComponents.Editors.DoubleInput();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.CommType = new System.Windows.Forms.CheckedListBox();
            this.ConfBtn = new DevComponents.DotNetBar.ButtonX();
            this.nodeinfolist = new System.Windows.Forms.ListBox();
            this.Set2Box = new System.Windows.Forms.ComboBox();
            this.Set1Box = new System.Windows.Forms.ComboBox();
            this.Set1 = new DevComponents.DotNetBar.LabelX();
            this.Set2 = new DevComponents.DotNetBar.LabelX();
            this.NodeType = new DevComponents.DotNetBar.LabelX();
            this.Nodetypebox = new System.Windows.Forms.ComboBox();
            this.NodeNameBox = new System.Windows.Forms.ComboBox();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.leftenergy = new DevComponents.Editors.IntegerInput();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.EmitSet = new DevComponents.Editors.IntegerInput();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.SaveNodeList = new DevComponents.DotNetBar.ButtonX();
            this.AddBtn = new DevComponents.DotNetBar.ButtonX();
            this.Lat = new DevComponents.Editors.DoubleInput();
            this.Lang = new DevComponents.Editors.DoubleInput();
            this.DestNodeBox = new System.Windows.Forms.ComboBox();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.DepthInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftenergy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmitSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lang)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(11, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(57, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "节点编号";
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(285, 12);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(36, 23);
            this.labelX2.TabIndex = 10;
            this.labelX2.Text = "经度";
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(159, 12);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(34, 23);
            this.labelX3.TabIndex = 11;
            this.labelX3.Text = "纬度";
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(415, 12);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(36, 23);
            this.labelX4.TabIndex = 14;
            this.labelX4.Text = "深度";
            // 
            // DepthInput
            // 
            // 
            // 
            // 
            this.DepthInput.BackgroundStyle.Class = "DateTimeInputBackground";
            this.DepthInput.ButtonFreeText.Checked = true;
            this.DepthInput.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.DepthInput.DisplayFormat = "F01";
            this.DepthInput.FieldNavigation = DevComponents.Editors.eInputFieldNavigation.Arrows;
            this.DepthInput.FreeTextEntryMode = true;
            this.DepthInput.Increment = 1D;
            this.DepthInput.Location = new System.Drawing.Point(448, 12);
            this.DepthInput.MaxValue = 6000D;
            this.DepthInput.MinValue = 0D;
            this.DepthInput.Name = "DepthInput";
            this.DepthInput.Size = new System.Drawing.Size(67, 21);
            this.DepthInput.TabIndex = 15;
            this.DepthInput.Value = 20D;
            this.DepthInput.WatermarkText = "米";
            // 
            // labelX5
            // 
            this.labelX5.Location = new System.Drawing.Point(11, 41);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(56, 23);
            this.labelX5.TabIndex = 16;
            this.labelX5.Text = "通信制式";
            // 
            // CommType
            // 
            this.CommType.CheckOnClick = true;
            this.CommType.FormattingEnabled = true;
            this.CommType.Items.AddRange(new object[] {
            "01#制式",
            "02#制式",
            "03#制式",
            "04#制式",
            "05#制式",
            "06#制式",
            "07#制式",
            "08#制式",
            "09#制式",
            "10#制式",
            "11#制式",
            "12#制式",
            "13#制式",
            "14#制式",
            "15#制式",
            "16#制式"});
            this.CommType.Location = new System.Drawing.Point(69, 41);
            this.CommType.Name = "CommType";
            this.CommType.Size = new System.Drawing.Size(84, 52);
            this.CommType.TabIndex = 17;
            this.CommType.ThreeDCheckBoxes = true;
            // 
            // ConfBtn
            // 
            this.ConfBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ConfBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ConfBtn.Location = new System.Drawing.Point(423, 211);
            this.ConfBtn.Name = "ConfBtn";
            this.ConfBtn.Size = new System.Drawing.Size(92, 23);
            this.ConfBtn.TabIndex = 18;
            this.ConfBtn.Text = "加入命令列表";
            this.ConfBtn.Tooltip = "加入列表准备下发命令";
            this.ConfBtn.Click += new System.EventHandler(this.ConfBtn_Click);
            // 
            // nodeinfolist
            // 
            this.nodeinfolist.FormattingEnabled = true;
            this.nodeinfolist.HorizontalScrollbar = true;
            this.nodeinfolist.ItemHeight = 12;
            this.nodeinfolist.Location = new System.Drawing.Point(12, 97);
            this.nodeinfolist.Name = "nodeinfolist";
            this.nodeinfolist.ScrollAlwaysVisible = true;
            this.nodeinfolist.Size = new System.Drawing.Size(503, 112);
            this.nodeinfolist.TabIndex = 19;
            this.nodeinfolist.SelectedIndexChanged += new System.EventHandler(this.nodeinfolist_SelectedIndexChanged);
            this.nodeinfolist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nodeinfolist_KeyDown);
            // 
            // Set2Box
            // 
            this.Set2Box.FormattingEnabled = true;
            this.Set2Box.Location = new System.Drawing.Point(200, 70);
            this.Set2Box.Name = "Set2Box";
            this.Set2Box.Size = new System.Drawing.Size(79, 20);
            this.Set2Box.TabIndex = 20;
            // 
            // Set1Box
            // 
            this.Set1Box.FormattingEnabled = true;
            this.Set1Box.Location = new System.Drawing.Point(199, 41);
            this.Set1Box.Name = "Set1Box";
            this.Set1Box.Size = new System.Drawing.Size(80, 20);
            this.Set1Box.TabIndex = 21;
            // 
            // Set1
            // 
            this.Set1.Location = new System.Drawing.Point(159, 41);
            this.Set1.Name = "Set1";
            this.Set1.Size = new System.Drawing.Size(41, 23);
            this.Set1.TabIndex = 22;
            this.Set1.Text = "外挂1";
            // 
            // Set2
            // 
            this.Set2.Location = new System.Drawing.Point(159, 70);
            this.Set2.Name = "Set2";
            this.Set2.Size = new System.Drawing.Size(41, 23);
            this.Set2.TabIndex = 23;
            this.Set2.Text = "外挂2";
            // 
            // NodeType
            // 
            this.NodeType.Location = new System.Drawing.Point(285, 41);
            this.NodeType.Name = "NodeType";
            this.NodeType.Size = new System.Drawing.Size(41, 23);
            this.NodeType.TabIndex = 24;
            this.NodeType.Text = "类型";
            // 
            // Nodetypebox
            // 
            this.Nodetypebox.FormattingEnabled = true;
            this.Nodetypebox.Items.AddRange(new object[] {
            "静态节点",
            "动态节点"});
            this.Nodetypebox.Location = new System.Drawing.Point(327, 41);
            this.Nodetypebox.Name = "Nodetypebox";
            this.Nodetypebox.Size = new System.Drawing.Size(80, 20);
            this.Nodetypebox.TabIndex = 25;
            this.Nodetypebox.Text = "静态节点";
            // 
            // NodeNameBox
            // 
            this.NodeNameBox.FormattingEnabled = true;
            this.NodeNameBox.Location = new System.Drawing.Point(69, 11);
            this.NodeNameBox.Name = "NodeNameBox";
            this.NodeNameBox.Size = new System.Drawing.Size(84, 20);
            this.NodeNameBox.Sorted = true;
            this.NodeNameBox.TabIndex = 26;
            this.NodeNameBox.DropDown += new System.EventHandler(this.NodeNameBox_DropDown);
            this.NodeNameBox.SelectionChangeCommitted += new System.EventHandler(this.NodeNameBox_SelectionChangeCommitted);
            // 
            // labelX6
            // 
            this.labelX6.Location = new System.Drawing.Point(415, 41);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(41, 23);
            this.labelX6.TabIndex = 27;
            this.labelX6.Text = "能量";
            // 
            // leftenergy
            // 
            // 
            // 
            // 
            this.leftenergy.BackgroundStyle.Class = "DateTimeInputBackground";
            this.leftenergy.ButtonFreeText.Checked = true;
            this.leftenergy.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.leftenergy.FieldNavigation = DevComponents.Editors.eInputFieldNavigation.Arrows;
            this.leftenergy.FreeTextEntryMode = true;
            this.leftenergy.Location = new System.Drawing.Point(448, 40);
            this.leftenergy.LockUpdateChecked = true;
            this.leftenergy.MaxValue = 100;
            this.leftenergy.MinValue = 0;
            this.leftenergy.Name = "leftenergy";
            this.leftenergy.ShowUpDown = true;
            this.leftenergy.Size = new System.Drawing.Size(58, 21);
            this.leftenergy.TabIndex = 28;
            this.leftenergy.Value = 50;
            // 
            // labelX7
            // 
            this.labelX7.Location = new System.Drawing.Point(285, 70);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(43, 23);
            this.labelX7.TabIndex = 29;
            this.labelX7.Text = "换能器";
            // 
            // EmitSet
            // 
            // 
            // 
            // 
            this.EmitSet.BackgroundStyle.Class = "DateTimeInputBackground";
            this.EmitSet.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.EmitSet.Location = new System.Drawing.Point(327, 70);
            this.EmitSet.LockUpdateChecked = true;
            this.EmitSet.MaxValue = 4;
            this.EmitSet.MinValue = 1;
            this.EmitSet.Name = "EmitSet";
            this.EmitSet.ShowUpDown = true;
            this.EmitSet.Size = new System.Drawing.Size(67, 21);
            this.EmitSet.TabIndex = 30;
            this.EmitSet.Value = 1;
            // 
            // labelX8
            // 
            this.labelX8.Location = new System.Drawing.Point(508, 42);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(16, 23);
            this.labelX8.TabIndex = 31;
            this.labelX8.Text = "%";
            // 
            // SaveNodeList
            // 
            this.SaveNodeList.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.SaveNodeList.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.SaveNodeList.Location = new System.Drawing.Point(327, 211);
            this.SaveNodeList.Name = "SaveNodeList";
            this.SaveNodeList.Size = new System.Drawing.Size(92, 23);
            this.SaveNodeList.TabIndex = 32;
            this.SaveNodeList.Text = "保存";
            this.SaveNodeList.Tooltip = "将信息表保存在本地";
            this.SaveNodeList.Click += new System.EventHandler(this.SaveNodeList_Click);
            // 
            // AddBtn
            // 
            this.AddBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.AddBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.AddBtn.Location = new System.Drawing.Point(415, 70);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(100, 23);
            this.AddBtn.TabIndex = 33;
            this.AddBtn.Text = "加入信息表";
            this.AddBtn.Tooltip = "加入信息列表，可删除";
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // Lat
            // 
            // 
            // 
            // 
            this.Lat.BackgroundStyle.Class = "DateTimeInputBackground";
            this.Lat.ButtonFreeText.Checked = true;
            this.Lat.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.Lat.DisplayFormat = "F06";
            this.Lat.FieldNavigation = DevComponents.Editors.eInputFieldNavigation.Arrows;
            this.Lat.FreeTextEntryMode = true;
            this.Lat.Increment = 1D;
            this.Lat.Location = new System.Drawing.Point(199, 11);
            this.Lat.MaxValue = 90D;
            this.Lat.MinValue = -90D;
            this.Lat.Name = "Lat";
            this.Lat.Size = new System.Drawing.Size(80, 21);
            this.Lat.TabIndex = 34;
            this.Lat.WatermarkText = "南纬-";
            // 
            // Lang
            // 
            // 
            // 
            // 
            this.Lang.BackgroundStyle.Class = "DateTimeInputBackground";
            this.Lang.ButtonFreeText.Checked = true;
            this.Lang.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.Lang.DisplayFormat = "F06";
            this.Lang.FieldNavigation = DevComponents.Editors.eInputFieldNavigation.Arrows;
            this.Lang.FreeTextEntryMode = true;
            this.Lang.Increment = 1D;
            this.Lang.Location = new System.Drawing.Point(327, 10);
            this.Lang.MaxValue = 180D;
            this.Lang.MinValue = -180D;
            this.Lang.Name = "Lang";
            this.Lang.Size = new System.Drawing.Size(80, 21);
            this.Lang.TabIndex = 35;
            this.Lang.WatermarkText = "西经-";
            // 
            // DestNodeBox
            // 
            this.DestNodeBox.FormattingEnabled = true;
            this.DestNodeBox.Location = new System.Drawing.Point(68, 214);
            this.DestNodeBox.Name = "DestNodeBox";
            this.DestNodeBox.Size = new System.Drawing.Size(84, 20);
            this.DestNodeBox.Sorted = true;
            this.DestNodeBox.TabIndex = 37;
            this.DestNodeBox.Text = "节点1";
            this.DestNodeBox.DropDown += new System.EventHandler(this.DestNodeBox_DropDown);
            // 
            // labelX9
            // 
            this.labelX9.Location = new System.Drawing.Point(10, 215);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(57, 23);
            this.labelX9.TabIndex = 36;
            this.labelX9.Text = "目标节点";
            // 
            // NodeInfoForm
            // 
            this.AcceptButton = this.ConfBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 237);
            this.Controls.Add(this.DestNodeBox);
            this.Controls.Add(this.labelX9);
            this.Controls.Add(this.Lang);
            this.Controls.Add(this.Lat);
            this.Controls.Add(this.AddBtn);
            this.Controls.Add(this.SaveNodeList);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.EmitSet);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.leftenergy);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.NodeNameBox);
            this.Controls.Add(this.Nodetypebox);
            this.Controls.Add(this.NodeType);
            this.Controls.Add(this.Set2);
            this.Controls.Add(this.Set1);
            this.Controls.Add(this.Set1Box);
            this.Controls.Add(this.Set2Box);
            this.Controls.Add(this.nodeinfolist);
            this.Controls.Add(this.ConfBtn);
            this.Controls.Add(this.CommType);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.DepthInput);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "NodeInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置节点信息表";
            this.Load += new System.EventHandler(this.NodeInfoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DepthInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftenergy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmitSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX5;
        public System.Windows.Forms.CheckedListBox CommType;
        private DevComponents.DotNetBar.ButtonX ConfBtn;
        private System.Windows.Forms.ListBox nodeinfolist;
        private System.Windows.Forms.ComboBox Set2Box;
        private System.Windows.Forms.ComboBox Set1Box;
        private DevComponents.DotNetBar.LabelX Set1;
        private DevComponents.DotNetBar.LabelX Set2;
        private DevComponents.DotNetBar.LabelX NodeType;
        private System.Windows.Forms.ComboBox Nodetypebox;
        private System.Windows.Forms.ComboBox NodeNameBox;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.Editors.IntegerInput leftenergy;
        private DevComponents.Editors.IntegerInput EmitSet;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.ButtonX SaveNodeList;
        private DevComponents.DotNetBar.ButtonX AddBtn;
        private DevComponents.Editors.DoubleInput Lat;
        private DevComponents.Editors.DoubleInput Lang;
        private DevComponents.Editors.DoubleInput DepthInput;
        private System.Windows.Forms.ComboBox DestNodeBox;
        private DevComponents.DotNetBar.LabelX labelX9;

    }
}