using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Diagnostics;
using webnode.Helper;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;
        
namespace webnode.Forms
{
    
    public partial class ComListForm : Office2007Form
    {
        public delegate void RefreshListHandler();//刷新命令栏委托，用于外界对列表刷新的需要
        private Hashtable CmdTable = new Hashtable();//命令哈希表
        public List<BitArray> CmdForSend = new List<BitArray>();//命令数据
        public List<string> CmdName = new List<string>();//命令涵义，与命令列表对应
        public List<string> CmdNode = new List<string>();//命令地址
        public List<string> NodeFindOut = new List<string>();//数据分组后的节点名集合
        string xmldoc;
        public static int PackageIndex;//块标识
        public ComListForm()
        {
            InitializeComponent();
            PackageIndex = 0;//块标识
            string MyExecPath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            xmldoc = MyExecPath + "\\" + "config.xml"; 
        }

        public void AddCmd(string nodename,string cmdname,BitArray ba)
        {
            CmdNode.Add(nodename);
            CmdName.Add(cmdname);
            CmdForSend.Add(ba);
            RefreshList();
            
        }

        public void DelCmdAt(int index)
        {
            CmdNode.RemoveAt(index);
            CmdName.RemoveAt(index);
            CmdForSend.RemoveAt(index);
            RefreshList();
        }
        public void Clear()
        {
            CmdNode.Clear();
            CmdName.Clear();
            CmdForSend.Clear();
            DeleteBtn.Enabled = false;
            RefreshList();
        }

        private void ComListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.ShowInTaskbar = false;
            if (e.CloseReason != CloseReason.ApplicationExitCall)//非系统关闭，只是关闭本窗口
                e.Cancel = true;
        }

        private void AddTrace_Click(object sender, EventArgs e)
        {
            if (CmdList.Items.Count > 0)
            {
                if (SourceNodeBox.Text != "")
                {
                    this.Hide();
                    pack();
                    TrackForm Tf = new TrackForm();
                    for (int i = 0; i < NodeFindOut.Count; i++)
                    {
                        string[] str = new string[] { NodeFindOut[i] };
                        Tf.Trace.Rows.Add(str);
                    }
                    //try
                    {
                        if (Tf.ShowDialog() == DialogResult.OK)
                        {
                            for (int j = 0; j < Tf.Trace.Rows.Count;j++ )
                            {
                                string str = (string)Tf.Trace.Rows[j].Cells[1].Value;
                                if (str == null)
                                    continue;
                                string[] nodename = str.Split(' ', ',');
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

                                //加入列表
                                MainForm.pMainForm.comlistwin.AddCmd((string)Tf.Trace.Rows[j].Cells[0].Value, "路径安排", SourceDataClass.packdata);
                            }
                        }
                    }
                    //catch (Exception MyEx)
                    {
                        //MessageBox.Show(MyEx.Message);
                    }
                    Tf.Dispose();
                    this.Show();
                }
            }
        }

        private void HideBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

       
        //打包所有数据区到未加0xAA为止，将有相同目的地址的数据打包在一个块中
        private byte[] pack()
        {
            NodeFindOut.Clear();
            CmdTable.Clear();//每次打包前清空。
            int total = 0;//数据总长度，不包括包头
            for (int i = 0; i < CmdNode.Count; i++)
            {
                if (CmdTable.ContainsKey(CmdNode[i]))
                {
                    BitArray ba = (BitArray)CmdTable[CmdNode[i]];//取出已有的数据
                    BitArray newba = new BitArray(ba.Length + CmdForSend[i].Length);
                    for (int a = 0; a < ba.Length; a++)
                    {
                        newba[a] = ba[a];
                    }
                    for (int j = 0; j < CmdForSend[i].Length; j++)
                    {
                        newba[ba.Length + j] = CmdForSend[i][j];
                    }
                    CmdTable[CmdNode[i]] = newba;//将新的数据放进哈希表
  

                }
                else
                {
                    CmdTable.Add(CmdNode[i], CmdForSend[i]);
                    NodeFindOut.Add(CmdNode[i]);
                    
                }
                total += CmdForSend[i].Length;
                
            }
            //打包协议
            int blocknum = CmdTable.Keys.Count;//块数
            total += blocknum*34 + 6;
            
            SourceDataClass.InitForPack(total);
            int[] b = new int[1];
            b[0] = blocknum;
            SourceDataClass.OutPutIntBit(b,6);
            for (int i = 0; i < blocknum; i++)
            {
                BitArray ba = (BitArray)CmdTable[NodeFindOut[i]];//数据区集合
                int blocklen = ba.Length + 34;
                BitArray blockba = new BitArray(blocklen);
                b[0] = PackageIndex;
                PackageIndex++;
                SourceDataClass.OutPutIntBit(b, 10);
                b[0] = blocklen;
                SourceDataClass.OutPutIntBit(b,12);
                b[0] = int.Parse(SourceNodeBox.Text.TrimStart('节','点'));
                SourceDataClass.OutPutIntBit(b, 6);
                b[0] = int.Parse(NodeFindOut[i].TrimStart('节', '点'));
                SourceDataClass.OutPutIntBit(b, 6);
                SourceDataClass.OutPutArrayBit(ba);
            }
            byte[] cmd = new byte[(int)Math.Ceiling(((double)total)/8)];
            SourceDataClass.packdata.CopyTo(cmd, 0);
            return cmd;

        }

        private void ComListForm_Load(object sender, EventArgs e)
        {
            
            RefreshList();
            
        }
        //更新显示命令列表
        public void RefreshList()
        {
            if (CmdList.InvokeRequired)
            {
                RefreshListHandler r = new RefreshListHandler(RefreshList);
                this.Invoke(r, new object[] { });
            }
            else
            {
                CmdList.Items.Clear();
                for (int i= 0 ; i < CmdNode.Count; i++)
                {
                    string rowstr = CmdNode[i] +" "+ CmdName[i];
                    CmdList.Items.Add(rowstr);    
                }
            }
            MainForm.pMainForm.RefreshListStat();
        }


        private void CmdList_MouseClick(object sender, MouseEventArgs e)
        {
            if ((CmdList.SelectedIndex > -1) && (CmdList.Focused))
            {
                DeleteBtn.Enabled = true;
            }
            else
            {
                DeleteBtn.Enabled = false;
            }   
        }

        private void ShowData_Click(object sender, EventArgs e)
        {
            if (CmdList.Items.Count > 0)
            {
                if (SourceNodeBox.Text != "")
                {
                    MainForm.pMainForm.mapdoc.Dvf.Text = "显示命令内容";
                    byte[] cmd = pack();
                    if (cmd.Length > 1024)
                    {
                        MessageBox.Show("命令长度多长，请删除部分命令！");
                        return;
                    }
                    MainForm.pMainForm.mapdoc.Dvf.str = CRCHelper.ConvertCharToHex(cmd, cmd.Length);
                    MainForm.pMainForm.mapdoc.Dvf.DataViewForm_ShowData();
                }
                else
                {
                    MessageBox.Show("请选择一个正确的源地址！");

                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            
            int index = CmdList.SelectedIndex;
            if ((index >= 0) && (index <= CmdList.Items.Count - 1))
            {
                DelCmdAt(index);
                if (CmdList.Items.Count > 0)
                    CmdList.SetSelected(CmdList.Items.Count-1, true);
                else
                    DeleteBtn.Enabled = false;

            }
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
        private void ViaNet_Click(object sender, EventArgs e)
        {
            if (CmdList.Items.Count > 0)
            {
                if (SourceNodeBox.Text != "")
                {
                    byte[] cmd = pack();
                    if (cmd.Length > 985)
                    {
                        MessageBox.Show("命令过长，请删除部分命令！");
                        return;
                    }
                    byte[] netcmd = SourceDataClass.NetPackage(cmd);
                    MainForm.pMainForm.CommandLineWin.SendCommand("gd -n");
                    if (MainForm.pMainForm.CommandLineWin.SendData(netcmd))//正确发送
                    {
                        //Clear();
                        RefreshList();
                        MessageBox.Show("命令已发送！");
                        
                    }
                }
                else
                {
                    MessageBox.Show("请选择一个正确的源地址！");

                }
            }
        }

        private void ViaSerial_Click(object sender, EventArgs e)
        {
            if (CmdList.Items.Count > 0)
            {
                if (SourceNodeBox.Text != "")
                {
                    byte[] cmd = pack();
                    if (cmd.Length > 1024)
                    {
                        MessageBox.Show("命令过长，请删除部分命令！");
                        return;
                    }
                    byte[] Serialcmd = SourceDataClass.CommPackage(171, cmd);
                    if (MainForm.pMainForm.mapdoc.WriteMSPCommand(Serialcmd))
                    {
                        MainForm.pMainForm.mapdoc.MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                        MainForm.pMainForm.mapdoc.MSPCmdFile.BinaryWrite(Serialcmd);
                        MainForm.pMainForm.mapdoc.MSPCmdFile.close();
                        //Clear();
                        RefreshList();
                        MessageBox.Show("命令已发送！");
                    }
                }
                else
                {
                    MessageBox.Show("请选择一个正确的源地址！");

                }
            }
        }

        //网络数据解析
        private void OpenData_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    BinaryReader br = new BinaryReader(openFileDialog.OpenFile());

                    byte[] b = br.ReadBytes((int)openFileDialog.OpenFile().Length);
                    
                    MainForm.pMainForm.mapdoc.Dvf.Text = "显示命令内容";
                    MainForm.pMainForm.mapdoc.Dvf.str = CRCHelper.ConvertCharToHex(b, b.Length);
                    SourceDataClass.isCommDepack = true;
                    MainForm.pMainForm.mapdoc.Dvf.DataViewForm_ShowData();
                    SourceDataClass.isCommDepack = false;
                    br.Close();
                }
                catch (Exception MyEx)
                {
                    MessageBox.Show(MyEx.Message);
                }
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            Clear();
            RefreshList();
        }
        //解EB90协议，读出源节点，串口收到时间，转发类型id及解开协议后的数据体。
        private bool DepackData(byte[] indata, out int NodeId, out string time, out byte[] data)
        {
            try
            {
                string strcmd = Encoding.ASCII.GetString(indata);//字符形式的命令，用于读取EB90协议的数据，内部数据用cmd存取。
                string[] str = strcmd.Split(',');
                NodeId = int.Parse(str[2]);
                int id;
                if (!SourceDataClass.DepackCommData(indata, out time, out id, out data))
                {

                    throw new Exception("数据校验错误");

                }
                return true;

            }
            catch (Exception e)
            {
                NodeId = 0;
                time = null;
                data = null;
                Debug.WriteLine(e.ToString());
                return false;
            }
        }
        //串口数据解析
        private void ParseSerialData_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    BinaryReader br = new BinaryReader(openFileDialog.OpenFile());

                    byte[] b = br.ReadBytes((int)openFileDialog.OpenFile().Length);
                    int NodeId;
                    string time;
                    byte[] data;
                    if (DepackData(b, out NodeId, out time, out data))
                    {
                        
                            MainForm.pMainForm.mapdoc.Dvf.Text = "显示命令内容";
                            MainForm.pMainForm.mapdoc.Dvf.str = CRCHelper.ConvertCharToHex(b, b.Length);
                            
                            MainForm.pMainForm.mapdoc.Dvf.DataViewForm_ShowData();

                    }
                    else
                    {
                        throw new Exception("数据校验错误！");
                    }
                    
                    br.Close();
                }
                catch (Exception MyEx)
                {
                    MessageBox.Show(MyEx.Message);
                }
            }
            
        }


    }
}
