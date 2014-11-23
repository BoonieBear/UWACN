using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Rendering;
using webnode.Helper;
namespace webnode.Forms
{
    public partial class BasicConfForm : Office2007Form
    {
        XmltoTree xmltransfer;
        DataSet ds;

        string xmldoc = "config.xml";
        public BasicConfForm()
        {
            InitializeComponent();
            xmltransfer = new XmltoTree();
            ds = new DataSet();
            
        }

        private void BasicConfForm_Load(object sender, EventArgs e)
        {
            string MyExecPath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            xmldoc = MyExecPath + "\\" + xmldoc;
            xmltransfer.populateTreeview(xmldoc, XmlTree);
            
            ds.ReadXml(xmldoc);
        }
        /*
        private void AllCancelCheck_Click(object sender, EventArgs e)
        {
            UnCheckNode(XmlTree.Nodes[0]);
                
        }
        private void UnCheckNode(TreeNode tn)
        {
            foreach (TreeNode node in tn.Nodes)
            {
                node.Checked = false;
                UnCheckNode(node);
            }
        }
        */
        private void XmlTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = XmlTree.SelectedNode;
            ds.Reset();
            
            ConfigDataView.Columns.Clear();
            if ((tn == null) || (tn.FirstNode == null))
                return;
            DataTable dt = new DataTable();
            dt.Columns.Add(tn.Text);
            object[] newRow = new object[tn.GetNodeCount(false)];
            for (int i = 0; i < tn.GetNodeCount(false); i++)
            {
                newRow[i] = tn.Nodes[i].Text;
                dt.Rows.Add(newRow[i]);
            }
            ConfigDataView.DataSource = dt;
            AddNode.Enabled = true;
            DelNode.Enabled = true;

        }

        private void AddNode_Click(object sender, EventArgs e)
        {
            TreeNode tn = XmlTree.SelectedNode;
            tn.Nodes.Add(new TreeNode("新建项"));
        }

        private void DelNode_Click(object sender, EventArgs e)
        {
            TreeNode tn = XmlTree.SelectedNode;
            tn.Parent.Nodes.Remove(tn);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            
            string msg = "参数保存成功！";
            try
            {
                xmltransfer.exportToXml(XmlTree, xmldoc);
            }
            catch (Exception MyEx)
            {
                msg = MyEx.StackTrace.ToString();
               
            }
            finally
            {
                 MessageBox.Show(msg);
                 MapForm.isMspContinue = false;
                MapForm.IsLoaderRunning = false;
                try
                {
                    if (MapForm.MspSerialPort.IsOpen)
                        MapForm.MspSerialPort.Close();
                    string[] str = { "节点串口参数", "端口号" };
                    MapForm.MspSerialPort.PortName = XmlHelper.GetConfigValue(xmldoc, str).ToUpper();

                    string[] newstr = { "节点串口参数", "波特率" };
                    MapForm.MspSerialPort.BaudRate = int.Parse(XmlHelper.GetConfigValue(xmldoc, newstr));
                    if (MapForm.MspSerialPort.PortName == null)
                        MessageBox.Show("读取MSP430端口错误！");
                    MainForm.pMainForm.mapdoc.MSP_StartWork();
                }
                catch
                { }
            }
        }

        private void ReloadConfig_Click(object sender, EventArgs e)
        {
            xmltransfer.populateTreeview(xmldoc, XmlTree);
        }

        private void ConfigDataView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            TreeNode tn = XmlTree.SelectedNode;
            tn.Nodes[e.RowIndex].Text = ConfigDataView.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

    }
}
