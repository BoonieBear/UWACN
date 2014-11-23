using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
namespace 通信网数据分析
{
    public partial class NodedataForm : Form
    {
        private List<string> TimeLst = new List<string>();
        Hashtable TimeData = new Hashtable();
        private List<string> nodeid = new List<string>();
        private List<string[]> DetailLstOfData = new List<string[]>();//数据解析后形成的列表
        //string[16] 内容：节点ID,时间,路径记录,节点类型,节点状态数据(16进制),3.3V电压,48V电压,3.3V剩余电量,48V剩余电量,节点温度,漏水报警,发射自动调节开关,发射幅度设置,接收自动调节开关,接收增益设置
        private List<string[]> DetailLstOfStatus = new List<string[]>();//数据解析后形成的状态列表
        private List<string[]> UpdateData = new List<string[]>();//筛选之后的数据列表
        private List<string[]> UpdateStatus = new List<string[]>();//筛选之后的状态列表
        public NodedataForm()
        {
            InitializeComponent();
           
        }
        private void CopyHashTable(Hashtable src, Hashtable dst)
        {
            dst.Clear();
            foreach (object obj in src.Keys)
            {
                string nodename = (string)obj;
                byte[] lst = (byte[])src[nodename];
                byte[] newlst = new byte[lst.Length];
                lst.CopyTo(newlst,0);
                dst.Add(nodename, newlst);

            }
        }
        public NodedataForm(List<string> timelst, Hashtable timeData, List<string[]> statuslst, List<string[]> datalst)
        {
            InitializeComponent();
            TimeLst.AddRange(timelst);
            CopyHashTable(timeData, TimeData);
            DetailLstOfStatus.AddRange(statuslst);
            DetailLstOfData.AddRange(datalst);
            AddDataToView();
            NodeBox.Items.Clear();
            NodeBox.Items.AddRange(nodeid.ToArray());
        }

        private void NewWindow_Click(object sender, EventArgs e)
        {
            NodedataForm ndf = new NodedataForm(TimeLst, TimeData, DetailLstOfStatus, DetailLstOfData);
            ndf.MdiParent = this.ParentForm;
            ndf.Show();
            ndf.WindowState = FormWindowState.Maximized;
        }
        private void AddDataToView()
        {
            FileList.Rows.Clear();
            DeviceList.Rows.Clear();
            StatusList.Rows.Clear();
            if (TimeLst.Count > 0)
            {
                
                foreach (string s in TimeLst)
                    FileList.Rows.Add(s);
            }
            label4.Text = "文件列表" + TimeLst.Count.ToString();
            if (DetailLstOfData.Count > 0)
            {

                foreach (string[] s in DetailLstOfData)
                {
                    DeviceList.Rows.Add(s);
                    UpdateData.Add(s);
                   if(!nodeid.Contains(s[0]))
                   {
                       nodeid.Add(s[0]);
                   }
                }
            }
            label5.Text = "设备数据表" + DetailLstOfData.Count.ToString();
            if (DetailLstOfStatus.Count > 0)
            {
                foreach (string[] s in DetailLstOfStatus)
                {
                    StatusList.Rows.Add(s);
                    UpdateStatus.Add(s);
                    if(!nodeid.Contains(s[0]))
                   {
                       nodeid.Add(s[0]);
                   }
                }
            }
            label6.Text = "节点状态表" + DetailLstOfStatus.Count.ToString();
            nodeid.Sort();
        }

        private void NodedataForm_Load(object sender, EventArgs e)
        {
            

        }

        private void SaveDeviceLst_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "txt (*.txt)|*.txt";
                    sfd.FileName = "设备数据信息";
                    
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {

                        using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                        {
                            string s ="";
                            //写标题
                            foreach (DataGridViewColumn c in DeviceList.Columns)
                            {
                                s +=  c.HeaderText.ToString() + ",";
                               
                            }
                            s = s.TrimEnd(',');
                            sw.WriteLine(s);
                            s = "";
                            for (int i=0;i<UpdateData.Count;i++)
                            {

                                foreach (string c in UpdateData[i])
                                {
                                    
                                    s += c + ",";
                                }
                                s = s.TrimEnd(',');
                                sw.WriteLine(s);
                                s = "";
                                
                            }
                        }

                        MessageBox.Show("设备数据信息已保存: " + sfd.FileName, "通信网数据分析", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败: " + ex.Message, "通信网数据分析", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveStatusLst_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "txt (*.txt)|*.txt";
                    sfd.FileName = "节点状态信息";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {

                        using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                        {
                            string s = "";
                            //写标题
                            foreach (DataGridViewColumn c in StatusList.Columns)
                            {
                                s += c.HeaderText + ",";

                            }
                            s = s.TrimEnd(',');
                            sw.WriteLine(s);
                            s = "";
                            for (int i = 0; i < UpdateStatus.Count; i++)
                            {

                                foreach (string c in UpdateStatus[i])
                                {

                                    s += c + ",";
                                }
                                s = s.TrimEnd(',');
                                sw.WriteLine(s);
                                s = "";

                            }
                        }

                        MessageBox.Show("节点状态信息已保存: " + sfd.FileName, "通信网数据分析", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败: " + ex.Message, "通信网数据分析", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //根据范围选择数据视图
        private void UpdataView(string id, DateTime starttime, DateTime endtime)
        {
   
            DeviceList.Rows.Clear();
            StatusList.Rows.Clear();
            UpdateData.Clear();//清空
            UpdateStatus.Clear();//清空
            if (DetailLstOfStatus.Count > 0)
            {

                foreach (string[] s in DetailLstOfStatus)
                {
                    bool sameid = true;
                    bool inTimeScale = true;
                    if (id != "")
                    {
                        sameid = s[0].Equals(id);
                    }
                    if (starttime < endtime)
                    {
                        DateTime d = Convert.ToDateTime(s[1]);
                        if ((d <= endtime) && (d > starttime))
                            inTimeScale = true;
                        else
                            inTimeScale = false;
                    }
                    else
                    {
                        inTimeScale = true;
                    }
                    if (sameid && inTimeScale)
                    {
                        StatusList.Rows.Add(s);
                        UpdateStatus.Add(s);
                    }
                    
                }
            }
            label6.Text = "节点状态表" + (StatusList.Rows.Count - 1).ToString();
            if (DetailLstOfData.Count > 0)
            {
                foreach (string[] s in DetailLstOfData)
                {
                    bool sameid = true;
                    bool inTimeScale = true;
                    if (id != "")
                    {
                        sameid = s[0].Equals(id);
                    }
                    if (starttime < endtime)
                    {
                        DateTime d = Convert.ToDateTime(s[2]);
                        if ((d <= endtime) && (d > starttime))
                            inTimeScale = true;
                        else
                            inTimeScale = false;
                    }
                    else
                    {
                        inTimeScale = true;
                    }
                    if (sameid && inTimeScale)
                    {
                        DeviceList.Rows.Add(s);
                        UpdateData.Add(s);
                    }
                   
                }
            }
            label5.Text = "设备数据表" + (DeviceList.Rows.Count - 1).ToString();

        }
        private void NodeBox_TextChanged(object sender, EventArgs e)
        {
            string id = NodeBox.Text;
            DateTime starttime = dateTimePicker1.Value;
            DateTime endtime = dateTimePicker2.Value;
            UpdataView(id,starttime,endtime);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string id = NodeBox.Text;
            DateTime starttime = dateTimePicker1.Value;
            DateTime endtime = dateTimePicker2.Value;
            UpdataView(id, starttime, endtime);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            string id = NodeBox.Text;
            DateTime starttime = dateTimePicker1.Value;
            DateTime endtime = dateTimePicker2.Value;
            UpdataView(id, starttime, endtime);
        }

        private void FileList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (FileList.Rows[index].Cells[e.ColumnIndex].Value != null)
            {
                string s = FileList.Rows[index].Cells[e.ColumnIndex].Value.ToString();
                Byte[] data = (Byte[])TimeData[s];
                DataViewForm dvf = new DataViewForm();
                dvf.str = CRCHelper.ConvertCharToHex(data, data.Length);
                dvf.MdiParent = this.ParentForm;
                dvf.ShowData();
                dvf.WindowState = FormWindowState.Maximized;
            }
        }





    }
}
