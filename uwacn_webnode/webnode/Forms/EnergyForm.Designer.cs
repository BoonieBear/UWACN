namespace webnode.Forms
{
    partial class EnergyForm
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
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.hightime = new DevComponents.DotNetBar.Controls.MaskedTextBoxAdv();
            this.medtime = new DevComponents.DotNetBar.Controls.MaskedTextBoxAdv();
            this.LowTime = new DevComponents.DotNetBar.Controls.MaskedTextBoxAdv();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.worktime = new DevComponents.DotNetBar.Controls.MaskedTextBoxAdv();
            this.waketime = new DevComponents.DotNetBar.Controls.MaskedTextBoxAdv();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.V3used = new DevComponents.DotNetBar.Controls.MaskedTextBoxAdv();
            this.V3left = new DevComponents.DotNetBar.Controls.MaskedTextBoxAdv();
            this.V48used = new DevComponents.DotNetBar.Controls.MaskedTextBoxAdv();
            this.V48left = new DevComponents.DotNetBar.Controls.MaskedTextBoxAdv();
            this.Votage48 = new DevComponents.DotNetBar.Controls.MaskedTextBoxAdv();
            this.Votage33 = new DevComponents.DotNetBar.Controls.MaskedTextBoxAdv();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.ConfBtn = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.hightime);
            this.groupPanel1.Controls.Add(this.medtime);
            this.groupPanel1.Controls.Add(this.LowTime);
            this.groupPanel1.Controls.Add(this.labelX5);
            this.groupPanel1.Controls.Add(this.labelX4);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel1.DrawTitleBox = false;
            this.groupPanel1.IsShadowEnabled = true;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(411, 111);
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
            this.groupPanel1.TabIndex = 0;
            this.groupPanel1.Text = "48V电流工作状态（s）";
            // 
            // hightime
            // 
            // 
            // 
            // 
            this.hightime.BackgroundStyle.Class = "TextBoxBorder";
            this.hightime.ButtonClear.Visible = true;
            this.hightime.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.hightime.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.hightime.Location = new System.Drawing.Point(91, 51);
            this.hightime.Mask = "9999999999";
            this.hightime.Name = "hightime";
            this.hightime.Size = new System.Drawing.Size(89, 21);
            this.hightime.TabIndex = 8;
            this.hightime.Text = "";
            this.hightime.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.hightime.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            // 
            // medtime
            // 
            // 
            // 
            // 
            this.medtime.BackgroundStyle.Class = "TextBoxBorder";
            this.medtime.ButtonClear.Visible = true;
            this.medtime.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.medtime.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.medtime.Location = new System.Drawing.Point(293, 15);
            this.medtime.Mask = "9999999999";
            this.medtime.Name = "medtime";
            this.medtime.Size = new System.Drawing.Size(89, 21);
            this.medtime.TabIndex = 7;
            this.medtime.Text = "";
            this.medtime.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.medtime.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            // 
            // LowTime
            // 
            // 
            // 
            // 
            this.LowTime.BackgroundStyle.Class = "TextBoxBorder";
            this.LowTime.ButtonClear.Visible = true;
            this.LowTime.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.LowTime.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.LowTime.Location = new System.Drawing.Point(90, 15);
            this.LowTime.Mask = "9999999999";
            this.LowTime.Name = "LowTime";
            this.LowTime.Size = new System.Drawing.Size(90, 21);
            this.LowTime.TabIndex = 6;
            this.LowTime.Text = "";
            this.LowTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.LowTime.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            // 
            // labelX5
            // 
            this.labelX5.Location = new System.Drawing.Point(9, 52);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(75, 23);
            this.labelX5.TabIndex = 5;
            this.labelX5.Text = "高电流时间";
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(196, 15);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(91, 23);
            this.labelX4.TabIndex = 3;
            this.labelX4.Text = "中等电流时间";
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(9, 13);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "低电流时间";
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.worktime);
            this.groupPanel2.Controls.Add(this.waketime);
            this.groupPanel2.Controls.Add(this.labelX6);
            this.groupPanel2.Controls.Add(this.labelX2);
            this.groupPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel2.DrawTitleBox = false;
            this.groupPanel2.IsShadowEnabled = true;
            this.groupPanel2.Location = new System.Drawing.Point(0, 111);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(411, 83);
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
            this.groupPanel2.TabIndex = 1;
            this.groupPanel2.Text = "单片机工作和休眠时间（H）";
            // 
            // worktime
            // 
            // 
            // 
            // 
            this.worktime.BackgroundStyle.Class = "TextBoxBorder";
            this.worktime.ButtonClear.Visible = true;
            this.worktime.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.worktime.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.worktime.Location = new System.Drawing.Point(293, 17);
            this.worktime.Mask = "999999";
            this.worktime.Name = "worktime";
            this.worktime.Size = new System.Drawing.Size(89, 21);
            this.worktime.TabIndex = 5;
            this.worktime.Text = "";
            this.worktime.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.worktime.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            // 
            // waketime
            // 
            // 
            // 
            // 
            this.waketime.BackgroundStyle.Class = "TextBoxBorder";
            this.waketime.ButtonClear.Visible = true;
            this.waketime.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.waketime.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.waketime.Location = new System.Drawing.Point(90, 17);
            this.waketime.Mask = "999999";
            this.waketime.Name = "waketime";
            this.waketime.Size = new System.Drawing.Size(90, 21);
            this.waketime.TabIndex = 4;
            this.waketime.Text = "";
            this.waketime.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.waketime.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            // 
            // labelX6
            // 
            this.labelX6.Location = new System.Drawing.Point(196, 15);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(75, 23);
            this.labelX6.TabIndex = 3;
            this.labelX6.Text = "工作时间";
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(9, 15);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "休眠时间";
            // 
            // groupPanel3
            // 
            this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.V3used);
            this.groupPanel3.Controls.Add(this.V3left);
            this.groupPanel3.Controls.Add(this.V48used);
            this.groupPanel3.Controls.Add(this.V48left);
            this.groupPanel3.Controls.Add(this.Votage48);
            this.groupPanel3.Controls.Add(this.Votage33);
            this.groupPanel3.Controls.Add(this.labelX11);
            this.groupPanel3.Controls.Add(this.labelX10);
            this.groupPanel3.Controls.Add(this.labelX9);
            this.groupPanel3.Controls.Add(this.labelX8);
            this.groupPanel3.Controls.Add(this.labelX7);
            this.groupPanel3.Controls.Add(this.labelX3);
            this.groupPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel3.DrawTitleBox = false;
            this.groupPanel3.IsShadowEnabled = true;
            this.groupPanel3.Location = new System.Drawing.Point(0, 194);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new System.Drawing.Size(411, 138);
            // 
            // 
            // 
            this.groupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel3.Style.BackColorGradientAngle = 90;
            this.groupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderBottomWidth = 1;
            this.groupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderLeftWidth = 1;
            this.groupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderRightWidth = 1;
            this.groupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderTopWidth = 1;
            this.groupPanel3.Style.CornerDiameter = 4;
            this.groupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel3.TabIndex = 2;
            this.groupPanel3.Text = "电源数据（mAH）";
            // 
            // V3used
            // 
            // 
            // 
            // 
            this.V3used.BackgroundStyle.Class = "TextBoxBorder";
            this.V3used.ButtonClear.Visible = true;
            this.V3used.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.V3used.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.V3used.Location = new System.Drawing.Point(291, 72);
            this.V3used.Mask = "99999.999";
            this.V3used.Name = "V3used";
            this.V3used.PromptChar = '0';
            this.V3used.Size = new System.Drawing.Size(89, 21);
            this.V3used.TabIndex = 17;
            this.V3used.Text = "";
            this.V3used.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.V3used.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            // 
            // V3left
            // 
            // 
            // 
            // 
            this.V3left.BackgroundStyle.Class = "TextBoxBorder";
            this.V3left.ButtonClear.Visible = true;
            this.V3left.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.V3left.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.V3left.Location = new System.Drawing.Point(98, 75);
            this.V3left.Mask = "99999.999";
            this.V3left.Name = "V3left";
            this.V3left.PromptChar = '0';
            this.V3left.Size = new System.Drawing.Size(90, 21);
            this.V3left.TabIndex = 16;
            this.V3left.Text = "";
            this.V3left.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.V3left.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.V3left.Validated += new System.EventHandler(this.V3left_Validated);
            // 
            // V48used
            // 
            // 
            // 
            // 
            this.V48used.BackgroundStyle.Class = "TextBoxBorder";
            this.V48used.ButtonClear.Visible = true;
            this.V48used.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.V48used.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.V48used.Location = new System.Drawing.Point(291, 40);
            this.V48used.Mask = "99999.999";
            this.V48used.Name = "V48used";
            this.V48used.PromptChar = '0';
            this.V48used.Size = new System.Drawing.Size(89, 21);
            this.V48used.TabIndex = 15;
            this.V48used.Text = "";
            this.V48used.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.V48used.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            // 
            // V48left
            // 
            // 
            // 
            // 
            this.V48left.BackgroundStyle.Class = "TextBoxBorder";
            this.V48left.ButtonClear.Visible = true;
            this.V48left.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.V48left.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.V48left.Location = new System.Drawing.Point(98, 42);
            this.V48left.Mask = "99999.999";
            this.V48left.Name = "V48left";
            this.V48left.PromptChar = '0';
            this.V48left.Size = new System.Drawing.Size(90, 21);
            this.V48left.TabIndex = 14;
            this.V48left.Text = "";
            this.V48left.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.V48left.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.V48left.Validated += new System.EventHandler(this.V48left_Validated);
            // 
            // Votage48
            // 
            // 
            // 
            // 
            this.Votage48.BackgroundStyle.Class = "TextBoxBorder";
            this.Votage48.ButtonClear.Visible = true;
            this.Votage48.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.Votage48.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.Votage48.Location = new System.Drawing.Point(291, 8);
            this.Votage48.Mask = "999.999";
            this.Votage48.Name = "Votage48";
            this.Votage48.PromptChar = '0';
            this.Votage48.Size = new System.Drawing.Size(89, 21);
            this.Votage48.TabIndex = 13;
            this.Votage48.Text = "";
            this.Votage48.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Votage48.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            // 
            // Votage33
            // 
            // 
            // 
            // 
            this.Votage33.BackgroundStyle.Class = "TextBoxBorder";
            this.Votage33.ButtonClear.Visible = true;
            this.Votage33.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.Votage33.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.Votage33.Location = new System.Drawing.Point(98, 9);
            this.Votage33.Mask = "9.999";
            this.Votage33.Name = "Votage33";
            this.Votage33.PromptChar = '0';
            this.Votage33.Size = new System.Drawing.Size(90, 21);
            this.Votage33.TabIndex = 12;
            this.Votage33.Text = "";
            this.Votage33.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Votage33.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            // 
            // labelX11
            // 
            this.labelX11.Location = new System.Drawing.Point(202, 73);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(75, 23);
            this.labelX11.TabIndex = 11;
            this.labelX11.Text = "3V已用电量";
            // 
            // labelX10
            // 
            this.labelX10.Location = new System.Drawing.Point(9, 73);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(75, 23);
            this.labelX10.TabIndex = 9;
            this.labelX10.Text = "3V剩余电量";
            // 
            // labelX9
            // 
            this.labelX9.Location = new System.Drawing.Point(202, 41);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(75, 23);
            this.labelX9.TabIndex = 7;
            this.labelX9.Text = "48V已用电量";
            // 
            // labelX8
            // 
            this.labelX8.Location = new System.Drawing.Point(9, 41);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(75, 23);
            this.labelX8.TabIndex = 5;
            this.labelX8.Text = "48V剩余电量";
            // 
            // labelX7
            // 
            this.labelX7.Location = new System.Drawing.Point(202, 9);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(75, 23);
            this.labelX7.TabIndex = 3;
            this.labelX7.Text = "48V电压值";
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(9, 9);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(75, 23);
            this.labelX3.TabIndex = 1;
            this.labelX3.Text = "3.3V电压值";
            // 
            // ConfBtn
            // 
            this.ConfBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ConfBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ConfBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ConfBtn.Location = new System.Drawing.Point(321, 339);
            this.ConfBtn.Name = "ConfBtn";
            this.ConfBtn.Size = new System.Drawing.Size(75, 23);
            this.ConfBtn.TabIndex = 3;
            this.ConfBtn.Text = "确定";
            this.ConfBtn.Click += new System.EventHandler(this.ConfBtn_Click);
            // 
            // EnergyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 365);
            this.Controls.Add(this.ConfBtn);
            this.Controls.Add(this.groupPanel3);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.groupPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "EnergyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "写入能源信息";
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel3;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.ButtonX ConfBtn;
        public DevComponents.DotNetBar.Controls.MaskedTextBoxAdv Votage33;
        public DevComponents.DotNetBar.Controls.MaskedTextBoxAdv V48left;
        public DevComponents.DotNetBar.Controls.MaskedTextBoxAdv Votage48;
        public DevComponents.DotNetBar.Controls.MaskedTextBoxAdv V3used;
        public DevComponents.DotNetBar.Controls.MaskedTextBoxAdv V3left;
        public DevComponents.DotNetBar.Controls.MaskedTextBoxAdv V48used;
        public DevComponents.DotNetBar.Controls.MaskedTextBoxAdv hightime;
        public DevComponents.DotNetBar.Controls.MaskedTextBoxAdv medtime;
        public DevComponents.DotNetBar.Controls.MaskedTextBoxAdv LowTime;
        public DevComponents.DotNetBar.Controls.MaskedTextBoxAdv worktime;
        public DevComponents.DotNetBar.Controls.MaskedTextBoxAdv waketime;
    }
}