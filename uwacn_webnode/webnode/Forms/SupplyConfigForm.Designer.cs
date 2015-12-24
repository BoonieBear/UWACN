namespace webnode.Forms
{
    partial class SupplyConfigForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Send = new DevComponents.DotNetBar.ButtonX();
            this.HighSelect = new System.Windows.Forms.ComboBox();
            this.LowSelect = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "高压选择";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(222, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "低压选择";
            // 
            // Send
            // 
            this.Send.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Send.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.Send.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Send.Location = new System.Drawing.Point(264, 135);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(82, 23);
            this.Send.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.Send.TabIndex = 8;
            this.Send.Text = "确认";
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // HighSelect
            // 
            this.HighSelect.FormattingEnabled = true;
            this.HighSelect.Items.AddRange(new object[] {
            "外电",
            "内电"});
            this.HighSelect.Location = new System.Drawing.Point(86, 49);
            this.HighSelect.Name = "HighSelect";
            this.HighSelect.Size = new System.Drawing.Size(67, 20);
            this.HighSelect.TabIndex = 9;
            // 
            // LowSelect
            // 
            this.LowSelect.FormattingEnabled = true;
            this.LowSelect.Items.AddRange(new object[] {
            "外电",
            "内电"});
            this.LowSelect.Location = new System.Drawing.Point(279, 53);
            this.LowSelect.Name = "LowSelect";
            this.LowSelect.Size = new System.Drawing.Size(67, 20);
            this.LowSelect.TabIndex = 9;
            // 
            // SupplyConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 170);
            this.Controls.Add(this.LowSelect);
            this.Controls.Add(this.HighSelect);
            this.Controls.Add(this.Send);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SupplyConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "外电配置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.ButtonX Send;
        public System.Windows.Forms.ComboBox HighSelect;
        public System.Windows.Forms.ComboBox LowSelect;
    }
}