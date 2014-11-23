using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
namespace webnode.Forms
{
    public partial class SetTimeForm : Office2007Form
    {
        public SetTimeForm()
        {
            InitializeComponent();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UseGPSTime_CheckedChanged(object sender, EventArgs e)
        {
            if (UseGPSTime.Checked)
                UTCTimer.Enabled = true;
        }

        private void UTCTimer_Tick(object sender, EventArgs e)
        {
            if (MainForm.pMainForm.GpsTimeUpdate.Text.Replace("UTC", "").Replace("GPS时间", "") != "")
            {
                dateTimeInput.Text = MainForm.pMainForm.GpsTimeUpdate.Text.Replace("UTC", "").Replace("GPS时间", "");
            }
        }

        private void SetTimeForm_Load(object sender, EventArgs e)
        {
            dateTimeInput.Value = DateTime.Now;
        }




    }
}
