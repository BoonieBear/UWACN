using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using webnode.Helper;
using DevComponents.DotNetBar;
namespace webnode.Forms
{
    public partial class EmitSetForm : Office2007Form
    {
        string xmldoc;
        public EmitSetForm()
        {
            InitializeComponent();
            string MyExecPath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            xmldoc = MyExecPath + "\\" + "config.xml";
        }
        private void AddToList_Click(object sender, EventArgs e)
        {
            int[] dat = new int[1];
            if (DestNodeName.Text != "")
            {
                
                SourceDataClass.InitForPack(20 +16);

                dat[0] = 141;
                SourceDataClass.OutPutIntBit(dat, 8);
                dat[0] = 36;
                SourceDataClass.OutPutIntBit(dat, 12);
                if (radioButton1.Checked)
                {
                    dat[0] = 0;
                    SourceDataClass.OutPutIntBit(dat, 1);
                    dat[0] = EmitAmp.Value;
                    SourceDataClass.OutPutIntBit(dat, 7);
                }
                else
                {
                    dat[0] = 1;
                    SourceDataClass.OutPutIntBit(dat, 1);
                    dat[0] = 0;
                    SourceDataClass.OutPutIntBit(dat, 7);
                }
                if (radioButton4.Checked)
                {
                    dat[0] = 0;
                    SourceDataClass.OutPutIntBit(dat, 1);
                    dat[0] = ReceGain.Value;
                    SourceDataClass.OutPutIntBit(dat, 7);
                }
                else
                {
                    dat[0] = 1;
                    SourceDataClass.OutPutIntBit(dat, 1);
                    dat[0] = 0;
                    SourceDataClass.OutPutIntBit(dat, 7);
                }
                //加入列表
                MainForm.pMainForm.comlistwin.AddCmd(DestNodeName.Text, "收发自动调节", SourceDataClass.packdata);

                MainForm.pMainForm.RefreshListStat();
                MessageBox.Show("收发自动调节命令已加入命令列表!");

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            EmitAmp.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            EmitAmp.Enabled = true;
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

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            ReceGain.Enabled = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            ReceGain.Enabled = false;
        }

        private void EmitSetForm_Load(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            radioButton3.Checked = true;
        }

        
    }
}
