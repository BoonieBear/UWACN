namespace webnode.Forms
{
    partial class GetInfoForm
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
            this.DestNodeName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.AddToList = new DevComponents.DotNetBar.ButtonX();
            this.COM_label = new DevComponents.DotNetBar.LabelX();
            this.CommBox = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.RebuildBox = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.SuspendLayout();
            // 
            // DestNodeName
            // 
            this.DestNodeName.DisplayMember = "Text";
            this.DestNodeName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.DestNodeName.FormattingEnabled = true;
            this.DestNodeName.ItemHeight = 15;
            this.DestNodeName.Location = new System.Drawing.Point(77, 7);
            this.DestNodeName.Name = "DestNodeName";
            this.DestNodeName.Size = new System.Drawing.Size(98, 21);
            this.DestNodeName.Sorted = true;
            this.DestNodeName.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.DestNodeName.TabIndex = 0;
            this.DestNodeName.DropDown += new System.EventHandler(this.DestNodeName_DropDown);
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(13, 7);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(58, 21);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "目标节点";
            // 
            // AddToList
            // 
            this.AddToList.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.AddToList.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.AddToList.Location = new System.Drawing.Point(207, 33);
            this.AddToList.Name = "AddToList";
            this.AddToList.Size = new System.Drawing.Size(80, 23);
            this.AddToList.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.AddToList.TabIndex = 2;
            this.AddToList.Text = "加入命令列表";
            this.AddToList.Click += new System.EventHandler(this.AddToList_Click);
            // 
            // COM_label
            // 
            this.COM_label.Location = new System.Drawing.Point(37, 34);
            this.COM_label.Name = "COM_label";
            this.COM_label.Size = new System.Drawing.Size(34, 21);
            this.COM_label.TabIndex = 3;
            this.COM_label.Text = "端口";
            // 
            // CommBox
            // 
            this.CommBox.DisplayMember = "Text";
            this.CommBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CommBox.FormattingEnabled = true;
            this.CommBox.ItemHeight = 15;
            this.CommBox.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem5,
            this.comboItem6});
            this.CommBox.Location = new System.Drawing.Point(77, 34);
            this.CommBox.Name = "CommBox";
            this.CommBox.Size = new System.Drawing.Size(98, 21);
            this.CommBox.Sorted = true;
            this.CommBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.CommBox.TabIndex = 4;
            this.CommBox.Text = "COM3";
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "COM0";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "COM1";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "COM2";
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "COM3";
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(206, 7);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(34, 21);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "重建";
            // 
            // RebuildBox
            // 
            this.RebuildBox.DisplayMember = "Text";
            this.RebuildBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.RebuildBox.FormattingEnabled = true;
            this.RebuildBox.ItemHeight = 15;
            this.RebuildBox.Items.AddRange(new object[] {
            this.comboItem3,
            this.comboItem4});
            this.RebuildBox.Location = new System.Drawing.Point(237, 6);
            this.RebuildBox.Name = "RebuildBox";
            this.RebuildBox.Size = new System.Drawing.Size(50, 21);
            this.RebuildBox.Sorted = true;
            this.RebuildBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.RebuildBox.TabIndex = 6;
            this.RebuildBox.Text = "关";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "关";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "开";
            // 
            // GetInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 66);
            this.Controls.Add(this.RebuildBox);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.CommBox);
            this.Controls.Add(this.COM_label);
            this.Controls.Add(this.AddToList);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.DestNodeName);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GetInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "获取节点信息";
            this.Load += new System.EventHandler(this.GetInfoForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        public DevComponents.DotNetBar.Controls.ComboBoxEx DestNodeName;
        private DevComponents.DotNetBar.ButtonX AddToList;
        private DevComponents.DotNetBar.LabelX COM_label;
        public DevComponents.DotNetBar.Controls.ComboBoxEx CommBox;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.DotNetBar.LabelX labelX2;
        public DevComponents.DotNetBar.Controls.ComboBoxEx RebuildBox;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.Editors.ComboItem comboItem6;
    }
}