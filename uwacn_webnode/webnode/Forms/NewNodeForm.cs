using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using webnode.Helper;
using System.Globalization;
using System.Windows.Forms;
using GMap.NET;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Rendering;

namespace webnode.Forms
{
    public partial class NewNodeForm : Office2007Form
    {
        //PointLatLng GmapToGpsOffset = new PointLatLng(-0.002649654980715, -0.00476212229727);
        DataSet ds;
        MapForm p = null;
        public bool isRight = false;
        string xmldoc;
        public NewNodeForm(MapForm form)
        {
            InitializeComponent();
            string MyExecPath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            xmldoc =MyExecPath + "\\" + "config.xml";
            ds = new DataSet();
            
            p = form;
            ds.ReadXml(xmldoc);
        }



        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            p.isNodeChosePlace = true;
        }

        private void NodeNameBox_ValueChanged(object sender, EventArgs e)
        {
            if (NodeNameBox.Text != "")
            {

                ComfirmBtn.Enabled = true;
            }
            else
            {
                ComfirmBtn.Enabled = false;
            }
        }

        private void GetGps_Click(object sender, EventArgs e)
        {
            PointLatLng pt = new PointLatLng(MainForm.pMainForm.mapdoc.NodeMarker.Position.Lat, MainForm.pMainForm.mapdoc.NodeMarker.Position.Lng);
            pt.Offset(p.GmapToGpsOffset);
            LngBox.Text = pt.Lng.ToString("F08");
            LatBox.Text = pt.Lat.ToString("F08");
        }

        private void ComfirmBtn_Click(object sender, EventArgs e)
        {
            isRight = true;
            if (!MainForm.pMainForm.mapdoc.isModify)
            {
                string fullname = "节点" + NodeNameBox.Text;

                string[] str = { "节点配置", fullname };
                if (XmlHelper.GetConfigValue(xmldoc, str) != null)
                {
                    MessageBox.Show("该编号已使用");
                    ComfirmBtn.Enabled = false;
                    isRight = false;
                }
                
            }
            

            
        }




    }
}
