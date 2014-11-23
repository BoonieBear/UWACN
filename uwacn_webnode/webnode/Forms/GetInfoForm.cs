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
    public partial class GetInfoForm : Office2007Form
    {
        string xmldoc;
        public GetInfoForm()
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
            int[] dat = new int[1];
            if (DestNodeName.Text !="")
            {
                
                if (this.Text == "获取节点信息")
                {
                    SourceDataClass.InitForPack(20);
                    dat[0] = 103;
                    SourceDataClass.OutPutIntBit(dat, 8);
                    dat[0] = 20;
                    SourceDataClass.OutPutIntBit(dat, 12);

                    //加入列表
                    MainForm.pMainForm.comlistwin.AddCmd(DestNodeName.Text, "获取节点信息", SourceDataClass.packdata);

                    MainForm.pMainForm.RefreshListStat();
                    MessageBox.Show("获取节点信息命令已加入命令列表!");
                }
                else if (this.Text == "获取路由信息")
                {
                    SourceDataClass.InitForPack(20);
                    dat[0] = 113;
                    SourceDataClass.OutPutIntBit(dat, 8);
                    dat[0] = 20;
                    SourceDataClass.OutPutIntBit(dat, 12);

                    //加入列表
                    MainForm.pMainForm.comlistwin.AddCmd(DestNodeName.Text, "获取路由信息", SourceDataClass.packdata);

                    MainForm.pMainForm.RefreshListStat();
                    MessageBox.Show("获取路由信息命令已加入命令列表!");
                }
                else if (this.Text == "获取设备数据")
                {
                    SourceDataClass.InitForPack(28);
                    dat[0] = 115;
                    SourceDataClass.OutPutIntBit(dat, 8);
                    dat[0] = 28;
                    SourceDataClass.OutPutIntBit(dat, 12);
                    dat[0] = 2;//默认值
                    if (CommBox.SelectedIndex != -1)
                        dat[0] = CommBox.SelectedIndex;
                    
                    SourceDataClass.OutPutIntBit(dat, 8);
                    //加入列表
                    MainForm.pMainForm.comlistwin.AddCmd(DestNodeName.Text, "获取设备数据", SourceDataClass.packdata);

                    MainForm.pMainForm.RefreshListStat();
                    MessageBox.Show("获取设备数据命令已加入命令列表!");
                }
                else if (this.Text == "获取邻节点信息")
                {
                    SourceDataClass.InitForPack(21);
                    dat[0] = 111;
                    SourceDataClass.OutPutIntBit(dat, 8);
                    dat[0] = 21;
                    SourceDataClass.OutPutIntBit(dat, 12);
                    dat[0] = 0;//默认值
                    if (RebuildBox.SelectedIndex == 0)
                        dat[0] = 0;
                    if (RebuildBox.SelectedIndex == 1)
                        dat[0] = 1;
                    SourceDataClass.OutPutIntBit(dat, 1);
                    //加入列表
                    MainForm.pMainForm.comlistwin.AddCmd(DestNodeName.Text, "获取邻节点信息", SourceDataClass.packdata);

                    MainForm.pMainForm.RefreshListStat();
                    MessageBox.Show("获取邻节点信息命令已加入命令列表!");
                }
                else if (this.Text == "获取网络简表")
                {
                    SourceDataClass.InitForPack(20);
                    dat[0] = 109;
                    SourceDataClass.OutPutIntBit(dat, 8);
                    dat[0] = 20;
                    SourceDataClass.OutPutIntBit(dat, 12);
                    
                    //加入列表
                    MainForm.pMainForm.comlistwin.AddCmd(DestNodeName.Text, "获取网络简表", SourceDataClass.packdata);

                    MainForm.pMainForm.RefreshListStat();
                    MessageBox.Show("获取网络简表命令已加入命令列表!");
                }
                else if (this.Text == "获取节点信息表")
                {
                    SourceDataClass.InitForPack(20);
                    dat[0] = 105;
                    SourceDataClass.OutPutIntBit(dat, 8);
                    dat[0] = 20;
                    SourceDataClass.OutPutIntBit(dat, 12);

                    //加入列表
                    MainForm.pMainForm.comlistwin.AddCmd(DestNodeName.Text, "获取节点信息表", SourceDataClass.packdata);

                    MainForm.pMainForm.RefreshListStat();
                    MessageBox.Show("获取节点信息表命令已加入命令列表!");
                }
                else if (this.Text == "获取网络表")
                {
                    SourceDataClass.InitForPack(21);
                    dat[0] = 107;
                    SourceDataClass.OutPutIntBit(dat, 8);
                    dat[0] = 21;
                    SourceDataClass.OutPutIntBit(dat, 12);
                    dat[0] = 0;//默认值
                    if (RebuildBox.SelectedIndex == 0)
                        dat[0] = 0;
                    if (RebuildBox.SelectedIndex == 1)
                        dat[0] = 1;
                    SourceDataClass.OutPutIntBit(dat, 1);
                    //加入列表
                    MainForm.pMainForm.comlistwin.AddCmd(DestNodeName.Text, "获取网络表", SourceDataClass.packdata);

                    MainForm.pMainForm.RefreshListStat();
                    MessageBox.Show("获取网络表命令已加入命令列表!");
                }
                else if (this.Text == "获取节点状态")
                {
                    SourceDataClass.InitForPack(20);
                    dat[0] = 121;
                    SourceDataClass.OutPutIntBit(dat, 8);
                    dat[0] = 20;
                    SourceDataClass.OutPutIntBit(dat, 12);

                    //加入列表

                    MainForm.pMainForm.comlistwin.AddCmd(DestNodeName.Text, "获取节点状态", SourceDataClass.packdata);

                    MainForm.pMainForm.RefreshListStat();
                    MessageBox.Show("获取节点状态命令已加入命令列表!");
                }
                else if (this.Text == "获取设备状态")
                {
                    SourceDataClass.InitForPack(28);
                    dat[0] = 117;
                    SourceDataClass.OutPutIntBit(dat, 8);
                    dat[0] = 28;
                    SourceDataClass.OutPutIntBit(dat, 12);
                    dat[0] = 2;//默认值
                    if (CommBox.SelectedIndex != -1)
                        dat[0] = CommBox.SelectedIndex;
                    SourceDataClass.OutPutIntBit(dat, 8);
                    //加入列表
                    MainForm.pMainForm.comlistwin.AddCmd(DestNodeName.Text, "获取设备状态", SourceDataClass.packdata);

                    MainForm.pMainForm.RefreshListStat();
                    MessageBox.Show("获取设备状态命令已加入命令列表!");
                }
                
            }
        }

        private void GetInfoForm_Load(object sender, EventArgs e)
        {
            COM_label.Visible = false;
            CommBox.Visible = false;
            labelX2.Visible = false;
            RebuildBox.Visible = false;
            CommBox.SelectedIndex = 3;
            RebuildBox.SelectedIndex = 0;
            if (this.Text == "获取设备数据")
            {
                COM_label.Visible = true;
                CommBox.Visible = true;
            }
            if (this.Text == "获取设备状态")
            {
                COM_label.Visible = true;
                CommBox.Visible = true;
            }
            if (this.Text == "获取邻节点信息")
            {
                labelX2.Visible = true;
                RebuildBox.Visible = true;
            }
            if (this.Text == "获取网络表")
            {
                labelX2.Visible = true;
                RebuildBox.Visible = true;
            }
        }
    }
}
