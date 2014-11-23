using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Rendering;
namespace webnode.Forms
{
    public partial class CollectionForm : Office2007Form
    {
        public CollectionForm()
        {
            InitializeComponent();
        }


        private void CollectionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CollectionBox.CheckedIndices.Count > 0)
                buttonX1.Enabled = true;
            else
                buttonX1.Enabled = false;
            
            
        }

        private void SelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectAll.Checked)
            {
                for (int i = 0; i < CollectionBox.Items.Count; i++)
                {
                    CollectionBox.SetItemChecked(i,true);
                    buttonX1.Enabled = true;
                }
            }
            else
            {
                for (int i = 0; i < CollectionBox.Items.Count; i++)
                {
                    CollectionBox.SetItemChecked(i, false);
                    buttonX1.Enabled = false;
                }
            }
        }

    }
}
