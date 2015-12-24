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
    public partial class SupplyConfigForm : Office2007Form
    {
        public SupplyConfigForm()
        {
            InitializeComponent();
            HighSelect.SelectedIndex=0;
            LowSelect.SelectedIndex = 0;
        }

        private void Send_Click(object sender, EventArgs e)
        {

        }
    }
}
