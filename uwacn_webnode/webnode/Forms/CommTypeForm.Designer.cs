namespace webnode.Forms
{
    partial class CommTypeForm
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
            this.CommType = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DestNodeName = new System.Windows.Forms.ComboBox();
            this.AddToList = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
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
            this.CommType.Location = new System.Drawing.Point(188, 12);
            this.CommType.Name = "CommType";
            this.CommType.Size = new System.Drawing.Size(84, 84);
            this.CommType.TabIndex = 18;
            this.CommType.ThreeDCheckBoxes = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "目标节点";
            // 
            // DestNodeName
            // 
            this.DestNodeName.FormattingEnabled = true;
            this.DestNodeName.Location = new System.Drawing.Point(71, 7);
            this.DestNodeName.Name = "DestNodeName";
            this.DestNodeName.Size = new System.Drawing.Size(75, 20);
            this.DestNodeName.TabIndex = 19;
            this.DestNodeName.DropDown += new System.EventHandler(this.DestNodeName_DropDown);
            // 
            // AddToList
            // 
            this.AddToList.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.AddToList.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.AddToList.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AddToList.Location = new System.Drawing.Point(188, 102);
            this.AddToList.Name = "AddToList";
            this.AddToList.Size = new System.Drawing.Size(82, 23);
            this.AddToList.TabIndex = 21;
            this.AddToList.Text = "加入命令列表";
            this.AddToList.Click += new System.EventHandler(this.AddToList_Click);
            // 
            // CommTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 136);
            this.Controls.Add(this.AddToList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DestNodeName);
            this.Controls.Add(this.CommType);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CommTypeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通信制式设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckedListBox CommType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox DestNodeName;
        private DevComponents.DotNetBar.ButtonX AddToList;
    }
}