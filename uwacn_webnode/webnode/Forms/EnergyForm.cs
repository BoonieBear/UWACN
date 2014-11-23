using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Globalization;
namespace webnode.Forms
{
    public partial class EnergyForm : Office2007Form
    {
        public EnergyForm()
        {
            InitializeComponent();
        }

        private void ConfBtn_Click(object sender, EventArgs e)
        {

        }

        private void V48left_Validated(object sender, EventArgs e)
        {
            //double d = double.Parse(V48left.Text.Replace(" ","0"));
            //if (d > 41667)
            //{
            //    MessageBox.Show("剩余电量不能大于41667！");
            //    V48used.Text = "";
            //    return;
            //}
            //V48used.Text = (41667 - d).ToString(".000", CultureInfo.InvariantCulture).PadLeft(9,'0');
        }

        private void V3left_Validated(object sender, EventArgs e)
        {
            //double d = double.Parse(V3left.Text.Replace(" ", "0"));
            //if (d > 68000)
            //{
            //    MessageBox.Show("剩余电量不能大于68000！");
            //    V3used.Text = "";
            //    return;
            //}
            //V3used.Text = (68000 - d).ToString(".000", CultureInfo.InvariantCulture).PadLeft(9, '0');
        }







    }
}
