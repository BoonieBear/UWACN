namespace webnode.Forms
{
    partial class PingForm
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
            this.DestNodeName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.SourceText = new System.Windows.Forms.TextBox();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.BackText = new System.Windows.Forms.TextBox();
            this.Sendbtn = new DevComponents.DotNetBar.ButtonX();
            this.ViaComm = new DevComponents.DotNetBar.ButtonItem();
            this.ViaNet = new DevComponents.DotNetBar.ButtonItem();
            this.PathAsignCheck = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.Commparelabel = new DevComponents.DotNetBar.LabelX();
            this.Path = new System.Windows.Forms.TextBox();
            this.SourceNodeBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ClearBtn = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // DestNodeName
            // 
            this.DestNodeName.FormattingEnabled = true;
            this.DestNodeName.Location = new System.Drawing.Point(228, 14);
            this.DestNodeName.Name = "DestNodeName";
            this.DestNodeName.Size = new System.Drawing.Size(83, 20);
            this.DestNodeName.TabIndex = 0;
            this.DestNodeName.DropDown += new System.EventHandler(this.DestNodeName_DropDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(169, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "目标节点";
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.SourceText);
            this.groupPanel1.Location = new System.Drawing.Point(14, 45);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(251, 190);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel1.TabIndex = 2;
            this.groupPanel1.Text = "源数据内容";
            // 
            // SourceText
            // 
            this.SourceText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SourceText.Location = new System.Drawing.Point(0, 0);
            this.SourceText.MaxLength = 480;
            this.SourceText.Multiline = true;
            this.SourceText.Name = "SourceText";
            this.SourceText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SourceText.Size = new System.Drawing.Size(245, 166);
            this.SourceText.TabIndex = 0;
            this.SourceText.TextChanged += new System.EventHandler(this.SourceText_TextChanged);
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.BackText);
            this.groupPanel2.Location = new System.Drawing.Point(274, 45);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(251, 190);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel2.TabIndex = 3;
            this.groupPanel2.Text = "反馈数据内容";
            // 
            // BackText
            // 
            this.BackText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackText.Location = new System.Drawing.Point(0, 0);
            this.BackText.Multiline = true;
            this.BackText.Name = "BackText";
            this.BackText.ReadOnly = true;
            this.BackText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.BackText.Size = new System.Drawing.Size(245, 166);
            this.BackText.TabIndex = 1;
            // 
            // Sendbtn
            // 
            this.Sendbtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Sendbtn.AutoExpandOnClick = true;
            this.Sendbtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.Sendbtn.Location = new System.Drawing.Point(454, 241);
            this.Sendbtn.Name = "Sendbtn";
            this.Sendbtn.Size = new System.Drawing.Size(71, 23);
            this.Sendbtn.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ViaComm,
            this.ViaNet});
            this.Sendbtn.TabIndex = 4;
            this.Sendbtn.Text = "直接发送";
            // 
            // ViaComm
            // 
            this.ViaComm.Name = "ViaComm";
            this.ViaComm.Text = "经串口";
            this.ViaComm.Click += new System.EventHandler(this.ViaComm_Click);
            // 
            // ViaNet
            // 
            this.ViaNet.Name = "ViaNet";
            this.ViaNet.Text = "经网络";
            this.ViaNet.Click += new System.EventHandler(this.ViaNet_Click);
            // 
            // PathAsignCheck
            // 
            this.PathAsignCheck.Location = new System.Drawing.Point(317, 13);
            this.PathAsignCheck.Name = "PathAsignCheck";
            this.PathAsignCheck.Size = new System.Drawing.Size(75, 23);
            this.PathAsignCheck.TabIndex = 5;
            this.PathAsignCheck.Text = "安排路径";
            this.PathAsignCheck.CheckedChanged += new System.EventHandler(this.PathAsignCheck_CheckedChanged);
            // 
            // Commparelabel
            // 
            this.Commparelabel.Location = new System.Drawing.Point(14, 240);
            this.Commparelabel.Name = "Commparelabel";
            this.Commparelabel.Size = new System.Drawing.Size(337, 23);
            this.Commparelabel.TabIndex = 6;
            this.Commparelabel.Text = "---";
            // 
            // Path
            // 
            this.Path.Enabled = false;
            this.Path.Location = new System.Drawing.Point(398, 13);
            this.Path.Name = "Path";
            this.Path.Size = new System.Drawing.Size(127, 21);
            this.Path.TabIndex = 7;
            // 
            // SourceNodeBox
            // 
            this.SourceNodeBox.FormattingEnabled = true;
            this.SourceNodeBox.Location = new System.Drawing.Point(68, 16);
            this.SourceNodeBox.Name = "SourceNodeBox";
            this.SourceNodeBox.Size = new System.Drawing.Size(95, 20);
            this.SourceNodeBox.TabIndex = 8;
            this.SourceNodeBox.DropDown += new System.EventHandler(this.SourceNodeBox_DropDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "源节点";
            // 
            // ClearBtn
            // 
            this.ClearBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ClearBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ClearBtn.Location = new System.Drawing.Point(373, 241);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(75, 23);
            this.ClearBtn.TabIndex = 10;
            this.ClearBtn.Text = "清空";
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // PingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 266);
            this.Controls.Add(this.ClearBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SourceNodeBox);
            this.Controls.Add(this.Path);
            this.Controls.Add(this.Commparelabel);
            this.Controls.Add(this.PathAsignCheck);
            this.Controls.Add(this.Sendbtn);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DestNodeName);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "PingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "回环测试";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PingForm_FormClosing);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox DestNodeName;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.ButtonX Sendbtn;
        private DevComponents.DotNetBar.ButtonItem ViaComm;
        private DevComponents.DotNetBar.ButtonItem ViaNet;
        private DevComponents.DotNetBar.Controls.CheckBoxX PathAsignCheck;
        private DevComponents.DotNetBar.LabelX Commparelabel;
        private System.Windows.Forms.TextBox Path;
        private System.Windows.Forms.TextBox SourceText;
        private System.Windows.Forms.TextBox BackText;
        private System.Windows.Forms.ComboBox SourceNodeBox;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.ButtonX ClearBtn;
    }
}