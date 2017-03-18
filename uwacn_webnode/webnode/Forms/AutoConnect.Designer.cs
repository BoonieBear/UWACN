namespace webnode.Forms
{
    partial class AutoConnect
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
            this.MsgBox = new System.Windows.Forms.TextBox();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.DisconnectBtn = new System.Windows.Forms.Button();
            this.IpaddBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CommportBox = new System.Windows.Forms.TextBox();
            this.DataportBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // MsgBox
            // 
            this.MsgBox.Location = new System.Drawing.Point(12, 12);
            this.MsgBox.Multiline = true;
            this.MsgBox.Name = "MsgBox";
            this.MsgBox.Size = new System.Drawing.Size(275, 191);
            this.MsgBox.TabIndex = 0;
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Location = new System.Drawing.Point(311, 180);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(75, 23);
            this.ConnectBtn.TabIndex = 2;
            this.ConnectBtn.Text = "重新连接";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            // 
            // DisconnectBtn
            // 
            this.DisconnectBtn.Location = new System.Drawing.Point(311, 151);
            this.DisconnectBtn.Name = "DisconnectBtn";
            this.DisconnectBtn.Size = new System.Drawing.Size(75, 23);
            this.DisconnectBtn.TabIndex = 3;
            this.DisconnectBtn.Text = "断开连接";
            this.DisconnectBtn.UseVisualStyleBackColor = true;
            // 
            // IpaddBox
            // 
            this.IpaddBox.Location = new System.Drawing.Point(311, 39);
            this.IpaddBox.Name = "IpaddBox";
            this.IpaddBox.Size = new System.Drawing.Size(121, 21);
            this.IpaddBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(309, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "IP地址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(309, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "命令端口";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(309, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "数据端口";
            // 
            // CommportBox
            // 
            this.CommportBox.Location = new System.Drawing.Point(311, 78);
            this.CommportBox.Name = "CommportBox";
            this.CommportBox.Size = new System.Drawing.Size(75, 21);
            this.CommportBox.TabIndex = 8;
            // 
            // DataportBox
            // 
            this.DataportBox.Location = new System.Drawing.Point(311, 117);
            this.DataportBox.Name = "DataportBox";
            this.DataportBox.Size = new System.Drawing.Size(75, 21);
            this.DataportBox.TabIndex = 9;
            // 
            // AutoConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 216);
            this.Controls.Add(this.DataportBox);
            this.Controls.Add(this.CommportBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IpaddBox);
            this.Controls.Add(this.DisconnectBtn);
            this.Controls.Add(this.ConnectBtn);
            this.Controls.Add(this.MsgBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AutoConnect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "自动网络连接";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button ConnectBtn;
        public System.Windows.Forms.Button DisconnectBtn;
        public System.Windows.Forms.TextBox IpaddBox;
        public System.Windows.Forms.TextBox CommportBox;
        public System.Windows.Forms.TextBox DataportBox;
        public System.Windows.Forms.TextBox MsgBox;
    }
}