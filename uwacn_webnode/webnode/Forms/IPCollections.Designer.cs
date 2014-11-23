namespace webnode.Forms
{
    partial class IPCollections
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
            this.IPList = new System.Windows.Forms.CheckedListBox();
            this.ConfBtn = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // IPList
            // 
            this.IPList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.IPList.CheckOnClick = true;
            this.IPList.FormattingEnabled = true;
            this.IPList.Location = new System.Drawing.Point(12, 3);
            this.IPList.Name = "IPList";
            this.IPList.Size = new System.Drawing.Size(378, 148);
            this.IPList.Sorted = true;
            this.IPList.TabIndex = 0;
            this.IPList.ThreeDCheckBoxes = true;
            this.IPList.SelectedIndexChanged += new System.EventHandler(this.IPList_SelectedIndexChanged);
            // 
            // ConfBtn
            // 
            this.ConfBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ConfBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ConfBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ConfBtn.Location = new System.Drawing.Point(315, 157);
            this.ConfBtn.Name = "ConfBtn";
            this.ConfBtn.Size = new System.Drawing.Size(75, 23);
            this.ConfBtn.TabIndex = 1;
            this.ConfBtn.Text = "确定";
            this.ConfBtn.Click += new System.EventHandler(this.ConfBtn_Click);
            // 
            // IPCollections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 186);
            this.Controls.Add(this.ConfBtn);
            this.Controls.Add(this.IPList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "IPCollections";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "节点选择";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX ConfBtn;
        public System.Windows.Forms.CheckedListBox IPList;
    }
}