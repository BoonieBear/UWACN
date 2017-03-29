using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using DevComponents.DotNetBar;
using System.Net;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.IO;
using System.Xml;
using System.Media;
using webnode.Helper;
using System.Threading;
using System.Collections.Generic;

namespace webnode.Forms
{
    
    public partial class CommLineForm : Office2007Form
    {
        //负责与节点交换数据
        public TcpClient Tclient,Dclient,HbClient;
        public NetworkStream Tstream, Dstream, Hbstream;
        public static bool bConnect;
        public bool hasRecv = false;
        string MyExecPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        string xmldoc;
        //文件操作
        BinaryReader filereader;
        BinaryWriter filewriter;
        //记录文件
        public AdFile ch1AdFile = new AdFile("Ch1AD");
        public AdFile ch2AdFile = new AdFile("Ch2AD");
        public AdFile ch3AdFile = new AdFile("Ch3AD");
        public AdFile ch4AdFile = new AdFile("Ch4AD");
        public AdFile WaveFile = new AdFile("BaseWave");
        public AdFile NetTCPFile = new AdFile("NetTCPData");
        public AdFile NetUDPFile = new AdFile("NetUDPData");
        public AdFile NetCmdFile = new AdFile("NetCmdData");
        static LogFile NetLogFile = new LogFile("NetTextLog");

        string command;//当前发送的命令
        //SoundPlayer AdPlayer = new SoundPlayer();
        public static int ADchannel=0;//AD显示通道号
        public EventWaitHandle ACPacketHandle;//AC响应包同步事件句柄

        #region 事件委托和响应

        /// <summary>
        /// 用于子窗口操作父窗控件
        /// </summary>
        public delegate void StatusEventHandler(object sender, EventsClass.StatusEventArgs e);//主窗口状态栏委托
        public event StatusEventHandler StatusLabelEvent;
        public delegate void TimeEventHandler(object sender, EventsClass.StatusEventArgs e);//主窗口状态栏委托
        public event TimeEventHandler TimeLabelEvent;
        public delegate void WaveEventHandler(object sender, EventsClass.WaveEventArgs e);//波形显示委托
        public event WaveEventHandler WaveDisplEvent;
        public delegate void DataEventHandler(object sender, EventsClass.DataEventArgs e);//网络数据委托
        public event DataEventHandler NetDataEvent;
        public delegate void ADEventHandler(object sender, EventsClass.DataEventArgs e);//AD数据委托
        public event ADEventHandler ADDataEvent;
        #endregion

  //      public delegate void InvokeDelegate();
        delegate void AddBoxCallback(Color newcolor,string command);
        
        private void AvailabilityChangedCallback(object sender, EventArgs e)
        {
            NetworkAvailabilityEventArgs myEg = (NetworkAvailabilityEventArgs)e;
            if (!myEg.IsAvailable)
            {
                
                if (Tclient == null || Dclient ==null)
                    return;
                if (Tclient.Client != null)
                {
                    if (Tclient.Connected)
                    {
                        Tstream.Close();
                        Tclient.Close();
                    }
                }
                else
                {
                    Tclient.Close();
                }
                if (Dclient.Client != null)
                {
                    if (Dclient.Connected)
                    {
                        Dstream.Close();
                        Dclient.Close();
                    }
                }
                else
                {
                    Dclient.Close();
                }
                if (DLoadDataWorker.IsBusy)
                {
                    DLoadDataWorker.CancelAsync();
                    ACPacketHandle.Set();
                    
                }
                MessageBox.Show("网络状态出错，请检查网络！");
                
                bConnect = false;
            }
        }



        public CommLineForm()
        {
            InitializeComponent();
            
            //wf = WaveWin
            //AddtoBox(Color.YellowGreen, "系统命令行" + "\r\n");
            CommandLineBox.SelectionColor = Color.Black;
            NetworkChange.NetworkAvailabilityChanged += new
            NetworkAvailabilityChangedEventHandler(AvailabilityChangedCallback);
            
            xmldoc = MyExecPath + "\\" + "config.xml";

        }

        private void CommLineForm_Load(object sender, EventArgs e)
        {
            
            this.StatusLabelEvent += new StatusEventHandler(MainForm.pMainForm.ChangeLabel);
            this.TimeLabelEvent += new TimeEventHandler(MainForm.pMainForm.ShowTime);
//            this.WaveDisplEvent += new WaveEventHandler(MainForm.pMainForm.mapdoc.DispWave);
            this.NetDataEvent += new DataEventHandler(MainForm.pMainForm.mapdoc.InsertNetList);
            this.ADDataEvent += new ADEventHandler(CommLineForm_ADDataEvent); 
            
        }
       
        private void CommLineForm_ADDataEvent(object sender, EventsClass.DataEventArgs e)
        {
            switch (e.DataSource)
            {
                case "1":
                    if (ADchannel == 0)
                        PlayAD(e.DataBuffer);
                    break;
                case "2":
                    if (ADchannel == 1)
                        PlayAD(e.DataBuffer);
                    break;
                case "3":
                    if (ADchannel == 2)
                        PlayAD(e.DataBuffer);
                    break;
                case "4":
                    if (ADchannel == 3)
                        PlayAD(e.DataBuffer);
                    break;
                default:
                    break;
            }
        }

        private void PlayAD(byte[] buf)
        {
           MainForm.pMainForm.ADform.AddToBox(buf);
           //Debug.WriteLine("AddToBox");
            
        }

        /// <summary>
        /// 发送网络消息包，并读取响应信息到命令提示符为止
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// 
        public bool SendCommand(string command)
        {
            try
            {
                if (Tstream!=null)
                {
                   
                    if (Tstream.CanWrite)
                    {
                        byte[] cbuf = System.Text.Encoding.ASCII.GetBytes(command + "\r");
                        Tstream.Write(cbuf, 0, command.Length + 1);
                        return true;
                    }
                  
                }
                return false;

            }
            catch (Exception MyEx)
            {

                SendStatusLabel(MyEx.Message);
                AddtoBox(Color.Black, MyEx.Message + ":" + MyEx.StackTrace + "\r\n");
                return false;
            }


        }

        /// <summary>
        /// 发送网络命令包
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// 
        public bool SendData(byte[] command)
        {
            try
            {
                if (Dstream != null)
                {
                    if (Dstream.CanWrite)
                    {

                        Dstream.Write(command, 0, command.Length);
                        NetCmdFile.OpenFile(MainForm.pMainForm.NetCmdDataPathInfo);
                        NetCmdFile.BinaryWrite(command);
                        NetCmdFile.close();
                        return true;
                    }
                    return false;
                }
                return false;


            }
            catch (Exception MyEx)
            {

                SendStatusLabel(MyEx.Message);
                
                AddtoBox(Color.Black, MyEx.Message + ":" + MyEx.StackTrace + "\r\n");
                return false;

            }


        }
        #region 命令行操作
        /// <summary>
        /// 执行输入或按键命令，即下发命令字符串
        /// </summary>
        /// <param name="strcommand"></param>
        public bool ExecCommand(string strcommand)
        {
            try
            {
                strcommand = strcommand.TrimEnd('\n');
                switch (strcommand)
                {
                    case "disconnect":
                        {
                            if (ConnNodeBtn.Text == "断开连接")
                                ConnNodeBtn.PerformClick();
                            if (ConnNodeBtn.Text == "取消连接")
                                ConnNodeBtn.PerformClick();
                            break;
                        }
                    case "exit":
                        CommandLineBox.SelectionColor = Color.Chocolate;
                        CommandLineBox.AppendText("关闭命令行" + "\r\n");
                        SendCommand("\n");
                        this.Hide();
                        break;
                    case "reboot":
                        {
                            AddtoBox(Color.Black, "重启节点\r\n");
                            if (Tstream.CanWrite)
                            {
                                
                                byte[] cbuf = System.Text.Encoding.ASCII.GetBytes(strcommand + "\r");
                                Tstream.Write(cbuf, 0, strcommand.Length + 1);
                            } 
                            break;
                        }
                    case "ad":
                        AddtoBox(Color.Black, "发送AD命令\r\n");
                        SendCommand(strcommand);
                        break;
                    case "ad -d":
                        AddtoBox(Color.Black, "发送AD命令,工作模式\r\n");
                        SendCommand(strcommand);
                        break;
                    case "gd":
                        {
                            AddtoBox(Color.Black, "下载数据文件（本命令只能通过主面板按钮实现）\r\n");
                            SendCommand(strcommand);
                            break;
                        }
                    case "gd -l":
                        {
                            AddtoBox(Color.Black, "在线加载程序文件（本命令只能通过主面板按钮实现）\r\n");
                            SendCommand(strcommand);
                            break;
                        }
                    case "gd -u":
                        {
                            AddtoBox(Color.Black, "更新程序文件（本命令只能通过主面板按钮实现）\r\n");
                            SendCommand(strcommand);
                            break;
                        }
                    case "gd -t":
                        {
                            AddtoBox(Color.Black, "更新程序文件（本命令只能通过主面板按钮实现）\r\n");
                            SendCommand(strcommand);
                            break;
                        }
                    case "gd -f":
                        {
                            AddtoBox(Color.Black, "更新fpga文件（本命令只能通过主面板按钮实现）\r\n");
                            SendCommand(strcommand);
                            break;
                        }
                    case "gd -b":
                        {
                            AddtoBox(Color.Black, "更新loader文件（本命令只能通过主面板按钮实现）\r\n");
                            SendCommand(strcommand);
                            break;
                        }
                    default:
                        SendCommand(strcommand);
                        break;
                }
                return true;
                //
            }
            catch (Exception MyEx)
            {
                SendStatusLabel(MyEx.Message);
                AddtoBox(Color.Black, MyEx.Message + ":" + MyEx.StackTrace + "\r\n/>");
                return false;
            }

        }
        /// <summary>
        ///  调用主窗口函数
        /// </summary>
        /// <param name="str"></param>
        public void SendStatusLabel(string str)
        {
            StatusEventHandler handler = StatusLabelEvent;
            EventsClass.StatusEventArgs e = new EventsClass.StatusEventArgs(str); 
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void ShowNodeTime(string str)
        {
            TimeEventHandler handler = TimeLabelEvent;
            EventsClass.StatusEventArgs e = new EventsClass.StatusEventArgs(str);
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public void AddtoBox(Color fontcolor,string strcommand)
        {
            if (CommandLineBox.InvokeRequired)//不是同一线程调用，调用控件的委托
            {
                AddBoxCallback d = new AddBoxCallback(AddtoBox);
                this.Invoke(d, new object[] {fontcolor, strcommand });

            }
            else//同一线程，直接操作
            {
                if (CommandLineBox.TextLength > 150000)
                {
                    CommandLineBox.Select(0, CommandLineBox.TextLength / 3);
                    CommandLineBox.Cut();
                }
                CommandLineBox.SelectionColor = fontcolor;
                CommandLineBox.AppendText(strcommand);
                CommandLineBox.ScrollToCaret();
                CommandLineBox.SelectionColor = Color.Black;
                strcommand = strcommand.Replace(@"/>","");
                MainForm.pMainForm.mapdoc.WriteNetLog(strcommand);
                if (NetLogFile.logfile.ws == null)//还未创建文件
                    NetLogFile.OpenFile(MainForm.pMainForm.RecordInfo);
                NetLogFile.writeLine("\r\n" + strcommand);
                if (NetLogFile.length >= 600 * 1024)//不允许大于600K
                {
                    NetLogFile.close();
                }
                if(MainForm.pMainForm.AutoConnectWin.Visible)
                    MainForm.pMainForm.AutoConnectWin.AddToBox(strcommand);
            }
            
        }
        
        #endregion


        #region 按钮操作
        public void ConnNodeBtn_Click(object sender, EventArgs e)
        {
             //开始连接
            if (ConnNodeBtn.Text == "连接节点")
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
                    ConnectNode(Nodeip);
                    
                }
                Icf.Dispose();
                

            }
            //取消连接
            else
            {
                bConnect = false;
                this.NodeLinker.CancelAsync();
                this.NodeReceiver.CancelAsync();
                this.CommAnsReceiver.CancelAsync();
                if (Tclient.Client != null)
                {
                    if (Tclient.Connected)
                    {
                        Tstream.Close();
                        Tclient.Close();
                    }
                }
                else
                {
                    Tclient.Close();
                }
                if (Dclient.Client != null)
                {
                    if (Dclient.Connected)
                    {
                        Dstream.Close();
                        Dclient.Close();
                    }
                }
                else
                {
                    Dclient.Close();
                }
                SendStatusLabel("未连接节点");
                ConnNodeBtn.Text = "连接节点";
                AddtoBox(Color.Black, "与节点断开\r\n");
                bConnect = false;
            }
        }
        #endregion

        #region 按键响应
        private void CommandLineBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Tclient != null)
            {
                if ((Tclient.Client != null) && (Tclient.Connected))
                {
                    CommandLineBox.SelectionColor = Color.Black;
                    if (e.KeyChar == '\r')
                    {
                        CommandLineBox.Select(CommandLineBox.TextLength > 64 ? CommandLineBox.TextLength / 2 : 0, CommandLineBox.TextLength);
                        int iStartIndex = CommandLineBox.SelectedText.LastIndexOf(@">");
                        if (iStartIndex == -1)
                            return;
                        command = CommandLineBox.SelectedText.Substring(iStartIndex + 1, CommandLineBox.SelectionLength - iStartIndex - 1);
                        //exec command
                        ExecCommand(command);
                        //
                        command = string.Empty;//可以用一个array存储历史command，就更像一个shell了
                        return;
                    }
                    
                    command = command + e.KeyChar;
                }
            }
        }
        private void CommandLineBox_KeyDown(object sender, KeyEventArgs e)
        {
            char cesc = (char)27;
            try
            {
                if (Tclient != null)
                {
                    if (Tclient != null && Tclient.Connected)
                    {
                        if (e.Control && e.KeyCode == Keys.D)
                        {
                            ExecCommand(cesc.ToString());
                            //WaveFile.close();
                            //ch1AdFile.close();
                            //ch2AdFile.close();
                            //ch3AdFile.close();
                            //ch4AdFile.close();
                            if (ACPacketHandle == null)
                                ACPacketHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
                            ACPacketHandle.Set();
                            DLoadDataWorker.CancelAsync();
                        }
                        if (e.KeyCode == Keys.Back)
                        {
                            if (CommandLineBox.Text.EndsWith(@">"))
                            {
                                e.SuppressKeyPress = true;
                                return;
                            }

                        }
                       
                    }
                }

            }
            catch (Exception MyEx)
            {
                SendStatusLabel(MyEx.Message);
                AddtoBox(Color.Black, MyEx.Message +":"+MyEx.StackTrace+ "\r\n/>");
            }
        }
        #endregion


        /// <summary>
        /// 窗口关闭处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommLineForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.ShowInTaskbar = false;
            if (e.CloseReason != CloseReason.ApplicationExitCall)//非系统关闭，只是关闭本窗口
                e.Cancel = true;
        }

        #region 连接节点操作
        /// <summary>
        /// 连接节点，使用nodelinker在后台连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ConnectNode(IPAddress Nodeip)
        {
           
            try
            {
                //string[] ip = { "吊放配置", "网络配置", "节点IP" };
                //IPAddress Nodeip = IPAddress.Parse(XmlHelper.GetConfigValue(xmldoc, ip));
                Tclient = new TcpClient();//每次close后都要重写new一个新的对象，因为close后源对象已释放

                //Tclient.ReceiveTimeout = 5000;
                Tclient.SendTimeout = 1000;
                Dclient = new TcpClient();//每次close后都要重写new一个新的对象，因为close后源对象已释放
                //Dclient.ReceiveTimeout = 5000;
                Dclient.SendTimeout = 1000;
                HbClient = new TcpClient();
                HbClient.SendTimeout = 1000;
            if (NodeLinker.IsBusy)
                {
                       
                    NodeLinker.CancelAsync();
                    Thread.Sleep(300);
                            
                }
                    
                NodeLinker.RunWorkerAsync(Nodeip);
                SendStatusLabel("连接节点中……");
                AddtoBox(Color.Black, "连接节点中……\n");
                ConnNodeBtn.Text = "取消连接";
            }

            catch (Exception MyEx)
            {

                SendStatusLabel(MyEx.Message);
                AddtoBox(Color.Black, MyEx.Message + ":" + MyEx.StackTrace + "\r\n/>");
                ConnNodeBtn.Text = "连接节点";
            }


        }
        private static void ConnnectCallBack(IAsyncResult ar)
        {
            try
            {
                TcpClient t = (TcpClient)ar.AsyncState;
                t.EndConnect(ar);
            }
            catch(Exception ex)
            {
                bConnect = false;
            }

        }
        private static void HBConnnectCallBack(IAsyncResult ar)
        {
            try
            {
                TcpClient t = (TcpClient)ar.AsyncState;
                t.EndConnect(ar);
            }
            catch (Exception ex)
            {
                //bConnect = false;
            }

        }
        private void connect(IPAddress ipaddr, BackgroundWorker MyWorker, DoWorkEventArgs e)
        {
            if (MyWorker.CancellationPending)
            {
                e.Cancel = true;
            }
            
            string[] cportstr = { "命令端口" };
            int cport = Int16.Parse(XmlHelper.GetConfigValue(xmldoc, cportstr));
            string[] dportstr = { "数据端口" };
            int dport = Int16.Parse(XmlHelper.GetConfigValue(xmldoc, dportstr));
            try
            {
                
                AddtoBox(Color.Black,"连接命令端口……\r\n");
                
                Tclient.BeginConnect(ipaddr, cport, new AsyncCallback(ConnnectCallBack), Tclient);
                while (true)
                {
                    Thread.Sleep(50);
                    if (MyWorker.CancellationPending == false)
                    {
                        if ((Tclient.Client != null) && (Tclient.Connected == true))
                        {
                            Tstream = Tclient.GetStream();
                            AddtoBox(Color.Black, "命令端口已连接。\r\n");
                            break;
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                
                
                

            }
            catch (SocketException myEx)
            {
                e.Result = myEx.ErrorCode;
                bConnect = false;
                AddtoBox(Color.Black, "命令端口连接失败。\r\n");
                return;

            }
            try
            {
                AddtoBox(Color.Black, "数据命令端口……\r\n");
                
                Dclient.BeginConnect(ipaddr, dport, new AsyncCallback(ConnnectCallBack), Dclient);
                while (true)
                {
                    Thread.Sleep(50);
                    if (MyWorker.CancellationPending == false)
                    {
                        if ((Dclient.Client != null) && (Dclient.Connected == true))
                        {
                            Dstream = Dclient.GetStream();
                            AddtoBox(Color.Black, "数据端口已连接。\r\n");
                            break;
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                        return;
                    }
                }  
                bConnect = true;
                //AddtoBox(Color.Black, "数据端口已连接。\r\n");
            }
            catch (SocketException myEx)
            {
                e.Result = myEx.ErrorCode;
                bConnect = false;
                AddtoBox(Color.Black, "数据端口连接失败。\r\n");
                return;
            }
            HbClient.BeginConnect(IPAddress.Parse("127.0.0.1"), 32100, new AsyncCallback(HBConnnectCallBack), HbClient);
            while (true)
            {
                Thread.Sleep(50);
                if (MyWorker.CancellationPending == false)
                {
                    if ((HbClient.Client != null) && (HbClient.Connected == true))
                    {
                        Hbstream = HbClient.GetStream();
                        AddtoBox(Color.Black, "HB端口已连接。\r\n");
                        break;
                    }
                }
                else
                {
                    //e.Cancel = true;
                    return;
                }
            }


        }
        private void NodeLinker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            connect((IPAddress)e.Argument, worker, e);
        }

        private void NodeLinker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (e.Cancelled)
            {
                ConnNodeBtn.Text = "连接节点";
                string errstring = "操作已被取消！";
                SendStatusLabel(errstring);
                AddtoBox(Color.Black, errstring + "\r\n");
                bConnect = false;
                Tclient.Close();
                Dclient.Close();
            }
            else if (e.Result != null)
            {

                ConnNodeBtn.Text = "连接节点";
                string errstring = " 操作失败！" + "错误号：" + e.Result.ToString();
                SendStatusLabel(errstring);
                AddtoBox(Color.Black, errstring + "\r\n");
                bConnect = false;
                Tclient.Close();
                Dclient.Close();

            }
            else
            {
                bConnect = true;
                ConnNodeBtn.Text = "断开连接";
                SendStatusLabel("已连接节点");
                AddtoBox(Color.Black, "已连接节点\r\n");
                if (Tclient.Connected)
                {
                    networkdata data = new networkdata();
                    data.DataType = 0;
                    CommAnsReceiver.RunWorkerAsync(data);
                }
                if (Dclient.Connected)
                {
                    networkdata data = new networkdata();
                    data.DataType = 1;
                    NodeReceiver.RunWorkerAsync(data);
                }


            }
        }
        #endregion

        #region 接收数据线程
        private void NodeReceiver_DoWork(object sender, DoWorkEventArgs e)
        {
            
            BackgroundWorker worker = sender as BackgroundWorker;
            
            networkdata DType = (networkdata)e.Argument;
            try
            {
                if (Dclient.Connected)
                {
                    byte[] myReadBuffer = new byte[4100];
                    while ((Dstream.CanRead) && (!worker.CancellationPending))
                    {
                        Array.Clear(myReadBuffer, 0, 4100);//置零
                        int numberOfBytesRead = 0;
                        Dstream.Read(myReadBuffer, 0, 4);//先读包头
                        UInt16 PacketLength = BitConverter.ToUInt16(myReadBuffer, 2);
                        UInt16 head = BitConverter.ToUInt16(myReadBuffer, 0);
                        if (PacketLength > 4096)
                        {
                            continue;
                        }
                        if((head!= 0xABCD)&&(head!= 0xAD01)&&(head!=0xAD02)&& (head != 0xAD03) && (head != 0xAD04)
                            &&(head != 0xEDED)&& (head != 0xEE01)&&(head != 0xBB01) && (head != 0xACAC) && (head != 0x45FF))
                        {
                            continue;
                        }
                        // Incoming message may be larger than the buffer size.
                        do
                        {
                            int n = Dstream.Read(myReadBuffer, 4 + numberOfBytesRead, PacketLength - numberOfBytesRead);
                            numberOfBytesRead += n;

                        }
                        while (numberOfBytesRead != PacketLength);
                        ParseNetworkPacket(myReadBuffer, PacketLength);
                    }
                    if (worker.CancellationPending)
                        e.Cancel = true;
                }
            }
            catch (SocketException MyEx)
            {
                e.Result = MyEx.ErrorCode;
                MainForm.ParseLock.ReleaseMutex();//ParseNetworkPacket中调用了lock
                AddtoBox(Color.Black, MyEx.Message + ":" + MyEx.StackTrace.ToString() + "\r\n/>");
                bConnect = false;
            }
            catch (IOException IOEx)
            {
                bConnect = false;
                AddtoBox(Color.Black, "网络连接关闭!\r\n/>");
                //SendStatusLabel("IO错误！网络连接关闭");
            }

        }

        private void NodeReceiver_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (e.Cancelled)
            {
                ConnNodeBtn.Text = "连接节点";
                bConnect = false;
                Tclient.Close();
                Dclient.Close();
                AddtoBox(Color.Black, "数据接收取消!\n/>");
            }
            else if (e.Error != null)
            {
                ConnNodeBtn.Text = "连接节点";
                AddtoBox(Color.Black, "数据接收中断!\n/>");
                Dclient.Close();
                Tclient.Close();
                AddtoBox(Color.Black, "错误信息：" + e.Error.ToString() + "\r\n/>");
            }
            else
            {
                ConnNodeBtn.Text = "连接节点";
                AddtoBox(Color.Black, "数据接收中断!\n/>");
                //AddtoBox(Color.Black, "错误号：" + e.Error.ToString() + "\r\n/>");
                Dclient.Close();
                Tclient.Close();
            }
        }
        #endregion

        #region 下载数据线程
        private void DLoadDataWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            networkdata data = (networkdata)e.Argument;
           
            try
            {
                
                filereader = new BinaryReader(File.Open(data.FileNamePath, FileMode.Open));
                FileInfo d = new FileInfo(data.FileNamePath);
                long filelength = d.Length;
                if(ACPacketHandle==null)
                    ACPacketHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
                if (Dclient.Connected)
                {
                    byte[] mySendBuffer = new byte[1028];
                    Int64 SendBytes = 0;
                    if (Dstream.CanWrite)
                    {

                        int numberOfBytesRead = 0;
                        uint head = 0xDADA;
                        Buffer.BlockCopy(BitConverter.GetBytes(head),0,mySendBuffer,0,2);
                        while (((numberOfBytesRead = filereader.Read(mySendBuffer, 4, 1024)) != 0) && (!worker.CancellationPending))
                        {
                            Buffer.BlockCopy(BitConverter.GetBytes(numberOfBytesRead), 0, mySendBuffer, 2, 2);
                            Dstream.Write(mySendBuffer, 0, numberOfBytesRead + 4);//
                            SendBytes += numberOfBytesRead;
                            numberOfBytesRead = 0;
                            worker.ReportProgress((int)((double)SendBytes * 100 /filelength));
                            if (!ACPacketHandle.WaitOne(10000))//等待信号超时
                            {
                                Exception MyEx = new Exception("接收应答数据超时！");
                                throw MyEx;
                            }
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                break;
                            }
                        }
                        if (worker.CancellationPending)
                            e.Cancel = true;
                        filereader.BaseStream.Seek(0,SeekOrigin.Begin);//回到文件头
                        byte[] totalb = filereader.ReadBytes((int)filereader.BaseStream.Length);
                        ushort crc = CRCHelper.CRC16byte(totalb);
                        filereader.Close();
                        head = 0xEDED;
                        Buffer.BlockCopy(BitConverter.GetBytes(head), 0, mySendBuffer, 0, 2);
                        Buffer.BlockCopy(BitConverter.GetBytes(crc), 0, mySendBuffer, 2, 2);
                        Dstream.Write(mySendBuffer, 0, 4);
                        
                    }
                }
            }
            catch (Exception MyEx)
            {
                MessageBox.Show(MyEx.Message);
                AddtoBox(Color.Black, MyEx.Message + ":" + MyEx.StackTrace.ToString() + "\r\n/>");
                filereader.Close();
            }

        }

        private void DLoadDataWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            MainForm.pMainForm.DloadprogressBar.Value =  e.ProgressPercentage;
            MainForm.pMainForm.DloadprogressBar.Text = "已下载" + e.ProgressPercentage+ "%";

        }

        private void DLoadDataWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error!=null)
            {
                AddtoBox(Color.Black, e.Error.Message + ":" + e.Error.StackTrace.ToString() + "\n/>");
            }
            if(e.Cancelled)
                AddtoBox(Color.Black, "下载被用户终止" + "\r\n/>");
            MainForm.pMainForm.DloadprogressBar.Visible = false;
        }
        #endregion

        private void ClearCommandLine_Click(object sender, EventArgs e)
        {
            CommandLineBox.Clear();
            SendCommand("\n");
            AddtoBox(Color.Black, @"/>");
        }



        #region 网络数据处理
        public void ParseNetworkPacket(byte[] netpacket,int length)
        {
            byte[] data = new byte[length];
            Buffer.BlockCopy(netpacket, 4, data, 0, length);
            switch (BitConverter.ToUInt16(netpacket,0))
            {
                case 0xABCD:
                    {   
                        object nodetime = new UtilityClass.UtcTime();
                        nodetime = UtilityClass.RawDeserialize(data,(Type)nodetime.GetType());
                        ShowNodeTime(nodetime.ToString());
                        break;
                    }
                case 0xAD01:
                    {
                        if (ch1AdFile.adfile.writeOpened == false)
                            ch1AdFile.OpenFile(MainForm.pMainForm.ADPathInfo);
                        ch1AdFile.BinaryWrite(data);
                        ADEventHandler handler = ADDataEvent;
                        EventsClass.DataEventArgs e = new EventsClass.DataEventArgs("1", data, length, "");
                        if (handler != null)
                        {
                            handler(this, e);
                        }
                        if (ch1AdFile.FileLen > 1024 * 1024 * 100)
                        {
                            ch1AdFile.close();
                            ch1AdFile.OpenFile(MainForm.pMainForm.ADPathInfo);
                        }
                        break;
                    }
                case 0xAD02:
                    {
                        if (ch2AdFile.adfile.writeOpened == false)
                            ch2AdFile.OpenFile(MainForm.pMainForm.ADPathInfo);
                        ch2AdFile.BinaryWrite(data);
                        ADEventHandler handler = ADDataEvent;
                        EventsClass.DataEventArgs e = new EventsClass.DataEventArgs("2", data, length, "");
                        if (handler != null)
                        {
                            handler(this, e);
                        }
                        if (ch2AdFile.FileLen > 1024 * 1024 * 100)
                        {
                            ch2AdFile.close();
                            ch2AdFile.OpenFile(MainForm.pMainForm.ADPathInfo);
                        }
                        break;
                    }
                case 0xAD03:
                    {
                        if (ch3AdFile.adfile.writeOpened == false)
                            ch3AdFile.OpenFile(MainForm.pMainForm.ADPathInfo);
                        ch3AdFile.BinaryWrite(data);
                        ADEventHandler handler = ADDataEvent;
                        EventsClass.DataEventArgs e = new EventsClass.DataEventArgs("3", data, length, "");
                        if (handler != null)
                        {
                            handler(this, e);
                        }
                        if (ch3AdFile.FileLen > 1024 * 1024 * 100)
                        {
                            ch3AdFile.close();
                            ch3AdFile.OpenFile(MainForm.pMainForm.ADPathInfo);
                        }
                        break;
                    }
                case 0xAD04:
                    {
                        if (ch4AdFile.adfile.writeOpened == false)
                            ch4AdFile.OpenFile(MainForm.pMainForm.ADPathInfo);
                        ch4AdFile.BinaryWrite(data);
                        ADEventHandler handler = ADDataEvent;
                        EventsClass.DataEventArgs e = new EventsClass.DataEventArgs("4", data, length, "");
                        if (handler != null)
                        {
                            handler(this, e);
                        }
                        if (ch4AdFile.FileLen > 1024 * 1024 * 100)
                        {
                            ch4AdFile.close();
                            ch4AdFile.OpenFile(MainForm.pMainForm.ADPathInfo);
                        }
                        break;
                    }
                case 0xEDED:
                    {
                        ch1AdFile.close();
                        ch2AdFile.close();
                        ch3AdFile.close();
                        ch4AdFile.close();

                    }
                    break;
                case 0xBB01:
                    {
                        if(!hasRecv)
                        {
                            WaveFile.OpenFile(MainForm.pMainForm.NetRecvDataPathInfo);
                            hasRecv = true;
                        }
                        WaveFile.BinaryWrite(data);
                        if (WaveFile.FileLen > 1024 * 1024 * 100)
                        {
                            WaveFile.close();
                            WaveFile.OpenFile(MainForm.pMainForm.NetRecvDataPathInfo);
                        }

                        WaveEventHandler handler = WaveDisplEvent;
                        EventsClass.WaveEventArgs e = new EventsClass.WaveEventArgs(data, length);
                        if (handler != null)
                        {
                            handler(this, e);
                        }
                        break; 
                    }

                case 0xEE01:
                    {
                        NetTCPFile.OpenFile(MainForm.pMainForm.NetRecvTCPPathInfo);
                        string filename = NetTCPFile.adfile.fileName;
                        NetTCPFile.BinaryWrite(data);
                        NetTCPFile.close();
                        try
                        {
                            MainForm.ParseLock.WaitOne();
                            SourceDataClass.GetData(data);
                            SourceDataClass.Parse();
                            MainForm.ParseLock.ReleaseMutex();
                        }
                        catch (Exception ex)
                        {

                            MainForm.ParseLock.ReleaseMutex();
                        }
                        DataEventHandler handler = NetDataEvent;
                        EventsClass.DataEventArgs e = new EventsClass.DataEventArgs(Dclient.Client.RemoteEndPoint.ToString().Split(':')[0], data, length, filename);
                        if (handler != null)
                        {
                            handler(this, e);
                        }
                        break;
                    }
                case 0xACAC:
                    {
                        ACPacketHandle.Set();
                        //Debug.WriteLine("收到包号：" + BitConverter.ToUInt16(netpacket,4));
                        break;
                    }
                    //4500test
                case 0x45FF:
                {
                    var cmd = new byte[260];
                    byte[] netcmd = SourceDataClass.NetPackage(cmd);
                    MainForm.pMainForm.CommandLineWin.SendCommand("gd -n");
                    if (MainForm.pMainForm.CommandLineWin.SendData(netcmd))//正确发送
                    {
                        MainForm.pMainForm.mapdoc.WriteNetLog("数据已发送！");

                    }
                    break;
                }
                default:
                    
                    break;
            }
        }
        #endregion

        private void CommAnsReceiver_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {
                while ((Tclient.Connected) && (!worker.CancellationPending))
                {
                    byte[] myReadBuffer = new byte[2048];
                    StringBuilder myCompleteMessage = new StringBuilder();
                    int numberOfBytesRead = 0;

                    // Incoming message may be larger than the buffer size.
                    do
                    {
                        numberOfBytesRead = Tstream.Read(myReadBuffer, 0, myReadBuffer.Length);

                        myCompleteMessage.AppendFormat("{0}", Encoding.Default.GetString(myReadBuffer, 0, numberOfBytesRead));
                    }
                    while (Tstream.DataAvailable);
                    AddtoBox(Color.Black, myCompleteMessage.ToString());

                }
                if (worker.CancellationPending)
                    e.Cancel = true;
            }
            catch (SocketException MyEx)
            {
                if (bConnect)
                {
                    e.Result = MyEx.ErrorCode;
                    AddtoBox(Color.Black, MyEx.Message + ":" + MyEx.StackTrace.ToString() + "\r\n/>");
                    bConnect = false;
                    return;
                }

            }
            catch (IOException IOEx)
            {
                bConnect = false;
                //AddtoBox(Color.Black,"IO错误!\r\n/>");
                //SendStatusLabel("IO错误！网络连接关闭");
            }
            
        }

        private void CommAnsReceiver_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (e.Cancelled)
            {
                ConnNodeBtn.Text = "连接节点";
                bConnect = false;
                AddtoBox(Color.Black, "Command ans receive Cancelled!\r\n/>");
            }
            else if (!bConnect)
            {
                ConnNodeBtn.Text = "连接节点";
                Tclient.Close();
                Dclient.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int heigth = this.Height;
            UtilityClass.hide_show(this, ref heigth, timer1);
            
            if (HbClient!=null&&HbClient.Client!=null&&HbClient.Connected&&Hbstream!=null)
            {
                var command = BitConverter.GetBytes(0xFE01);
                try
                {
                    Hbstream.Write(command, 0, command.Length);
                }
                catch(Exception)
                {
                    HbClient.Close();
                }
                
            }

        }

        private void CommLineForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            ch1AdFile.close();
            ch2AdFile.close();
            ch3AdFile.close();
            ch4AdFile.close();
            WaveFile.close();
            NetTCPFile.close();
            NetUDPFile.close();
            NetLogFile.close();
        }

        private void AlwaysOnFront_CheckedChanged(object sender, EventArgs e)
        {
            if (AlwaysOnFront.Checked)
                this.TopMost = true;
            else
                this.TopMost = false;
        }

    }
    

    public class networkdata
    {
        public int DataType;//0：命令，1：AD，2：下载
        public string FileNamePath;//读取或存储的文件路径
    }
    //
}
