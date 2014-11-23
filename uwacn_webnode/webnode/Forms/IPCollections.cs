using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using webnode.Helper;
using System.Xml;
using System.Globalization;
namespace webnode.Forms
{
    public partial class IPCollections : Office2007Form
    {
        public IPCollections()
        {
            InitializeComponent();
        }

        private void IPList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IPList.SelectedIndex >= 0)
            {
                for (int i = 0; i < IPList.Items.Count; i++)
                {
                    IPList.SetItemChecked(i, false);
                }

                IPList.SetItemChecked(IPList.SelectedIndex, true);
            }

            
        }

        private void ConfBtn_Click(object sender, EventArgs e)
        {
            if (IPList.CheckedIndices.Count == 0)
                DialogResult = DialogResult.Cancel;
        }

       
    }
}
