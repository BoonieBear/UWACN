using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using webnode.Helper;

namespace webnode.Forms
{
    public partial class AutoConnect : Form
    {
        string MyExecPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        string xmldoc;
        delegate void AddBoxCallback(string s);
        public AutoConnect()
        {
            InitializeComponent();
            xmldoc = MyExecPath + "\\" + "config.xml";
        }

        private void AutoConnect_Load(object sender, EventArgs e)
        {

            string[] str = { "吊放IP" };
            IPAddress addr = new IPAddress(0x1234);
            string[] cportstr = { "命令端口" };
            int cport = Int16.Parse(XmlHelper.GetConfigValue(xmldoc, cportstr));
            string[] dportstr = { "数据端口" };
            int dport = Int16.Parse(XmlHelper.GetConfigValue(xmldoc, dportstr));

            CommportBox.Text = cport.ToString();
            DataportBox.Text = dport.ToString();

            if (IPAddress.TryParse(XmlHelper.GetConfigValue(xmldoc, str), out addr))
            {
                IpaddBox.Text = addr.ToString();
                MainForm.pMainForm.CommandLineWin.ConnectNode(addr);
                ConnectBtn.Enabled = false;
                DisconnectBtn.Enabled = true;
            }
            else
            {
                MessageBox.Show("无法解析配置中IP地址！请修改吊放IP");
            }
        }
        private void AddToBox(string s)
        {
            if(MsgBox.InvokeRequired)
            {
                AddBoxCallback d = new AddBoxCallback(AddToBox);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                MsgBox.AppendText(s);
            }
        }
        private void DisconnectBtn_Click(object sender, EventArgs e)
        {
            MainForm.pMainForm.CommandLineWin.NodeLinker.CancelAsync();
            if (CommLineForm.bConnect)
                MainForm.pMainForm.CommandLineWin.ExecCommand("disconnect");
        }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
