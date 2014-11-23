using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using webnode.Helper;
using GMap.NET;
namespace webnode.Forms
{
    public partial class ConfForm : Office2007Form
    {
        PointLatLng GmapToGpsOffset = new PointLatLng(-0.002649654980715, -0.00476212229727);
        public ConfForm()
        {
            InitializeComponent();
        }

        private void ConfForm_Load(object sender, EventArgs e)
        {
            COMM2Set.DataSource =Enum.GetValues(typeof(SourceDataClass.DeviceAddr));
            COMM3Set.DataSource = Enum.GetValues(typeof(SourceDataClass.DeviceAddr));
            EmitTypeBox.DataSource = Enum.GetValues(typeof(SourceDataClass.EmitType));
            NetCheck.SelectedIndex = 0;
            GetGps.PerformClick();
        }

        private void GetGps_Click(object sender, EventArgs e)
        {
            PointLatLng pt = new PointLatLng(MainForm.pMainForm.mapdoc.NodeMarker.Position.Lat, MainForm.pMainForm.mapdoc.NodeMarker.Position.Lng);
            pt.Offset(GmapToGpsOffset);
            langInput.Text = pt.Lng.ToString("F08");
            latinput.Text = pt.Lat.ToString("F08");
        }

        private void NodeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NodeType.SelectedIndex == 1)
            {
                AccessMode.Enabled = true;
            }
            else
            {
                AccessMode.Enabled = false;
                AccessMode.Text = "0";
                AccessMode.SelectedIndex = 1;
            }
        }


 


    }
}
