using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Rendering;
using webnode.Forms;
using System.Threading;
using System.Globalization;
using System.Net.NetworkInformation;
using webnode.Helper;
using System.IO;
using System.Xml;
using System.Net;
using System.Net.Sockets;
namespace webnode
{
    public partial class MainForm : Office2007RibbonForm
    {
        #region 类成员
        System.Windows.Forms.Timer serialtime = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer Commtime = new System.Windows.Forms.Timer();
        //网络消息委托
        private NetworkInterface currentInterface = null;
        private NetworkInterface[] networkInterfaces = null;
        double bytesFormerReceivedInKB;
        double bytesFormerSentInKB;

        public static MainForm pMainForm;//主窗口指针
        string MyExecPath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);

        //命令列表委托
        private delegate void CmdListNotice();
        private delegate void PositionShow(string str);
        //状态栏委托
        private delegate void ChangeLaber(object sender, EventsClass.StatusEventArgs e);
        //数据解析锁
        public static Mutex ParseLock = new Mutex();
        //常驻窗体
        public CommLineForm CommandLineWin = new CommLineForm();
        public ADShowForm ADform = new ADShowForm();
        public MapForm mapdoc = new MapForm();
        public ComListForm comlistwin = new ComListForm();
        public DirectoryInfo ADPathInfo;
        public DirectoryInfo ImgInfo;
        public DirectoryInfo RecordInfo;
        public DirectoryInfo SerialRecvPathInfo;
        public DirectoryInfo SerialCmdPathInfo;
        public DirectoryInfo NetCmdDataPathInfo;
        public DirectoryInfo NetRecvDataPathInfo;
        //配置文件
        public string xmldoc = "config.xml";
        //配置
        #endregion 类成员



        public MainForm()
        {
            
            InitializeComponent();
            //地图常驻系统，所以只有开始时打开，不检查是否已经打开tab
            pMainForm = this;
            UpLoadADBtn.Enabled = false;
            DLoadExecBtn.Enabled = false;

        }
       
        private void MainForm_Load(object sender, EventArgs e)
        {
            CreateLogDirectory();
            Commtime.Interval = 1000;
            Commtime.Tick += new EventHandler(Commtime_Tick);
            Commtime.Start();
            mapdoc.MdiParent = this;
            mapdoc.WindowState = FormWindowState.Maximized;
            mapdoc.Show();
            mapdoc.Update();
            ADform.Show();
            ADform.WindowState = FormWindowState.Minimized;
            ADform.ShowInTaskbar = false;
            BuoyChoice.SelectedIndex = 0;
            WebnodeComm.SelectedIndex = 0;

            CommandLineWin.WindowState = FormWindowState.Minimized;
            CommandLineWin.Show();
            comlistwin.WindowState = FormWindowState.Minimized;
            comlistwin.Show();
            xmldoc = MyExecPath + "\\" + xmldoc;
            string[] str = { "GPS配置", "串口参数", "端口号" };
            mapdoc.comm = XmlHelper.GetConfigValue(xmldoc, str);
            GpsPort.ControlText = mapdoc.comm;
            string[] newstr =  { "GPS配置", "串口参数", "波特率" };
            string br = XmlHelper.GetConfigValue(xmldoc, newstr);
            if (mapdoc.comm == null || br == null)
                MessageBox.Show("读取GPS端口错误！");
            mapdoc.baudrate = int.Parse(br);
            GpsBaudrate.ControlText = br;
            networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            if (networkInterfaces.Length == 0)
            {

                MessageBox.Show("没找到系统网卡！");
            }
            else
            {
                currentInterface = networkInterfaces[0];
                NicInfotimer.Enabled = true;
            }
//            CommandLineWin.Hide();
            
        }

        #region 网络速度测试
        private void UpdateNicStats()
        {
            // Get the IPv4	statistics for the currently selected interface.
            IPv4InterfaceStatistics ipStats =
                currentInterface.GetIPv4Statistics();

            NumberFormatInfo numberFormat = NumberFormatInfo.CurrentInfo;

            double receiveByte = ipStats.BytesReceived / 1024;
            double SentBype = ipStats.BytesSent / 1024;
            double bytesReceivedInKB = (receiveByte - bytesFormerReceivedInKB) /( (double)NicInfotimer.Interval/1000);
            double bytesSentInKB = (SentBype - bytesFormerSentInKB) / ((double)NicInfotimer.Interval / 1000);
            this.NetSpeedLabel.Text ="接收:" + bytesReceivedInKB.ToString("N0", numberFormat) 
                + " KB/s" + "发送:" + bytesSentInKB.ToString("N0", numberFormat) + " KB/s";

            bytesFormerReceivedInKB = receiveByte;
            bytesFormerSentInKB = SentBype;

        }

        // Provide better formatting for some common speeds.
        static private string GetSpeedString(long speed)
        {
            switch (speed)
            {
                case 10000000:
                    return "10 MB";
                case 11000000:
                    return "11 MB";
                case 54000000:
                    return "54 MB";
                case 100000000:
                    return "100 MB";
                case 1000000000:
                    return "1 GB";
                default:
                    return speed.ToString(NumberFormatInfo.CurrentInfo);
            }
        }

        private void NicInfotimer_Tick(object sender, EventArgs e)
        {
            UpdateNicStats();
            
        }

        #endregion
        /*
        // <summary>
        /// 防止打开多个同样的子窗体,检查是否已打开某个子窗体,如果打开了就返回true,否则返回false
        /// </summary>
        /// <param name="tabName">子窗体的窗体名称</param>
        /// <returns></returns>
        private bool IsOpenTab(string tabName)
        {
            bool isOpened = false;

            foreach (TabItem tab in tabStrip1.Tabs)
            {
                if (tab.Text.Trim() == tabName)
                {
                    isOpened = true;
                    tabStrip1.SelectedTab = tab;
                    break;
                }
            }

            return isOpened;
        }
        */
        private void CreateLogDirectory()
        {
            try
            {
                string log = MyExecPath + "\\ACN_LOG";
                Directory.CreateDirectory(log);
                string LogPath = log + "\\" + "ACN_SaveData" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString();
                Directory.CreateDirectory(LogPath);
                string ADfilepath = LogPath + "\\" + "ACN_ADFile";
                ADPathInfo = Directory.CreateDirectory(ADfilepath);
                string Imgpath = LogPath + "\\" + "ACN_ImgFile";
                ImgInfo = Directory.CreateDirectory(Imgpath);
                string recordpath = LogPath + "\\" + "ACN_SysRecord";
                RecordInfo = Directory.CreateDirectory(recordpath);

                string ComRecvDatapath = LogPath + "\\" + "ACN_Serial";
                Directory.CreateDirectory(ComRecvDatapath);
                ComRecvDatapath = ComRecvDatapath + "\\" + "RecvData";
                SerialRecvPathInfo = Directory.CreateDirectory(ComRecvDatapath);

                string SerialCmdPath = LogPath + "\\" + "ACN_Serial";
                SerialCmdPath = SerialCmdPath + "\\" + "CmdData";
                SerialCmdPathInfo = Directory.CreateDirectory(SerialCmdPath);

                string NetRecvDatapath = LogPath + "\\" + "ACN_Net";
                Directory.CreateDirectory(NetRecvDatapath);
                NetRecvDatapath = NetRecvDatapath + "\\" + "RecvData";
                NetRecvDataPathInfo = Directory.CreateDirectory(NetRecvDatapath);


                string NetCmdPath = LogPath + "\\" + "ACN_Net";
                NetCmdPath = NetCmdPath + "\\" + "CmdData";
                NetCmdDataPathInfo = Directory.CreateDirectory(NetCmdPath);
            }
            catch (Exception MyEx)
            {
                MessageBox.Show(MyEx.Message.ToString());
            }
        }

        #region UI 界面按钮响应
        private void buttonItem7_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 退出按钮消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitBtn_Click(object sender, EventArgs e)
        {
            string message = "是否退出？";
            string caption = "消息";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            MessageBoxIcon icon = MessageBoxIcon.Question;
            MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button2;
            // Show message box
            DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
            if (result == DialogResult.OK)
            {
                
               Environment.Exit(0);
            }
        }
        /// <summary>
        /// 打开命令行对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandLineBtn_Click(object sender, EventArgs e)
        {
            CommandLineWin.WindowState = FormWindowState.Normal;
            CommandLineWin.ShowInTaskbar = true;
            CommandLineWin.Show();
            CommandLineWin.BringToFront();

        }
        /// <summary>
        /// 连接节点操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            if (NetConnect.Text == "断开连接")
            {
                CommandLineWin.ExecCommand("disconnect");
            }
            else
            {
                IPCollections Icf = new IPCollections();
                //读取节点
                XmlDocument xmlfile = new XmlDocument();
                xmlfile.Load(xmldoc);
                string strip;
                XmlNode xn = xmlfile.DocumentElement;
                xn = xn.SelectSingleNode("descendant::节点配置");
                foreach (XmlNode subnode in xn.ChildNodes)
                {
                    string[] ip = { "节点配置", subnode.Name, "网络配置", "节点IP" };
                    strip = XmlHelper.GetConfigValue(xmldoc, ip);
                    Icf.IPList.Items.Add(subnode.Name + " " + strip);

                }
                if (Icf.ShowDialog() == DialogResult.OK)
                {
                    int g = Icf.IPList.CheckedIndices[0];
                    string[] str = Icf.IPList.Items[g].ToString().Split(' ');
                    IPAddress Nodeip = IPAddress.Parse(str[1]);
                    CommandLineWin.ConnectNode(Nodeip);
                }
                Icf.Dispose();
            }
            //CommandLineWin.ExecCommand("connect");
        }

        private void BasicConffBtn_Click(object sender, EventArgs e)
        {
           
            BasicConfForm basicForm = new BasicConfForm();
            basicForm.Show();

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;//因为命令行对话框设置了true，该窗体不会关闭，要将父窗口参数设成false才能全部关闭窗口
        }

        public void ChangeLabel(object sender, EventsClass.StatusEventArgs e)
        {
            if (statusStrip.InvokeRequired)
            {
                ChangeLaber c = new ChangeLaber(ChangeLabel);
                this.Invoke(c, new object[] { sender, e });
            }
            else
            {
                if (CommLineForm.bConnect)
                {

                    NetConnect.Text = "断开连接";
                    UpLoadADBtn.Enabled = true;
                    DLoadExecBtn.Enabled = true;
                    linkstatus.Text = e.label;

                }
                else
                {
                    NetConnect.Text = "连接网络";
                    UpLoadADBtn.Enabled = false;
                    DLoadExecBtn.Enabled = false;
                    linkstatus.Text = e.label;
                }
            }
        }
        public void ShowTime(object sender, EventsClass.StatusEventArgs e)
        {
            nodetimestrip.Text = e.label;
        }

        public void ShowUtcTime(object sender, EventsClass.UtcTimeEventArgs e)
        {
            GpsTimeUpdate.Text = "UTC：" + e.GpsUtcTime.ToString();
        }
        public void showlatlng(string str)
        {
            if (statusStrip.InvokeRequired)
            {
                PositionShow p = new PositionShow(showlatlng);
                this.Invoke(p, new object[] { str });
            }
            else
            {
                
                PointLoacation.Text = str;
            }
        }

        private void UpLoadADBtn_Click(object sender, EventArgs e)
        {
            if (UpLoadADBtn.Text == "回传AD")
            {
                UpLoadADBtn.Text = "停止回传";
                CommandLineWin.ExecCommand("ad");
                
            }
            else
            {
                char cesc = (char)27;
                CommandLineWin.ExecCommand(cesc.ToString());
                UpLoadADBtn.Text = "回传AD";
            }

        }
        
        //更新547程序文件
        private void DLoadExecBtn_Click(object sender, EventArgs e)
        {
            openDataDialog.Title = "选择更新程序文件";
            if (openDataDialog.ShowDialog() == DialogResult.OK)
            {
                networkdata data = new networkdata();
                data.DataType = 2;
                data.FileNamePath = openDataDialog.FileName;
                CommandLineWin.ExecCommand("gd -u");
                DloadprogressBar.Visible = true;
                CommandLineWin.DLoadDataWorker.RunWorkerAsync(data);
            }
        }
        
        //在线加载程序
        private void DloadOnline_Click(object sender, EventArgs e)
        {
            openDataDialog.Title = "选择在线运行程序文件";
            if (openDataDialog.ShowDialog() == DialogResult.OK)
            {
                networkdata data = new networkdata();
                data.DataType = 2;
                data.FileNamePath = openDataDialog.FileName;
                CommandLineWin.ExecCommand("gd -l");
                DloadprogressBar.Visible = true;
                CommandLineWin.DLoadDataWorker.RunWorkerAsync(data);
            }
        }
        private void DloadM2_Click(object sender, EventArgs e)
        {

            openDataDialog.Title = "选择TS101程序文件(M2)";
            if (openDataDialog.ShowDialog() == DialogResult.OK)
            {
                networkdata data = new networkdata();
                data.DataType = 2;
                data.FileNamePath = openDataDialog.FileName;
                CommandLineWin.ExecCommand("gd -t -m2");
                DloadprogressBar.Visible = true;
                CommandLineWin.DLoadDataWorker.RunWorkerAsync(data);
            }

        }

        private void DloadM4_Click(object sender, EventArgs e)
        {
            openDataDialog.Title = "选择TS101程序文件(M4)";
            if (openDataDialog.ShowDialog() == DialogResult.OK)
            {
                networkdata data = new networkdata();
                data.DataType = 2;
                data.FileNamePath = openDataDialog.FileName;
                CommandLineWin.ExecCommand("gd -t -m4");
                DloadprogressBar.Visible = true;
                CommandLineWin.DLoadDataWorker.RunWorkerAsync(data);
            }
        }
        //下载数据文件
        private void DloadSendWave_Click(object sender, EventArgs e)
        {
            openDataDialog.Title = "选择下载数据文件";
            if (openDataDialog.ShowDialog() == DialogResult.OK)
            {
                networkdata data = new networkdata();
                data.DataType = 2;
                data.FileNamePath = openDataDialog.FileName;
                CommandLineWin.ExecCommand("gd");
                DloadprogressBar.Visible = true;
                CommandLineWin.DLoadDataWorker.RunWorkerAsync(data);
            }
        }
        //下载FPGA文件
        private void Dloadfpga_Click(object sender, EventArgs e)
        {
            openDataDialog.Title = "选择fpga文件";
            if (openDataDialog.ShowDialog() == DialogResult.OK)
            {
                networkdata data = new networkdata();
                data.DataType = 2;
                data.FileNamePath = openDataDialog.FileName;
                CommandLineWin.ExecCommand("gd -f");
                DloadprogressBar.Visible = true;
                CommandLineWin.DLoadDataWorker.RunWorkerAsync(data);
            }
        }
        //下载bootloader
        private void DloadBootLoad_Click(object sender, EventArgs e)
        {
            openDataDialog.Title = "选择loader文件";
            if (openDataDialog.ShowDialog() == DialogResult.OK)
            {
                networkdata data = new networkdata();
                data.DataType = 2;
                data.FileNamePath = openDataDialog.FileName;
                CommandLineWin.ExecCommand("gd -b");
                DloadprogressBar.Visible = true;
                CommandLineWin.DLoadDataWorker.RunWorkerAsync(data);
            }
        }
        private void serialtime_Tick(object sender, EventArgs e)
        {
            if (!MapForm.GpsSerialPort.IsOpen)
            {
                OpenGps.Checked = false;
                GpsPort.Enabled = true;
                GpsBaudrate.Enabled = true;
                serialtime.Stop();
            }
          
        }
        private void buttonItem2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
        

        #endregion

        private void OpenGps_CheckedChanged(object sender, EventArgs e)
        {
            if (OpenGps.Checked)
            {
                if ((GpsPort.SelectedIndex != -1) && (GpsBaudrate.SelectedIndex != -1))
                {
                    mapdoc.comm = GpsPort.Items[GpsPort.SelectedIndex].ToString();
                    mapdoc.baudrate = int.Parse(GpsBaudrate.Items[GpsBaudrate.SelectedIndex].ToString());
                    mapdoc.GPS_StartWork();
                    if (!MapForm.GpsSerialPort.IsOpen)
                    {
                        OpenGps.Checked = false;
                    }
                    else
                    {
                        GpsPort.Enabled = false;
                        GpsBaudrate.Enabled = false;
                        
                        serialtime.Interval = 500;
                        serialtime.Tick += new EventHandler(serialtime_Tick);
                        serialtime.Start();
                    }
                }
                else
                {
                    MessageBox.Show("请选择正确的配置！");
                }
            }
            else
            {
                GpsPort.Enabled = true;
                GpsBaudrate.Enabled = true;
                MapForm.isContinue = false;
                try
                {
                    if (MapForm.GpsSerialPort.IsOpen)
                        MapForm.GpsSerialPort.Close();
                }
                catch
                { }

            }
        }
        private void Commtime_Tick(object sender, EventArgs e)
        {
            if (!MapForm.MspSerialPort.IsOpen)
            {
                OpenCom.Text = "打开串口";
                //Commtime.Stop();
                MapForm.isMspContinue = false;
                MapForm.IsLoaderRunning = false;
                linkstatus.Text = "通信串口连接已关闭！";
                mapdoc.BurnStat.Enabled = false;
                mapdoc.BurnStat.Checked = false;
                mapdoc.AddFile.Enabled = false;
            }
            else
            {
                MapForm.isMspContinue = true;
                MapForm.IsLoaderRunning = true;
                OpenCom.Text = "关闭串口";
                linkstatus.Text = "通信串口已连接！";
                mapdoc.BurnStat.Enabled = true;
                //mapdoc.BurnStat.Checked = true;
                mapdoc.AddFile.Enabled = true;
             
            }

        }
        private void OpenCom_Click(object sender, EventArgs e)
        {
            if (MapForm.MspSerialPort.IsOpen)
            {
                string message = "是否关闭串口？";
                string caption = "消息";
                MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                MessageBoxIcon icon = MessageBoxIcon.Question;
                MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button2;
                // Show message box
                DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
                if (result == DialogResult.OK)
                {
                    MapForm.isMspContinue = false;
                    MapForm.IsLoaderRunning = false;
                    try
                    {
                        if (MapForm.MspSerialPort.IsOpen)
                            MapForm.MspSerialPort.Close();
                        OpenCom.Text = "打开串口";
                        linkstatus.Text = "通信串口连接已关闭！";
                        //Commtime.Stop();
                        mapdoc.BurnStat.Enabled = false;
                        mapdoc.BurnStat.Checked = false;
                        mapdoc.AddFile.Enabled = false;
                    }
                    catch
                    { }
                }
            }
            else
            {
                string[] str = { "节点串口参数", "端口号" };
                MapForm.MspSerialPort.PortName = XmlHelper.GetConfigValue(xmldoc, str).ToUpper();

                string[] newstr = { "节点串口参数","波特率" };
                MapForm.MspSerialPort.BaudRate = int.Parse(XmlHelper.GetConfigValue(xmldoc, newstr));
                if (MapForm.MspSerialPort.PortName == null)
                    MessageBox.Show("读取MSP430端口错误！");
                mapdoc.MSP_StartWork();
                if (MapForm.MspSerialPort.IsOpen)
                {
                    OpenCom.Text = "关闭串口";
                    linkstatus.Text = "通信串口已连接！";
                    
                }

            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            
        }

        private void LiestBtn_Click(object sender, EventArgs e)
        {
            comlistwin.ShowInTaskbar = true;
            comlistwin.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            //comlistwin.TopMost = true;
            comlistwin.Show();
            comlistwin.BringToFront();
      
        }
        public void RefreshListStat()
        {
            if(LiestBtn.InvokeRequired)
            {
                CmdListNotice Cln = new CmdListNotice(RefreshListStat);
                this.Invoke(Cln,new object[]{});

            }
            else
            {
                if (comlistwin.CmdForSend.Count > 0)
                    this.Text = "水声通信网吊放显控程序(命令列表中有" + comlistwin.CmdForSend.Count.ToString() + "条命令待发送)";
                else
                    this.Text = "水声通信网吊放显控程序";
            }
        }


        private void ConnectNode_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            
        }

        private void ConnectNode_CheckedChanging(object sender, CheckBoxChangeEventArgs e)
        {
            e.Cancel = true;

            if (CommLineForm.bConnect == true)
            {
                if (ConnectNode.Checked)
                {
                    string message = "是否关闭当前连接？";
                    string caption = "消息";
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                    MessageBoxIcon icon = MessageBoxIcon.Question;
                    MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button2;
                    // Show message box
                    DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
                    if (result == DialogResult.OK)
                    {
                        CommandLineWin.ExecCommand("disconnect");

                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    e.Cancel = false;
                    
                    IpConnect.ControlText = MainForm.pMainForm.CommandLineWin.Tclient.Client.RemoteEndPoint.ToString().Split(':')[0];

                }
            }
            else if ((CommandLineWin.ConnNodeBtn.Text == "取消连接")&&(!CommLineForm.bConnect))
            {
                CommandLineWin.ExecCommand("disconnect");
                e.Cancel = true;
            }
            else
            {
                IPAddress addr = new IPAddress(0x1111);
                if (ConnectNode.Checked)//断开
                {
                    e.Cancel = false;
                }
                else//要连接
                {
                    if (IPAddress.TryParse(IpConnect.TextBox.Text, out addr))
                    {
                        ConnectNode.Text = "取消连接";
                        CommandLineWin.ConnectNode(addr);
                    }
                    
                }
            }


        }

        private void ADWindow_Click(object sender, EventArgs e)
        {
            ADform.Show();
            ADform.ShowInTaskbar = true;
            if (ADform.WindowState == FormWindowState.Minimized)
                ADform.WindowState = FormWindowState.Normal;
            ADform.BringToFront();
        }

        private void ConnectNode_Click(object sender, EventArgs e)
        {

        }

        private void about_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }

        private void AutoTest_Click(object sender, EventArgs e)
        {
            TestForm tf = new TestForm();
            tf.ShowDialog();
            tf.Dispose();
        }

        private void NetConnectDetectertimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (CommLineForm.bConnect == true)
                {
                    ConnectNode.Text = "已连接";
                    NetConnect.Text = "断开连接";
                    if (ConnectNode.Checked == false)
                    {
                        ConnectNode.Checked = true;
                        
                    }
                }
                else
                {
                    ConnectNode.Text = "连接节点";
                    NetConnect.Text = "连接网络";
                    if (ConnectNode.Checked == true)
                    {
                        ConnectNode.Checked = false;
                        
                    }
                }
            }
            catch (NullReferenceException MySockEx)
            {
                if (ConnectNode.Checked == true)
                {
                    ConnectNode.Checked = false;
                }
            }
        }

        private void BuoyChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BuoyChoice.SelectedIndex == 0)
                MainForm.pMainForm.mapdoc.BuoyID = "00";
            if (BuoyChoice.SelectedIndex == 1)
                MainForm.pMainForm.mapdoc.BuoyID = "01";
            if (BuoyChoice.SelectedIndex == 2)
                MainForm.pMainForm.mapdoc.BuoyID = "02";
        }

       

       

        

    }
}
