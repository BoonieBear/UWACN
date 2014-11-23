using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using webnode.Helper;
using System.Windows.Forms;
using DevComponents.DotNetBar;
namespace webnode.Forms
{
    public partial class DataBackSetForm :Office2007Form
    {
        string xmldoc;
        public DataBackSetForm()
        {
            InitializeComponent();
            string MyExecPath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            xmldoc = MyExecPath + "\\" + "config.xml";
        }

        private void NodeChoice_DropDown(object sender, EventArgs e)
        {
            //读取节点
            XmlDocument xmlfile = new XmlDocument();
            xmlfile.Load(xmldoc);
            XmlNode xn = xmlfile.DocumentElement;
            xn = xn.SelectSingleNode("descendant::节点配置");
            DestNodeName.Items.Clear();
            foreach (XmlNode subnode in xn.ChildNodes)
            {

                DestNodeName.Items.Add(subnode.Name);
            }
        }

        private void AddToList_Click(object sender, EventArgs e)
        {
            int[] dat = new int[1];
            if (DestNodeName.Text != "")
            {
                
                    SourceDataClass.InitForPack(20 + 32);
                    dat[0] = 140;
                    SourceDataClass.OutPutIntBit(dat, 8);
   
                    dat[0] = 52;
                    SourceDataClass.OutPutIntBit(dat, 12);
                    if (radioButton2.Checked)
                    {
                        dat[0] = SetTime.Value;
                        SourceDataClass.OutPutIntBit(dat, 32);
                    }
                    else
                    {
                        dat[0] = 0;
                        SourceDataClass.OutPutIntBit(dat, 32);
                    }
                    //加入列表
                    MainForm.pMainForm.comlistwin.AddCmd(DestNodeName.Text, "设备数据定时回传开关", SourceDataClass.packdata);

                    MainForm.pMainForm.RefreshListStat();
                    MessageBox.Show("设备数据定时回传开关命令已加入命令列表!");
                
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                SetTime.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                SetTime.Enabled = true;
        }
    }
}
