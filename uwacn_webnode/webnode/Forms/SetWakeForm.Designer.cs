namespace webnode.Forms
{
    partial class SetWakeForm
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
            this.Comm2Wake = new System.Windows.Forms.MaskedTextBox();
            this.Comm3Wake = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ConfBtn = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "串口2唤醒时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "串口3唤醒时间";
            // 
            // Comm2Wake
            // 
            this.Comm2Wake.Location = new System.Drawing.Point(101, 17);
            this.Comm2Wake.Mask = "9999999999";
            this.Comm2Wake.Name = "Comm2Wake";
            this.Comm2Wake.Size = new System.Drawing.Size(72, 21);
            this.Comm2Wake.TabIndex = 2;
            // 
            // Comm3Wake
            // 
            this.Comm3Wake.Location = new System.Drawing.Point(280, 17);
            this.Comm3Wake.Mask = "9999999999";
            this.Comm3Wake.Name = "Comm3Wake";
            this.Comm3Wake.Size = new System.Drawing.Size(74, 21);
            this.Comm3Wake.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(179, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "秒";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(360, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "秒";
            // 
            // ConfBtn
            // 
            this.ConfBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ConfBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ConfBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ConfBtn.Location = new System.Drawing.Point(383, 15);
            this.ConfBtn.Name = "ConfBtn";
            this.ConfBtn.Size = new System.Drawing.Size(55, 23);
            this.ConfBtn.TabIndex = 6;
            this.ConfBtn.Text = "确定";
            // 
            // SetWakeForm
            // 
            this.AcceptButton = this.ConfBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 48);
            this.Controls.Add(this.ConfBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Comm3Wake);
            this.Controls.Add(this.Comm2Wake);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetWakeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置串口2和3用户设备定时唤醒时间";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.ButtonX ConfBtn;
        public System.Windows.Forms.MaskedTextBox Comm2Wake;
        public System.Windows.Forms.MaskedTextBox Comm3Wake;
    }
}