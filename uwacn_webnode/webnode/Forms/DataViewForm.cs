using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Rendering;
using webnode.Helper;

namespace webnode.Forms
{
    public partial class DataViewForm : Office2007Form
    {
        public string str;
        List<string[]> ll;
        private delegate void ShowDataHandle();
        public DataViewForm()
        {
            InitializeComponent();

            
        }
        
        private void DataViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            if (e.CloseReason != CloseReason.ApplicationExitCall)//非系统关闭，只是关闭本窗口
                e.Cancel = true;
        }
        private void InputGrid()
        {
            if (DatatreeGrid.InvokeRequired)
            {
                ShowDataHandle sdh = new ShowDataHandle(InputGrid);
                this.Invoke(sdh, new object[] { });
            }
            else
            {
                DatatreeGrid.Nodes.Clear();
                int i = int.Parse(ll[0][0]);//最上级节点
                AdvancedDataGridView.TreeGridNode node = DatatreeGrid.Nodes.Add("信源数据包", "");
                foreach (string[] str in ll)
                {
                    int j = int.Parse(str[0]);
                    if (j > i)
                    {
                        node = node.Nodes.Add(str[1], str[2], str[3]);
                    }
                    else
                    {
                        if (i == j)
                        {
                            node = node.Parent.Nodes.Add(str[1], str[2], str[3]);
                        }
                        if (i == j + 1)
                        {
                            node = node.Parent.Parent.Nodes.Add(str[1], str[2], str[3]);
                        }
                        if (i == j + 2)
                        {
                            node = node.Parent.Parent.Parent.Nodes.Add(str[1], str[2], str[3]);
                        }
                        if (i == j + 3)
                        {
                            node = node.Parent.Parent.Parent.Parent.Nodes.Add(str[1], str[2], str[3]);
                        }
                        if (i == j + 4)
                        {
                            node = node.Parent.Parent.Parent.Parent.Parent.Nodes.Add(str[1], str[2], str[3]);
                        }
                    }
                    i = j;

                }
            }
        }

        public void DataViewForm_ShowData()
        {
            try
            {
                SourceDataClass.isNodeTick = false;

                MainForm.ParseLock.WaitOne();
                if (str.StartsWith("01EE"))
                {
                    str = str.Substring(8);
                }
                SourceDataClass.GetData((CRCHelper.ConvertHexToChar(str)));
                ll = SourceDataClass.Parse();

                
                MainForm.ParseLock.ReleaseMutex();
                SourceDataClass.isNodeTick = true;
                InputGrid();

                this.Show();
            }
            catch(Exception e)
            {
                MainForm.ParseLock.ReleaseMutex();
                SourceDataClass.isNodeTick = true;
                MessageBox.Show("无法解析文件："+e.StackTrace);
            }
            //this.TopMost = true;
        }

    }
}
