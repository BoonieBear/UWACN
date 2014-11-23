using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Xml;
using webnode.Helper;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;
namespace webnode.Forms
{
    public partial class PingForm : Office2007Form
    {
        string xmldoc;
        private  delegate void ShowDataHandle(int startid, string Backstr);

        List<BitArray> balst = new List<BitArray>();

        public PingForm()
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

        private void ViaNet_Click(object sender, EventArgs e)
        {
            if (DestNodeName.Text != "")
            {
                byte[] cmd = Pack();
                if (cmd.Length == 0)
                    return;
                byte[] netcmd = SourceDataClass.NetPackage(cmd);
                MainForm.pMainForm.CommandLineWin.SendCommand("gd -n");
                if (MainForm.pMainForm.CommandLineWin.SendData(netcmd))//正确发送
                {
                    BackText.Clear();
                    Commparelabel.Text = "---";
                    MessageBox.Show("命令已发送！");

                }
            }
            else
            {
                MessageBox.Show("请选择一个正确的目的地址！");

            }
        }

        private void ViaComm_Click(object sender, EventArgs e)
        {
            if (DestNodeName.Text != "")
            {
                byte[] cmd = Pack();
                if (cmd.Length == 0)
                    return;
                byte[] Serialcmd = SourceDataClass.CommPackage(171, cmd);
                if (MainForm.pMainForm.mapdoc.WriteMSPCommand(Serialcmd))
                {
                    BackText.Clear();
                    Commparelabel.Text = "---";
                    MainForm.pMainForm.mapdoc.MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MainForm.pMainForm.mapdoc.MSPCmdFile.BinaryWrite(Serialcmd);
                    MainForm.pMainForm.mapdoc.MSPCmdFile.close();
                    MessageBox.Show("命令已发送！");
                }
            }
            else
            {
                MessageBox.Show("请选择一个正确的目的地址！");

            }
        }
        private byte[] Pack()
        {
            //打包协议
            try
            {
                //路径安排
                if (PathAsignCheck.Checked)
                {
                    if (Path.Text == "")
                    {
                        PathAsignCheck.Checked = false;
                        throw new Exception("路径安排不能为空");

                    }
                    string[] nodename = Path.Text.Split(' ', ',');
                    int[] dat = new int[1];

                    int nodenum = nodename.Length;
                    SourceDataClass.InitForPack(nodenum * 6 + 20);
                    dat[0] = 8;
                    SourceDataClass.OutPutIntBit(dat, 8);
                    dat[0] = nodenum * 6 + 20;
                    SourceDataClass.OutPutIntBit(dat, 12);
                    for (int i = 0; i < nodenum; i++)
                    {
                        dat[0] = int.Parse(nodename[i]);
                        SourceDataClass.OutPutIntBit(dat, 6);//节点
                    }
                    BitArray ba = SourceDataClass.packdata;
                    balst.Add(ba);
                }
                //
                byte[] bstr = System.Text.Encoding.Default.GetBytes(SourceText.Text);
                BitArray bta = new BitArray(bstr);
                SourceDataClass.InitForPack(bta.Length + 20);
                int[] b = new int[1];
                b[0] = 101;
                SourceDataClass.OutPutIntBit(b, 8);          
                b[0] = bta.Length  + 20;
                SourceDataClass.OutPutIntBit(b, 12);
                SourceDataClass.OutPutArrayBit(bta);
                balst.Add(SourceDataClass.packdata);

                int blocklen = 34+6;//
                if (balst.Count > 0)
                {
                    for (int i = 0; i < balst.Count; i++)
                    {
                        blocklen += balst[i].Length;
                    }
                }
                SourceDataClass.InitForPack(blocklen);

                b[0] = 1;
                SourceDataClass.OutPutIntBit(b, 6);
                BitArray blockba = new BitArray(blocklen);
                b[0] = ComListForm.PackageIndex;
                ComListForm.PackageIndex++;
                SourceDataClass.OutPutIntBit(b, 10);
                b[0] = blocklen - 6;//仅包括块头定义,块长度不包括块数长度
                SourceDataClass.OutPutIntBit(b, 12);
                b[0] = int.Parse(SourceNodeBox.Text.TrimStart('节', '点'));
                SourceDataClass.OutPutIntBit(b, 6);
                b[0] = int.Parse(DestNodeName.Text.TrimStart('节', '点'));
                SourceDataClass.OutPutIntBit(b, 6);
                for (int i = 0; i < balst.Count; i++)
                {
                    SourceDataClass.OutPutArrayBit(balst[i]);
                }
                balst.Clear();
                byte[] cmd = new byte[(int)Math.Ceiling(((double)blocklen) / 8)];
                SourceDataClass.packdata.CopyTo(cmd, 0);
                //BinaryWriter bw = new BinaryWriter(new FileStream("ping.dat", FileMode.Create));
                //bw.Write(cmd);
                //bw.Close();
                return cmd;
            }
            catch (Exception MyEx)
            {
                balst.Clear();
                MessageBox.Show(MyEx.Message);
                return new byte[0];
            }


        }
        public void ShowData(int startid,string Backstr)
        {
            if (BackText.InvokeRequired)
            {
                ShowDataHandle sdh = new ShowDataHandle(ShowData);
                this.Invoke(sdh, new object[] { startid ,Backstr });
            }
            else
            {
                try
                {
                    BackText.Text = Backstr;
                    if (BackText.Text.Equals(SourceText.Text))
                        Commparelabel.Text = "收到节点" + startid.ToString() + "回环数据与源数据一致！";
                    else
                        Commparelabel.Text = "收到节点" + startid.ToString() + "回环数据与源数据不一致！";
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
        private void PathAsignCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (PathAsignCheck.Checked)
                Path.Enabled = true;
            else
                Path.Enabled = false;
        }

        private void PingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            SourceDataClass.isShowCircle = false;
            if (e.CloseReason != CloseReason.ApplicationExitCall)//非系统关闭，只是关闭本窗口
                e.Cancel = true;
        }

        private void SourceNodeBox_DropDown(object sender, EventArgs e)
        {
            //读取节点
            XmlDocument xmlfile = new XmlDocument();
            xmlfile.Load(xmldoc);
            XmlNode xn = xmlfile.DocumentElement;
            xn = xn.SelectSingleNode("descendant::节点配置");
            SourceNodeBox.Items.Clear();
            foreach (XmlNode subnode in xn.ChildNodes)
            {

                SourceNodeBox.Items.Add(subnode.Name);
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            SourceText.Clear();
            BackText.Clear();
            Commparelabel.Text = "---";
        }

        private void SourceText_TextChanged(object sender, EventArgs e)
        {
            byte[] b =Encoding.Default.GetBytes(SourceText.Text);
            int len = b.Length;
            if (len != 0)
            {
                while (len > 480)
                {
                    SourceText.Text = SourceText.Text.Remove(SourceText.Text.Length - 1);
                    b =Encoding.Default.GetBytes(SourceText.Text);
                    len = b.Length;
                }
                groupPanel1.Text = "源数据内容" + len.ToString();
            }
            else
                groupPanel1.Text = "源数据内容";
        }
    }
}
