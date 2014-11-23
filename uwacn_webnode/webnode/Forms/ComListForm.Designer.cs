namespace webnode.Forms
{
    partial class ComListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComListForm));
            this.listgroup = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.CmdList = new System.Windows.Forms.ListBox();
            this.SendBtn = new DevComponents.DotNetBar.ButtonX();
            this.ViaNet = new DevComponents.DotNetBar.ButtonItem();
            this.ViaSerial = new DevComponents.DotNetBar.ButtonItem();
            this.galleryContainer1 = new DevComponents.DotNetBar.GalleryContainer();
            this.HideBtn = new DevComponents.DotNetBar.ButtonX();
            this.AddTrace = new DevComponents.DotNetBar.ButtonX();
            this.DeleteBtn = new DevComponents.DotNetBar.ButtonX();
            this.ShowData = new DevComponents.DotNetBar.ButtonX();
            this.SourceNodeBox = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.OpenData = new DevComponents.DotNetBar.ButtonX();
            this.ParseNetData = new DevComponents.DotNetBar.ButtonItem();
            this.ParseSerialData = new DevComponents.DotNetBar.ButtonItem();
            this.ClearBtn = new DevComponents.DotNetBar.ButtonX();
            this.listgroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // listgroup
            // 
            this.listgroup.CanvasColor = System.Drawing.SystemColors.Control;
            this.listgroup.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.listgroup.Controls.Add(this.CmdList);
            this.listgroup.Dock = System.Windows.Forms.DockStyle.Left;
            this.listgroup.DrawTitleBox = false;
            this.listgroup.IsShadowEnabled = true;
            this.listgroup.Location = new System.Drawing.Point(0, 0);
            this.listgroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listgroup.Name = "listgroup";
            this.listgroup.Size = new System.Drawing.Size(376, 385);
            // 
            // 
            // 
            this.listgroup.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.listgroup.Style.BackColorGradientAngle = 90;
            this.listgroup.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.listgroup.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.listgroup.Style.BorderBottomWidth = 1;
            this.listgroup.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.listgroup.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.listgroup.Style.BorderLeftWidth = 1;
            this.listgroup.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.listgroup.Style.BorderRightWidth = 1;
            this.listgroup.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.listgroup.Style.BorderTopWidth = 1;
            this.listgroup.Style.CornerDiameter = 4;
            this.listgroup.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.listgroup.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.listgroup.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.listgroup.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.listgroup.TabIndex = 0;
            this.listgroup.Text = "数据区命令";
            // 
            // CmdList
            // 
            this.CmdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CmdList.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CmdList.FormattingEnabled = true;
            this.CmdList.ItemHeight = 17;
            this.CmdList.Location = new System.Drawing.Point(0, 0);
            this.CmdList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CmdList.Name = "CmdList";
            this.CmdList.ScrollAlwaysVisible = true;
            this.CmdList.Size = new System.Drawing.Size(370, 358);
            this.CmdList.TabIndex = 0;
            this.CmdList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CmdList_MouseClick);
            // 
            // SendBtn
            // 
            this.SendBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.SendBtn.AutoExpandOnClick = true;
            this.SendBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.SendBtn.Location = new System.Drawing.Point(393, 330);
            this.SendBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(100, 29);
            this.SendBtn.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ViaNet,
            this.ViaSerial});
            this.SendBtn.TabIndex = 3;
            this.SendBtn.Text = "打包发送";
            // 
            // ViaNet
            // 
            this.ViaNet.GlobalItem = false;
            this.ViaNet.Name = "ViaNet";
            this.ViaNet.Text = "经网络";
            this.ViaNet.Click += new System.EventHandler(this.ViaNet_Click);
            // 
            // ViaSerial
            // 
            this.ViaSerial.GlobalItem = false;
            this.ViaSerial.Name = "ViaSerial";
            this.ViaSerial.Text = "经串口";
            this.ViaSerial.Click += new System.EventHandler(this.ViaSerial_Click);
            // 
            // galleryContainer1
            // 
            this.galleryContainer1.EnableGalleryPopup = false;
            this.galleryContainer1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.galleryContainer1.MinimumSize = new System.Drawing.Size(150, 200);
            this.galleryContainer1.MultiLine = false;
            this.galleryContainer1.Name = "galleryContainer1";
            this.galleryContainer1.PopupUsesStandardScrollbars = false;
            // 
            // HideBtn
            // 
            this.HideBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.HideBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.HideBtn.Location = new System.Drawing.Point(393, 15);
            this.HideBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.HideBtn.Name = "HideBtn";
            this.HideBtn.Size = new System.Drawing.Size(100, 29);
            this.HideBtn.TabIndex = 4;
            this.HideBtn.Text = "隐藏列表";
            this.HideBtn.Tooltip = "隐藏命令对话框";
            this.HideBtn.Click += new System.EventHandler(this.HideBtn_Click);
            // 
            // AddTrace
            // 
            this.AddTrace.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.AddTrace.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.AddTrace.Location = new System.Drawing.Point(393, 258);
            this.AddTrace.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AddTrace.Name = "AddTrace";
            this.AddTrace.Size = new System.Drawing.Size(100, 29);
            this.AddTrace.TabIndex = 5;
            this.AddTrace.Text = "加入路径";
            this.AddTrace.Click += new System.EventHandler(this.AddTrace_Click);
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.DeleteBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.DeleteBtn.Location = new System.Drawing.Point(393, 221);
            this.DeleteBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(100, 29);
            this.DeleteBtn.TabIndex = 6;
            this.DeleteBtn.Text = "删除命令";
            this.DeleteBtn.Tooltip = "删除一条命令";
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // ShowData
            // 
            this.ShowData.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ShowData.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ShowData.Location = new System.Drawing.Point(393, 294);
            this.ShowData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ShowData.Name = "ShowData";
            this.ShowData.Size = new System.Drawing.Size(100, 29);
            this.ShowData.TabIndex = 7;
            this.ShowData.Text = "显示数据";
            this.ShowData.Tooltip = "逐项显示命令包内容";
            this.ShowData.Click += new System.EventHandler(this.ShowData_Click);
            // 
            // SourceNodeBox
            // 
            this.SourceNodeBox.DisplayMember = "Text";
            this.SourceNodeBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.SourceNodeBox.FormattingEnabled = true;
            this.SourceNodeBox.ItemHeight = 15;
            this.SourceNodeBox.Location = new System.Drawing.Point(395, 51);
            this.SourceNodeBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SourceNodeBox.Name = "SourceNodeBox";
            this.SourceNodeBox.Size = new System.Drawing.Size(97, 21);
            this.SourceNodeBox.Sorted = true;
            this.SourceNodeBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.SourceNodeBox.TabIndex = 8;
            this.SourceNodeBox.Text = "节点1";
            this.SourceNodeBox.WatermarkText = "源地址";
            this.SourceNodeBox.DropDown += new System.EventHandler(this.SourceNodeBox_DropDown);
            // 
            // OpenData
            // 
            this.OpenData.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.OpenData.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.OpenData.Location = new System.Drawing.Point(395, 149);
            this.OpenData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.OpenData.Name = "OpenData";
            this.OpenData.Size = new System.Drawing.Size(100, 29);
            this.OpenData.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ParseNetData,
            this.ParseSerialData});
            this.OpenData.TabIndex = 9;
            this.OpenData.Text = "解析文件";
            // 
            // ParseNetData
            // 
            this.ParseNetData.GlobalItem = false;
            this.ParseNetData.Name = "ParseNetData";
            this.ParseNetData.Text = "网络数据&&命令";
            this.ParseNetData.Click += new System.EventHandler(this.OpenData_Click);
            // 
            // ParseSerialData
            // 
            this.ParseSerialData.GlobalItem = false;
            this.ParseSerialData.Name = "ParseSerialData";
            this.ParseSerialData.Text = "串口数据&&命令";
            this.ParseSerialData.Click += new System.EventHandler(this.ParseSerialData_Click);
            // 
            // ClearBtn
            // 
            this.ClearBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ClearBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ClearBtn.Location = new System.Drawing.Point(393, 185);
            this.ClearBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(100, 29);
            this.ClearBtn.TabIndex = 10;
            this.ClearBtn.Text = "清空命令";
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // ComListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 385);
            this.ControlBox = false;
            this.Controls.Add(this.ClearBtn);
            this.Controls.Add(this.OpenData);
            this.Controls.Add(this.SourceNodeBox);
            this.Controls.Add(this.ShowData);
            this.Controls.Add(this.DeleteBtn);
            this.Controls.Add(this.AddTrace);
            this.Controls.Add(this.HideBtn);
            this.Controls.Add(this.SendBtn);
            this.Controls.Add(this.listgroup);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ComListForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "命令列表";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ComListForm_FormClosing);
            this.Load += new System.EventHandler(this.ComListForm_Load);
            this.listgroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel listgroup;
        private System.Windows.Forms.ListBox CmdList;
        private DevComponents.DotNetBar.ButtonX SendBtn;
        private DevComponents.DotNetBar.GalleryContainer galleryContainer1;
        private DevComponents.DotNetBar.ButtonX HideBtn;
        private DevComponents.DotNetBar.ButtonX AddTrace;
        private DevComponents.DotNetBar.ButtonItem ViaNet;
        private DevComponents.DotNetBar.ButtonItem ViaSerial;
        private DevComponents.DotNetBar.ButtonX DeleteBtn;
        private DevComponents.DotNetBar.ButtonX ShowData;
        private DevComponents.DotNetBar.Controls.ComboBoxEx SourceNodeBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private DevComponents.DotNetBar.ButtonX OpenData;
        private DevComponents.DotNetBar.ButtonX ClearBtn;
        private DevComponents.DotNetBar.ButtonItem ParseNetData;
        private DevComponents.DotNetBar.ButtonItem ParseSerialData;

    }
}