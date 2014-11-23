using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Xml;
using webnode.Helper;
using System.Globalization;
using System.Collections;
namespace webnode.Forms
{
    public partial class CommTypeForm : Office2007Form
    {
        string xmldoc;
        public CommTypeForm()
        {
            InitializeComponent();
            string MyExecPath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            xmldoc = MyExecPath + "\\" + "config.xml";
        }

        private void DestNodeName_DropDown(object sender, EventArgs e)
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
            if (DestNodeName.Text == "")
                return;
            int[] dat = new int[1];
            SourceDataClass.clear();
            SourceDataClass.InitForPack(20+16);
            dat[0] = 142;
            SourceDataClass.OutPutIntBit(dat, 8);
            dat[0] = 36;
            SourceDataClass.OutPutIntBit(dat, 12);
            BitArray a = new BitArray(16);
            for (int i = 0; i < 16; i++)
                a[i] = CommType.GetItemChecked(i);
            SourceDataClass.OutPutArrayBit(a);
            //加入列表
            MainForm.pMainForm.comlistwin.AddCmd(DestNodeName.Text, "通信制式开关", SourceDataClass.packdata);

            MainForm.pMainForm.RefreshListStat();
            MessageBox.Show("通信制式开关命令已加入命令列表!");
        }
    }
}
