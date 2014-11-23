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
    public partial class SetADLimiteForm : Office2007Form
    {
        public SetADLimiteForm()
        {
            InitializeComponent();
        }

        private void slider1_ValueChanged(object sender, EventArgs e)
        {
            ConfBtn.Text = slider1.Value.ToString();
        }
    }
}
