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
    public partial class SetParameterForm : Office2007Form
    {
        string xmldoc;
        public SetParameterForm()
        {
            InitializeComponent();
            string MyExecPath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            xmldoc = MyExecPath + "\\" + "config.xml";
        }

        private void AddToList_Click(object sender, EventArgs e)
        {
            int[] dat = new int[1];
            if (DestNodeName.Text == "")
            {
                MessageBox.Show("未选择目标节点！！！");
                return;
            }
            if (HexCheck.Checked)
            {
                byte[] end = CRCHelper.ConvertHexToChar(parameter.Text);
                if (end == null)
                    return;
                int len = 20 + 8 + end.Length * 8;
                SourceDataClass.InitForPack(len);
                dat[0] = 119;
                SourceDataClass.OutPutIntBit(dat, 8);
                dat[0] = len;
                SourceDataClass.OutPutIntBit(dat, 12);
                dat[0] = 2;//默认值
                if (CommBox.SelectedIndex == 0)
                    dat[0] = 2;
                if (CommBox.SelectedIndex == 1)
                    dat[0] = 3;
                SourceDataClass.OutPutIntBit(dat, 8);
                for (int i = 0; i < end.Length; i++)
                {
                    dat[0] = end[i];
                    SourceDataClass.OutPutIntBit(dat, 8);
                }   
                //SourceDataClass.OutPutIntBit(dat, endchar.Text.Length / 2 * 8);
                
            }
            else
            {
                int arraylen = parameter.Text.Length;//int[] 长度
                int len = 20 + 8 + arraylen * 8;
                SourceDataClass.InitForPack(len);
                dat[0] = 119;
                SourceDataClass.OutPutIntBit(dat, 8);
                dat[0] = len;
                SourceDataClass.OutPutIntBit(dat, 12);
                dat[0] = 2;//默认值
                if (CommBox.SelectedIndex == 0)
                    dat[0] = 2;
                if (CommBox.SelectedIndex == 1)
                    dat[0] = 3;

                byte[] para = Encoding.Default.GetBytes(parameter.Text);
                for (int i = 0; i < arraylen; i++)
                {
                    dat[0] = para[i];
                    SourceDataClass.OutPutIntBit(dat, 8);
                }
            }
            //加入列表
            MainForm.pMainForm.comlistwin.AddCmd(DestNodeName.Text, "设备参数设置", SourceDataClass.packdata);

            MainForm.pMainForm.RefreshListStat();
            MessageBox.Show("设备参数设置命令已加入命令列表!");
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
    }
}
