using System;
using System.Collections;
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
    public partial class NodeInfoForm : Office2007Form
    {
        string xmldoc;
        int GridIndex = 0;
        Hashtable NodeInfo = new Hashtable();
        public NodeInfoForm()
        {
            InitializeComponent();
            string MyExecPath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            xmldoc = MyExecPath + "\\" + "config.xml"; 
        }

        private void NodeInfoForm_Load(object sender, EventArgs e)
        {
            Set1Box.DataSource = Enum.GetValues(typeof(SourceDataClass.DeviceAddr));
            Set2Box.DataSource = Enum.GetValues(typeof(SourceDataClass.DeviceAddr));
            Set1Box.SelectedIndex = 0;
            Set2Box.SelectedIndex = 0;
            NodeInfo = (Hashtable)MainForm.pMainForm.mapdoc.nodeinfoMap.Clone();
            foreach (string node in NodeInfo.Keys)
            {
                nodeinfolist.Items.Add(ParseDataAndDisplay((BitArray)NodeInfo[node],false));
            }
        }

        private void ConfBtn_Click(object sender, EventArgs e)
        {
            if (nodeinfolist.Items.Count > 0)
            {
                if (DestNodeBox.Text == "")
                    return;
                int nodenum = nodeinfolist.Items.Count;
                int[] dat = new int[1];
                SourceDataClass.InitForPack(nodenum * 115 + 6 + 20);
                dat[0] = 2;
                SourceDataClass.OutPutIntBit(dat, 8);
                dat[0] = nodenum * 115 + 6 + 20;
                SourceDataClass.OutPutIntBit(dat, 12);
                dat[0] = nodenum;
                SourceDataClass.OutPutIntBit(dat, 6);//节点数
                foreach (string nodename in NodeInfo.Keys)
                {
                    SourceDataClass.OutPutArrayBit((BitArray)NodeInfo[nodename]);
                }

                //加入列表
                MainForm.pMainForm.comlistwin.AddCmd("节点" + DestNodeBox.Text.TrimStart('节','点'), "节点信息表", SourceDataClass.packdata);
                MainForm.pMainForm.RefreshListStat();
                MessageBox.Show("节点信息表已加入命令列表!");

                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("尚未添加任何节点信息!");
                DialogResult = DialogResult.Ignore;
            }
        }

        private void NodeNameBox_DropDown(object sender, EventArgs e)
        {
            NodeNameBox.Items.Clear();
            //读取节点
            XmlDocument xmlfile = new XmlDocument();
            xmlfile.Load(xmldoc);
            XmlNode xn = xmlfile.DocumentElement;
            xn = xn.SelectSingleNode("descendant::节点配置");
            foreach (XmlNode subnode in xn.ChildNodes)
            {
                NodeNameBox.Items.Add(subnode.Name);
            }
            
        }


        private void NodeNameBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string startnode = (string)NodeNameBox.SelectedItem;
            try
            {
                string[] sstr = { "节点配置", startnode, "节点位置", "纬度" };
                double slat = double.Parse(XmlHelper.GetConfigValue(xmldoc, sstr));
                string[] lngsstr = { "节点配置", startnode, "节点位置", "经度" };
                double slng = double.Parse(XmlHelper.GetConfigValue(xmldoc, lngsstr));
                Lat.Text = slat.ToString();
                Lang.Text = slng.ToString();
            }
            catch 
            { 
                Lat.Text = "";
                Lang.Text = "";
            }

        }

        private void nodeinfolist_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridIndex = nodeinfolist.SelectedIndex;
            if (GridIndex < 0)
                return;
            string str = (string)nodeinfolist.SelectedItem;
            string[] info = str.Split(':');//info[0]是节点号
            BitArray ba = (BitArray)NodeInfo[info[0]];
            ParseDataAndDisplay(ba, true);
        }

        //将信息加入下面的列表
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (NodeNameBox.Text == "")
                return;
            try
            {
                int[] dat = new int[1];
                SourceDataClass.clear();
                SourceDataClass.InitForPack(115);
                dat[0] = int.Parse(NodeNameBox.Text.TrimStart('节', '点'));
                SourceDataClass.OutPutIntBit(dat, 6);
                dat[0] = Nodetypebox.Text == "静态节点" ? 0 : 1;
                SourceDataClass.OutPutIntBit(dat, 1);
                dat[0] = int.Parse(EmitSet.Text);
                SourceDataClass.OutPutIntBit(dat, 3);
                dat[0] = Convert.ToInt32(Enum.Parse(typeof(SourceDataClass.DeviceAddr), Set1Box.Text));
                SourceDataClass.OutPutIntBit(dat, 8);
                dat[0] = Convert.ToInt32(Enum.Parse(typeof(SourceDataClass.DeviceAddr), Set2Box.Text));
                SourceDataClass.OutPutIntBit(dat, 8);
                int energy = int.Parse(leftenergy.Text);
                if (energy < 5) dat[0] = 0;
                else if ((energy >= 5) && (energy < 20)) dat[0] = 1;
                else if ((energy >= 20) && (energy < 35)) dat[0] = 2;
                else if ((energy >= 35) && (energy < 50)) dat[0] = 3;
                else if ((energy >= 50) && (energy < 65)) dat[0] = 4;
                else if ((energy >= 65) && (energy < 80)) dat[0] = 5;
                else if ((energy >= 80) && (energy < 95)) dat[0] = 6;
                else if ((energy >= 95)) dat[0] = 7;
                SourceDataClass.OutPutIntBit(dat, 3);
                BitArray a = new BitArray(16);
                for (int i = 0; i < 16; i++)
                   a[i] =  CommType.GetItemChecked(i);
                SourceDataClass.OutPutArrayBit(a);
                
                
                if (Lang.Value < 0)
                {
                    dat[0] = 0x8ffffff + (int)((double)Math.Abs(Lang.Value) * 60 * 10000);
                }
                else
                {
                    dat[0] = (int)((double)Math.Abs(Lang.Value) * 60 * 10000);
                }
                SourceDataClass.OutPutIntBit(dat, 28);
                if (Lat.Value < 0)
                {
                    dat[0] = 0x8ffffff + (int)((double)Math.Abs(Lat.Value) * 60 * 10000);
                }
                else
                {
                    dat[0] = (int)((double)Math.Abs(Lat.Value) * 60 * 10000);
                }
                SourceDataClass.OutPutIntBit(dat, 28);
                dat[0] = (int)(DepthInput.Value / 0.5);
                SourceDataClass.OutPutIntBit(dat, 14);
                if (NodeInfo.ContainsKey(NodeNameBox.Text))
                {
                    NodeInfo.Remove(NodeNameBox.Text);
                }
                //加入hash表
                NodeInfo.Add(NodeNameBox.Text, SourceDataClass.packdata);
                //替换之前的记录
                string log = ParseDataAndDisplay((BitArray)NodeInfo[NodeNameBox.Text],false);
                if (nodeinfolist.Items.Count == 0)//第一条记录
                {
                    nodeinfolist.Items.Add(log);
                }
                for (int i = 0; i < nodeinfolist.Items.Count; i++)
                {
                    if (CheckListNodeName(i, NodeNameBox.Text))
                    {
                        nodeinfolist.Items[i] = log;
                        break;
                    }
                    if (i == nodeinfolist.Items.Count - 1)//最后一项也比过了，说明当前列表中没有
                    {
                        nodeinfolist.Items.Add(log);
                    }
                }
                SourceDataClass.clear();
            }
            catch (Exception ex)
            {
                SourceDataClass.clear();
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 解析列表某一行，如果有需要的节点号则返回真
        /// </summary>
        /// <param name="index"></param>
        private bool CheckListNodeName(int index,string name)
        {
            string info = (string)nodeinfolist.Items[index];
            string[] infos = info.Split(':');
            if (infos[0].Equals(name))
                return true;
            return false;
        }

        /// <summary>
        /// 解析数据并显示在对话框中,如果shown为真，则同时在对话框的设置项中显示单独数据
        /// </summary>
        /// <param name="ba"></param>
        private string ParseDataAndDisplay(BitArray ba,bool shown)
        {
            MainForm.ParseLock.WaitOne();
            SourceDataClass.GetData(ba);
            int nodeid = SourceDataClass.GetIntValueFromBit(6);
            int nodetype = SourceDataClass.GetIntValueFromBit(1);
            int emit = SourceDataClass.GetIntValueFromBit(3);
            int set1 = SourceDataClass.GetIntValueFromBit(8);
            int set2 = SourceDataClass.GetIntValueFromBit(8);
            int energy = SourceDataClass.GetIntValueFromBit(3);
            Int16 commtype = (short)SourceDataClass.GetIntValueFromBit(16);
            int n = SourceDataClass.GetIntValueFromBit(28);
            double lang = 0;
            if (n >> 27 == 1)//西经
            {
                n &= 0x7ffffff;
                lang = (double)n / 10000 / 60;
                lang = -lang;
            }
            else//北纬
            {
                n &= 0x7ffffff;
                lang = (double)n / 10000 / 60;
               
            }
            n = SourceDataClass.GetIntValueFromBit(28);
            double lat = 0;
            if (n >> 27 == 1)//南纬
            {
                n &= 0x7ffffff;
                lat = (double)n / 10000 / 60;
                lat = -lang;
            }
            else//北纬
            {
                n &= 0x7ffffff;
                lat = (double)n / 10000 / 60;

            }
            double depth  = SourceDataClass.GetIntValueFromBit(14)*0.5;
            SourceDataClass.clear();
            MainForm.ParseLock.ReleaseMutex();
            if(shown)
            {
                NodeNameBox.Text = "节点" + nodeid;
                Nodetypebox.Text = nodetype==1?"移动节点":"静态节点";
                EmitSet.Value = emit;
                Set1Box.Text = Enum.GetName(typeof(SourceDataClass.DeviceAddr), set1);
                Set2Box.Text = Enum.GetName(typeof(SourceDataClass.DeviceAddr), set2);
                if (energy == 0) leftenergy.Value = 5;
                else if (energy == 1) leftenergy.Value = 20;
                else if (energy == 2) leftenergy.Value = 35;
                else if (energy == 3) leftenergy.Value = 50;
                else if (energy == 4) leftenergy.Value = 65;
                else if (energy == 5) leftenergy.Value = 80;
                else if (energy == 6) leftenergy.Value = 95;
                byte[] b = BitConverter.GetBytes(commtype);
                BitArray a = new BitArray(b);
                for (int i = 0; i < 16; i++)
                {
                    CommType.SetItemChecked(i,false);
                }
                for (int i = 0; i < a.Count; i++)
                {
                    CommType.SetItemChecked(i, a[i]);
                }
                Lat.Value = lat;
                Lang.Value = lang;
                DepthInput.Value = depth;
            }
            return "节点" + nodeid + ":" + ((nodetype == 1) ? " 移动节点" : " 静态节点") + " 换能器数" + emit.ToString()
                + " " + Enum.GetName(typeof(SourceDataClass.DeviceAddr), set1) + " " + Enum.GetName(typeof(SourceDataClass.DeviceAddr), set2)
                + " 能量 " + energy.ToString() + " 纬度 " + lat.ToString() + " 经度 " + lang.ToString() + " 深度 "
                + depth.ToString();
        }
        private void SaveNodeList_Click(object sender, EventArgs e)
        {
            MainForm.pMainForm.mapdoc.nodeinfoMap = (Hashtable)NodeInfo.Clone();
            MainForm.pMainForm.mapdoc.SaveInit();
        }

        private void nodeinfolist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (GridIndex > -1)
                {
                    string message = "是否删除当前选择节点信息？";
                    string caption = "询问";
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                    MessageBoxIcon icon = MessageBoxIcon.Question;
                    MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button2;
                    // Show message box
                    DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
                    if (result == DialogResult.OK)
                    {

                        string str = (string)nodeinfolist.Items[GridIndex];
                        string[] info = str.Split(':');
                        NodeInfo.Remove(info[0]);
                        nodeinfolist.Items.RemoveAt(GridIndex);
                        
                    }
                }
            }
        }

        private void DestNodeBox_DropDown(object sender, EventArgs e)
        {
            DestNodeBox.Items.Clear();
            //读取节点
            XmlDocument xmlfile = new XmlDocument();
            xmlfile.Load(xmldoc);
            XmlNode xn = xmlfile.DocumentElement;
            xn = xn.SelectSingleNode("descendant::节点配置");
            foreach (XmlNode subnode in xn.ChildNodes)
            {
                DestNodeBox.Items.Add(subnode.Name);
            }
        }


       

    }
}
