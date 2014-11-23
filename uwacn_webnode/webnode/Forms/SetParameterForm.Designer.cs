namespace webnode.Forms
{
    partial class SetParameterForm
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
            this.CommBox = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.COM_label = new DevComponents.DotNetBar.LabelX();
            this.AddToList = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.DestNodeName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.parameter = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.HexCheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // CommBox
            // 
            this.CommBox.DisplayMember = "Text";
            this.CommBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CommBox.FormattingEnabled = true;
            this.CommBox.ItemHeight = 15;
            this.CommBox.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.CommBox.Location = new System.Drawing.Point(220, 22);
            this.CommBox.Name = "CommBox";
            this.CommBox.Size = new System.Drawing.Size(98, 21);
            this.CommBox.Sorted = true;
            this.CommBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.CommBox.TabIndex = 9;
            this.CommBox.Text = "COM2";
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "COM2";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "COM3";
            // 
            // COM_label
            // 
            this.COM_label.Location = new System.Drawing.Point(180, 22);
            this.COM_label.Name = "COM_label";
            this.COM_label.Size = new System.Drawing.Size(34, 21);
            this.COM_label.TabIndex = 8;
            this.COM_label.Text = "端口";
            // 
            // AddToList
            // 
            this.AddToList.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.AddToList.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.AddToList.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AddToList.Location = new System.Drawing.Point(238, 98);
            this.AddToList.Name = "AddToList";
            this.AddToList.Size = new System.Drawing.Size(80, 23);
            this.AddToList.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.AddToList.TabIndex = 7;
            this.AddToList.Text = "加入命令列表";
            this.AddToList.Click += new System.EventHandler(this.AddToList_Click);
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(12, 22);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(58, 21);
            this.labelX1.TabIndex = 6;
            this.labelX1.Text = "目标节点";
            // 
            // DestNodeName
            // 
            this.DestNodeName.DisplayMember = "Text";
            this.DestNodeName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.DestNodeName.FormattingEnabled = true;
            this.DestNodeName.ItemHeight = 15;
            this.DestNodeName.Location = new System.Drawing.Point(76, 22);
            this.DestNodeName.Name = "DestNodeName";
            this.DestNodeName.Size = new System.Drawing.Size(98, 21);
            this.DestNodeName.Sorted = true;
            this.DestNodeName.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.DestNodeName.TabIndex = 5;
            this.DestNodeName.DropDown += new System.EventHandler(this.DestNodeName_DropDown);
            // 
            // parameter
            // 
            // 
            // 
            // 
            this.parameter.Border.Class = "TextBoxBorder";
            this.parameter.Location = new System.Drawing.Point(12, 52);
            this.parameter.Multiline = true;
            this.parameter.Name = "parameter";
            this.parameter.Size = new System.Drawing.Size(306, 40);
            this.parameter.TabIndex = 10;
            // 
            // HexCheck
            // 
            this.HexCheck.AutoSize = true;
            this.HexCheck.Location = new System.Drawing.Point(12, 103);
            this.HexCheck.Name = "HexCheck";
            this.HexCheck.Size = new System.Drawing.Size(66, 16);
            this.HexCheck.TabIndex = 11;
            this.HexCheck.Text = "HEX发送";
            this.HexCheck.UseVisualStyleBackColor = true;
            // 
            // SetParameterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 131);
            this.Controls.Add(this.HexCheck);
            this.Controls.Add(this.parameter);
            this.Controls.Add(this.CommBox);
            this.Controls.Add(this.COM_label);
            this.Controls.Add(this.AddToList);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.DestNodeName);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SetParameterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置设备参数";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevComponents.DotNetBar.Controls.ComboBoxEx CommBox;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.DotNetBar.LabelX COM_label;
        private DevComponents.DotNetBar.ButtonX AddToList;
        private DevComponents.DotNetBar.LabelX labelX1;
        public DevComponents.DotNetBar.Controls.ComboBoxEx DestNodeName;
        private DevComponents.DotNetBar.Controls.TextBoxX parameter;
        private System.Windows.Forms.CheckBox HexCheck;
    }
}