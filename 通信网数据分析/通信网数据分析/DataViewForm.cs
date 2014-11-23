using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 通信网数据分析
{
    public partial class DataViewForm : Form
    {
        public string str;
        public List<string[]> ll;
        private delegate void ShowDataHandle();
        public DataViewForm()
        {
            InitializeComponent();
        }
        public void InputGrid()
        {
            if (DatatreeGrid.InvokeRequired)
            {
                ShowDataHandle sdh = new ShowDataHandle(InputGrid);
                this.Invoke(sdh, new object[] { });
            }
            else
            {
                DatatreeGrid.Nodes.Clear();
                int i = 0;//最上级节点
                AdvancedDataGridView.TreeGridNode node = DatatreeGrid.Nodes.Add("信源数据");
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

        public void ShowData()
        {

            SourceDataClass.GetData((CRCHelper.ConvertHexToChar(str)));
            ll = SourceDataClass.Parse(); 
            
            InputGrid();



            this.Show();
            //this.TopMost = true;
        }
    }
}
