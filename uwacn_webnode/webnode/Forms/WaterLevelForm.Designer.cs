namespace webnode.Forms
{
    partial class WaterLevelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaterLevelForm));
            this.DataFileLine = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.ConfirmBtn = new DevComponents.DotNetBar.ButtonX();
            this.CurrentLine = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // DataFileLine
            // 
            // 
            // 
            // 
            this.DataFileLine.Border.Class = "TextBoxBorder";
            this.DataFileLine.Location = new System.Drawing.Point(108, 12);
            this.DataFileLine.Name = "DataFileLine";
            this.DataFileLine.Size = new System.Drawing.Size(100, 21);
            this.DataFileLine.TabIndex = 0;
            this.DataFileLine.Text = "60";
            // 
            // ConfirmBtn
            // 
            this.ConfirmBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ConfirmBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ConfirmBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ConfirmBtn.Location = new System.Drawing.Point(133, 74);
            this.ConfirmBtn.Name = "ConfirmBtn";
            this.ConfirmBtn.Size = new System.Drawing.Size(75, 23);
            this.ConfirmBtn.TabIndex = 1;
            this.ConfirmBtn.Text = "确定";
            // 
            // CurrentLine
            // 
            // 
            // 
            // 
            this.CurrentLine.Border.Class = "TextBoxBorder";
            this.CurrentLine.Location = new System.Drawing.Point(108, 43);
            this.CurrentLine.Name = "CurrentLine";
            this.CurrentLine.Size = new System.Drawing.Size(100, 21);
            this.CurrentLine.TabIndex = 2;
            this.CurrentLine.Text = "60";
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(15, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(87, 23);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "数据文件水位";
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(15, 43);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 4;
            this.labelX2.Text = "当前水位";
            // 
            // WaterLevelForm
            // 
            this.AcceptButton = this.ConfirmBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 103);
            this.ControlBox = false;
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.CurrentLine);
            this.Controls.Add(this.ConfirmBtn);
            this.Controls.Add(this.DataFileLine);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WaterLevelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "水位设置";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX ConfirmBtn;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        public DevComponents.DotNetBar.Controls.TextBoxX DataFileLine;
        public DevComponents.DotNetBar.Controls.TextBoxX CurrentLine;

    }
}