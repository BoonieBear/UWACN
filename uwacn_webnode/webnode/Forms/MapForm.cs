using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using DevComponents.DotNetBar;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using webnode.Helper;
using webnode.MapCustom;
using webnode.Properties;

namespace webnode.Forms
{
    
    public partial class MapForm : Office2007Form
    {
        // marker
        public UserMarker NodeMarker;
        GMapMarker center;
        string xmldoc ;
        // polygons
        GMapPolygon polygon;
        PointLatLng gtl;
        PointLatLng gbr;
        
        //地图覆盖层
        readonly GMapOverlay mainOverlay;
        readonly GMapImage clouds;

        // layers
        internal GMapOverlay DistanceInfo;
        internal GMapOverlay objects;
        //internal GMapOverlay routes;
        internal GMapOverlay Net;
        internal GMapOverlay track;
        internal GMapOverlay rulers;
        internal GMapOverlay infolayers;
        internal GMapOverlay WebNodeLayer;

        // etc
        [Serializable]
        public class initobject
        {
            public Hashtable grid;//路由
            public Hashtable net;//网络
            public Hashtable dist;//网络表距离
            public Hashtable nodeinfo;//节点信息
        }
        DepthInfoClass depthinfo = new DepthInfoClass();
        initobject initpara = new initobject();
        public Hashtable DistMap = new Hashtable();
        public Hashtable nodeinfoMap = new Hashtable();
        public Hashtable NodeRouteMap = new Hashtable();//节点路由表
        public Hashtable NodeNetMap = new Hashtable();//节点领接表，在表中有对应值的说明是领接节点
        readonly Random rnd = new Random();
        public DataViewForm Dvf = new DataViewForm();
        public PingForm pf = new PingForm();
        GMapMarkerRect CurentRectMarker = null;
        GMapMarker CurrentMarker = null;
        GMapMarker ModifyMarker = null;
        GMapMarker DistanceMarker = null;
        string mobileGpsLog = string.Empty;
        bool isMouseDown = false;
        public bool isRulerDown = false;
        bool isOnMarker = false;
        bool isStartDown = false;
        bool isNodeShown = true;
        bool isInfoClick = false;
        bool isShowAni = true;
        bool isUDPrun = true;
        public bool isNodeChosePlace = false;
        public bool isModify = false;
        PointLatLng start;
        PointLatLng end;
        Graphics g;
        int factor = 1;//动画的比例因子，如果是正常时间为1，速度加快几倍就是几
        string nodechoice = "";
        string debuglevel= "";
        public string BuoyID = "00";//预制浮标号
        Queue<string> DebugLog = new Queue<string>(1000);//网络广播接收队列
        NewNodeForm nn;
        public PointLatLng GmapToGpsOffset = new PointLatLng(-0.002649654980715, -0.00476212229727);
        public PointLatLng GpsToGmapOffset = new PointLatLng(0.002649654980715, 0.00476212229727);
        List<PointLatLng> gpstrack = new List<PointLatLng>();
        public float bearing = 0.0F;
        private int DataNumCount = 0;//从串口收到的数据记录数
        StreamReader playbacksr;//回放
        
        //udp队列同步基元
        private static Mutex mut = new Mutex();
        //nodeticks同步基元
        private static Mutex nodeticksmut = new Mutex();
        //网络命令行记录队列
        private static List<string> ComList = new List<string>();
        private static int ComListIndex = 0;//队列下标
         /// <summary>
        /// 动画显示委托
        delegate void DoAniCallback(Object Aniparameter);
        delegate void AddToDebugLogCallback(string str);
        delegate void ADDNodeCallback(XmlNode node);
        delegate void AddNodeCallback(string nodename, PointLatLng p, int type,float direction);
        ///记录数据个数数组
        int[] NodeTicks = new int[64];
        //每次收到数据后解析出的源节点列表
        public static List<int> SourceNode = new List<int>();
        /// 委托
        public delegate void TimeEventHandler(object sender, EventsClass.UtcTimeEventArgs e);//主窗口时间状态栏委托
        public event TimeEventHandler UtcTimeEvent;
        public delegate void GpsEventHandler(object sender, EventsClass.GpsEventArgs e);//GPS消息委托
        public static event GpsEventHandler GpsLogEvent;
        public delegate void latlngEventHandler(object sender, EventsClass.lnglatEventArgs e);//吊放位置改变委托
        public static event latlngEventHandler GpslatlngEvent;
        public delegate void MspEventHandler(object sender, EventsClass.WaveEventArgs e);//串口数据处理委托
        public static event MspEventHandler MspEvent;
        public delegate void MspLoaderEventHandler(object sender, EventsClass.GpsEventArgs e);//串口数据处理委托
        public static event MspLoaderEventHandler MspLoaderEvent;

        //消息窗口委托
        //gps窗口显示
        delegate void GpsBoxCallback(object sender, EventsClass.GpsEventArgs e);
        //将信源信息加入显示窗
        delegate void AddBoxCallback(string s, string filename);
        //系统消息记录
        delegate void WriteLogCallback(string msg);
        //系统消息记录
        delegate void WriteCommLogCallback(string msg);
        //系统消息记录
        delegate void WriteNetLogCallback(string msg);
        //UDP窗口显示
        delegate void udpBoxCallback();
        //数据框计数显示
        delegate void SetRecvTimesCallback();
        //节点通信时间戳显示
        delegate void AddTimeMarkerCallback(int id,string strtime);
        //路由显示
        delegate void AddRouteCallback();
        /// <summary>
        /// 记录文件初始化
        /// </summary>
        static bool isfirst = false;
        static LogFile GpsLogFile = new LogFile("Gps");
        static LogFile SystemLog = new LogFile("SysLog");
        static LogFile SerialLog = new LogFile("MSPLog");
        static LogFile UDPLog = new LogFile("UDP");
        public static System.IO.Ports.SerialPort GpsSerialPort;
        public static System.IO.Ports.SerialPort MspSerialPort;
        public AdFile MSPCmdFile = new AdFile("MSPCmd");
        public AdFile MSPRecvDataFile = new AdFile("MSPPackageData");
        public AdFile MSPDataFile = new AdFile("MSPRecvData");
        Thread readThread;
        static Thread readMspThread;
        static Thread readMspLoaderThread;
        public static bool isContinue = true;
        public static bool isMspContinue = true;
        public static bool IsLoaderRunning = true;
        public string comm = "COM4";
        public int baudrate = 9600;
        public string MyExecPath;//程序执行目录
        UdpClient receivingUdpClient;//调试UDP
        UdpClient DataUdpClient;//数据UDP
        public UdpClient OutDataudpClient;//往外广播数据
        int outputport = 10020;//广播数据端口
        //记录消息类型
        public enum MsgMode
        {
            RecvSerialData = 0,
            SerialCmd = 1,
            RecvNetData = 2,
            NetCmd = 3,
        }
      
        //系统变量赋值
        public MapForm()
        {
            InitializeComponent();
            Array.Clear(NodeTicks,0,64);

            mainOverlay = new GMapOverlay(MainMap, "map");
            MainMap.Overlays.Add(mainOverlay);
           
            //routes = new GMapOverlay(MainMap, "routes");
            //MainMap.Overlays.Add(routes);

            Net = new GMapOverlay(MainMap, "net");
            MainMap.Overlays.Add(Net);

            track = new GMapOverlay(MainMap, "polygons");
            MainMap.Overlays.Add(track);

            objects = new GMapOverlay(MainMap, "objects");
            MainMap.Overlays.Add(objects);

            rulers = new GMapOverlay(MainMap, "rulers");
            MainMap.Overlays.Add(rulers);

            DistanceInfo = new GMapOverlay(MainMap, "DistanceInfo");
            MainMap.Overlays.Add(DistanceInfo);

            infolayers = new GMapOverlay(MainMap, "Info");
            MainMap.Overlays.Add(infolayers);

            WebNodeLayer = new GMapOverlay(MainMap, "webnode");
            MainMap.Overlays.Add(WebNodeLayer);
            //
            GpsSerialPort = new System.IO.Ports.SerialPort();
            GpsSerialPort.NewLine = "\r\n";
            MspSerialPort = new System.IO.Ports.SerialPort();
            MspSerialPort.NewLine = "END";
            MspSerialPort.Encoding = Encoding.UTF8;
            GMaps.Instance.UseRouteCache = true;
            GMaps.Instance.UseGeocoderCache = true;
            GMaps.Instance.UsePlacemarkCache = true;
            g = MainMap.CreateGraphics();
            MyExecPath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            xmldoc = MyExecPath + "\\" + "config.xml"; ;
        }

        /// <summary>
        /// 初始化系统配置信息
        /// </summary>
        private void InitialSystem()
        {
            //SourceDataClass.describstr = "信源数据包";
            //DSP信源数据
            SourceDataClass.WebId.Add(0, "结束标识");
            SourceDataClass.WebId.Add(1, "分段标识");
            SourceDataClass.WebId.Add(2, "节点信息表");
            SourceDataClass.WebId.Add(3, "邻节点表");
            SourceDataClass.WebId.Add(4, "网络表");
            SourceDataClass.WebId.Add(5, "网络简表");
            SourceDataClass.WebId.Add(6, "路由表");
            SourceDataClass.WebId.Add(7, "路径记录");
            SourceDataClass.WebId.Add(8, "路径安排");
            SourceDataClass.WebId.Add(9, "路径中断");
            SourceDataClass.WebId.Add(10, "转发失败");


            SourceDataClass.WebId.Add(61, "设备数据");
            SourceDataClass.WebId.Add(62, "设备状态");
            SourceDataClass.WebId.Add(63, "节点状态");
            SourceDataClass.WebId.Add(101, "回环测试");
            SourceDataClass.WebId.Add(102, "回环测试应答");
            SourceDataClass.WebId.Add(103, "索取节点信息");
            SourceDataClass.WebId.Add(104, "索取节点信息应答");
            SourceDataClass.WebId.Add(105, "索取节点信息表");
            SourceDataClass.WebId.Add(106, "索取节点信息表应答");
            SourceDataClass.WebId.Add(107, "索取网络表");
            SourceDataClass.WebId.Add(108, "索取网络表应答");
            SourceDataClass.WebId.Add(109, "索取网络简表");
            SourceDataClass.WebId.Add(110, "索取网络简表应答");
            SourceDataClass.WebId.Add(111, "索取节点邻节点表");
            SourceDataClass.WebId.Add(112, "索取节点邻节点表应答");
            SourceDataClass.WebId.Add(113, "索取节点路由表");
            SourceDataClass.WebId.Add(114, "索取节点路由表应答");
            SourceDataClass.WebId.Add(115, "索取节点设备数据");
            SourceDataClass.WebId.Add(116, "索取节点设备数据应答");
            SourceDataClass.WebId.Add(117, "索取节点设备状态");
            SourceDataClass.WebId.Add(118, "索取节点设备状态应答");
            SourceDataClass.WebId.Add(119, "设备参数设置命令");
            SourceDataClass.WebId.Add(120, "设备参数设置命令应答");
            SourceDataClass.WebId.Add(121, "索取通信机状态");
            SourceDataClass.WebId.Add(122, "索取通信机状态应答");

            //收发控制命令定义
            SourceDataClass.WebId.Add(140, "设备数据定时回传开关");
            SourceDataClass.WebId.Add(141, "收发自动调节开关");
            SourceDataClass.WebId.Add(142, "通信制式开关");

            //
            SourceDataClass.WebId.Add(200, "全网复位");
            //设备数据
            //SourceDataClass.DataId.Add(16, "SBE39_TD");
            //SourceDataClass.DataId.Add(17, "SBE37_CTD");
            //SourceDataClass.DataId.Add(18, "设备1数据类型");
            //SourceDataClass.DataId.Add(19, "设备2数据类型");
            //SourceDataClass.DataId.Add(20, "设备3数据类型");
            //SourceDataClass.DataId.Add(21, "设备4数据类型");
            //SourceDataClass.DataId.Add(22, "设备5数据类型");
            //SourceDataClass.DataId.Add(23, "设备6数据类型");
            //设备数据ID由DeviceAddr枚举类型确定
            //DSP网络调试数据

            SourceDataClass.DataId.Add(201, "网络路由命令");
            SourceDataClass.DataId.Add(202, "网络路由命令");
            SourceDataClass.DataId.Add(203, "网络路由命令");
            SourceDataClass.DataId.Add(204, "网络路由命令");
            SourceDataClass.DataId.Add(205, "网络路由命令");
            SourceDataClass.DataId.Add(206, "扩展网络路由命令");

            //MSP特殊命令
            SourceDataClass.DataId.Add(255, "设置AD门限");
            SourceDataClass.DataId.Add(254, "将430能量数据写入FLASH");
            SourceDataClass.DataId.Add(253, "串口2、3，GPS，DSP配置命令");
            SourceDataClass.DataId.Add(252, "430校时命令");
            SourceDataClass.DataId.Add(251, "430休眠命令");
            SourceDataClass.DataId.Add(250, "调试状态，DSP开网络调试");
            SourceDataClass.DataId.Add(249, "430上电复位，为loader做准备");
            SourceDataClass.DataId.Add(248, "设置串口2和3定时唤醒时间");
            SourceDataClass.DataId.Add(247, "读取430实时状态");
            SourceDataClass.DataId.Add(246, "DSP喂狗开关");
            SourceDataClass.DataId.Add(245, "关DSP");
            SourceDataClass.DataId.Add(244, "给DSP上电");
            SourceDataClass.DataId.Add(243, "清零单片机重启次数");
            SourceDataClass.DataId.Add(242, "DSP进入Loader模式");

            //MSP上传数据命令
            SourceDataClass.DataId.Add(2, "DSP故障命令");
            SourceDataClass.DataId.Add(6, "上位机发布430休眠命令");
            SourceDataClass.DataId.Add(7, "上位机发布DSP关机命令");
            SourceDataClass.DataId.Add(8, "上位机读取通信机工作状态");
            SourceDataClass.DataId.Add(9, "通信机芯漏水命令");
            SourceDataClass.DataId.Add(10, "通信机电量低报警");
            SourceDataClass.DataId.Add(11, "温度过高报警命令");
            SourceDataClass.DataId.Add(12, "返回430工作状态数据");
            SourceDataClass.DataId.Add(13, "上位机读取430程序版本信息");
            SourceDataClass.DataId.Add(14, "返回430程序版本信息");
            SourceDataClass.DataId.Add(15, "休眠时间错误命令");
            SourceDataClass.DataId.Add(16, "浮标工作状态命令");
            SourceDataClass.DataId.Add(17, "DSP进入loader模式");
            SourceDataClass.DataId.Add(18, "DSP进入调试模式");
            SourceDataClass.DataId.Add(19, "关闭DSP");
            SourceDataClass.DataId.Add(20, "读取MSP430状态");
            SourceDataClass.DataId.Add(170, "DSP回传数据命令");
            SourceDataClass.DataId.Add(171, "上位机下达DSP命令");
 

            MainMap.Manager.Mode = AccessMode.ServerAndCache;
            double latoffset =0;
            double lngoffset =0;
            string[] visionstr = { "地图配置", "视图视角" };
            double v = double.Parse(XmlHelper.GetConfigValue(xmldoc, visionstr));
            string[] offsetstr = { "地图配置", "偏移校准", "纬度" };
            string[] offsetstr1 = { "地图配置", "偏移校准", "经度" };
            string offset = XmlHelper.GetConfigValue(xmldoc, offsetstr);
            if (offset != null)
            {
                latoffset = double.Parse(offset);
            }
            offset = XmlHelper.GetConfigValue(xmldoc, offsetstr1);
            if (offset != null)
            {
                lngoffset = double.Parse(offset);
            }
            if ((lngoffset != 0) && (latoffset != 0))
            {
                GmapToGpsOffset = new PointLatLng(latoffset, lngoffset);
                GpsToGmapOffset = new PointLatLng(-latoffset, -lngoffset);
            }
            else
            {
                MessageBox.Show("无法读入地图偏移值！使用默认偏移。");
            }
            // config map 
            string[] str = { "地图配置", "地图中心", "纬度" };
            double lat = double.Parse(XmlHelper.GetConfigValue(xmldoc, str));
            string[] lngstr = { "地图配置", "地图中心", "经度" };
            double lng = double.Parse(XmlHelper.GetConfigValue(xmldoc, lngstr));
            PointLatLng p = new PointLatLng(lat, lng);
            // set node marker
            显示节点计数ToolStripMenuItem.Checked = true;
            p.Offset(GpsToGmapOffset);
            // map center
            center = new GMapMarkerCross(MainMap.Position);
            objects.Markers.Add(center);
            MainMap.Position = p;
            NodeMarker = new UserMarker(p);
            NodeVisibleCheck.Checked = NodeMarker.IsVisible;
            NodeMarker.IsHitTestVisible = true;
            objects.Markers.Add(NodeMarker);
            NodeMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            CultureInfo ci = new CultureInfo("en-us");
            PointLatLng gpspos = NodeMarker.Position;
            gpspos.Offset(GmapToGpsOffset);
            NodeMarker.ToolTipText = "GPS\r\n{经度=" + gpspos.Lng.ToString("F08", ci) + "，纬度=" + gpspos.Lat.ToString("F08", ci) + "}";
            

            MainMap.MapType = MapType.GoogleSatelliteChina;
            MainMap.MinZoom = 1;
            MainMap.MaxZoom = 18;
            MainMap.Zoom = 16;
            string[] mapname = { "地图配置", "地图名称" };
            MainMap.MapName = XmlHelper.GetConfigValue(xmldoc, mapname);
            // acccess mode
            comboBoxMode.Items.AddRange(Enum.GetNames(typeof(AccessMode)));
            comboBoxMode.SelectedItem = Enum.GetName(typeof(AccessMode), GMaps.Instance.Mode);
            //MapType
            MapTypeBox.Items.AddRange(Enum.GetNames(typeof(MapType)));

            MapTypeBox.SelectedItem = Enum.GetName(typeof(MapType), MainMap.MapType);
            gtl = new PointLatLng(lat + v, lng - v);
            gtl.Offset(GpsToGmapOffset);
            gbr = new PointLatLng(lat - v, lng + v);
            gbr.Offset(GpsToGmapOffset);

            string[] strport = { "广播端口" };
            string port = XmlHelper.GetConfigValue(xmldoc, strport);
            if(port!=null)
                outputport = int.Parse(port);
            
            WebVisibleCheck.Checked = isNodeShown;
            AnimCheck.Checked = isShowAni;
            //读取节点
            XmlDocument xmlfile = new XmlDocument();
            xmlfile.Load(xmldoc);
            XmlNode xn = xmlfile.DocumentElement;
            xn = xn.SelectSingleNode("descendant::节点配置");
            foreach (XmlNode subnode in xn.ChildNodes)
            {
                AddNodeToMap(subnode);
            }
            ReadInit();
            AddNet();
            //AddRoutes();
            //routes.IsVisibile = false;
            Net.IsVisibile = false;
            try
            {
                receivingUdpClient = new UdpClient(8082);//调试UDP
                DataUdpClient = new UdpClient(8083);//数据UDP
                OutDataudpClient = new UdpClient();//数据广播UDP
                //打开udp监听端口
                Thread UdpReceiver = new Thread(listensenUDP);
                UdpReceiver.Start();
                Thread UdpDataReceiver = new Thread(listensenUDPData);
                UdpDataReceiver.Start();
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            
            
        }
        public void SendData(string str)
        {
            Byte[] sendBytes = Encoding.Default.GetBytes(str);
            try
            {
                OutDataudpClient.Send(sendBytes, sendBytes.Length, "255.255.255.255", outputport);
            }
            catch (Exception e)
            {
                WriteNetLog(e.ToString());
            }

 
        }
        private void listensenUDPData()
        {
            isUDPrun = true;
            WriteNetLog("开始接收网络数据信息\r\n");
            Byte[] buffer = new byte[4096];
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while (isUDPrun)
            {
                try
                {
                    int numberOfBytesRead = 0;
                    Array.Clear(buffer, 0, 4096);
                    Byte[] receiveBytes = DataUdpClient.Receive(ref RemoteIpEndPoint);
                    if (BitConverter.ToUInt16(receiveBytes, 0) != 0xEE01)
                        continue;
                    if (receiveBytes.Length < 4)
                        continue;
                    UInt16 PacketLength = BitConverter.ToUInt16(receiveBytes, 2);
                    
                    Array.Copy(receiveBytes, 4,buffer,numberOfBytesRead, receiveBytes.Length-4);
                    numberOfBytesRead = receiveBytes.Length-4;
                    // Incoming message may be larger than the buffer size.
                    while (numberOfBytesRead < PacketLength)
                    {
                        receiveBytes = DataUdpClient.Receive(ref RemoteIpEndPoint);
                        Array.Copy(receiveBytes, 0,buffer,numberOfBytesRead, receiveBytes.Length);
                        numberOfBytesRead += receiveBytes.Length;

                    }
                    byte[] data = new byte[PacketLength];
                    Array.Copy(buffer, data, PacketLength);
                    MainForm.pMainForm.CommandLineWin.NetDataFile.OpenFile(MainForm.pMainForm.NetRecvDataPathInfo);
                    string filename = MainForm.pMainForm.CommandLineWin.NetDataFile.adfile.fileName;
                    MainForm.pMainForm.CommandLineWin.NetDataFile.BinaryWrite(data);
                    MainForm.pMainForm.CommandLineWin.NetDataFile.close();
                    try
                    {
                        MainForm.ParseLock.WaitOne();
                        SourceDataClass.GetData(data);
                        List<string[]> ss = SourceDataClass.Parse();
                        MainForm.ParseLock.ReleaseMutex();
                    }
                    catch (Exception ex)
                    {

                        MainForm.ParseLock.ReleaseMutex();
                    }
                    
                    AddtoList(RemoteIpEndPoint.ToString().Split(':')[0], filename);
                }
                catch (SocketException e)
                {
                    string error = e.Message;
                    if (e.ErrorCode != 0x2714)//程序关闭
                        WriteNetLog("\r\n停止接收调试信息:" + error);
                    isUDPrun = false;
                }
            }
        }
        private void listensenUDP()
        {
            isUDPrun = true;
            WriteNetLog("开始接收网络调试信息\r\n");
            
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while (isUDPrun)
            {
                try
                {
                    Byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);

                    string returnData = Encoding.Default.GetString(receiveBytes);
                    if (UDPLog.logfile.ws == null)//还未创建文件
                        UDPLog.OpenFile(MainForm.pMainForm.RecordInfo);
                    UDPLog.logfile.ws.WriteLine(returnData);//不记录时间，调试信息中有
                    if (UDPLog.length >= 1024 * 1024)//不允许大于1024K
                    {
                        UDPLog.close();
                    }
                    mut.WaitOne();
                    DebugLog.Enqueue(returnData);
                    if (DebugLog.Count > 1000)
                        DebugLog.Dequeue();
                    mut.ReleaseMutex();
                    //RefreshUDPLog();
                    string nodename = this.nodechoice;
                    nodename = nodename.TrimStart('节','点');
                    if (nodename != "")
                        nodename = string.Concat("[", nodename, "]");
                    string[] debuglevel = this.debuglevel.Split('&');
                    for (int i = 0; i < debuglevel.Length; i++)
                    {
                        if ((returnData.Contains(nodename)) && (returnData.Contains(debuglevel[i])))
                        {
                            AddToDebugBox(returnData);

                        }
                    }
                    if (isShowAni)//显示动画
                    {
                        if((returnData.Contains("网络监控"))&&(!returnData.Contains("未知")))
                            ShowNetActive(returnData);
                    }

                }
                catch (SocketException e)
                {
                    string error = e.Message;
                    if(e.ErrorCode !=0x2714 )//程序关闭
                        WriteNetLog("\r\n停止接收调试信息:" + error);
                    isUDPrun = false;
                }
            }
        }
        
        //
        private void MapForm_Load(object sender, EventArgs e)
        {
            
            UtcTimeEvent += new TimeEventHandler(MainForm.pMainForm.ShowUtcTime);
            GpsLogEvent += new GpsEventHandler(GPSForm_GpsLogEvent);
            GpslatlngEvent += new latlngEventHandler(GetGpsData);
            MspEvent += new MspEventHandler(MspRecvEvent);
            MspLoaderEvent += new MspLoaderEventHandler(MspLoaderRecvEvent);
            InitialSystem();//初始化完后再注册事件，否则出现意想不到的问题，比如边加节点边删除路由
            
            //routes.Routes.CollectionChanged += new GMap.NET.ObjectModel.NotifyCollectionChangedEventHandler(Routes_CollectionChanged);
            WebNodeLayer.Markers.CollectionChanged += new GMap.NET.ObjectModel.NotifyCollectionChangedEventHandler(Markers_CollectionChanged);
            
            //CircleEmitAnimation(ref MainMap, NodeMarker.Position, 10000);
            //PointLatLng p = NodeMarker.Position;
            //p.Offset(GpsToGmapOffset);
            //LineEmitAnimation(ref MainMap, NodeMarker.Position, p, 10000);
        }

        #region 系统功能实现，添加节点到地图，将节点保存进配置文件，实时得到gps数据，地图内容变化响应
        private void AutoSaveImg()
        {
            if (自动保存截图ToolStripMenuItem.Checked)
            {
                string timestring = DateTime.Now.Year.ToString("0000", CultureInfo.InvariantCulture) + DateTime.Now.Month.ToString("00", CultureInfo.InvariantCulture) + DateTime.Now.Day.ToString("00", CultureInfo.InvariantCulture) + DateTime.Now.Hour.ToString("00", CultureInfo.InvariantCulture)
                    + DateTime.Now.Minute.ToString("00", CultureInfo.InvariantCulture) + DateTime.Now.Second.ToString("00", CultureInfo.InvariantCulture);

                string filename = MainForm.pMainForm.ImgInfo.FullName;//文件路径
                filename = filename + "\\" + "Img" + timestring + ".png";
                Image tmpImage = MainMap.ToImage();
                if (tmpImage != null)
                {
                    using (tmpImage)
                    {

                        tmpImage.Save(filename);
                        //Debug.WriteLine("Save a Map!");
                    }
                }
            }
        }
        /// <summary>
        /// 给节点加上上一次通信的时间戳
        /// </summary>
        public void AddLastTimeMarker(int id, string strtime)
        {
            if (!显示节点计数ToolStripMenuItem.Checked)
                return;
            if (MainMap.InvokeRequired)//不是同一线程调用，调用控件的委托
            {
                AddTimeMarkerCallback d = new AddTimeMarkerCallback(AddLastTimeMarker);
                this.Invoke(d, new object[] { id, strtime });

            }
            else//同一线程，直接操作
            {
                string nodename = "节点" + id.ToString();
                nodeticksmut.WaitOne();
                AddNodeTicks(id);
                string ticks = NodeTicks[id].ToString();
                nodeticksmut.ReleaseMutex();
                foreach (GMapMarker g in WebNodeLayer.Markers)
                {
                    if (g.Tag.ToString() == nodename)//有同名的节点
                    {
                        string[] desc = { "节点配置", nodename, "节点描述" };
                        string describe = XmlHelper.GetConfigValue(xmldoc, desc);

                        g.ToolTipText = nodename + "\r\n" + describe + "\r\n" + strtime.Remove(0, 5) + "(" + ticks + ")";
                    }
                }
               

            }
        }

        /// <summary>
        /// 根据回传数据id给每个节点计数，
        /// </summary>
        /// <param name="id"></param>
        private void AddNodeTicks(int id)
        {

            int d = (int)NodeTicks[id];
            d++;
            NodeTicks[id] = d;
           
            
        }
        /// <summary>
        /// 根据网络监控信息显示节点间动作，主要功能为解析调试语句，调用动画线程。
        /// </summary>
        /// <param name="debuginfo">debuginfo:[010][30][2012-12-18 11:32:05:909]: 网络监控：[ACK-0][12-13-07]</param>
        private void ShowNetActive(string debuginfo)
        {
            try
            {
                string[] splitstr = { "[", "]", "][" };
                string[] str = debuginfo.Split(splitstr, StringSplitOptions.RemoveEmptyEntries);
                DateTime d = Convert.ToDateTime(str[2].Remove(19));
                string text = str[4];
                if (text == "ACK-1")
                    text = "ACK Error!";
                string[] node = str[5].Split('-');
                string startnode = "节点" + int.Parse(node[1]).ToString();//先转int型再转string去掉01前的0
                
                string endnode = "节点" + int.Parse(node[0]).ToString();
                text += "[" + node[2] + "]";
                if (endnode == "节点0")//广播
                {
                    
                    //PointLatLng startp = GetNodeGmapPosition(startnode);
                    
                    int time = 3000*1000 / (1500 * factor);
                    Ping(startnode, time, "PING");
                }
                else
                {
                    PointLatLng startp = GetNodeGmapPosition(startnode);
                    PointLatLng endp = GetNodeGmapPosition(endnode);
                    if ((startp != PointLatLng.Zero) && (endp != PointLatLng.Zero))
                    {
                        int time = (int)(UtilityClass.CalcDistance(endp, startp) * 1000 / (1500 * factor));
                        LineEmitAnimation(ref MainMap, startp, endp, time, text);
                    }
                   
                }
                    
                
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }

        }
        /// <summary>
        /// 得到节点的GPS坐标，未偏移的
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public PointLatLng GetNodeGPSPosition(string node)
        {
            /*
            string[] str = { "节点配置", node, "节点位置", "纬度" };
            double lat = double.Parse(XmlHelper.GetConfigValue(xmldoc, str));
            string[] lngstr = { "节点配置", node, "节点位置", "经度" };
            double lng = double.Parse(XmlHelper.GetConfigValue(xmldoc, lngstr));
            PointLatLng p = new PointLatLng(lat, lng);
            //p.Offset(GpsToGmapOffset);
            return p;
            */
            foreach (GMapMarker g in WebNodeLayer.Markers)
            {
                if (g.Tag.ToString() == node)//有同名的节点
                {
                    PointLatLng p = g.Position;
                    p.Offset(GmapToGpsOffset);
                    return p;
                }
                
            }

            return PointLatLng.Zero;//一般不可能，因为调用程序前应判断是否存在节点
        }
        /// <summary>
        /// 得到节点的Gmap坐标
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private PointLatLng GetNodeGmapPosition(string node)
        {
            foreach (GMapMarker g in WebNodeLayer.Markers)
            {
                if (g.Tag.ToString() == node)//有同名的节点
                {
                    PointLatLng p = g.Position;
                    return p;
                }

            }
            return PointLatLng.Zero;//一般不可能，因为调用程序前应判断是否存在节点
        }
        private void AddToDebugBox(string str)
        {
            if (NetDebugLog.InvokeRequired)//与控件不在同一线程，委托操作
            {
                AddToDebugLogCallback d = new AddToDebugLogCallback(AddToDebugBox);
                this.Invoke(d, new object[] { str });
            }
            else
            {
                NetDebugLog.Items.Add(str);
                NetDebugLog.SelectedIndex = NetDebugLog.Items.Count - 1;
            }
        }


        //保存初始化数据
        public void SaveInit()
        {
            string gridname = MyExecPath + "\\" + "default.conf";
            Stream stream = new FileStream(gridname, FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                initpara.dist = DistMap;
                initpara.grid = NodeRouteMap;
                initpara.net = NodeNetMap;
                initpara.nodeinfo = nodeinfoMap;
                
                formatter.Serialize(stream, initpara);
                stream.Close();
            }
            catch (Exception MyEx)
            {
                stream.Close();
                MessageBox.Show("保存初始化数据出错！\n" + MyEx.Message);
            }
        }
        //读取初始化表
        private void ReadInit()
        {
            string gridname = MyExecPath + "\\" + "default.conf";
            Stream stream= null;
            try
            {
                stream = new FileStream(gridname, FileMode.Open, FileAccess.Read, FileShare.Read);
                IFormatter formatter = new BinaryFormatter();
                initpara = (initobject)formatter.Deserialize(stream);
                stream.Close();
                DistMap = initpara.dist;
                NodeRouteMap = initpara.grid ;
                NodeNetMap = initpara.net;
                nodeinfoMap = initpara.nodeinfo ;

            }
            catch (Exception MyEx)
            {
                if(stream!=null)
                    stream.Close();
                MessageBox.Show("载入初始化表出错！\n" + MyEx.Message);
            }
        }
        
        public void AddNodeToMap(XmlNode node)
        {
            if (MainMap.InvokeRequired)
            {
                ADDNodeCallback d = new ADDNodeCallback(AddNodeToMap);
                this.Invoke(d,new object[]{node});
            }
            else
            {
                string[] str = { "节点配置", node.Name, "节点位置", "纬度" };
                double lat = double.Parse(XmlHelper.GetConfigValue(xmldoc, str));
                string[] lngstr = { "节点配置", node.Name, "节点位置", "经度" };
                double lng = double.Parse(XmlHelper.GetConfigValue(xmldoc, lngstr));
                string[] desc = { "节点配置", node.Name, "节点描述" };
                string describe = XmlHelper.GetConfigValue(xmldoc, desc);
                PointLatLng p = new PointLatLng(lat, lng);
                p.Offset(GpsToGmapOffset);
                GMapMarkerGoogleGreen newnode = new GMapMarkerGoogleGreen(p);
                newnode.Tag = node.Name;
                newnode.IsHitTestVisible = true;
                newnode.ToolTipMode = WebVisibleCheck.Checked ? MarkerTooltipMode.Always : MarkerTooltipMode.OnMouseOver;
                p.Offset(GmapToGpsOffset);
                newnode.ToolTipText = newnode.Tag.ToString() + "\r\n" + describe + "\r\n"; //+ "{经度=" + p.Lng.ToString("F08", CultureInfo.InvariantCulture) + "，纬度=" + p.Lat.ToString("F08", CultureInfo.InvariantCulture) + "}";
                WebNodeLayer.Markers.Add(newnode);
            }
        }

        /// <summary>
        /// 根据位置和类型添加节点到地图上
        /// </summary>
        /// <param name="nodename">节点名</param>
        /// <param name="p">位置gps</param>
        /// <param name="type">节点类型，移动or静止</param>
        public void AddNewNodeToMap(string nodename,PointLatLng p,int type,float direction)
        {
            if (MainMap.InvokeRequired)
            {
                AddNodeCallback d = new AddNodeCallback(AddNewNodeToMap);
                this.Invoke(d, new object[] { nodename,p,type,direction});
            }
            else
            {
                bool HasNode = false;
                p.Offset(GpsToGmapOffset);
                foreach (GMapMarker g in WebNodeLayer.Markers)
                {
                    if (g.Tag.ToString() == nodename)//有同名的节点
                        HasNode = true;
                    
                }
                if (HasNode)//已存在节点
                {
                    MoveNode(nodename, p, direction);
                    if(direction>=0)//AUV
                        Ping(nodename, 1000, "AUV");
                }
                else//新节点
                {
                    if (type == 1)//移动节点
                    {
                        GMapMarkerGoogleRed newnode = new GMapMarkerGoogleRed(p);
                        newnode.Tag = nodename;
                        newnode.Position = p;
                        if (direction >= 0)//AUV
                            newnode.Bearing = direction;
                        newnode.IsHitTestVisible = true;
                        newnode.ToolTipMode = WebVisibleCheck.Checked ? MarkerTooltipMode.Always : MarkerTooltipMode.OnMouseOver;
                        p.Offset(GmapToGpsOffset);
                        newnode.ToolTipText = newnode.Tag.ToString() + "\r\n" + "AUV" + "\r\n" + "{经度=" + p.Lng.ToString("F08", CultureInfo.InvariantCulture) + "，纬度=" + p.Lat.ToString("F08", CultureInfo.InvariantCulture) + "}";
                        WebNodeLayer.Markers.Add(newnode);
                        if (direction >= 0)//AUV
                            Ping(nodename, 3000, "AUV");
                    }
                    else//静态
                    {
                        GMapMarkerGoogleGreen newnode = new GMapMarkerGoogleGreen(p);
                        newnode.Tag = nodename;
                        newnode.IsHitTestVisible = true;
                        newnode.ToolTipMode = WebVisibleCheck.Checked ? MarkerTooltipMode.Always : MarkerTooltipMode.OnMouseOver;
                        p.Offset(GmapToGpsOffset);
                        newnode.ToolTipText = newnode.Tag.ToString() + "\r\n" + "新增节点" + "\r\n";
                        WebNodeLayer.Markers.Add(newnode);
                    }
                }
            }
        }

        /// <summary>
        /// 在地图上移动节点
        /// </summary>
        /// <param name="nodename"></param>
        /// <param name="p">地图上的坐标gmap</param>
        public void MoveNode(string nodename, PointLatLng p, float direction)
        {
            
            
            foreach (GMapMarker g in WebNodeLayer.Markers)
            {
                if (g.Tag.ToString() == nodename)//有同名的节点
                {
                    g.Position = p;//移动节点位置
                    if (direction >= 0)
                    {
                        if (g.GetType() == typeof(GMapMarkerGoogleRed))
                        ((GMapMarkerGoogleRed)g).Bearing = direction;
                        if (g.GetType() == typeof(GMapMarkerGoogleGreen))
                            ((GMapMarkerGoogleGreen)g).Bearing = direction;
                    }
                    p.Offset(GmapToGpsOffset);
                    g.ToolTipText = g.Tag.ToString() + "\r\n" + "{经度=" + p.Lng.ToString("F08", CultureInfo.InvariantCulture) + "，纬度=" + p.Lat.ToString("F08", CultureInfo.InvariantCulture) + "}";
                }
            
                
            }
            
            
        }
        //得到gps数据
        public void GetGpsData(object sender, EventsClass.lnglatEventArgs e)
        {
            PointLatLng pt = new PointLatLng(e.position_lat, e.position_lng);
            pt.Offset(GpsToGmapOffset);
            if (gpstrack.Count == 0)
            {
                gpstrack.Add(pt);
            }
            if (gpstrack.Count > 500)
            {
                gpstrack.RemoveAt(0);
            }
            if (UtilityClass.CalcDistance(gpstrack[gpstrack.Count - 1], pt) > 10)
            {
                gpstrack.Add(pt);
                if (TrackCheck.Checked)
                {
                    track.Routes.Clear();
                    GMapRoute gr = new GMapRoute(gpstrack, "Track");
                    track.Routes.Add(gr);
                }
            }

            NodeMarker.Position = pt;
            NodeMarker.Bearing = e.direction_bearing;
            CultureInfo ci = new CultureInfo("en-us");
            pt.Offset(GmapToGpsOffset);
            NodeMarker.ToolTipText = "GPS\r\n{经度=" + pt.Lng.ToString("F08", ci) + "，纬度=" + pt.Lat.ToString("F08", ci) + "}";
            //MainMap.Refresh();
        }

        private void AddNodeToXml(GMapMarkerGoogleGreen newnode, string Desc,string ipaddress)
        {
            PointLatLng p = newnode.Position;
            p.Offset(GmapToGpsOffset);
            XmlDocument xmlfile = new XmlDocument();
            xmlfile.Load(xmldoc);
            XmlNode xn = xmlfile.DocumentElement;
            xn = xn.SelectSingleNode("descendant::节点配置");
            XmlElement lngxmlnode = xmlfile.CreateElement("经度");
            lngxmlnode.InnerText = p.Lng.ToString("F08");
            XmlElement latxmlnode = xmlfile.CreateElement("纬度");
            latxmlnode.InnerText = p.Lat.ToString("F08");
            XmlElement posxmlnode = xmlfile.CreateElement("节点位置");
            posxmlnode.AppendChild(lngxmlnode);
            posxmlnode.AppendChild(latxmlnode);
            XmlElement ipxmlnode = xmlfile.CreateElement("节点IP");
            ipxmlnode.InnerText = ipaddress;
            XmlElement netxmlnode = xmlfile.CreateElement("网络配置");
            netxmlnode.AppendChild(ipxmlnode);
            XmlElement descxmlnode = xmlfile.CreateElement("节点描述");
            descxmlnode.InnerText = Desc;
            XmlElement xmlnode = xmlfile.CreateElement(newnode.Tag.ToString());
            xmlnode.AppendChild(posxmlnode);
            xmlnode.AppendChild(descxmlnode);
            xmlnode.AppendChild(netxmlnode);
            xn.AppendChild(xmlnode);
            xmlfile.Save(xmldoc);

        }

        void Markers_CollectionChanged(object sender, GMap.NET.ObjectModel.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == GMap.NET.ObjectModel.NotifyCollectionChangedAction.Remove)
            {
                //节点集合改变，领接点表重新设置。
                
            }
        }

        void Routes_CollectionChanged(object sender, GMap.NET.ObjectModel.NotifyCollectionChangedEventArgs e)
        {
            //AddRoutes();
        }
        #endregion

        #region 地图事件
        void MainMap_OnMarkerLeave(GMapMarker item)
        {
            isOnMarker = false;
            this.Cursor = Cursors.Default;
            if (item is GMapMarkerRect)
            {
                CurentRectMarker = null;

                GMapMarkerRect rc = item as GMapMarkerRect;
                rc.Pen.Color = Color.Blue;
                MainMap.Invalidate(false);

                Debug.WriteLine("OnMarkerLeave: " + item.Position);
            }
            if (item is GMapMarker)
            {
                CurrentMarker = null;
            }
        }

        void MainMap_OnMarkerEnter(GMapMarker item)
        {
            isOnMarker = true;
            CurrentMarker = item;
            this.Cursor = Cursors.Hand;
            //if (item is GMapMarkerRect)
            //{
            //    GMapMarkerRect rc = item as GMapMarkerRect;
            //    rc.Pen.Color = Color.Black;
            //    MainMap.Invalidate(false);

            //    CurentRectMarker = rc;

            //    Debug.WriteLine("OnMarkerEnter: " + item.Position);
            //}
        }

        void MainMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        void MainMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                if (isRulerDown)
                {
                    if (isStartDown)
                    {
                        end = MainMap.FromLocalToLatLng(e.X, e.Y);
                        double d = UtilityClass.CalcDistance(start, end);
                        //isRulerDown = false;
                        isStartDown = false;
                        
                        String drawString = "距离 = " + d.ToString("00", CultureInfo.InvariantCulture) + "m";
                        PointLatLng middle = new PointLatLng();
                        middle.Lat = start.Lat + (end.Lat - start.Lat) / 2;
                        middle.Lng = start.Lng + (end.Lng - start.Lng) / 2;
                        DistanceMarker = new GMapInfoBoard(middle, drawString);
                        DistanceMarker.DisableRegionCheck = false;
                        DistanceInfo.Markers.Add(DistanceMarker);
                        List<PointLatLng> track = new List<PointLatLng>();
                        track.Add(start);
                        track.Add(end);
                        GMapRoute Ruler = new GMapRoute(track, drawString);
                        rulers.Routes.Add(Ruler);
                        DistanceMarker.ToolTipMode = MarkerTooltipMode.Never;

                    }
                    else
                    {
                        start = MainMap.FromLocalToLatLng(e.X, e.Y);
                        isStartDown = true; ;
                    }
                }
                if (isInfoClick)
                {
                    InfoForm newinfo = new InfoForm();
                    if (newinfo.ShowDialog() == DialogResult.OK)
                    {
                        if (newinfo.infotext.Text.Length > 0)
                        {
                            GMapInfoBoard infoboard = new GMapInfoBoard(MainMap.FromLocalToLatLng(e.X, e.Y), newinfo.infotext.Text);
                            infolayers.Markers.Add(infoboard);
                        }
                        
                    }
                    isInfoClick = false;
                    this.Cursor = Cursors.Default;
                    newinfo.Dispose();
                }
                if ((isNodeChosePlace)&&(!isOnMarker))
                {
                    if (isModify)
                    {
                        PointLatLng node = MainMap.FromLocalToLatLng(e.X, e.Y);
                        node.Offset(GmapToGpsOffset);
                        nn.LatBox.Value = node.Lat;
                        nn.LngBox.Value = node.Lng;
                        if (nn.ShowDialog(this) == DialogResult.OK)
                        {
                            isModify = false;
                            if (nn.isRight != true)
                            {
                                return;
                            }
                            XmlDocument xmlfile = new XmlDocument();
                            xmlfile.Load(xmldoc);
                            XmlNode xn = xmlfile.DocumentElement;
                            if (ModifyMarker == null)
                                return;
                            GMapMarker delnode = ModifyMarker;
                            xn = xn.SelectSingleNode("descendant::" + delnode.Tag.ToString());
                            XmlNode father = xmlfile.SelectSingleNode("descendant::节点配置");
                            father.RemoveChild(xn);
                            xmlfile.Save(xmldoc);

                            WebNodeLayer.Markers.Remove(ModifyMarker);
                            PointLatLng p = new PointLatLng(nn.LatBox.Value, nn.LngBox.Value);
                            p.Offset(GpsToGmapOffset);
                            GMapMarkerGoogleGreen newnode = new GMapMarkerGoogleGreen(p);
                            newnode.Tag = "节点" + nn.NodeNameBox.Text;
                            newnode.IsHitTestVisible = true;
                            newnode.ToolTipMode = WebVisibleCheck.Checked ? MarkerTooltipMode.Always : MarkerTooltipMode.OnMouseOver;
                            p.Offset(GmapToGpsOffset);
                            newnode.ToolTipText = newnode.Tag.ToString() + "\r\n" + nn.DescBox.Text;// +"\r\n" + "{经度=" + p.Lng.ToString("F08", CultureInfo.InvariantCulture) + "，纬度=" + p.Lat.ToString("F08", CultureInfo.InvariantCulture) + "}";
                            WebNodeLayer.Markers.Add(newnode);
                            string ip = nn.ipaddress.Text.Replace(" ", "");
                            try
                            {
                                if (ip == "")
                                {
                                    MessageBox.Show("节点IP自动设置为缺省值：192.168.2.100");
                                    ip = "192.168.2.100";
                                }
                                IPAddress ia = IPAddress.Parse(ip);
                                ip = ia.ToString();
                            }
                            catch (Exception eip)
                            {
                                MessageBox.Show(eip.ToString() + "\r\n" + "节点IP自动设置为缺省值：192.168.2.100");
                                ip = "192.168.2.100";

                            }
                            
                            AddNodeToXml(newnode, nn.DescBox.Text, ip);
                            isModify = false;
                            if (newnode.Tag.ToString() != delnode.Tag.ToString())//修改了节点名
                            {
                                string message = "改变节点集合后请重设路由";
                                string caption = "通知";
                                MessageBoxButtons buttons = MessageBoxButtons.OK;
                                MessageBoxIcon icon = MessageBoxIcon.Information;
                                MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button1;
                                // Show message box
                                DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);

                                NodeRouteMap.Clear();
                                NodeNetMap.Clear();
                                SaveInit();
                            }
                            AddNet();
                            //AddRoutes();
                        }
                        isModify = false;
                        ModifyMarker = null;
                    }
                    else
                    {
                        PointLatLng node = MainMap.FromLocalToLatLng(e.X, e.Y);
                        node.Offset(GmapToGpsOffset);
                        nn.LatBox.Value = node.Lat;
                        nn.LngBox.Value = node.Lng;

                        if (nn.ShowDialog(this) == DialogResult.OK)
                        {
                            if (nn.isRight != true)
                            {

                                return;
                            }
                            PointLatLng p = new PointLatLng(nn.LatBox.Value, nn.LngBox.Value);
                            p.Offset(GpsToGmapOffset);
                            GMapMarkerGoogleGreen newnode = new GMapMarkerGoogleGreen(p);
                            newnode.Tag = "节点" + nn.NodeNameBox.Text;
                            newnode.IsHitTestVisible = true;
                            newnode.ToolTipMode = WebVisibleCheck.Checked ? MarkerTooltipMode.Always : MarkerTooltipMode.OnMouseOver;
                            PointLatLng gpspos = newnode.Position;
                            gpspos.Offset(GmapToGpsOffset);
                            newnode.ToolTipText = newnode.Tag.ToString() + "\r\n" + nn.DescBox.Text;// +"\r\n" + "{经度=" + gpspos.Lng.ToString("F08", CultureInfo.InvariantCulture) + "，纬度=" + gpspos.Lat.ToString("F08", CultureInfo.InvariantCulture) + "}";
                            WebNodeLayer.Markers.Add(newnode);
                            string ip = nn.ipaddress.Text.Replace(" ", "");
                            try
                            {
                                if (ip == "")
                                {
                                    MessageBox.Show("节点IP自动设置为缺省值：192.168.2.100");
                                    ip = "192.168.2.100";
                                }
                                IPAddress ia = IPAddress.Parse(ip);
                                ip = ia.ToString();
                            }
                            catch (Exception eip)
                            {
                                MessageBox.Show(eip.ToString() + "\r\n" + "节点IP自动设置为缺省值：192.168.2.100");
                                ip = "192.168.2.100";

                            }
                            
                            AddNodeToXml(newnode, nn.DescBox.Text, ip);
                        }
                    }
                    isNodeChosePlace = false;
                }

            }
        }

        // move current marker with left holding
        void MainMap_MouseMove(object sender, MouseEventArgs e)
        {
            PointLatLng Gp = MainMap.FromLocalToLatLng(e.X, e.Y);
            Gp.Offset(GmapToGpsOffset);
            string str = Gp.ToString();
            float fd = depthinfo.GetDepth(Gp);
            if (fd > 0.001)
                str += "深度"+fd.ToString()+"米";
            MainForm.pMainForm.showlatlng(str);
            if (isInfoClick||isNodeChosePlace)
                MainMap.Cursor = Cursors.Cross;
            else if(!isOnMarker)
                MainMap.Cursor = Cursors.Default;
            if (e.Button == MouseButtons.Left && isMouseDown)
            {
                if (CurentRectMarker == null)
                {
                    //if (currentMarker.IsVisible)
                    //{
                    //    currentMarker.Position = MainMap.FromLocalToLatLng(e.X, e.Y);
                    //}
                }
                else // move rect marker
                {
                    PointLatLng pnew = MainMap.FromLocalToLatLng(e.X, e.Y);

                    int? pIndex = (int?)CurentRectMarker.Tag;
                    if (pIndex.HasValue)
                    {
                        if (pIndex < polygon.Points.Count)
                        {
                            polygon.Points[pIndex.Value] = pnew;
                            MainMap.UpdatePolygonLocalPosition(polygon);
                        }
                    }

                    if (NodeMarker.IsVisible)
                    {
                        NodeMarker.Position = pnew;
                    }
                    CurentRectMarker.Position = pnew;

                    if (CurentRectMarker.InnerMarker != null)
                    {
                        CurentRectMarker.InnerMarker.Position = pnew;
                        PointLatLng gpspos = CurentRectMarker.InnerMarker.Position;
                        gpspos.Offset(GmapToGpsOffset);
                        CurentRectMarker.ToolTipText = "{经度=" + gpspos.Lng.ToString(".00", CultureInfo.InvariantCulture) + "，纬度=" + gpspos.Lat.ToString(".00", CultureInfo.InvariantCulture) + "}";
                    }
                }
            }
            if (isRulerDown)
            {
                g = MainMap.CreateGraphics();
                MainMap.Refresh();
                g.DrawImageUnscaled(Resources.ruler, e.X, e.Y);
                if (isStartDown)
                {
                    using (Pen myPen = new Pen(Brushes.Black))
                    {

                        GPoint p = MainMap.FromLatLngToLocal(start);
                        end = MainMap.FromLocalToLatLng(e.X, e.Y);
                        Point pp = new Point(p.X,p.Y);
                        g.DrawLine(myPen, pp, e.Location);
                        double d = UtilityClass.CalcDistance(start, end);
                        if (d > 1)
                        {
                            String drawString = "距离 = " + d.ToString("00", CultureInfo.InvariantCulture)+"米";
                        
                        // Create font and brush.
                        Font drawFont = new Font("Arial", 16);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(e.X, e.Y);
                        g.DrawString(drawString, drawFont, drawBrush, drawPoint);
                        }
                    }
                }
                    
            }
        }
        void MainMap_OnMapTypeChanged(MapType type)
        {

            if (Net.Routes.Count > 0)
            {
                MainMap.ZoomAndCenterRoutes(null);
            }
        }
        // MapZoomChanged
        void MainMap_OnMapZoomChanged()
        {

            if ((int)MainMap.Zoom == 18)
                ZoomOUtMenuItem.Enabled = false;
            else if ((int)MainMap.Zoom == 1)
                ZoominMenuItem.Enabled = false;
            else 
            {
                ZoomOUtMenuItem.Enabled = true;
                ZoominMenuItem.Enabled = true;
            }
            center.Position = MainMap.Position;
            if (clouds != null)
            {
                var tl = MainMap.FromLatLngToLocal(gtl);
                var br = MainMap.FromLatLngToLocal(gbr);

                clouds.Position = gtl;
                clouds.Size = new System.Drawing.Size(br.X - tl.X, br.Y - tl.Y);
            }
        }

        // click on some marker
        void MainMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (item is GMapMarker)
                {
                    
                    //Placemark pos = GMaps.Instance.GetPlacemarkFromGeocoder(item.Position);
                    //if (pos != null)
                    //{
                    //    GMapMarkerRect v = item as GMapMarkerRect;
                    //    {
                    //        v.ToolTipText = pos.Address;
                    //    }
                    //    MainMap.Invalidate(false);
                    //}
                }
                else
                {
                    //if (item.Tag != null)
                    //{
                    //    if (currentTransport != null)
                    //    {
                    //        currentTransport.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                    //        currentTransport = null;
                    //    }
                    //    currentTransport = item;
                    //    currentTransport.ToolTipMode = MarkerTooltipMode.Always;
                    //}
                }
            }
        }

        // loader start loading tiles
        void MainMap_OnTileLoadStart()
        {
            MethodInvoker m = delegate()
            {
                //bar2.Text = "实验区图载入中……";
            };
            try
            {
                BeginInvoke(m);
            }
            catch
            {
            }
        }

        // loader end loading tiles
        void MainMap_OnTileLoadComplete(long ElapsedMilliseconds)
        {
            MainMap.ElapsedMilliseconds = ElapsedMilliseconds;

            MethodInvoker m = delegate()
            {
//                bar2.Text = "实验区图载入完成：" + MainMap.ElapsedMilliseconds + "ms";

            };
            try
            {
                BeginInvoke(m);
            }
            catch
            {
            }
        }

        // current point changed
        void MainMap_OnCurrentPositionChanged(PointLatLng point)
        {
            center.Position = point;
       
        }

        // center markers on start
        private void MainForm_Load(object sender, EventArgs e)
        {
            MainMap.Position = center.Position;
            if (objects.Markers.Count > 0)
            {
                MainMap.ZoomAndCenterMarkers(null);
                //ZoomSlider.Value = (int)MainMap.Zoom;
            }
        }

        // ensure focus on map, trackbar can have it too
        private void MainMap_MouseEnter(object sender, EventArgs e)
        {
            //MainMap.Focus();
            
        }

        private void MainMap_MouseClick(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (!isOnMarker))
            {
                MapContextMenuStrip.Show(e.X, e.Y);
            }
        }
        
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            //MainMap.SetZoomToFitRect(RectLatLng.FromLTRB(gtl.Lng, gtl.Lat, gbr.Lng, gbr.Lat));
            
        }

        private void MainMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isOnMarker)
            {
                ///do something
                if (CurrentMarker.GetType() != typeof(GMapMarkerGoogleGreen))
                    return;
                string nodename = CurrentMarker.Tag.ToString();
                ModifyMarker = CurrentMarker;
                if (nn == null)
                    nn = new NewNodeForm(this);
                nn.NodeNameBox.Value = int.Parse(nodename.TrimStart('节','点'));
                string[] str = { "节点配置", nodename, "节点位置", "纬度" };
                double lat = double.Parse(XmlHelper.GetConfigValue(xmldoc, str));
                string[] nstr = { "节点配置", nodename, "节点位置", "经度" };
                double lng = double.Parse(XmlHelper.GetConfigValue(xmldoc, nstr));
                string[] nnstr = { "节点配置", nodename, "节点描述" };
                string desc = XmlHelper.GetConfigValue(xmldoc, nnstr);
                string[] ipstr = { "节点配置", nodename, "网络配置", "节点IP" };
                string nodeip = XmlHelper.GetConfigValue(xmldoc, ipstr);
                nn.LngBox.Value = lng;
                nn.LatBox.Value = lat;
                nn.DescBox.Text = desc;
                nn.ipaddress.Text = nodeip;
                nn.Text = "修改节点";
                nn.ComfirmBtn.Text = "修改";
                nn.ComfirmBtn.Enabled = true;
                isModify = true;
                if (nn.ShowDialog(this) == DialogResult.OK)
                {
                    if (nn.isRight != true)
                    {
                        isModify = false;
                        return;
                    }
                    XmlDocument xmlfile = new XmlDocument();
                    xmlfile.Load(xmldoc);
                    XmlNode xn = xmlfile.DocumentElement;
                    GMapMarker delnode = CurrentMarker;
                    xn = xn.SelectSingleNode("descendant::" + delnode.Tag.ToString());
                    XmlNode father = xmlfile.SelectSingleNode("descendant::节点配置");
                    father.RemoveChild(xn);
                    xmlfile.Save(xmldoc);
                    WebNodeLayer.Markers.Remove(CurrentMarker);
                    PointLatLng p = new PointLatLng(nn.LatBox.Value, nn.LngBox.Value);
                    p.Offset(GpsToGmapOffset);
                    GMapMarkerGoogleGreen newnode = new GMapMarkerGoogleGreen(p);
                    newnode.Tag = "节点" + nn.NodeNameBox.Text;
                    newnode.IsHitTestVisible = true;
                    newnode.ToolTipMode = WebVisibleCheck.Checked ? MarkerTooltipMode.Always : MarkerTooltipMode.OnMouseOver;
                    p.Offset(GmapToGpsOffset);
                    newnode.ToolTipText = newnode.Tag.ToString() + "\r\n" + nn.DescBox.Text;// +"\r\n" + "{经度=" + p.Lng.ToString("F08", CultureInfo.InvariantCulture) + "，纬度=" + p.Lat.ToString("F08", CultureInfo.InvariantCulture) + "}";
                    WebNodeLayer.Markers.Add(newnode);
                    string ip = nn.ipaddress.Text.Replace(" ","");
                    try
                    {
                        if (ip == "")
                        {
                            MessageBox.Show("节点IP自动设置为缺省值：192.168.2.100");
                            ip = "192.168.2.100";
                        }
                        IPAddress ia = IPAddress.Parse(ip);
                        ip = ia.ToString();
                    }
                    catch (Exception eip)
                    {
                        MessageBox.Show(eip.ToString()+"\r\n"+"节点IP自动设置为缺省值：192.168.2.100");
                        ip = "192.168.2.100";

                    }
                    AddNodeToXml(newnode, nn.DescBox.Text,ip);
                    isModify = false;
                    if (newnode.Tag.ToString()!= delnode.Tag.ToString())//修改了节点名
                    {
                        string message = "改变节点集合后请重设路由";
                        string caption = "通知";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        MessageBoxIcon icon = MessageBoxIcon.Information;
                        MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button1;
                        // Show message box
                        DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);

                        NodeRouteMap.Clear();
                        NodeNetMap.Clear();
                        SaveInit();
                    }
                    //AddRoutes();
                    AddNet();
                }
                
            }
        }
        private void MainMap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (MainMap.Focused)
            {
                if (e.KeyChar == '+')
                {
                    MainMap.Zoom += 1;
                }
                else if (e.KeyChar == '-')
                {
                    MainMap.Zoom -= 1;
                }
                else if (e.KeyChar == 'a')
                {
                    MainMap.Bearing--;
                }
                else if (e.KeyChar == 'z')
                {
                    MainMap.Bearing++;
                }
            }
        }
        private void MainMap_KeyDown(object sender, KeyEventArgs e)
        {
            if (MainMap.Focused)
            {
                if (e.KeyCode == Keys.Escape && isRulerDown)
                {
                    isRulerDown = false;
                    isStartDown = false;
                    MainMap.Refresh();
                }

            }
        }

        #endregion

        #region 动画显示部分
        public void Ping(string node, int ElaspseTime,string str)
        {
            PointLatLng point = GetNodeGmapPosition(node);
           CircleEmitAnimation(ref MainMap,point,ElaspseTime,str);
        }
        #region 圆环动画
        private void CircleEmitAnimation(ref MapCustom.Map map,PointLatLng point,int ElaspseTime,string str)
        {
            g = map.CreateGraphics();
            UtilityClass.AniParameter parameter = new UtilityClass.AniParameter(ref map, g, point, (int)Math.Pow(2, (int)(map.Zoom / 2)), ElaspseTime,str);
            Thread newThread = new Thread(AnimationThread);
            newThread.Start(parameter);
            
        }
        private void AnimationThread(object data)
        {
            AutoResetEvent autoEvent = new AutoResetEvent(false);
            UtilityClass.AniParameter parameter = (UtilityClass.AniParameter)data;
            System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(DoDraw), data, 0, 50);
            autoEvent.WaitOne(parameter.TimeUp, false);
            Debug.WriteLine("Time`s Up!");
            timer.Dispose();
        }


        private void DoDraw(Object Aniparameter)
        {
            UtilityClass.AniParameter parameter = (UtilityClass.AniParameter)Aniparameter;
            if (parameter.mainmap.InvokeRequired)//与控件不在同一线程，委托操作
            {
                DoAniCallback d = new DoAniCallback(DoDraw);
                this.Invoke(d, new object[] { Aniparameter });
            }
            else
            {
                using (Pen myPen = new Pen(Brushes.Aquamarine))
                {
                    GPoint Gp = parameter.mainmap.FromLatLngToLocal(parameter.AniPoint);
                    Point p = new Point(Gp.X, Gp.Y);
                    myPen.DashCap = DashCap.Round;
                    myPen.DashPattern = new float[] { 4.0F, 2.0F, 1.0F, 3.0F };
                    myPen.Width = 2;
                    WriteTextOnMap(parameter.Gp, parameter.AniText, p);
                    int factor = (int)Math.Sqrt(parameter.R) * (int)parameter.mainmap.Zoom/9;//根据地图缩放倍数调整动画的缩放因子
                    if (parameter.r * factor <= parameter.R * (int)parameter.mainmap.Zoom/9)//这个比例关系比较合适
                    {
                        
                        parameter.Gp.DrawEllipse(myPen, p.X - parameter.r * factor, p.Y - parameter.r * factor, parameter.r * factor * 2, parameter.r * factor * 2);
                        parameter.r++;
                        //parameter.mainmap.Refresh();
                    }
                    else
                        parameter.r = 1;
                }
            }
        }
        #endregion 圆环动画

        #region 直线动画
        public void LineEmitAnimation(ref MapCustom.Map map,PointLatLng startpoint, PointLatLng endpoint,int ElaspseTime,string AniString)
        {
            g = map.CreateGraphics();
            UtilityClass.LineParameter parameter = new UtilityClass.LineParameter(ref map, g, startpoint, endpoint, ElaspseTime, AniString);
            Thread newThread = new Thread(AnimationLineThread);
            newThread.Start(parameter);

        }
        private void AnimationLineThread(object data)
        {
            AutoResetEvent autoEvent = new AutoResetEvent(false);
            UtilityClass.LineParameter parameter = (UtilityClass.LineParameter)data;
            System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(DoLineDraw), data, 0, parameter.TimeUp/18);
            autoEvent.WaitOne((parameter.TimeUp + parameter.TimeUp / 2), false);
            //Thread.Sleep(parameter.TimeUp);
            //Debug.WriteLine("Time`s Up!");
            timer.Dispose();
            //parameter.mainmap.Refresh();
        }
        private void WriteTextOnMap( Graphics g, string text, Point middle)
        {
            if (text != "")
            {
                if (text.StartsWith("RTS"))
                {
                    middle.Offset(0, -30);
                    g.DrawString(text, new Font("Arial Regular", 12), Brushes.Red, middle);
                }
                else if (text.StartsWith("CTS"))
                    g.DrawString(text, new Font("Arial Regular", 12), Brushes.LightBlue, middle);
                else if (text.StartsWith("ACK"))
                {
                    middle.Offset(0, -30);
                    g.DrawString(text, new Font("Arial Regular", 12), Brushes.Black, middle);
                }
                else if (text.StartsWith("PING"))
                {
                    g.DrawString(text, new Font("Arial Regular", 12), Brushes.Red, middle);
                }
                else if (text.StartsWith("AUV"))
                {
                    g.DrawString(text, new Font("Arial Regular", 12), Brushes.White, middle);
                }
                else
                {
                    g.DrawString(text, new Font("Arial Regular", 12), Brushes.LightBlue, middle);
                }
            }
        }
        
        private void DoLineDraw(Object Aniparameter)
        {
            UtilityClass.LineParameter parameter = (UtilityClass.LineParameter)Aniparameter;
            try
            {
                if (parameter.mainmap.InvokeRequired)//与控件不在同一线程，委托操作
                {
                    DoAniCallback d = new DoAniCallback(DoLineDraw);
                    this.Invoke(d, new object[] { Aniparameter });
                }
                else
                {
                    using (Pen myPen = new Pen(Brushes.Aquamarine))
                    {
                        GPoint Gsp = parameter.mainmap.FromLatLngToLocal(parameter.startAniPoint);
                        GPoint Gep = parameter.mainmap.FromLatLngToLocal(parameter.endAniPoint);
                        Point sp = new Point(Gsp.X, Gsp.Y);
                        Point ep = new Point(Gep.X, Gep.Y);
                        string maptext = parameter.AniText;
                        Point middle = new Point((sp.X + ep.X) / 2, (sp.Y + ep.Y) / 2);
                        myPen.DashCap = DashCap.Round;
                        //myPen.DashPattern = new float[] {1.0F, 3.0F };
                        myPen.Width = 4;
                        myPen.Color = Color.Blue;
                        //parameter.Gp.DrawLine(myPen, sp.X, sp.Y, ep.X, ep.Y);
                        if (parameter.factor <= 104)
                        {
                            WriteTextOnMap(parameter.Gp, maptext, middle);
                            if (parameter.factor > 100)
                            {
                                parameter.factor = 100;
                                parameter.Gp.DrawLine(myPen, sp.X, sp.Y, sp.X + (int)((ep.X - sp.X) * parameter.factor / 100), sp.Y + (int)((ep.Y - sp.Y) * parameter.factor / 100));
                                AutoSaveImg();
                            }
                            else
                            {
                                parameter.Gp.DrawLine(myPen, sp.X, sp.Y, sp.X + (int)((ep.X - sp.X) * parameter.factor / 100), sp.Y + (int)((ep.Y - sp.Y) * parameter.factor / 100));
                            }

                            parameter.factor += 8;
                            
                            //parameter.mainmap.Refresh();
                            //Debug.WriteLine("Map Refresh!");
                            //Debug.WriteLine("{parameter.factor}", parameter.factor.ToString());
                        }
                        else if ((parameter.factor > 104)&&(parameter.factor < 172))
                        {
                            WriteTextOnMap(parameter.Gp, maptext, middle);

                            parameter.factor += 8;//再加一次
                            //Debug.WriteLine("Map Refresh!");
                        }
                        else 
                        {
                            WriteTextOnMap(parameter.Gp, maptext, middle);
                            parameter.mainmap.Refresh();//过头了，刷新一下
                        }

                    }
                }
            }
            catch(Exception e)
            {
                //c出错什么也不干
            }
        }
        #endregion 直线动画
        #endregion

        #region 右键菜单项 包括节点/标识/距离显示，保存视图等
        private void 读入深度数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openDepthFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    depthinfo.GetFileData(openDepthFile.OpenFile());

                }
                catch (Exception MyEx)
                {
                    
                    MessageBox.Show(MyEx.Message);
                }
            }
        }
        private void 显示节点计数ToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (显示节点计数ToolStripMenuItem.Checked)
            {
                
            }
        }
        private void 显示节点计数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nodeticksmut.WaitOne();
            Array.Clear(NodeTicks, 0, 64);
            nodeticksmut.ReleaseMutex();

            foreach (GMapMarker g in WebNodeLayer.Markers)
            {
                if (g.GetType() != typeof(GMapMarkerGoogleGreen))
                    continue;
                string[] desc = { "节点配置", g.Tag.ToString(), "节点描述" };
                string describe = XmlHelper.GetConfigValue(xmldoc, desc);
                g.ToolTipText = g.Tag.ToString() + "\r\n" + describe + "\r\n";

            }
        }

        private void 清空节点数据计数ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            nodeticksmut.WaitOne();
            Array.Clear(NodeTicks, 0, 64);
            nodeticksmut.ReleaseMutex();
            
            foreach (GMapMarker g in WebNodeLayer.Markers)
            {
                if (g.GetType() != typeof(GMapMarkerGoogleGreen))
                    continue;
                string[] desc = { "节点配置", g.Tag.ToString(), "节点描述" };
                string describe = XmlHelper.GetConfigValue(xmldoc, desc);

                g.ToolTipText = g.Tag.ToString() + "\r\n" + describe + "\r\n";
                
            }
            
            清空节点数据计数ToolStripMenuItem.ToolTipText = "清空节点数据计数值，上次清空时间为" + DateTime.Now.ToString();
        }
        private void NodeVisibleCheck_CheckedChanged(object sender, EventArgs e)
        {
            NodeMarker.IsVisible = NodeVisibleCheck.Checked;
            NodeInfoCheck.Enabled = NodeVisibleCheck.Checked;
        }


        private void NodeInfoCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (NodeInfoCheck.Checked)
            {
                NodeMarker.ToolTipMode = MarkerTooltipMode.Always;
                MainMap.Invalidate();
            }
            else
            {
                NodeMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                MainMap.Invalidate();
            }
             
        }

        private void WebVisibleCheck_Click(object sender, EventArgs e)
        {
            if (WebVisibleCheck.Checked)
            {
                isNodeShown = true;
                foreach (GMapMarker gm in WebNodeLayer.Markers)
                {
                    gm.ToolTipMode = MarkerTooltipMode.Always;
                }
                
            }
            else
            {
                isNodeShown = false;
                foreach (GMapMarker gm in WebNodeLayer.Markers)
                {
                    gm.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                }
            }
            MainMap.Invalidate();
        }

        private void showNet_CheckedChanged(object sender, EventArgs e)
        {
            if (showNet.Checked)
            {
                AddNet();
                Net.IsVisibile = true;
            }
            else
            {
                Net.IsVisibile = false;
            }
        }
        /*
        //添加预存的路由
        public void AddRoutes()
        {
            try
            {
                if (MainMap.InvokeRequired)
                {
                    AddRouteCallback d = new AddRouteCallback(AddRoutes);
                    this.Invoke(d, new object[] { });
                }
                else
                {
                    routes.Routes.Clear();
                    if (NodeRouteMap.Count == 0)
                        return;
                    foreach (object obj in NodeRouteMap.Keys)
                    {
                        string node = (string)obj;
                        List<string[]> lst = (List<string[]>)NodeRouteMap[node];
                        foreach (string[] endnode in lst)
                        {

                            //起点坐标
                            string[] sstr = { "节点配置", node, "节点位置", "纬度" };
                            double slat = double.Parse(XmlHelper.GetConfigValue(xmldoc, sstr));
                            string[] lngsstr = { "节点配置", node, "节点位置", "经度" };
                            double slng = double.Parse(XmlHelper.GetConfigValue(xmldoc, lngsstr));
                            PointLatLng sp = new PointLatLng(slat, slng);
                            sp.Offset(GpsToGmapOffset);
                            List<PointLatLng> route = new List<PointLatLng>();
                            route.Add(sp);
                            //路由点坐标
                            for (int i = 0; i < endnode.Length; i++)
                            {
                                string[] str = { "节点配置", endnode[i], "节点位置", "纬度" };
                                double lat = double.Parse(XmlHelper.GetConfigValue(xmldoc, str));
                                string[] lngstr = { "节点配置", endnode[i], "节点位置", "经度" };
                                double lng = double.Parse(XmlHelper.GetConfigValue(xmldoc, lngstr));
                                PointLatLng ep = new PointLatLng(lat, lng);
                                ep.Offset(GpsToGmapOffset);
                                route.Add(ep);
                            }

                            GMapRoute newroute = new GMapRoute(route, node);
                            newroute.Stroke.Color = Color.FromArgb(222, Color.Red);
                            newroute.Stroke.Width = 2;
                            routes.Routes.Add(newroute);

                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                MessageBox.Show("路由表中节点与节点配置不一致，无法正常显示路由！");
            }
        }
        */
        //添加预存的网络表
        public void AddNet()
        {
            try
            {
                if (MainMap.InvokeRequired)
                {
                    AddRouteCallback d = new AddRouteCallback(AddNet);
                    this.Invoke(d, new object[] { });
                }
                else
                {
                    Net.Routes.Clear();
                    if (NodeNetMap.Count == 0)
                        return;
                    List<string> errnode = new List<string>();//当前配置没有的节点，最后要删除
                    foreach (object obj in NodeNetMap.Keys)
                    {
                        string node = (string)obj;
                        if (PointLatLng.Zero == GetNodeGPSPosition(node))//地图上没有不显示
                        {
                            errnode.Add(node);
                            continue;
                        }
                        bool isFindRoute = false;//找到路由标志。
                        List<string> lst = (List<string>)NodeNetMap[node];
                        foreach (string endnode in lst)
                        {
                            if (PointLatLng.Zero == GetNodeGPSPosition(endnode))//地图上没有不显示
                            {
                                continue;
                            }
                            isFindRoute = false;
                            foreach (GMapRoute r in Net.Routes)
                            {
                                //用两个名字匹配，方法蠢点，暂时没想到其他办法
                                string routename1 = string.Concat(node, "-", endnode);
                                string routename2 = string.Concat(endnode, "-", node);
                                if (string.Equals(routename1, r.Name) || string.Equals(routename2, r.Name))//有一个符合，说明找到了
                                {
                                    isFindRoute = true;
                                    break;
                                }
                            }
                            if (!isFindRoute)//未找到路由，需要添加
                            {
                                //起点坐标
                                string[] sstr = { "节点配置", node, "节点位置", "纬度" };
                                double slat = double.Parse(XmlHelper.GetConfigValue(xmldoc, sstr));
                                string[] lngsstr = { "节点配置", node, "节点位置", "经度" };
                                double slng = double.Parse(XmlHelper.GetConfigValue(xmldoc, lngsstr));
                                PointLatLng sp = new PointLatLng(slat, slng);
                                sp.Offset(GpsToGmapOffset);
                                List<PointLatLng> route = new List<PointLatLng>();
                                route.Add(sp);
                                //终点坐标
                                string[] str = { "节点配置", endnode, "节点位置", "纬度" };
                                double lat = double.Parse(XmlHelper.GetConfigValue(xmldoc, str));
                                string[] lngstr = { "节点配置", endnode, "节点位置", "经度" };
                                double lng = double.Parse(XmlHelper.GetConfigValue(xmldoc, lngstr));
                                PointLatLng ep = new PointLatLng(lat, lng);
                                ep.Offset(GpsToGmapOffset);
                                route.Add(ep);
                                GMapRoute newroute = new GMapRoute(route, string.Concat(node, "-", endnode));
                                newroute.Stroke.Color = Color.FromArgb(222, Color.AliceBlue);
                                newroute.Stroke.Width = 2;
                                Net.Routes.Add(newroute);
                            }
                        }
                    }
                    //删除未配置的点
                    for (int i = 0; i < errnode.Count; i++)
                    {
                        NodeNetMap.Remove(errnode[i]);//删除拥有未配置点的key/value对，只要非空不会异常
                    }
                    Hashtable ht = new Hashtable();
                    foreach (object obj in NodeNetMap.Keys)
                    {
                        string nodename = (string)obj;
                        List<string> lst = (List<string>)NodeNetMap[nodename];
                        List<string> newlst = new List<string>();
                        foreach (string str in lst)
                        {
                            if (errnode.Contains(str))//有未配置的点
                                continue;
                            string newstr = str;
                            newlst.Add(newstr);
                        }
                        ht.Add(nodename, newlst);

                    }
                    //删除了无效点，复制回原表
                    UtilityClass.CopyHashTableStringList(ht, NodeNetMap);
                    //删除未配置的点
                    SaveInit();
                }
            }
            catch (Exception Ex)
            {
                
                MessageBox.Show(Ex.Message+"\r\n添加网络表出错，请检查网络表中节点是否与节点配置一致！请清空所有配置！");
                Net.Routes.Clear();
                //NodeNetMap.Clear();
                //NodeRouteMap.Clear();
                //DistMap.Clear();
                SaveInit();
                showNet.Checked = false;
            }
        }

        private void TrackCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (!TrackCheck.Checked)
            {
                track.Routes.Clear();
            }
        }

        private void AnimCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (!AnimCheck.Checked)
            {
                isShowAni = false;
            }
            else 
            {
                isShowAni = true;
            }
        }

        private void comboBoxMode_DropDownClosed(object sender, EventArgs e)
        {
            GMaps.Instance.Mode = (AccessMode)Enum.Parse(typeof(AccessMode),comboBoxMode.SelectedItem.ToString());
            MainMap.ReloadMap();
        }

        private void MapTypeBox_DropDownClosed(object sender, EventArgs e)
        {
            MainMap.MapType = (MapType)Enum.Parse(typeof(MapType), MapTypeBox.SelectedItem.ToString());
            MainMap.ReloadMap();
        }

        private void SaveMapMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "PNG (*.png)|*.png";
                    sfd.FileName = "地图";
                    MapContextMenuStrip.Visible = false;
                    MainForm.pMainForm.Update();
                    Image tmpImage = MainMap.ToImage();
                    if (tmpImage != null)
                    {
                        using (tmpImage)
                        {
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                tmpImage.Save(sfd.FileName);

                                MessageBox.Show("地图保存: " + sfd.FileName, "WebNode", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败: " + ex.Message, "WebNode", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BackToMenuItem_Click(object sender, EventArgs e)
        {
            string[] str = { "地图配置", "地图中心", "纬度" };
            double lat = double.Parse(XmlHelper.GetConfigValue(xmldoc, str));
            string[] lngstr = { "地图配置", "地图中心", "经度" };
            double lng = double.Parse(XmlHelper.GetConfigValue(xmldoc, lngstr));
            PointLatLng p = new PointLatLng(lat, lng);
            p.Offset(GpsToGmapOffset);
            MainMap.Position = p;
            MainMap.Bearing = 0;
        }

        private void DelInfoMenuItem_Click(object sender, EventArgs e)
        {
            CollectionForm cf = new CollectionForm();
            cf.CollectionBox.Sorted = false;
            foreach (GMapInfoBoard g in infolayers.Markers)
            {
                PointLatLng p = g.Position;
                p.Offset(GmapToGpsOffset);
                cf.CollectionBox.Items.Add(g.InfoText + p.ToString());
            }

            if (cf.ShowDialog() == DialogResult.OK)
            {
                
                foreach (int g in cf.CollectionBox.CheckedIndices)
                {
                    string node = cf.CollectionBox.Items[g].ToString();
                    string[] nodename = node.Split('{','}');
                    foreach (GMapInfoBoard i in infolayers.Markers)
                    {
                        PointLatLng p = i.Position;
                        p.Offset(GmapToGpsOffset);
                        if ((i.InfoText == nodename[0])&&(p.ToString().Trim('{','}') == nodename[1]))
                        {
                            infolayers.Markers.Remove(i);
                            break;
                        }

                    } 
                }
            }
            cf.Dispose();
        }

        private void AddInfoMenuItem_Click(object sender, EventArgs e)
        {
            isInfoClick = true;
        }

        private void DelNodeMenuItem_Click(object sender, EventArgs e)
        {
            CollectionForm cf = new CollectionForm();
            foreach (GMapMarker m in WebNodeLayer.Markers)
            {
                PointLatLng p = m.Position;
                p.Offset(GmapToGpsOffset);
                cf.CollectionBox.Items.Add(m.Tag.ToString() + ":" + p.ToString());
            }
            XmlDocument xmlfile = new XmlDocument();

            if (cf.ShowDialog() == DialogResult.OK)
            {
                int former = 0;
                foreach (int i in cf.CollectionBox.CheckedIndices)
                {

                    xmlfile.Load(xmldoc);
                    XmlNode xn = xmlfile.DocumentElement;
                    //for the action will make the collection push the items forward
                    string node = cf.CollectionBox.Items[i].ToString();
                    string[] nodename = node.Split(':');
                    foreach (GMapMarker m in WebNodeLayer.Markers)
                    {
                        if (m.Tag.ToString() == nodename[0])
                        {
                            WebNodeLayer.Markers.Remove(m);
                            break;
                        }
                    }
                    if(nodename[0].Contains("("))
                        nodename[0] = nodename[0].Replace("(","");
                    if (nodename[0].Contains(")"))
                        nodename[0] = nodename[0].Replace(")", "");
                    xn = xn.SelectSingleNode("descendant::" + nodename[0]);
                    XmlNode father = xmlfile.SelectSingleNode("descendant::节点配置");
                    if (xn != null)
                    {
                        father.RemoveChild(xn);
                        xmlfile.Save(xmldoc);
                    }
                    
                }
                
            }
            cf.Dispose();
        }

        private void AddNodeMenuItem_Click(object sender, EventArgs e)
        {
            isNodeChosePlace = false;
            isInfoClick = false;
            if (nn == null)
            {
                nn = new NewNodeForm(this);
            }
            else
            {
                if (nn.Text == "修改节点")
                    nn.Close();
                nn = new NewNodeForm(this);
            }
            nn.NodeNameBox.Value = 0;
            PointLatLng pt = new PointLatLng(NodeMarker.Position.Lat,NodeMarker.Position.Lng);//地图上的点
            pt.Offset(GmapToGpsOffset);
            nn.LngBox.Value = pt.Lng;
            nn.LatBox.Value = pt.Lat;
            nn.Text = "新增节点";
            nn.ComfirmBtn.Text = "添加";
            nn.ComfirmBtn.Enabled = false;
            if (nn.ShowDialog(this) == DialogResult.OK)
            {
                if (nn.isRight != true)
                    return;
                PointLatLng p = new PointLatLng(nn.LatBox.Value, nn.LngBox.Value);
                p.Offset(GpsToGmapOffset);
                GMapMarkerGoogleGreen newnode = new GMapMarkerGoogleGreen(p);
                newnode.Tag = "节点" + nn.NodeNameBox.Text;
                newnode.IsHitTestVisible = true;
                newnode.ToolTipMode = WebVisibleCheck.Checked ? MarkerTooltipMode.Always : MarkerTooltipMode.OnMouseOver;
                p.Offset(GmapToGpsOffset);
                newnode.ToolTipText = newnode.Tag.ToString() + "\r\n" + nn.DescBox.Text;// +"\r\n" + "{经度=" + p.Lng.ToString("F08", CultureInfo.InvariantCulture) + "，纬度=" + p.Lat.ToString("F08", CultureInfo.InvariantCulture) + "}";
                WebNodeLayer.Markers.Add(newnode);
                string ip = nn.ipaddress.Text.Replace(" ", "");
                try
                {
                    if (ip == "")
                    {
                        MessageBox.Show("节点IP自动设置为缺省值：192.168.2.100");
                        ip = "192.168.2.100";
                    }
                    IPAddress ia = IPAddress.Parse(ip);
                    ip = ia.ToString();
                }
                catch (Exception eip)
                {
                    MessageBox.Show(eip.ToString() + "\r\n" + "节点IP自动设置为缺省值：192.168.2.100");
                    ip = "192.168.2.100";

                }
                
                AddNodeToXml(newnode, nn.DescBox.Text,ip);

            }
        }

        private void DelDistMenuItem_Click(object sender, EventArgs e)
        {
            CollectionForm cf = new CollectionForm();
            foreach (GMapInfoBoard g in DistanceInfo.Markers)
            {
                PointLatLng p = g.Position;
                p.Offset(GmapToGpsOffset);
                cf.CollectionBox.Items.Add(g.InfoText + p.ToString());
            }

            if (cf.ShowDialog() == DialogResult.OK)
            {
                foreach (int g in cf.CollectionBox.CheckedIndices)
                {
                    string node = cf.CollectionBox.Items[g].ToString();
                    string[] nodename = node.Split('{','}');
                    foreach (GMapInfoBoard i in DistanceInfo.Markers)
                    {
                        PointLatLng p = i.Position;
                        p.Offset(GmapToGpsOffset);
                        if ((i.InfoText == nodename[0])&&(p.ToString().Trim('{','}') == nodename[1]))
                        {
 
                            foreach (GMapRoute r in rulers.Routes)
                            {
                                if (r.Name == nodename[0])
                                {
                                    rulers.Routes.Remove(r);
                                    break;
                                }
                                
                            }
                            DistanceInfo.Markers.Remove(i);
                            break;
                        }
                        
                    } 

                }
            }
            cf.Dispose();
           
        }

        private void surveyMenuItem_Click(object sender, EventArgs e)
        {
            isRulerDown = true;
        }

        private void ZoominMenuItem_Click(object sender, EventArgs e)
        {
            MainMap.Zoom -= 1;
        }

        private void ZoomOUtMenuItem_Click(object sender, EventArgs e)
        {
            MainMap.Zoom += 1;
        }

        private void 输出节点位置信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "txt (*.txt)|*.txt";
                    sfd.FileName = "节点信息";
                    
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            foreach (GMapMarker g in WebNodeLayer.Markers)
                            {
                                if (g.GetType().Equals(typeof(GMapMarkerGoogleRed)))
                                    continue;
                                PointLatLng p = g.Position;
                                p.Offset(GmapToGpsOffset);
                                sw.WriteLine(g.Tag.ToString() + " " + p.Lng.ToString() + " " + p.Lat.ToString());
                            }
                        }

                        MessageBox.Show("节点信息保存: " + sfd.FileName, "WebNode", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败: " + ex.Message, "WebNode", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 输出标识信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "txt (*.txt)|*.txt";
                    sfd.FileName = "标识信息";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            foreach (GMapInfoBoard g in infolayers.Markers)
                            {
                                PointLatLng p = g.Position;
                                p.Offset(GmapToGpsOffset);
                                sw.WriteLine(g.InfoText + " " + p.Lng.ToString() + " " + p.Lat.ToString());
                            }
                        }

                        MessageBox.Show("标识信息保存: " + sfd.FileName, "WebNode", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败: " + ex.Message, "WebNode", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 读入标识信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "txt (*.txt)|*.txt";
                    ofd.FileName = "标识信息";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamReader sr = new StreamReader(ofd.FileName))
                        {
                            string str;
                            while ((str = sr.ReadLine()) != null)
                            {
                                string[] infos = str.Split(' ');
                                PointLatLng middle = new PointLatLng();
                                middle.Lat = double.Parse(infos[2]);
                                middle.Lng = double.Parse(infos[1]);

                                middle.Offset(GpsToGmapOffset);
                                GMapInfoBoard g = new GMapInfoBoard(middle,infos[0]);
                                infolayers.Markers.Add(g);
                            }
                        }

                        MessageBox.Show("标识信息读入成功: " + ofd.FileName, "WebNode", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("标识信息读入失败: " + ex.Message, "WebNode", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region GPS接收显示
        public void GPS_StartWork()
        {
            
            try
            {
                GpsSerialPort.PortName = comm;
                GpsSerialPort.BaudRate = baudrate;
                isContinue = true;

                GpsSerialPort.Open();
                if (GpsSerialPort.IsOpen)
                {
                    readThread = new Thread(Read);
                    readThread.Start();
                }
            }
            catch (Exception MyEx)
            {
                //MessageBox.Show(MyEx.Message);
            }
        }
        /// <summary>
        ///  处理gps信息，刷新控件及发送信息给主窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">gps信息</param>
        void GPSForm_GpsLogEvent(object sender, EventsClass.GpsEventArgs e)
        {
            if (GpsLog.InvokeRequired)
            {
                GpsBoxCallback d = new GpsBoxCallback(GPSForm_GpsLogEvent);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                GpsLog.AppendText(e.gpsdata + "\r\n");
                GpsLog.ScrollToCaret();
                string newcomming = e.gpsdata;
                char[] charSeparators = new char[] {',','*','#','$',';'};
                try
                {
                    string[] gpsinfo = newcomming.Split(charSeparators, StringSplitOptions.None);
                    switch (gpsinfo[1])
                    {
                        case "GPRMC":
                            if (GpsHelper.CalculateCheckSum(newcomming).ToUpper() == gpsinfo[gpsinfo.Length - 1])
                            {
                                if (gpsinfo[3] == "A")//数据有效
                                {
                                    UtilityClass.UtcTime tm = new UtilityClass.UtcTime();
                                    tm.year = UInt16.Parse(gpsinfo[10].Substring(4));
                                    tm.year += 2000;
                                    tm.mon = UInt16.Parse(gpsinfo[10].Substring(2, 2));
                                    tm.day = UInt16.Parse(gpsinfo[10].Substring(0, 2));
                                    tm.hour = UInt16.Parse(gpsinfo[2].Substring(0, 2));
                                    tm.min = UInt16.Parse(gpsinfo[2].Substring(2, 2));
                                    tm.sec = UInt16.Parse(gpsinfo[2].Substring(4, 2));
                                    timelabel.Text = tm.ToString();

                                    //发送时间信息到主窗口
                                    EventsClass.UtcTimeEventArgs time = new EventsClass.UtcTimeEventArgs(tm.ToString());
                                    TimeEventHandler handler = UtcTimeEvent;
                                    if (handler != null)
                                    {
                                        handler(this, time);
                                    }

                                    //刷新航向、航速信息
                                    speedlabel.Text = gpsinfo[8] + "节";
                                    degreelabel.Text = gpsinfo[9];
                                    bearing = float.Parse(gpsinfo[9]);
                                    
                                    
                                }
                            }
                            //else
                            {
                                string str = GpsHelper.CalculateCheckSum(newcomming);
                            }
                            break;
                        case "BESTPOSA":
                            if (GpsHelper.CalculateBlockCRC32(newcomming.Substring(1, newcomming.Length-10)) == gpsinfo[gpsinfo.Length - 1])
                            {
                                sollabel.Text = gpsinfo[11];
                                poslabel.Text = gpsinfo[12];
                                latlabel.Text = gpsinfo[13];
                                lnglabel.Text = gpsinfo[14];
                                hgtlabel.Text = gpsinfo[15];
                                latsdlabel.Text = gpsinfo[18];
                                lngsdlabel.Text = gpsinfo[19];
                                hgtsdlabel.Text = gpsinfo[20];
                                tracklabel.Text = gpsinfo[24];
                                uselabel.Text = gpsinfo[25];
                                EventsClass.lnglatEventArgs ll = new EventsClass.lnglatEventArgs(Double.Parse(gpsinfo[14]), Double.Parse(gpsinfo[13]),bearing);
                                latlngEventHandler handler = GpslatlngEvent;
                                if(handler != null)
                                {
                                    handler(this,ll);
                                }
                            }   
                            break;

                        default:
                            break;

                    }
                }
                catch
                {}
                

            }
            

        }

        /// <summary>
        /// 读串口线程
        /// </summary>
        public static void Read()
        {
            while (isContinue)
            {
                try
                {
                    if (GpsSerialPort.IsOpen)
                    {
                        string message = GpsSerialPort.ReadLine();
                        if (!isfirst)
                        {
                            GpsLogFile.OpenFile(MainForm.pMainForm.RecordInfo);
                            isfirst = true;
                        }
                        GpsLogFile.writeLine(message);
                        if (GpsLogFile.length > 1024 * 1024 * 1)//打开新文件
                        {
                            GpsLogFile.close();
                            GpsLogFile.OpenFile(MainForm.pMainForm.RecordInfo);
                        }
                        EventsClass.GpsEventArgs e = new EventsClass.GpsEventArgs(message);
                        GpsEventHandler handler = GpsLogEvent;
                        if (handler != null)
                        {
                            handler(null, e);
                        }
                    }
                }
                catch (Exception MyEx)
                {
                    //MessageBox.Show(MyEx.Message + ":" + MyEx.StackTrace);
                    isContinue = false;
                }
            }

        }
    
        #endregion

        #region MSP接收,转发,命令
        public void MSP_StartWork()
        {

            try
            {
                isMspContinue = true;
                MspSerialPort.Open();
                
                if (MspSerialPort.IsOpen)
                {
                    BurnStat.Enabled = true;
                    readMspThread = new Thread(ReadMSP);
                    readMspThread.Priority = ThreadPriority.AboveNormal;
                    readMspThread.Start();
                    WriteCommLog("打开与节点串口连接端口，开始接收数据！");
                    
                }
            }
            catch (Exception MyEx)
            {
                MessageBox.Show(MyEx.Message);
                BurnStat.Enabled = false;
            }
        }

        /// <summary>
        ///  处理MSP信息，发送信息给窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">信息</param>
        public void MspRecvEvent(object sender, EventsClass.WaveEventArgs e)
        {
            try
            {
                byte[] cmd = new byte[e.WaveBufferLength];//复制方便操作，不复制也行
                Array.Copy(e.WaveBuffer, cmd, e.WaveBufferLength);
                string strcmd = Encoding.ASCII.GetString(cmd);//字符形式的命令，用于读取EB90协议的数据，内部数据用cmd存取。
                string[] str = strcmd.Split(',');
                BuoyID = str[2];
                //MainForm.pMainForm.BuoyChoice.SelectedIndex = int.Parse(BuoyID);
                if (str[1] == "03")
                {
                    if (str[3] == "Y")
                    {
                        //写消息框校验正确
                        WriteCommLog("MSP430校验正确。");
                        //
                    }
                    else
                    {
                        //写消息框校验错误
                        WriteCommLog("MSP430校验错误。");
                        //
                    }
                }
                else//不是应答包
                {
                    //保存数据
                    MSPRecvDataFile.OpenFile(MainForm.pMainForm.SerialRecvPathInfo);
                    MSPRecvDataFile.BinaryWrite(cmd);
                    MSPRecvDataFile.close();
                    string time;
                    byte[] data;
                    int id;
                    if (SourceDataClass.DepackCommData(cmd, out time,out id, out data))
                    {
                        string ack = "EB90,03," + BuoyID + ",Y,";
                        UInt16 crc =  CRCHelper.CRC16(ack);
                        byte[] bcrc = BitConverter.GetBytes(crc);
                        byte tmp = bcrc[0];
                        bcrc[0] = bcrc[1];
                        bcrc[1] = tmp;
                        //ack += Encoding.Default.GetString(bcrc);
                        ack += "  ,END";
                        byte[] b = Encoding.Default.GetBytes(ack);
                        b[13] = bcrc[0];
                        b[14] = bcrc[1];
                        //MsgLog(MsgMode.RecvSerialData, id, "MSP430", "上位机", SourceDataClass.DataId[id].ToString());
                        if (WriteMSPCommand(b))
                        {
                            //写消息框
                            //WriteCommLog("收到数据校验正确。");
                            //
                        }
                        //处理数据分为特殊命令和dsp数据
                        if (id == 170)
                        {
                            MSPDataFile.OpenFile(MainForm.pMainForm.SerialRecvPathInfo);
                            string DataFilename = MSPDataFile.adfile.fileName;
                            MSPDataFile.BinaryWrite(data);
                            MSPDataFile.close();
                            WriteCommLog("收到DSP转发数据。");
                            
                            try
                            {
                                MainForm.ParseLock.WaitOne();
                                SourceDataClass.isCommDepack = true;
                                SourceDataClass.GetData(data);
                                List<string[]> ss = SourceDataClass.Parse();

                                SourceDataClass.isCommDepack = false;
                                MainForm.ParseLock.ReleaseMutex();
                            }
                            catch (Exception MyEx)
                            {
                                WriteCommLog("无法正确解析数据文件：" + MyEx.Message + MyEx.StackTrace);
                                MainForm.ParseLock.ReleaseMutex();
                                SourceDataClass.isCommDepack = false;
                                //tabControl1.SelectedIndex = 1;
                            }
                            
                            
                            AddtoList(MspSerialPort.PortName, DataFilename);
                            
                            SetDataRecvTimes();
                        }
                        else
                        {
                            string HexStr = CRCHelper.ConvertCharToHex(data, data.Length);
                            if (id == 20)//浮标返回
                                id = 12;
                            switch (id)
                            {
                                    
                                case 12://实时状态
                                    WriteCommLog("收到状态数据：");
                                    string RtcTime = HexStr.Substring(0, 4) + "年" + HexStr.Substring(4, 2) + "月"
                                        + HexStr.Substring(6, 2) + "日" + HexStr.Substring(8, 2) + "时"
                                        + HexStr.Substring(10, 2) + "分" + HexStr.Substring(12, 2) + "秒";
                                    WriteCommLog("RTC时间：" + RtcTime);
                                    string DateTimeStr = HexStr.Substring(0, 4) + "-" + HexStr.Substring(4, 2) + "-"
                                        + HexStr.Substring(6, 2) + " " + HexStr.Substring(8, 2) + ":"
                                        + HexStr.Substring(10, 2) + ":" + HexStr.Substring(12, 2);
                                    DateTime dt = Convert.ToDateTime(DateTimeStr);
                                    TimeSpan ts = dt.Subtract(DateTime.Now);
                                    if (ts.TotalSeconds < 0)
                                        WriteCommLog("RTC时间比本地时间慢" + Math.Abs(ts.TotalSeconds).ToString() + "秒");
                                    else
                                        WriteCommLog("RTC时间比本地时间快" + Math.Abs(ts.TotalSeconds).ToString() + "秒");
                                    WriteCommLog("浮标号：" + HexStr.Substring(14, 2));
                                    WriteCommLog("节点号：" + HexStr.Substring(16, 2));
                                    string sn = HexStr.Substring(18, 2);
                                    if (sn == "00")
                                        WriteCommLog("东经：" + int.Parse(HexStr.Substring(20, 4)) + "°" + int.Parse(HexStr.Substring(24, 2)) + "." + int.Parse(HexStr.Substring(26, 4)));
                                    else if (sn == "01")
                                        WriteCommLog("西经：" + int.Parse(HexStr.Substring(20, 4)) + "°" + int.Parse(HexStr.Substring(24, 2)) + "." + int.Parse(HexStr.Substring(26, 4)));
                                    else
                                        WriteCommLog("经度位置无可用数据");
                                    sn = HexStr.Substring(30, 2);
                                    if (sn == "00")
                                        WriteCommLog("北纬：" + int.Parse(HexStr.Substring(32, 2)) + "°" + int.Parse(HexStr.Substring(34, 2)) + "." + int.Parse(HexStr.Substring(36, 4)));
                                    else if (sn == "01")
                                        WriteCommLog("南纬：" + int.Parse(HexStr.Substring(32, 2)) + "°" + int.Parse(HexStr.Substring(34, 2)) + "." + int.Parse(HexStr.Substring(36, 4)));
                                    else
                                        WriteCommLog("纬度位置无可用数据");
                                    WriteCommLog("串口2设备:" + Enum.GetName(typeof(SourceDataClass.DeviceAddr), int.Parse(HexStr.Substring(40, 4))));
                                    WriteCommLog("串口3设备:" + Enum.GetName(typeof(SourceDataClass.DeviceAddr), int.Parse(HexStr.Substring(44, 4))));

                                    int emittype = int.Parse(HexStr.Substring(48, 2));
                                    if (emittype == 0)
                                        WriteCommLog("发射机类型:PWM发射机");
                                    else
                                        WriteCommLog("发射机类型:线性发射机");
                                    WriteCommLog("换能器个数:" + HexStr.Substring(50, 2));
                                    WriteCommLog("48V电流工作状态时间:");
                                    WriteCommLog("低电流时间:" + Int64.Parse(HexStr.Substring(52, 10)));
                                    WriteCommLog("中电流时间:" + Int64.Parse(HexStr.Substring(62, 10)));
                                    WriteCommLog("高电流时间:" + Int64.Parse(HexStr.Substring(72, 10)));
                                    WriteCommLog("单片机工作和休眠时间:");
                                    WriteCommLog("休眠时间:" + int.Parse(HexStr.Substring(82, 6)));
                                    WriteCommLog("工作时间:" + int.Parse(HexStr.Substring(88, 6)));
                                    WriteCommLog("电源数据:");
                                    WriteCommLog("3.3V电压值:" + (double.Parse(HexStr.Substring(94, 4)) / 1000).ToString() + "V");
                                    WriteCommLog("48V电压值:" + (double.Parse(HexStr.Substring(98, 6)) / 1000).ToString() + "V");
                                    WriteCommLog("48V电池剩余电量:" + double.Parse(HexStr.Substring(104, 8)) / 1000 + "mA*h");
                                    WriteCommLog("48V已用电量:" + double.Parse(HexStr.Substring(112, 8)) / 1000 + "mA*h");
                                    WriteCommLog("3V电池剩余电量:" + double.Parse(HexStr.Substring(120, 8)) / 1000 + "mA*h");
                                    WriteCommLog("3V已用电量:" + double.Parse(HexStr.Substring(128, 8)) / 1000 + "mA*h");
                                    int minus = int.Parse(HexStr.Substring(136, 2));
                                    if (minus == 0)
                                        WriteCommLog("温度: 零上" + (double.Parse(HexStr.Substring(138, 6)) / 1000).ToString() + "°C");
                                    else
                                        WriteCommLog("温度: 零下" + (double.Parse(HexStr.Substring(138, 6)) / 1000).ToString() + "°C");
                                    minus = int.Parse(HexStr.Substring(144, 2));
                                    if (minus == 0)
                                        WriteCommLog("喂狗开关:" + "关");
                                    else
                                        WriteCommLog("喂狗开关:" + "开");
                                    WriteCommLog("AD门限值:" + int.Parse(HexStr.Substring(146, 2)));
                                    WriteCommLog("串口2定时唤醒时间:" + int.Parse(HexStr.Substring(148, 10)) + "秒");
                                    WriteCommLog("串口3定时唤醒时间:" + int.Parse(HexStr.Substring(158, 10)) + "秒");
                                    WriteCommLog("单片机重启次数:" + int.Parse(HexStr.Substring(168, 4)));
                                    WriteCommLog("浮标工作状态:" + (HexStr.Substring(172, 2) == "00" ? "休眠" : "工作"));
                                    WriteCommLog("版本信息:" + (double.Parse(HexStr.Substring(174, 4)) / 1000).ToString("F03") + " " + HexStr.Substring(178, 4) + "年" + HexStr.Substring(182, 2) + "月"
                                        + HexStr.Substring(184, 2) + "日");
                                    
                                    break;
                                case 14://版本号。2013年1月14日之后舍弃
                                    WriteCommLog("版本信息:" + (double.Parse(HexStr.Substring(0, 4)) / 1000).ToString("F03") + " " + HexStr.Substring(4, 4) + "年" + HexStr.Substring(8, 2) + "月"
                                        + HexStr.Substring(10, 2) + "日");
                                    break;
                                case 10:
                                    WriteCommLog("通信机电量低报警！！！！！！！！！！！！");
                                    WriteCommLog("3.3V电压值:" + (double.Parse(HexStr.Substring(0, 4)) / 1000).ToString() + "V");
                                    WriteCommLog("48V电压值:" + (double.Parse(HexStr.Substring(4, 6)) / 1000).ToString() + "V");
                                    WriteCommLog("48V电池剩余电量:" + double.Parse(HexStr.Substring(10, 8)) / 1000 + "mA*h");
                                    WriteCommLog("48V已用电量:" + double.Parse(HexStr.Substring(18, 8)) / 1000 + "mA*h");
                                    WriteCommLog("3V电池剩余电量:" + double.Parse(HexStr.Substring(26, 8)) / 1000 + "mA*h");
                                    WriteCommLog("3V已用电量:" + double.Parse(HexStr.Substring(34, 8)) / 1000 + "mA*h");
                                    break;
                                case 2:
                                    WriteCommLog("DSP故障!!!");
                                    //tabControl1.SelectedIndex = 1;
                                    break;
                                case 15:
                                    WriteCommLog("休眠时间错误!!!");
                                    break;
                                case 9:
                                    WriteCommLog("漏水报警！！！！！！！！！！！");
                                    //tabControl1.SelectedIndex = 1;
                                    break;
                                default:
                                    if (HexStr.Length > 0)
                                        WriteCommLog("收到数据：" + HexStr);
                                    break;

                            }


                        }

                    }
                    else
                    {
                        string ack = "EB90,03," + BuoyID + ",N,";
                        UInt16 crc = CRCHelper.CRC16(ack);
                        byte[] bcrc = BitConverter.GetBytes(crc);
                        byte tmp = bcrc[0];
                        bcrc[0] = bcrc[1];
                        bcrc[1] = tmp;
                        //ack += Encoding.Default.GetString(bcrc);
                        ack += "  ,END";
                        byte[] b = Encoding.Default.GetBytes(ack);
                        b[13] = bcrc[0];
                        b[14] = bcrc[1];
                      
                        if (WriteMSPCommand(b))
                        {
                            //写消息框
                            WriteCommLog("收到转发数据校验错误。");
                            //
                        }
                        return;

                    }
                    

                }
            }
            catch (Exception MyEx)
            {
                MessageBox.Show(MyEx.Message + ":" + MyEx.StackTrace);
            }
            

        }

        public void MspLoaderRecvEvent(object sender, EventsClass.GpsEventArgs e)
        {
            WriteLoaderLog(e.gpsdata);
        }
        /// <summary>
        /// 读串口线程1
        /// </summary>
        public static void ReadMSP()
        {
            byte b;
            byte[] tail = new byte[3];
            List<byte> recvcmd = new List<byte>();
            while (isMspContinue)
            {
                try
                {
                    if (MspSerialPort.IsOpen)
                    {

                        b = (byte)MspSerialPort.ReadByte();

                        if (b == 0x45)//发现E
                        {
                            recvcmd.Clear();//命令列表清空
                            recvcmd.Add(b);
                            b = (byte)MspSerialPort.ReadByte();
                            if (b == 0x42)//发现B
                            {
                                recvcmd.Add(b);
                                b = (byte)MspSerialPort.ReadByte();
                                if (b == 0x39)//发现9
                                {
                                    recvcmd.Add(b);
                                    b = (byte)MspSerialPort.ReadByte();
                                    if (b == 0x30)//发现0,EB90，说明找到头了。
                                    {
                                        recvcmd.Add(b);
                                        while (true)//一直读到End
                                        {
                                            b = (byte)MspSerialPort.ReadByte();
                                            if (b == 0x45)//发现E
                                            {
                                                recvcmd.Add(b);
                                                b = (byte)MspSerialPort.ReadByte();
                                                if (b == 0x4E)//发现N
                                                {
                                                    recvcmd.Add(b);
                                                    b = (byte)MspSerialPort.ReadByte();
                                                    if (b == 0x44)//发现D
                                                    {
                                                        recvcmd.Add(b);

                                                        byte[] cmd = new byte[recvcmd.Count];
                                                        recvcmd.CopyTo(cmd, 0);
                                                        recvcmd.Clear();
                                                        EventsClass.WaveEventArgs e = new EventsClass.WaveEventArgs(cmd, cmd.Length);
                                                        MspEventHandler handler = MspEvent;
                                                        if (handler != null)
                                                        {
                                                            handler(null, e);
                                                        }
                                                        break;
                                                    }
                                                }
                                            }
                                            recvcmd.Add(b);
                                        }
                                    }
                                }
                            }
                        }

                        
                    }
                }
                catch (Exception MyEx)
                {
                    //MessageBox.Show(MyEx.Message + ":" + MyEx.StackTrace);
                    isMspContinue = false;
                }
            }

        }

        /// <summary>
        /// 读串口线程2
        /// </summary>
        public static void ReadMSPLoader()
        {
            string msg;
            
            while (IsLoaderRunning)
            {
                try
                {
                    if (MspSerialPort.IsOpen)
                    {

                        msg = MspSerialPort.ReadExisting();
                        if (msg == "")
                        {
                            Thread.Sleep(100);
                            continue;
                        }
                        EventsClass.GpsEventArgs e = new EventsClass.GpsEventArgs(msg);
                        if (MspLoaderEvent != null)
                            MspLoaderEvent(null,e);
                        Thread.Sleep(50);

                    }
                }
                catch (Exception MyEx)
                {

                    MessageBox.Show(MyEx.Message);
                    
                    IsLoaderRunning = false;
                    
                }

            }

        }
        // <summary>
        /// 通过串口发送命令
        /// </summary>
        /// <param name="str">需要发送的命令</param>
        public bool WriteMSPCommand(byte[] str)
        {
            try
            {
                if (str.Length == 0)
                    return false;
                if (MspSerialPort.IsOpen)
                {
                    MspSerialPort.Write(str, 0, str.Length);

                    return true;
                }
                else
                {
                    WriteCommLog("串口命令发送失败");
                    MessageBox.Show("串口命令发送失败！");
                }
                return false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        #endregion

        #region 接收信息属性框相关操作
        private void NetdataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0)&&(e.RowIndex<NetdataGrid.RowCount-1))
            {
                try
                {
                    
                    string strfilename = NetdataGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                    csFile cf = new csFile(strfilename);
                    cf.BinaryOpenRead();
                    FileInfo fi  = new FileInfo(strfilename);
                    byte[] d = new byte[fi.Length];
                    d = cf.br.ReadBytes((int)fi.Length);
                    cf.close();
                    Dvf.Text = NetdataGrid.Rows[e.RowIndex].Cells[0].Value.ToString() + "收到信源信息";
                    Dvf.str = CRCHelper.ConvertCharToHex(d, (int)fi.Length);
                    SourceDataClass.isNodeTick = false;
                    SourceDataClass.isShowData = true;
                    Dvf.DataViewForm_ShowData();
                    SourceDataClass.isShowData = false;
                    SourceDataClass.isNodeTick = true;
                }
                catch (Exception MyEx)
                {
                    MainForm.ParseLock.ReleaseMutex();
                    MessageBox.Show(MyEx.Message + ":" + MyEx.StackTrace.ToString());
                }
            }
            
        }
        /// <summary>
        /// 接收网络发送的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void InsertNetList(object sender, EventsClass.DataEventArgs e)
        {
 
            AddtoList(e.DataSource, e.FileName);    
        }
        /// <summary>
        /// 将信息写入表格待查，并选择最新的一行
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="msg"></param>
        private void AddtoList(string s, string filename)
        {
            if (NetdataGrid.InvokeRequired)//不是同一线程调用，调用控件的委托
            {
                AddBoxCallback d = new AddBoxCallback(AddtoList);
                this.Invoke(d, new object[] {s, filename });

            }
            else//同一线程，直接操作
            {
                string source = "节点";
                if (s.Contains("COM"))
                    source = "串口:" + source;
                foreach (int nodeid in SourceNode)
                {
                    source += nodeid.ToString() + " ";
                }
                string[] fulrow = new string[] { DateTime.Now.ToLongTimeString(), source, filename };
                NetdataGrid.Rows.Add(fulrow);
                
            }
 
        }

        //网络调试接收区域操作
        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            debuglevel = toolStripComboBox1.Text;
            RefreshUDPLog();
        }

        private void NodeComboBox_DropDown(object sender, EventArgs e)
        {
            NodeComboBox.Items.Clear();
            //读取节点
            XmlDocument xmlfile = new XmlDocument();
            xmlfile.Load(xmldoc);
            XmlNode xn = xmlfile.DocumentElement;
            xn = xn.SelectSingleNode("descendant::节点配置");
            foreach (XmlNode subnode in xn.ChildNodes)
            {
                NodeComboBox.Items.Add(subnode.Name.Replace("节点","").PadLeft(2,'0'));
            }
        }

        private void NodeComboBox_TextChanged(object sender, EventArgs e)
        {
            nodechoice = NodeComboBox.Text;
            RefreshUDPLog();
        }

        //在筛选项变化时调用
        private void RefreshUDPLog()
        {
            if (NetDebugLog.InvokeRequired)//不是同一线程调用，调用控件的委托
            {
                udpBoxCallback d = new udpBoxCallback(RefreshUDPLog);
                this.Invoke(d, new object[] { });

            }
            else//同一线程，直接操作
            {
                string nodename = NodeComboBox.Text;
                nodename = nodename.Replace("节点", "");
                if (nodename!="")
                    nodename = string.Concat("[", nodename, "]");
                string[] debuglevel = toolStripComboBox1.Text.Split('&');
                
                NetDebugLog.Items.Clear();
                mut.WaitOne();
                IEnumerator istring = DebugLog.GetEnumerator();
                while (istring.MoveNext())
                {
                    string str = (string)istring.Current;
                    for (int i = 0; i < debuglevel.Length; i++)
                    {
                        if ((str.Contains(nodename)) && (str.Contains(debuglevel[i])))
                        {
                            NetDebugLog.Items.Add(str);

                        }
                    }
                }
                NetDebugLog.SelectedIndex = NetDebugLog.Items.Count - 1;
                mut.ReleaseMutex();
            }

        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            NetdataGrid.Rows.Clear();
        }
        private void SetDataRecvTimes()
        {
            if (toolStrip2.InvokeRequired)
            {
                SetRecvTimesCallback st = new SetRecvTimesCallback(SetDataRecvTimes);
                this.Invoke(st, new object[] { });

            }
            else
            {
                DataNumCount++;
                string numstr = "收到串口转发数据" + DataNumCount.ToString() + "个";
                RecvDataTimes.Text = numstr;
            }
        }
        private void RecvDataTimes_DoubleClick(object sender, EventArgs e)
        {
            DataNumCount = 0;
            RecvDataTimes.Text = "---";
        }
        private void 清空显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NetdataGrid.Rows.Clear();
        }



        private void PlayBackBtn_Click(object sender, EventArgs e)
        {
            if (PlayBackBtn.Text == "回放")
            {
                if (OpenMspFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        playbacksr = new StreamReader(OpenMspFile.OpenFile(),Encoding.Default);
                        //PlayBackTimer.Enabled = true;
                        play.Enabled = true;
                        //PlayBackBtn.Text = "停止";
                    }
                    catch (Exception MyEx)
                    {

                        MessageBox.Show(MyEx.Message);
                    }
                }
            }
            else
            {
                PlayBackBtn.Text = "回放";
                PlayBackTimer.Stop();
                play.Enabled = false;
                pause.Enabled = false;
            }
        }

        #endregion

        #region 系统记录相关
        /// <summary>
        /// 写系统信息记录，并记录到文件中
        /// </summary>
        /// <param name="msg"></param>
        public void WriteLog(string msg)
        {
            try
            {
                if (LogBox.InvokeRequired)//不是同一线程调用，调用控件的委托
                {

                    WriteLogCallback d = new WriteLogCallback(WriteLog);
                    this.Invoke(d, new object[] { msg });

                }
                else//同一线程，直接操作
                {
                    if (LogBox.Lines.Length > 300)
                    {
                        string[] s = LogBox.Lines;
                        Array.Reverse(s);
                        Array.Resize<string>(ref s, 200);
                        Array.Reverse(s);
                        LogBox.Lines = s;
                    }
                    string time = "(" + DateTime.Now.Month.ToString("00", CultureInfo.InvariantCulture) + "/" + DateTime.Now.Day.ToString("00", CultureInfo.InvariantCulture) + " " + DateTime.Now.Hour.ToString("00", CultureInfo.InvariantCulture)
                        + ":" + DateTime.Now.Minute.ToString("00", CultureInfo.InvariantCulture) + ":" + DateTime.Now.Second.ToString("00", CultureInfo.InvariantCulture) + ")";
                    LogBox.AppendText(time + msg + "\r\n");
                    LogBox.ScrollToCaret();
                    //tabControl1.SelectedIndex = 0;
                    if (SystemLog.logfile.ws == null)//还未创建文件
                        SystemLog.OpenFile(MainForm.pMainForm.RecordInfo);
                    SystemLog.writeLine(msg);
                    if (SystemLog.length >= 1024 * 1024)//不允许大于1M
                    {
                        SystemLog.close();
                    }
                }
            }
            catch (Exception e)
            {
                //do nothing ,因此程序已退出
            }
        }
        /// <summary>
        /// 写串口信息记录，并记录到文件中
        /// </summary>
        /// <param name="msg"></param>
        public void WriteCommLog(string msg)
        {
            try
            {
                if (CommLogBox.InvokeRequired)//不是同一线程调用，调用控件的委托
                {

                    WriteCommLogCallback d = new WriteCommLogCallback(WriteCommLog);
                    this.Invoke(d, new object[] { msg });

                }
                else//同一线程，直接操作
                {
                    if (CommLogBox.Lines.Length > 300)
                    {
                        string[] s = CommLogBox.Lines;
                        Array.Reverse(s);
                        Array.Resize<string>(ref s, 200);
                        Array.Reverse(s);
                        CommLogBox.Lines = s;
                    }
                    string time = "(" + DateTime.Now.Month.ToString("00", CultureInfo.InvariantCulture) + "/" + DateTime.Now.Day.ToString("00", CultureInfo.InvariantCulture) + " " + DateTime.Now.Hour.ToString("00", CultureInfo.InvariantCulture)
                        + ":" + DateTime.Now.Minute.ToString("00", CultureInfo.InvariantCulture) + ":" + DateTime.Now.Second.ToString("00", CultureInfo.InvariantCulture) + ")";
                    CommLogBox.AppendText(time + msg + "\r\n");
                    CommLogBox.ScrollToCaret();
                    //tabControl1.SelectedIndex = 1;
                    
                    if (SerialLog.logfile.ws == null)//还未创建文件
                        SerialLog.OpenFile(MainForm.pMainForm.RecordInfo);
                    SerialLog.writeLine(msg);
                    if (SerialLog.length >= 1024 * 1024)//不允许大于1M
                    {
                        SerialLog.close();
                    }
                }
            }
            catch (Exception e)
            {
                //do nothing ,因此程序已退出
            }
        }

        /// <summary>
        /// 写串口信息记录，并记录到文件中
        /// </summary>
        /// <param name="msg"></param>
        public void WriteLoaderLog(string msg)
        {
            try
            {
                if (CommLogBox.InvokeRequired)//不是同一线程调用，调用控件的委托
                {

                    WriteCommLogCallback d = new WriteCommLogCallback(WriteLoaderLog);
                    this.Invoke(d, new object[] { msg });

                }
                else//同一线程，直接操作
                {
                    if (CommLogBox.Lines.Length > 300)
                    {
                        string[] s = CommLogBox.Lines;
                        Array.Reverse(s);
                        Array.Resize<string>(ref s, 200);
                        Array.Reverse(s);
                        CommLogBox.Lines = s;
                    }
                    //string time = "(" + DateTime.Now.Month.ToString("00", CultureInfo.InvariantCulture) + "/" + DateTime.Now.Day.ToString("00", CultureInfo.InvariantCulture) + " " + DateTime.Now.Hour.ToString("00", CultureInfo.InvariantCulture)
                    //    + ":" + DateTime.Now.Minute.ToString("00", CultureInfo.InvariantCulture) + ":" + DateTime.Now.Second.ToString("00", CultureInfo.InvariantCulture) + ")";
                    CommLogBox.AppendText(msg);
                    CommLogBox.ScrollToCaret();
                    //tabControl1.SelectedIndex = 1;
                    SimpleCommBox.Focus();
                    if (SerialLog.logfile.ws == null)//还未创建文件
                        SerialLog.OpenFile(MainForm.pMainForm.RecordInfo);
                    SerialLog.writeLine(msg);
                    if (SerialLog.length >= 1024 * 1024)//不允许大于1M
                    {
                        SerialLog.close();
                    }
                }
            }
            catch (Exception e)
            {
                //do nothing ,因此程序已退出
            }
        }

        /// <summary>
        /// 写网络信息记录，不用记录，因为在网络线程里已经有记录了
        /// </summary>
        /// <param name="msg"></param>
        public void WriteNetLog(string msg)
        {
            try
            {
                if (NetLogBox.InvokeRequired)//不是同一线程调用，调用控件的委托
                {

                    WriteNetLogCallback d = new WriteNetLogCallback(WriteNetLog);
                    this.Invoke(d, new object[] { msg });

                }
                else//同一线程，直接操作
                {
                    if (NetLogBox.Lines.Length > 1000)
                    {
                        string[] s = NetLogBox.Lines;
                        Array.Reverse(s);
                        Array.Resize<string>(ref s, 800);
                        Array.Reverse(s);
                        NetLogBox.Lines = s;
                    }
                    //string time = "(" + DateTime.Now.Month.ToString("00", CultureInfo.InvariantCulture) + "/" + DateTime.Now.Day.ToString("00", CultureInfo.InvariantCulture) + " " + DateTime.Now.Hour.ToString("00", CultureInfo.InvariantCulture)
                    //    + ":" + DateTime.Now.Minute.ToString("00", CultureInfo.InvariantCulture) + ":" + DateTime.Now.Second.ToString("00", CultureInfo.InvariantCulture) + ")";
                    NetLogBox.AppendText(msg );
                    NetLogBox.ScrollToCaret();
                    //tabControl1.SelectedIndex = 3;
                    SimpleCommandList.Focus();
                }
            }
            catch (Exception e)
            {
                //do nothing ,因此程序已退出
            }
        }
        /// <summary>
        /// 根据参数形成记录语句，调用WriteLog写入log对话和文件。
        /// </summary>
        public void MsgLog(MsgMode mode,int id,  string source, string dest,string desc)
        {
            string log;
            switch (mode)
            {
                case MsgMode.NetCmd://网络命令
                    log = source + "经网络发送命令给节点(" + dest + "):" + "(ID:" + id.ToString() + ") " + desc;
                    WriteNetLog(log);
                    break;
                case MsgMode.RecvNetData://网络收到数据
                    log = dest + "经网络收到节点(" + source + ")" + "数据:" + "(ID:" + id.ToString() + ") " + desc;
                    WriteNetLog(log);
                    break;
                case MsgMode.RecvSerialData://串口收到数据
                    log = dest + "经串口收到" + source + "数据:" + "(ID:" + id.ToString() + ") " + desc;
                    WriteCommLog(log);
                    break;
                case MsgMode.SerialCmd://串口命令
                    log = source + "经串口发送命令" + dest + ":" + "(ID:" + id.ToString() + ") " + desc;
                    WriteCommLog(log);
                    break;
                default:
                    break;
            }

        }
        #endregion
        

        #region 430特殊命令
        public void ClearRebootBtn_Click(object sender, EventArgs e)
        {
            string message = "是否需要清零单片机重启次数";
            string caption = "特殊命令";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            MessageBoxIcon icon = MessageBoxIcon.Question;
            MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button1;
            // Show message box
            DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
            if (result == DialogResult.OK)
            {
                byte[] cmd = new byte[1];
                cmd[0] = 0xF3;
                byte[] b = SourceDataClass.CommPackage(243,cmd);
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 243, "上位机", "MSP430", (string)SourceDataClass.DataId[243]);

                }
                
            }
            
        }

        private void Configure_Click(object sender, EventArgs e)
        {
            try
            {
                ConfForm cf = new ConfForm();
                cf.Text = "系统配置";
                if (cf.ShowDialog() == DialogResult.OK)
                {
                    string cmd;
                    cmd = cf.BuoyName.Text.PadLeft(2, '0');
                    cmd += cf.NodeName.Text.PadLeft(2, '0');
                    string sagn = cf.langInput.Text.Substring(0,1);
                    if(sagn =="-")
                    {
                        sagn = "01";
                    }
                    else
                    {
                        sagn = "00";
                    }
                    cmd += sagn;
                    string lang =  cf.langInput.Text.TrimStart('-');
                    double langd = double.Parse(lang);
                    string[] splitlang = lang.Split('.');
                    cmd += splitlang[0].PadLeft(4, '0');
                    cmd += ((langd - double.Parse(splitlang[0]))*60).ToString("F04").Replace(".","").PadLeft(6,'0');
                    sagn = cf.latinput.Text.Substring(0, 1);
                    if (sagn == "-")
                    {
                        sagn = "01";
                    }
                    else
                    {
                        sagn = "00";
                    }
                    cmd += sagn;
                    string lat = cf.latinput.Text.TrimStart('-');
                    double latd = double.Parse(lat);
                    string[] splitlat = lat.Split('.');
                    cmd += splitlat[0].PadLeft(2, '0');
                    cmd += ((latd - double.Parse(splitlat[0])) * 60).ToString("F04").Replace(".", "").PadLeft(6,'0');
                    int value = Convert.ToInt32(Enum.Parse(typeof(SourceDataClass.DeviceAddr), cf.COMM2Set.Text));
                    cmd += value.ToString().PadLeft(4, '0');
                    value = Convert.ToInt32(Enum.Parse(typeof(SourceDataClass.DeviceAddr), cf.COMM3Set.Text));
                    cmd += value.ToString().PadLeft(4, '0');
                    value = Convert.ToInt32(Enum.Parse(typeof(SourceDataClass.EmitType), cf.EmitTypeBox.Text));
                    cmd += value.ToString().PadLeft(2, '0');
                    value = cf.NetCheck.SelectedIndex;
                    cmd += value.ToString().PadLeft(2, '0');
                    cmd += cf.EmitNum.Text.PadLeft(2, '0');
                    cmd += (cf.NodeType.Text=="静态节点")?"00":"01";
                    cmd += cf.AccessMode.Text.PadLeft(2, '0'); ;
                    cmd = cmd.Replace(" ", "0");
                    byte[] bytecmd = CRCHelper.ConvertHexToChar(cmd);
                    //
                    if (bytecmd != null)
                    {
                        byte[] b = SourceDataClass.CommPackage(253, bytecmd);
                        if (WriteMSPCommand(b))
                        {
                            MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                            MSPCmdFile.BinaryWrite(b);
                            MSPCmdFile.close();
                            MsgLog(MsgMode.SerialCmd, 253, "上位机", "MSP430", (string)SourceDataClass.DataId[253]);
                        }
                    }
                }
                cf.Dispose();
            }
            catch (Exception MyEx)
            {
                MessageBox.Show(MyEx.Message + ":" + MyEx.StackTrace);
            }

        }

        private void SetTime_Click(object sender, EventArgs e)
        {
            SetTimeForm stf = new SetTimeForm();
            stf.Text = "设置系统时间";
            if (stf.ShowDialog() == DialogResult.OK)
            {
                
                string settime = stf.dateTimeInput.Value.Year.ToString("0000", CultureInfo.InvariantCulture) + stf.dateTimeInput.Value.Month.ToString("00", CultureInfo.InvariantCulture) + stf.dateTimeInput.Value.Day.ToString("00", CultureInfo.InvariantCulture) + stf.dateTimeInput.Value.Hour.ToString("00", CultureInfo.InvariantCulture)
                    + stf.dateTimeInput.Value.Minute.ToString("00", CultureInfo.InvariantCulture) + stf.dateTimeInput.Value.Second.ToString("00", CultureInfo.InvariantCulture);

                byte[] time = CRCHelper.ConvertHexToChar(settime);
                byte[] cmd = new byte[time.Length];

                Buffer.BlockCopy(time,0, cmd, 0, time.Length);
                byte[] b = SourceDataClass.CommPackage(252, cmd);
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 252, "上位机", "MSP430", (string)SourceDataClass.DataId[252] + ":" +stf.dateTimeInput.Value.ToLongTimeString());
                }
                
            }
            stf.Dispose();
        }

        private void SetAlarm_Click(object sender, EventArgs e)
        {
            SetTimeForm stf = new SetTimeForm();
            stf.Text = "休眠设置";
            if (stf.ShowDialog() == DialogResult.OK)
            {
                string settime = stf.dateTimeInput.Value.Year.ToString("0000", CultureInfo.InvariantCulture) + stf.dateTimeInput.Value.Month.ToString("00", CultureInfo.InvariantCulture) + stf.dateTimeInput.Value.Day.ToString("00", CultureInfo.InvariantCulture) + stf.dateTimeInput.Value.Hour.ToString("00", CultureInfo.InvariantCulture)
                    + stf.dateTimeInput.Value.Minute.ToString("00", CultureInfo.InvariantCulture) + stf.dateTimeInput.Value.Second.ToString("00", CultureInfo.InvariantCulture);
                byte[] time = CRCHelper.ConvertHexToChar(settime);
                byte[] cmd = new byte[time.Length];
                Buffer.BlockCopy(time, 0, cmd, 0, time.Length);
                byte[] b = SourceDataClass.CommPackage(251, cmd);
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 251, "上位机", "MSP430", (string)SourceDataClass.DataId[251] + ":" + stf.dateTimeInput.Value.Date.ToString());
                }

            }
            stf.Dispose();
        }

        private void ReadStat_Click(object sender, EventArgs e)
        {
            string message = "是否需要获取MSP430状态信息";
            string caption = "特殊命令";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            MessageBoxIcon icon = MessageBoxIcon.Question;
            MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button1;
            // Show message box
            DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
            if (result == DialogResult.OK)
            {
                byte[] cmd = new byte[1];
                cmd[0] = 0xF7;
                byte[] b = SourceDataClass.CommPackage(247, cmd);
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 247, "上位机", "MSP430", (string)SourceDataClass.DataId[247]);

                }

            }
        }

        private void OpenDspDog_Click(object sender, EventArgs e)
        {
            string message = "设DSP喂狗开关状态为打开？";
            string caption = "特殊命令";
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            MessageBoxIcon icon = MessageBoxIcon.Question;
            MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button1;
            // Show message box
            DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
            if (result == DialogResult.Yes)
            {
                byte[] cmd = new byte[1];
                cmd[0] = 0x01;
                byte[] b = SourceDataClass.CommPackage(246,cmd);
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 246, "上位机", "MSP430", (string)SourceDataClass.DataId[246] + " 开");
                }
               
            }
            else if (result == DialogResult.No)
            {
                byte[] cmd = new byte[1];
                cmd[0] = 0x00;
                byte[] b = SourceDataClass.CommPackage(246,cmd);
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 246, "上位机", "MSP430", (string)SourceDataClass.DataId[246] + " 关");
                }
               
            }
        }

        private void OpenDsp_Click(object sender, EventArgs e)
        {
            string message = "是否确定给DSP上电";
            string caption = "特殊命令";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            MessageBoxIcon icon = MessageBoxIcon.Question;
            MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button1;
            // Show message box
            DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
            if (result == DialogResult.OK)
            {
                byte[] cmd = new byte[1];
                cmd[0] =0xF4;//空命令传ID，有数据传数据
                byte[] b = SourceDataClass.CommPackage(244,cmd);
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 244, "上位机", "MSP430", (string)SourceDataClass.DataId[244]);
                }
                
            }
        }


        private void OpenDspNet_Click(object sender, EventArgs e)
        {
            string message = "打开或关闭DSP调试开关？";
            string caption = "特殊命令";
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            MessageBoxIcon icon = MessageBoxIcon.Question;
            MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button1;
            // Show message box
            DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
            if (result == DialogResult.Yes)
            {
                byte[] cmd = new byte[1];
                cmd[0] = 0x01;
                byte[] b = SourceDataClass.CommPackage(250, cmd);
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 250, "上位机", "MSP430", (string)SourceDataClass.DataId[250] + " 开");
                }

            }
            else if (result == DialogResult.No)
            {
                byte[] cmd = new byte[1];
                cmd[0] = 0x00;
                byte[] b = SourceDataClass.CommPackage(250, cmd);
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 250, "上位机", "MSP430", (string)SourceDataClass.DataId[250] + " 关");
                }

            }
        }
        

        

        private void SetAD_Click(object sender, EventArgs e)
        {
            SetADLimiteForm sadf = new SetADLimiteForm();
            sadf.ShowDialog();
            if (sadf.DialogResult == DialogResult.OK)
            {
                byte[] cmd = new byte[1];
                string ad = sadf.ConfBtn.Text;
                cmd[0] = Convert.ToByte(ad, 16); ;
                byte[] b = SourceDataClass.CommPackage(255, cmd);
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 255, "上位机", "MSP430", (string)SourceDataClass.DataId[255]);
                }
            }
            sadf.Dispose();

        }
        /// <summary>
        /// 返回数据的固定长度字符串，如2.34，固定长度为5，小数点位置在3，则返回00234，小数点位置在2，则返回02340
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dotindex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private string GetFormedString(string src, int dotindex, int length)
        {
            int SrcDotIndex = src.IndexOf('.');
            src = src.Replace(".", "");
            if (SrcDotIndex > dotindex)
            {
                throw (new Exception("GetFormedString输入格式错误！"));
            }
            int move = dotindex - SrcDotIndex;
            src = src.PadLeft(src.Length + move, '0');
            src = src.PadRight(length, '0');
            return src;

        }
        private void WriteEnergyStat_Click(object sender, EventArgs e)
        {
            try
            {
                EnergyForm ef = new EnergyForm();
                ef.ShowDialog();
                if (ef.DialogResult == DialogResult.OK)
                {
                    string cmd = ef.LowTime.Text.PadLeft(10, '0') + ef.medtime.Text.PadLeft(10, '0') +
                        ef.hightime.Text.PadLeft(10, '0') + ef.waketime.Text.PadLeft(6, '0') +
                        ef.worktime.Text.PadLeft(6, '0') + GetFormedString(ef.Votage33.Text.Replace(' ','0'),1,4) +
                        GetFormedString(ef.Votage48.Text.Replace(' ', '0'), 3, 6) + GetFormedString(ef.V48left.Text.Replace(' ', '0'),5,8) +
                        GetFormedString(ef.V48used.Text.Replace(' ', '0'), 5, 8) + GetFormedString(ef.V3left.Text.Replace(' ', '0'),5,8) +
                        GetFormedString(ef.V3used.Text.Replace(' ', '0'),5,8);
                    cmd = cmd.Replace(" ", "0");
                    byte[] bytecmd = CRCHelper.ConvertHexToChar(cmd);
                    byte[] b = SourceDataClass.CommPackage(254, bytecmd);
                    if (WriteMSPCommand(b))
                    {
                        MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                        MSPCmdFile.BinaryWrite(b);
                        MSPCmdFile.close();
                        MsgLog(MsgMode.SerialCmd, 254, "上位机", "MSP430", (string)SourceDataClass.DataId[254]);
                    }
                }
                ef.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void Reset430_Click(object sender, EventArgs e)
        {
            string message = "是否重启430单片机";
            string caption = "特殊命令";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            MessageBoxIcon icon = MessageBoxIcon.Question;
            MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button1;
            // Show message box
            DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
            if (result == DialogResult.OK)
            {
                byte[] cmd = new byte[1];
                cmd[0] = 0xF9;//空命令
                byte[] b = SourceDataClass.CommPackage(249, cmd);
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 249, "上位机", "MSP430", (string)SourceDataClass.DataId[249]);
                }

            }
        }

        private void SetWakeBtn_Click(object sender, EventArgs e)
        {
            SetWakeForm swf = new SetWakeForm();
            if (swf.ShowDialog() == DialogResult.OK)
            {
                string cmd = swf.Comm2Wake.Text.PadLeft(10, '0') + swf.Comm3Wake.Text.PadLeft(10, '0');
                cmd = cmd.Replace(" ", "0");
                byte[] b = SourceDataClass.CommPackage(248, CRCHelper.ConvertHexToChar(cmd));
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 248, "上位机", "MSP430", (string)SourceDataClass.DataId[248]);
                }
            }
            swf.Dispose();
        }
        private void ShutDownDSP_Click(object sender, EventArgs e)
        {
            string message = "是否关闭DSP";
            string caption = "特殊命令";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            MessageBoxIcon icon = MessageBoxIcon.Question;
            MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button1;
            // Show message box
            DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
            if (result == DialogResult.OK)
            {
                byte[] cmd = new byte[1];
                cmd[0] = 0xF5;//空命令
                byte[] b = SourceDataClass.CommPackage(245, cmd);
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 245, "上位机", "MSP430", (string)SourceDataClass.DataId[245]);
                }

            }
        }
        private void DSPloader_Click(object sender, EventArgs e)
        {
            string message = "DSP是否要进入Loader模式";
            string caption = "特殊命令";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            MessageBoxIcon icon = MessageBoxIcon.Question;
            MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button1;
            // Show message box
            DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
            if (result == DialogResult.OK)
            {
                byte[] cmd = new byte[1];
                cmd[0] = 0xF2;//空命令
                byte[] b = SourceDataClass.CommPackage(242, cmd);
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 242, "上位机", "MSP430", (string)SourceDataClass.DataId[242]);
                }

            }
        }
        //浮标命令
        private void BUOYLoader_Click(object sender, EventArgs e)
        {
            string message = MainForm.pMainForm.BuoyChoice.SelectedItem.ToString() + "DSP是否要进入Loader模式";
            string caption = "浮标命令";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            MessageBoxIcon icon = MessageBoxIcon.Question;
            MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button1;
            // Show message box
            DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
            if (result == DialogResult.OK)
            {
                byte[] cmd = new byte[1];
                cmd[0] = 17;//空命令
                byte[] b = SourceDataClass.CommPackage(17, cmd);
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 17, "上位机", "MSP430", (string)SourceDataClass.DataId[17]);
                }

            }
        }
        private void ShutDownBuOYDSP_Click(object sender, EventArgs e)
        {
            string message = MainForm.pMainForm.BuoyChoice.SelectedItem.ToString() + "关闭DSP?";
            string caption = "浮标命令";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            MessageBoxIcon icon = MessageBoxIcon.Question;
            MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button1;
            // Show message box
            DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
            if (result == DialogResult.OK)
            {
                byte[] cmd = new byte[1];
                cmd[0] = 19;//空命令
                byte[] b = SourceDataClass.CommPackage(19, cmd);
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 19, "上位机", "MSP430", (string)SourceDataClass.DataId[19]);
                }

            }
        }
        private void BUOY_state_Click(object sender, EventArgs e)
        {
            string message = MainForm.pMainForm.BuoyChoice.SelectedItem.ToString() + "浮标是否要进入工作模式?" + "\r\n" + "进入工作模式选是，休眠选否";
            string caption = "浮标命令";
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            MessageBoxIcon icon = MessageBoxIcon.Question;
            MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button1;
            // Show message box
            DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
            if (result == DialogResult.Yes)
            {
                byte[] cmd = new byte[1];
                cmd[0] = 1;//
                byte[] b = SourceDataClass.CommPackage(16, cmd);
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 16, "上位机", "MSP430", (string)SourceDataClass.DataId[16] + ":浮标工作");
                }
            }
            else if (result == DialogResult.No)
            {
                byte[] cmd = new byte[1];
                cmd[0] = 0;//
                byte[] b = SourceDataClass.CommPackage(16, cmd);
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 16, "上位机", "MSP430", (string)SourceDataClass.DataId[16] + ":浮标休眠");
                }
            }
        }

        private void DSP_DEBUGMode_Click(object sender, EventArgs e)
        {
            string message = MainForm.pMainForm.BuoyChoice.SelectedItem.ToString() + "DSP是否要进入调试模式:" + "\r\n" + "进入调试模式选是，关闭网络选否"; ;
            string caption = "浮标命令";
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            MessageBoxIcon icon = MessageBoxIcon.Question;
            MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button1;
            // Show message box
            DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
            if (result == DialogResult.Yes)
            {
                byte[] cmd = new byte[1];
                cmd[0] = 1;//空命令
                byte[] b = SourceDataClass.CommPackage(18, cmd);
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 18, "上位机", "MSP430", (string)SourceDataClass.DataId[18]+":打开网络");
                }

            }
            else if (result == DialogResult.No)
            {
                byte[] cmd = new byte[1];
                cmd[0] = 0;//
                byte[] b = SourceDataClass.CommPackage(18, cmd);
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 18, "上位机", "MSP430", (string)SourceDataClass.DataId[18] + ":关闭网络");
                }
            }
        }
        private void ReadBUOY430Stat_Click(object sender, EventArgs e)
        {
            string message = MainForm.pMainForm.BuoyChoice.SelectedItem.ToString() + "读取状态?";
            string caption = "浮标命令";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            MessageBoxIcon icon = MessageBoxIcon.Question;
            MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button1;
            // Show message box
            DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
            if (result == DialogResult.OK)
            {
                byte[] cmd = new byte[1];
                cmd[0] = 20;//空命令
                byte[] b = SourceDataClass.CommPackage(20, cmd);
                if (WriteMSPCommand(b))
                {
                    MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MSPCmdFile.BinaryWrite(b);
                    MSPCmdFile.close();
                    MsgLog(MsgMode.SerialCmd, 20, "上位机", "MSP430", (string)SourceDataClass.DataId[20]);
                }

            }
        }
        #endregion

        #region 调试栏操作
        private void databar_Closing(object sender, BarClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void MapForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void SimpleCommandList_KeyDown(object sender, KeyEventArgs e)
        {
            
            char cesc = (char)27;
            try
            {
                if (MainForm.pMainForm.CommandLineWin.Tclient != null)
                {
                    if (MainForm.pMainForm.CommandLineWin.Tclient.Client != null && MainForm.pMainForm.CommandLineWin.Tclient.Connected)
                    {
                        if (e.Control && e.KeyCode == Keys.D)
                        {
                            MainForm.pMainForm.CommandLineWin.ExecCommand(cesc.ToString());
                            //MainForm.pMainForm.CommandLineWin.WaveFile.close();
                            //MainForm.pMainForm.CommandLineWin.ch1AdFile.close();
                            //MainForm.pMainForm.CommandLineWin.ch2AdFile.close();
                            //MainForm.pMainForm.CommandLineWin.ch3AdFile.close();
                            //MainForm.pMainForm.CommandLineWin.ch4AdFile.close();
                            if (MainForm.pMainForm.CommandLineWin.ACPacketHandle == null)
                                MainForm.pMainForm.CommandLineWin.ACPacketHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
                            MainForm.pMainForm.CommandLineWin.ACPacketHandle.Set();
                            MainForm.pMainForm.CommandLineWin.DLoadDataWorker.CancelAsync();
                        }
                        if (e.KeyCode == Keys.Up)
                        {
                            ComListIndex--;
                            if (ComListIndex < 0)
                                ComListIndex = 0;
                            SimpleCommandList.Text = ComList[ComListIndex];

                        }
                        if (e.KeyCode == Keys.Down)
                        {
                            ComListIndex++;
                            if (ComListIndex > ComList.Count - 1)
                            {
                                ComListIndex = ComList.Count - 1;
                            }
                            SimpleCommandList.Text = ComList[ComListIndex];

                        }
                        if (e.KeyCode == Keys.Enter)
                        {
                            if (MainForm.pMainForm.CommandLineWin.ExecCommand(SimpleCommandList.Text))
                            {
                                if (ComList.Count == 20)
                                    ComList.RemoveAt(0);
                                ComList.Add(SimpleCommandList.Text);
                                ComListIndex = ComList.Count;
                                SimpleCommandList.Text = "";
                            }
                        }
                    }
                }
            }
            catch(NullReferenceException Ex)
            {   
                //do nothing 
            }
            

        }

        private void toolStripButton_Click(object sender, EventArgs e)
        {
            NetDebugLog.Items.Clear();
            DebugLog.Clear();
        }

        private void label14_Click(object sender, EventArgs e)
        {

            MainForm.pMainForm.CommandLineWin.ConnNodeBtn.PerformClick();  
            
        }

        private void Nettimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (CommLineForm.bConnect == true)
                    label14.Text = "已连接" + MainForm.pMainForm.CommandLineWin.Tclient.Client.RemoteEndPoint.ToString().Split(':')[0];
                else
                    label14.Text = MainForm.pMainForm.CommandLineWin.ConnNodeBtn.Text;
            }
            catch
            {
                label14.Text = MainForm.pMainForm.CommandLineWin.ConnNodeBtn.Text;
            }
                
        }

        private void SimpleCommBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (MspSerialPort.IsOpen)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (WriteMSPCommand(CRCHelper.ConvertHexToChar(CRCHelper.ConvertStrToHex(SimpleCommBox.Text))))
                    {

                        SimpleCommBox.Text = "";
                    }
                }
            }
            
        }


        private void BurnStat_CheckedChanged(object sender, EventArgs e)
        {
            if (MspSerialPort.IsOpen)
            {
                if (BurnStat.Checked)
                {
                    SimpleCommBox.Enabled = true;
                    AddFile.Enabled = true;
                    if (readMspThread != null)
                    {
                        readMspThread.Suspend();
                    }
                    if (readMspLoaderThread == null)
                    {
                        readMspLoaderThread = new Thread(ReadMSPLoader);
                        readMspThread.Priority = ThreadPriority.AboveNormal;
                        readMspLoaderThread.Start();
                    }
                    else
                    {
                        readMspLoaderThread.Resume();
                    }
                }
                else
                {
                    SimpleCommBox.Enabled = false;
                    AddFile.Enabled = false;
                    if (readMspLoaderThread != null)
                        readMspLoaderThread.Suspend();
                    if (readMspThread == null)
                    {
                        readMspThread = new Thread(ReadMSP);
                        readMspThread.Priority = ThreadPriority.Highest;
                        readMspThread.Start();
                    }
                    else
                    {
                        readMspThread.Resume();
                        
                    }
                }
            }
        }

        private void AddFile_Click(object sender, EventArgs e)
        {
            if (OpenMspFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader sr = new StreamReader(OpenMspFile.OpenFile());
                    if (MspSerialPort.IsOpen)
                    {
                        string block = sr.ReadToEnd();
                        EventsClass.GpsEventArgs str = new EventsClass.GpsEventArgs(block);
                        Thread newThread = new Thread(WriteExec);
                        newThread.Start(str);
                        
                    }
                    sr.Close();
                }
                catch (Exception MyEx)
                {

                    MessageBox.Show(MyEx.Message);
                }
            }
        }
        /// <summary>
        /// 读串口线程2
        /// </summary>
        public static void WriteExec(Object e)
        {
            EventsClass.GpsEventArgs str = (EventsClass.GpsEventArgs)e;
            string exec = str.gpsdata;
           
                try
                {
                    if (MspSerialPort.IsOpen)
                    {
                        MspSerialPort.Write(exec);
                    }
                }
                catch (Exception MyEx)
                {

                    MessageBox.Show(MyEx.Message);

                   

                }



        }
        private void databar_MouseDoubleClick(object sender, MouseEventArgs e)
        {

                databar.AutoHide = false;

        }

        #endregion


        #region DSP命令
        private void GetNeibor_Click(object sender, EventArgs e)
        {
            GetInfoForm Gif = new GetInfoForm();
            Gif.Text = "获取邻节点信息";
            if (Gif.ShowDialog() == DialogResult.OK)
            { }
            Gif.Dispose();
        }

        private void GetRoute_Click(object sender, EventArgs e)
        {
            GetInfoForm Gif = new GetInfoForm();
            Gif.Text = "获取路由信息";
            if (Gif.ShowDialog() == DialogResult.OK)
            { }
            Gif.Dispose();
        }
        private void SetNodeInfo_Click(object sender, EventArgs e)
        {
            NodeInfoForm nif = new NodeInfoForm();
            nif.ShowDialog();
            if (nif.DialogResult == DialogResult.OK)
            {

            }
            nif.Dispose();
        }


        private void SetRouter_Click(object sender, EventArgs e)
        {
            SetRouteForm srf = new SetRouteForm(this);
            srf.ShowDialog();
            if (srf.DialogResult == DialogResult.OK)
            {

            }
            //srf.Dispose();
        }

        private void GetInfo_Click(object sender, EventArgs e)
        {
            GetInfoForm Gif = new GetInfoForm();
            Gif.Text = "获取节点信息";
            if (Gif.ShowDialog() == DialogResult.OK)
            { }
            Gif.Dispose();

        }

        

        private void GetTDData_Click(object sender, EventArgs e)
        {
            GetInfoForm Gif = new GetInfoForm();
            Gif.Text = "获取设备数据";
            if (Gif.ShowDialog() == DialogResult.OK)
            { }
            Gif.Dispose();
        }

        private void 获取设备状态_Click(object sender, EventArgs e)
        {
            GetInfoForm Gif = new GetInfoForm();
            Gif.Text = "获取设备状态";
            if (Gif.ShowDialog() == DialogResult.OK)
            { }
            Gif.Dispose();
        }


        private void GetInfoList_Click(object sender, EventArgs e)
        {
            GetInfoForm Gif = new GetInfoForm();
            Gif.Text = "获取节点信息表";
            if (Gif.ShowDialog() == DialogResult.OK)
            { }
            Gif.Dispose();
        }

        private void GetSimpleGrid_Click(object sender, EventArgs e)
        {
            GetInfoForm Gif = new GetInfoForm();
            Gif.Text = "获取网络简表";
            if (Gif.ShowDialog() == DialogResult.OK)
            { }
            Gif.Dispose();
        }

        private void CommTypeSet_Click(object sender, EventArgs e)
        {
            CommTypeForm ctf = new CommTypeForm();
            if (ctf.ShowDialog() == DialogResult.OK)
            { }
            ctf.Dispose();
        }
        private void GetGrid_Click(object sender, EventArgs e)
        {
            GetInfoForm Gif = new GetInfoForm();
            Gif.Text = "获取网络表";
            if (Gif.ShowDialog() == DialogResult.OK)
            { }
            Gif.Dispose();
        }

        

        private void CirclePing_Click(object sender, EventArgs e)
        {
            SourceDataClass.isShowCircle = true;
            pf.Show();
            pf.BringToFront();
        }

        private void DataEmitSet_Click(object sender, EventArgs e)
        {
            DataBackSetForm dbf = new DataBackSetForm();
            if (dbf.ShowDialog() == DialogResult.OK)
            { }
            dbf.Dispose();
        }

        private void EmitSet_Click(object sender, EventArgs e)
        {
            EmitSetForm esf = new EmitSetForm();
            if (esf.ShowDialog() == DialogResult.OK)
            { }
            esf.Dispose();
        }

        private void GetNodeStat_Click(object sender, EventArgs e)
        {
            GetInfoForm Gif = new GetInfoForm();
            Gif.Text = "获取节点状态";
            if (Gif.ShowDialog() == DialogResult.OK)
            { }
            Gif.Dispose();
        }

        private void SetDeviceParameter_Click(object sender, EventArgs e)
        {
            SetParameterForm spf = new SetParameterForm();
            if (spf.ShowDialog() == DialogResult.OK)
            { }
            spf.Dispose();
        }

        private void GridReset_Click(object sender, EventArgs e)
        {
            string message = "是否确定全网复位";
            string caption = "网络命令";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            MessageBoxIcon icon = MessageBoxIcon.Question;
            MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button1;
            // Show message box
            DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
            if (result == DialogResult.OK)
            {
                int[] dat = new int[1];
                SourceDataClass.InitForPack(20);
                dat[0] = 200;
                SourceDataClass.OutPutIntBit(dat, 8);
                dat[0] = 20;
                SourceDataClass.OutPutIntBit(dat, 12);
                //加入列表
                MainForm.pMainForm.comlistwin.Clear();
                MainForm.pMainForm.comlistwin.AddCmd("节点1", "全网复位", SourceDataClass.packdata);

                MainForm.pMainForm.RefreshListStat();
                MessageBox.Show("全网命令已加入命令列表!");
            }
        }

        #endregion

        private void MapForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsLoaderRunning = false;
            isContinue = false;
            isMspContinue = false;
            isUDPrun = false;
            GpsLogFile.close();
            SystemLog.close();
            MSPCmdFile.close();
            MSPRecvDataFile.close();
            MspSerialPort.Close();
            receivingUdpClient.Close();
            DataUdpClient.Close();
        }

        //每隔1s刷新地图显示
        private void MapRefreshTimer_Tick(object sender, EventArgs e)
        {
            MainMap.Refresh();
        }
        #region 回放
        private void PlayBackTimer_Tick(object sender, EventArgs e)
        {

            string str = playbacksr.ReadLine();
            if (str != null)
            {
                mut.WaitOne();
                DebugLog.Enqueue(str);
                if (DebugLog.Count > 1000)
                    DebugLog.Dequeue();
                mut.ReleaseMutex();
                //RefreshUDPLog();
                string nodename = this.nodechoice;
                nodename = nodename.TrimStart('节', '点');
                if (nodename != "")
                    nodename = string.Concat("[", nodename, "]");
                string[] debuglevel = this.debuglevel.Split('&');
                for (int i = 0; i < debuglevel.Length; i++)
                {
                    if ((str.Contains(nodename)) && (str.Contains(debuglevel[i])))
                    {
                        AddToDebugBox(str);

                    }
                }
                if (isShowAni)//显示动画
                {
                    if ((str.Contains("网络监控")) && (!str.Contains("未知")))
                        ShowNetActive(str);
                }

            }
            else
            {
                PlayBackTimer.Enabled = false;
                playbacksr.BaseStream.Seek(0, SeekOrigin.Begin);
                play.Enabled = true;
                pause.Enabled = false;
                PlayBackBtn.Text = "回放";
            }
        }

        private void play_Click(object sender, EventArgs e)
        {
            PlayBackTimer.Start();
            play.Enabled = false;
            pause.Enabled = true;
            PlayBackBtn.Text = "停止";
        }

        private void pause_Click(object sender, EventArgs e)
        {
            PlayBackTimer.Stop();
            play.Enabled = true;
            pause.Enabled = false;

        }
        #endregion




































    }
}
