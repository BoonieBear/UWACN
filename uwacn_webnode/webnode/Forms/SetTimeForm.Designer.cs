namespace webnode.Forms
{
    partial class SetTimeForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetTimeForm));
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.OKbtn = new DevComponents.DotNetBar.ButtonX();
            this.cancelbtn = new DevComponents.DotNetBar.ButtonX();
            this.dateTimeInput = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.UseGPSTime = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.UTCTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeInput)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(12, 25);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(44, 21);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "时间：";
            // 
            // OKbtn
            // 
            this.OKbtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.OKbtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.OKbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKbtn.Location = new System.Drawing.Point(124, 77);
            this.OKbtn.Name = "OKbtn";
            this.OKbtn.Size = new System.Drawing.Size(75, 23);
            this.OKbtn.TabIndex = 2;
            this.OKbtn.Text = "确定";
            // 
            // cancelbtn
            // 
            this.cancelbtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cancelbtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cancelbtn.Location = new System.Drawing.Point(205, 77);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.cancelbtn.TabIndex = 3;
            this.cancelbtn.Text = "取消";
            this.cancelbtn.Click += new System.EventHandler(this.cancelbtn_Click);
            // 
            // dateTimeInput
            // 
            // 
            // 
            // 
            this.dateTimeInput.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dateTimeInput.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dateTimeInput.ButtonDropDown.Visible = true;
            this.dateTimeInput.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimeInput.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dateTimeInput.Location = new System.Drawing.Point(61, 25);
            this.dateTimeInput.MinDate = new System.DateTime(2012, 11, 16, 0, 0, 0, 0);
            // 
            // 
            // 
            this.dateTimeInput.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateTimeInput.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dateTimeInput.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dateTimeInput.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dateTimeInput.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dateTimeInput.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dateTimeInput.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dateTimeInput.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dateTimeInput.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dateTimeInput.MonthCalendar.DisplayMonth = new System.DateTime(2012, 10, 1, 0, 0, 0, 0);
            this.dateTimeInput.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dateTimeInput.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateTimeInput.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dateTimeInput.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dateTimeInput.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dateTimeInput.MonthCalendar.TodayButtonVisible = true;
            this.dateTimeInput.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dateTimeInput.Name = "dateTimeInput";
            this.dateTimeInput.ShowUpDown = true;
            this.dateTimeInput.Size = new System.Drawing.Size(219, 21);
            this.dateTimeInput.TabIndex = 5;
            // 
            // UseGPSTime
            // 
            this.UseGPSTime.Location = new System.Drawing.Point(12, 77);
            this.UseGPSTime.Name = "UseGPSTime";
            this.UseGPSTime.Size = new System.Drawing.Size(75, 23);
            this.UseGPSTime.TabIndex = 6;
            this.UseGPSTime.Text = "UTC时间";
            this.UseGPSTime.CheckedChanged += new System.EventHandler(this.UseGPSTime_CheckedChanged);
            // 
            // UTCTimer
            // 
            this.UTCTimer.Tick += new System.EventHandler(this.UTCTimer_Tick);
            // 
            // SetTimeForm
            // 
            this.AcceptButton = this.OKbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 112);
            this.Controls.Add(this.UseGPSTime);
            this.Controls.Add(this.dateTimeInput);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.OKbtn);
            this.Controls.Add(this.labelX1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetTimeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置时间";
            this.Load += new System.EventHandler(this.SetTimeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX OKbtn;
        private DevComponents.DotNetBar.ButtonX cancelbtn;
        public DevComponents.Editors.DateTimeAdv.DateTimeInput dateTimeInput;
        private DevComponents.DotNetBar.Controls.CheckBoxX UseGPSTime;
        private System.Windows.Forms.Timer UTCTimer;
    }
}