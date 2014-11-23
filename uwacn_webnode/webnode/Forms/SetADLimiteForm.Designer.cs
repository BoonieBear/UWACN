namespace webnode.Forms
{
    partial class SetADLimiteForm
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
            this.slider1 = new DevComponents.DotNetBar.Controls.Slider();
            this.ConfBtn = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // slider1
            // 
            this.slider1.Location = new System.Drawing.Point(12, 12);
            this.slider1.Maximum = 99;
            this.slider1.Name = "slider1";
            this.slider1.Size = new System.Drawing.Size(264, 23);
            this.slider1.TabIndex = 0;
            this.slider1.Text = "门限";
            this.slider1.Value = 0;
            this.slider1.ValueChanged += new System.EventHandler(this.slider1_ValueChanged);
            // 
            // ConfBtn
            // 
            this.ConfBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ConfBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ConfBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ConfBtn.Location = new System.Drawing.Point(291, 12);
            this.ConfBtn.Name = "ConfBtn";
            this.ConfBtn.Size = new System.Drawing.Size(69, 23);
            this.ConfBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.ConfBtn.TabIndex = 1;
            this.ConfBtn.Text = "设为";
            this.ConfBtn.Tooltip = "点击确定";
            // 
            // SetADLimiteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 52);
            this.Controls.Add(this.ConfBtn);
            this.Controls.Add(this.slider1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetADLimiteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置AD门限";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.Slider slider1;
        public DevComponents.DotNetBar.ButtonX ConfBtn;
    }
}