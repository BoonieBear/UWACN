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
        private void LoadAndConnect()
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
                ConnectBtn.Enabled = true;
                DisconnectBtn.Enabled = false;
                MessageBox.Show("无法解析配置中IP地址！请修改吊放IP");
            }
        }
        private void AutoConnect_Load(object sender, EventArgs e)
        {
            LoadAndConnect();
            NetworkTimer.Enabled = true;
        }
        public void AddToBox(string s)
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
            ConnectBtn.Enabled = true;
            DisconnectBtn.Enabled = false;

        }
        private void SaveNetworks()
        {
            string[] str = { "吊放IP" };
            
            string[] cportstr = { "命令端口" };
 
            string[] dportstr = { "数据端口" };
            XmlHelper.SetConfigValue(xmldoc, str, IpaddBox.Text);
            XmlHelper.SetConfigValue(xmldoc, cportstr, CommportBox.Text);
            XmlHelper.SetConfigValue(xmldoc, dportstr, DataportBox.Text);
        }
        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            IPAddress addr = new IPAddress(0x1234);
            if (IPAddress.TryParse(IpaddBox.Text, out addr)==false)
            {
                MessageBox.Show("无法解析IP地址！请修改吊放IP");
                return;
            }
            SaveNetworks();
            LoadAndConnect();
        }

        private void NetworkTimer_Tick(object sender, EventArgs e)
        {
            if (MainForm.pMainForm.CommandLineWin.Tclient != null && MainForm.pMainForm.CommandLineWin.Tclient.Client != null&&
                MainForm.pMainForm.CommandLineWin.Tclient.Connected)
            {
                ConnectBtn.Enabled = false;
                DisconnectBtn.Enabled = true;
                Hide();
            }
            else
            {
                ConnectBtn.Enabled = true;
                DisconnectBtn.Enabled = false;
            }
        }
    }
}
