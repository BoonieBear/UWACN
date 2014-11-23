namespace webnode.Forms
{
    partial class BasicConfForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BasicConfForm));
            this.ConfigDataView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.XmlTree = new System.Windows.Forms.TreeView();
            this.DelNode = new DevComponents.DotNetBar.ButtonX();
            this.AddNode = new DevComponents.DotNetBar.ButtonX();
            this.SaveBtn = new DevComponents.DotNetBar.ButtonX();
            this.ReloadConfig = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.ConfigDataView)).BeginInit();
            this.SuspendLayout();
            // 
            // ConfigDataView
            // 
            this.ConfigDataView.AllowUserToAddRows = false;
            this.ConfigDataView.AllowUserToDeleteRows = false;
            this.ConfigDataView.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.ConfigDataView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ConfigDataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ConfigDataView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.ConfigDataView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ConfigDataView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ConfigDataView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.ConfigDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConfigDataView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ConfigDataView.Location = new System.Drawing.Point(342, 32);
            this.ConfigDataView.Name = "ConfigDataView";
            this.ConfigDataView.RowTemplate.Height = 23;
            this.ConfigDataView.Size = new System.Drawing.Size(245, 264);
            this.ConfigDataView.StandardTab = true;
            this.ConfigDataView.TabIndex = 0;
            this.ConfigDataView.VirtualMode = true;
            this.ConfigDataView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ConfigDataView_CellValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(340, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "配置属性";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "配置";
            // 
            // XmlTree
            // 
            this.XmlTree.Location = new System.Drawing.Point(15, 32);
            this.XmlTree.Name = "XmlTree";
            this.XmlTree.ShowNodeToolTips = true;
            this.XmlTree.Size = new System.Drawing.Size(301, 266);
            this.XmlTree.TabIndex = 5;
            this.XmlTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.XmlTree_AfterSelect);
            // 
            // DelNode
            // 
            this.DelNode.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.DelNode.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.DelNode.Enabled = false;
            this.DelNode.Location = new System.Drawing.Point(220, 304);
            this.DelNode.Name = "DelNode";
            this.DelNode.Size = new System.Drawing.Size(96, 23);
            this.DelNode.TabIndex = 13;
            this.DelNode.Text = "删除配置";
            this.DelNode.Tooltip = "删除选中的配置";
            this.DelNode.Click += new System.EventHandler(this.DelNode_Click);
            // 
            // AddNode
            // 
            this.AddNode.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.AddNode.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.AddNode.Enabled = false;
            this.AddNode.Location = new System.Drawing.Point(118, 304);
            this.AddNode.Name = "AddNode";
            this.AddNode.Size = new System.Drawing.Size(96, 23);
            this.AddNode.TabIndex = 14;
            this.AddNode.Text = "增加配置";
            this.AddNode.Tooltip = "增加配置到配置树中";
            this.AddNode.Click += new System.EventHandler(this.AddNode_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.SaveBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.SaveBtn.Location = new System.Drawing.Point(512, 302);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveBtn.TabIndex = 15;
            this.SaveBtn.Text = "保存配置";
            this.SaveBtn.Tooltip = "保存当前配置文件";
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // ReloadConfig
            // 
            this.ReloadConfig.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ReloadConfig.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ReloadConfig.Location = new System.Drawing.Point(431, 302);
            this.ReloadConfig.Name = "ReloadConfig";
            this.ReloadConfig.Size = new System.Drawing.Size(75, 23);
            this.ReloadConfig.TabIndex = 16;
            this.ReloadConfig.Text = "重载配置";
            this.ReloadConfig.Tooltip = "重新加载已保存的配置文件";
            this.ReloadConfig.Click += new System.EventHandler(this.ReloadConfig_Click);
            // 
            // BasicConfForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 359);
            this.Controls.Add(this.ReloadConfig);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.AddNode);
            this.Controls.Add(this.DelNode);
            this.Controls.Add(this.XmlTree);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConfigDataView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BasicConfForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "基本设置 ";
            this.TopMost = false;
            this.Load += new System.EventHandler(this.BasicConfForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ConfigDataView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ConfigDataView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView XmlTree;
        private DevComponents.DotNetBar.ButtonX DelNode;
        private DevComponents.DotNetBar.ButtonX AddNode;
        private DevComponents.DotNetBar.ButtonX SaveBtn;
        private DevComponents.DotNetBar.ButtonX ReloadConfig;
    }
}