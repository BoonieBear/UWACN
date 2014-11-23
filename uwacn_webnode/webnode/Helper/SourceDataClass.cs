using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Collections;
using System.Threading;
using webnode.Forms;
using GMap.NET;
namespace webnode.Helper
{
    /// <summary>
    /// 信源数据解析类
    /// </summary>
    class SourceDataClass
    {

        #region 私有成员
        private static BitArray data;//将数据转换成bit数组，低位在前。
        
        private static int index = 0;//累进解析器的下标位置。
        private static int packindex = 0;//累进打包器的下标位置
        private static List<string[]> parselist = new List<string[]>();
        private static string[] str;
        

       

        #region 协议成员类
        //{"结束标识","分段标识","邻节点表","网络表","网络简表","路由表","路径记录","路径安排",
        //                          "路径中断","转发失败"};
        //节点信息类
        //节点ID、节点能量、节点支持的通信制式、经纬度和深度
        class Nodeinfo
        {
            public int NodeId;
            public int NodePower;
            public int CommType;
            public int Lang;
            public int Lat;
            public int depth;
            public int NodeType;
            public int RecvNum;
            public int Set1type;
            public int Set2type;
            public Nodeinfo(int id,int nodetype,int Num,int set1,int set2,int power,int type,int lang,int lat,int deep)
            {
                NodeId = id;
                NodeType = nodetype;
                RecvNum = Num;
                Set1type = set1;
                Set2type = set2;
                NodePower = power;
                CommType = type;
                Lang = lang;
                Lat = lat;
                depth = deep;
            }
            public string NodeID
            { 
                get { return NodeId.ToString(); } 
                
            }
            public string MoveType
            {
                get { return (NodeType == 1) ? "移动节点" : "静止节点"; }
            }
            public string Receiver
            {
                get { return RecvNum.ToString(); }
            }

            public string Set1Type
            {
                get 
                {
                    return Enum.GetName(typeof(DeviceAddr),Set1type);
                }

            }
            public string Set2Type
            {
                get 
                {
                    return Enum.GetName(typeof(DeviceAddr),Set2type);
                }
            }
            public string NodePW
            {
                get 
                {
                    switch (NodePower)
                    {
                        case 0:
                            return @"<5%";
                        case 1:
                            return @"5%~20%";
                        case 2:
                            return @"20%~35%";
                        case 3:
                            return @"35%~50%";
                        case 4:
                            return @"50%~65%";
                        case 5:
                            return @"65%~80%";
                        case 6:
                            return @"80%~95%";
                        case 7:
                            return @">95%";
                    }
                    return @"NULL";
           
                }

            }
            public string Type
            {
                get 
                {
                    string comstr="";
                    byte[] b = BitConverter.GetBytes((short)CommType);
                    BitArray ba = new BitArray(b);
                    for (int i = 0; i < ba.Count; i++)
                    {
                        comstr += ba[i] ? "1" : "0";
                    }
                    return comstr.PadRight(16,'0'); 
                }

            }
            public string Langtude
            {
                
                get 
                {
                    string str;
                    if (Lang >> 27==1)//西经
                    {
                        Lang &= 0x7ffffff;
                        double d = (double)Lang / 10000 / 60;
                        str = d.ToString() + "°W";
                    }
                    else//东经
                    {
                        Lang &= 0x7ffffff;
                        double d = (double)Lang / 10000 / 60;
                        str = d.ToString() + "°E";
                    }


                    return str; 
                }

            }
            public string Latitude
            {
                get 
                {
                    string str;
                    if (Lat >> 27 == 1)//南纬
                    {
                        Lat &= 0x7ffffff;
                        double d = (double)Lat / 10000 / 60;
                        str = d.ToString() + "°S";
                    }
                    else//北纬
                    {
                        Lat &= 0x7ffffff;
                        double d = (double)Lat / 10000 / 60;
                        str = d.ToString() + "°N";
                    }


                    return str;  
                }

            }
            public string Depth
            {
                get
                {
                    return (depth*0.5).ToString();
                }

            }


        }

        //邻节点信息类
        //邻节点ID、节点距离、信道评价
        class NeiborNodeinfo
        {
            public int NodeId;
            public int Nodedist;
            public int ChannelEstimate;

            public NeiborNodeinfo(int id, int distance, int rate)
            {
                NodeId = id;
                Nodedist = distance;
                ChannelEstimate = rate;

            }
            public int NodeID
            {
                set { NodeId = value; }
                get { return NodeId; }

            }
            public double Distance
            {
                get
                {

                    return (double)Nodedist * 0.2;

                }

            }
            public string ChanEsti
            {
                get
                {
                    switch (ChannelEstimate)
                    {
                        case 0:
                            return @"信道条件良好，可支持高速通信";
                        case 1:
                            return @"信道条件较好，可支持中速通信";
                        case 2:
                            return @"信道条件较差，需降低通信速率";
                        case 3:
                            return @"信道条件恶劣，需采用高可靠性的通信";
                        default:
                            return @"错误的状态";
                    }
                }

            }


        }

        //网络表
        //节点的连接关系
        class NetworkList
        {
            public int SourceNodeId;
            public int DestinNodeId;
            public int Nodedist;
            public int ChannelEstimate;
            public NetworkList(int sid,int did, int distance, int rate)
            {
                SourceNodeId = sid;
                DestinNodeId = did;
                Nodedist = distance;
                ChannelEstimate = rate;

            }
            public int SourceNodeID
            {
                set { SourceNodeId = value; }
                get { return SourceNodeId; }

            }
            public int DestinationNodeID
            {
                set { DestinNodeId = value; }
                get { return DestinNodeId; }

            }
            public double Distance
            {
                get
                {

                    return (double)Nodedist * 0.2;

                }

            }
            public string ChanEsti
            {
                get
                {
                    switch (ChannelEstimate)
                    {
                        case 0:
                            return @"信道条件良好，可支持高速通信";
                        case 1:
                            return @"信道条件较好，可支持中速通信";
                        case 2:
                            return @"信道条件较差，需降低通信速率";
                        case 3:
                            return @"信道条件恶劣，需采用高可靠性的通信";
                        default:
                            return @"错误的状态";
                    }
                }

            }
        }

        //路由表
        //源节点到各目标节点的路由，包括目标节点、下一跳地址和跳数
        class RourteList
        {
            public int DNodeId;
            public int NextNodeId;
            public int Hops;
            public int DestSerial;
            public int RouteStatus;
            public RourteList(int did, int nid, int hop, int Serial, int Status)
            {
                DNodeId = did;
                NextNodeId = nid;
                Hops = hop;
                DestSerial = Serial;
                RouteStatus = Status;
            }
            public int DestinationNodeID
            {
                get { return DNodeId; }
            }
            public int NextNodeID
            {
                get { return NextNodeId; }
            }
            public int Hop
            {
                get { return Hops; }
            }
            public string Status
            {
                get
                {
                    switch (RouteStatus)
                    {
                        case 0:
                            return @"路由有效";
                        case 1:
                            return @"路由无效";
                        case 2:
                            return @"路由正在修复";
                        case 3:
                            return @"非法状态标志";
                        default:
                            return @"错误的状态";
                    }


                }
            }
            public int Serial
            {
                get { return DestSerial; }
            }

        }

        //路径记录
        //记录了所在数据块已经过的节点有哪些
        class RouterLog
        {
            int StartId;
            List<int> IdLog;
            public RouterLog(int Startid, List<int> id)
            {
                StartId = Startid;
                IdLog = id;
            }
            public int BeginID
            { get { return StartId; } }
            public List<int> ViaID
            { get { return IdLog; } }
        }

        //路径安排
        //“路径安排”是“起始节点”对数据块按照什么样的路径进行传输的要求
        class RouterAssign
        {
            int StartId;
            List<int> IdLog;
            int EndId;
            public RouterAssign(int Startid, List<int> id,int Endid)
            {
                StartId = Startid;
                IdLog = id;
                EndId = Endid;
            }
            public int BeginID
            { get { return StartId; } }
            public List<int> ViaID
            { get { return IdLog; } }
            public int EndID
            { get { return EndId; } }
        }

        //路径中断
        //“路径中断”是在路径中断后，判定路径中断的节点向网关发送的错误报告
        class RouterBroken
        {
            int StartId;
            //List<int> IdLog;
            int EndId;
            public RouterBroken(int Startid, List<int> id, int Endid)
            {
                StartId = Startid;
                //IdLog = id;
                EndId = Endid;
            }
            public int BeginID
            { get { return StartId; } }
            //public List<int> ViaID
            //{ get { return IdLog; } }
            public int EndID
            { get { return EndId; } }
        }

        //转发失败
        //“转发失败”是节点在尝试各可选路径后均无法完成转发任务后，向中继链路的上一个节点的发送的错误报告
        class TransError
        {

            List<TransErrorBlock> ErrorBlock;

            public TransError(List<TransErrorBlock> Eb)
            {
                ErrorBlock = Eb;
            }
            public List<TransErrorBlock> ErrorReport
            { get { return ErrorBlock; } }

        }

        //转发失败数据体由成对的块标识和起始源地址组成
        class TransErrorBlock
        {
            int ErrNum;
            int StartId;
            public TransErrorBlock(int num, int sid)
            {
                ErrNum = num;
                StartId = sid;
            }
            public int ERRNO
            { get { return ErrNum; } }
            public int BeginID
            { get { return StartId; } }
        }
        #endregion

        //将数据说明加入到列表中
        private static void AddtoList(string level, string decription, string data, string meanings)
        {
            str = new string[4];
            str[0] = level;
            str[1] = decription;
            str[2] = data;
            str[3] = meanings;
            parselist.Add(str);
            
        }
        
        #endregion
            
        #region 公有成员

        #region 公共变量
        public static Hashtable DataId = new Hashtable();
        public static Hashtable WebId = new Hashtable();
        public static BitArray packdata;//将打包数据转换成bit数组
        public static bool isShowCircle = false;
        public static bool isNodeTick = true;
        public static bool isCommDepack = false;
        public static bool isShowData = false;
        public static string optputdata = "";
        #endregion

        #region 枚举成员
        public enum  DeviceAddr
        {
            无设备 = 0,
            声学所浮标 = 1,
            SBE39_TD = 16,
            SBE37_CTD = 17,
            ZJU_AUV_CTD = 18,
            IOA_ADCP = 19,
            ZJU_AUV = 101,
            
        }
        public enum NodeType
        {
            声学所节点 = 0,
            _715通信机 = 4,
            哈工程通信机 = 8,
            
        }
        public enum EmitType
        {
            PWM发射机 = 0,
            线性发射机 = 1,
        }
        public enum RouteStatus
        {
            路由有效 = 0,
            路由无效 = 1,
            路由正在修复 = 2,
            非法状态标志 = 3,

        }
        public enum ChannlValue
        {
            良好 = 0,
            较好 = 1,
            较差 = 2,
            恶劣 = 3,

        }
        #endregion

        #region 类成员函数
        public SourceDataClass()
        {
        }

        static public void GetData(byte[] d)
        {
            index = 0;
            data = new BitArray(d);
        }
        static public void GetData(BitArray d)
        {
            index = 0;
            data = new BitArray(d);
        }
        static public void clear()
        {
            if(parselist!=null)
                parselist.Clear();
            index = 0;
            optputdata = "";
            
        }
        static public void InitForPack(int bitlen)
        {
            packdata = new BitArray(bitlen);
            packindex = 0;
        }
        #endregion

        #region 协议打包
        //网络打包函数，加0xAA，校验，网络包头
        static public byte[] NetPackage(byte[] outcmd)
        {
            byte[] fullpackage = new byte[outcmd.Length + 4];//完整命令 
            UInt16 uid = 0xEE01;
            Buffer.BlockCopy(BitConverter.GetBytes(uid), 0, fullpackage, 0, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(outcmd.Length), 0, fullpackage, 2, 2);
            Buffer.BlockCopy(outcmd, 0, fullpackage, 4, outcmd.Length);
            return fullpackage;
             
        }

        //串口打包函数，加0xAA，校验，浮标协议包头
        static public byte[] CommPackage(int id, byte[] outcmd)
        {
            string head;
            byte type;
            string time = DateTime.Now.Year.ToString().TrimStart('2', '0') +","+ DateTime.Now.Month.ToString("00")
                + "," + DateTime.Now.Day.ToString("00") + "," + DateTime.Now.Hour.ToString("00") + "," + DateTime.Now.Minute.ToString("00")
                + "," + DateTime.Now.Second.ToString("00") + ",";
            string tail = ",END";
            byte[] headbyte;
            byte[] tailbyte;
            int timelen = 0;
            if ((id >= 240) && (id <= 255))//特殊命令
            {
                head = "EB90,10,";
                type = (byte)id;
                timelen = 0;
            }
            else//转发命令或浮标命令
            {
                head = "EB90,01,";
                type = 171;
                if ((id <= 22) && (id >= 16))
                    type = (byte)id;
                timelen = 23;//加上长度域
                
                
            }
            byte[] AAHeadCmd = PackageAAHead(type, outcmd);//加AA协议
            headbyte = System.Text.Encoding.Default.GetBytes(head);
            tailbyte = System.Text.Encoding.Default.GetBytes(tail);
            string buoyid = MainForm.pMainForm.mapdoc.BuoyID+",";
            byte[] bytebuoyid = System.Text.Encoding.Default.GetBytes(buoyid);
            byte[] cmd = new byte[headbyte.Length + AAHeadCmd.Length + 4 + timelen];
            Buffer.BlockCopy(headbyte, 0, cmd, 0, headbyte.Length);
            Buffer.BlockCopy(bytebuoyid,0,cmd,headbyte.Length,3);
            if (type == 171)
            {
                
                int total = headbyte.Length + AAHeadCmd.Length + 4 + timelen + 6 ;
                string lenstr = total.ToString("0000");
                time = lenstr + "," + time;
                byte[] timechar = CRCHelper.ConvertHexToChar(CRCHelper.ConvertStrToHex(time));
                Buffer.BlockCopy(timechar, 0, cmd, headbyte.Length + 3, timechar.Length);
            }
            Buffer.BlockCopy(AAHeadCmd, 0, cmd, headbyte.Length + 3 + timelen, AAHeadCmd.Length);
            cmd[headbyte.Length + 3 + timelen + AAHeadCmd.Length] = 0x2C;//逗号
            UInt16 crc = CRCHelper.CRC16byte(cmd);
            byte[] bytecrc = BitConverter.GetBytes(crc);
            //长度高低位转换，for dsp
            byte temp = bytecrc[0];
            bytecrc[0] = bytecrc[1];
            bytecrc[1] = temp;
            byte[] fulcmd = new byte[headbyte.Length + AAHeadCmd.Length + tailbyte.Length + 6  + timelen];
            Buffer.BlockCopy(cmd, 0, fulcmd, 0, headbyte.Length + AAHeadCmd.Length + 4  + timelen);
            Buffer.BlockCopy(bytecrc, 0, fulcmd, headbyte.Length + 4 + timelen + AAHeadCmd.Length, 2);
            Buffer.BlockCopy(tailbyte, 0, fulcmd, headbyte.Length + 4 + timelen + AAHeadCmd.Length + 2, tailbyte.Length);
            return fulcmd;
        }
        //给数据打成AA头的包
        static private byte[] PackageAAHead(int id,byte[] outcmd)
        {
            byte head = 0xAA;
            UInt16 crc = 0;
            byte[] package;
            byte source = (byte)(MainForm.pMainForm.WebnodeComm.SelectedIndex + 2);
            byte dest = 0;
            if((id<=22)&&(id>=16))
                dest =1;
            byte type = (byte)id;
            UInt16 length = (UInt16)outcmd.Length;
            if (type != outcmd[0])//数据域非空
            {
                package = new byte[length + 6];//不带校验的命令
                package[0] = head;
                package[1] = dest;
                package[2] = source;
                package[3] = type;
                Buffer.BlockCopy(BitConverter.GetBytes(length), 0, package, 4, 2);
                //长度高低位转换，for dsp
                byte temp = package[4];
                package[4] = package[5];
                package[5] = temp;
                Buffer.BlockCopy(outcmd, 0, package, 6, length);
                crc = CRCHelper.CRC16byte(package);

            }
            else//空数据
            {
                package = new byte[6];//不带校验的命令
                length = 0;
                package[0] = head;
                package[1] = dest;
                package[2] = source;
                package[3] = type;
                package[4] = 0;
                package[5] = 0;
                crc = CRCHelper.CRC16byte(package); ;
             }
            byte[] fulpackage = new byte[length + 8];//带校验的命令
            Buffer.BlockCopy(package, 0, fulpackage, 0, length + 6);
            Buffer.BlockCopy(BitConverter.GetBytes(crc), 0, fulpackage, length + 6, 2);
            byte crctemp = fulpackage[length + 6];
            fulpackage[length + 6] = fulpackage[length + 7];
            fulpackage[length + 7] = crctemp;
            return fulpackage;
        }
        #endregion

        #region 协议拆包
        //将串口回传数据拆包，返回数据记录时间，数据id，数据内容，信源混合包拆到ID为171为止，后面调用parse
        public static bool DepackCommData(byte[] cmd, out string time, out int id ,out byte[] data)
        {
            try
            {
                string oldstr = Encoding.ASCII.GetString(cmd);
                byte[] shortcmd = new byte[cmd.Length - 6];
                Buffer.BlockCopy(cmd, 0, shortcmd, 0, cmd.Length - 6);
                UInt16 crcnew = CRCHelper.CRC16byte(shortcmd);
                byte[] crcchar = new byte[2];
                Buffer.BlockCopy(cmd, cmd.Length - 6, crcchar, 0, 2);
                byte tmp;
                tmp = crcchar[1];
                crcchar[1] = crcchar[0];
                crcchar[0] = tmp;
                UInt16 crcold = BitConverter.ToUInt16(crcchar,0);
                if (crcold == crcnew)
                {
                    string[] str = oldstr.Split(',');
                    time = "20"+str[4]+" "+str[5]+"."+str[6]+" "+str[7]+":"+str[8]+":"+str[9];
                    int cmslen = int.Parse(str[3])-41;
                    byte[] aadata = new byte[cmslen];
                    Buffer.BlockCopy(cmd,34,aadata,0,cmslen);
                    byte[] cmdnocrc = new byte[aadata.Length - 2];
                    Array.Copy(aadata, cmdnocrc, aadata.Length - 2);
                    int len = (int)(aadata[4] << 8) + (int)aadata[5];
                    data = new byte[len];
                    Buffer.BlockCopy(aadata, 6, data, 0, len);
                    UInt16 aacrc = CRCHelper.CRC16byte(cmdnocrc);
                    UInt16 newcrc = (UInt16)((int)(aadata[aadata.Length - 2] << 8) + (int)aadata[aadata.Length - 1]);
                    id = cmdnocrc[3];
                    if (aacrc == newcrc)
                    {
                        return true;
                    }
                    else//里层校验错误
                    {
                        return false;
                    }
                    
                }
                else
                {
                    time = DateTime.Now.ToShortTimeString();
                    data = new byte[1];
                    id = 0;
                    return false;
                }
            }
            catch(Exception e)
            {
                time = DateTime.Now.ToShortTimeString();
                data = new byte[1];
                id = 0;
                return false;
            }
        }
        struct info
        {
            public Hashtable nodelist;
            public Hashtable distlist;
        }
        static void ProcessRoute(object obj)
        {
            info o = (info)obj;
            Hashtable nodelist = o.nodelist;
            Hashtable distlist = o.distlist;
            List<RouteBuildClass.SortPolicy> sp = new List<RouteBuildClass.SortPolicy>();
            sp.Add(RouteBuildClass.SortPolicy.MinHops);
            sp.Add(RouteBuildClass.SortPolicy.MinMaxDist);
            try
            {
                RouteBuildClass rbc = new RouteBuildClass(nodelist, distlist, RouteBuildClass.FindPolicy.Recursion, sp, 2);
                Hashtable ht = rbc.FindRoutes();

                if (ht != null)
                {
                    UtilityClass.CopyHashTableStringListArray(ht, MainForm.pMainForm.mapdoc.NodeRouteMap);
                    MainForm.pMainForm.mapdoc.SaveInit();
                    
                    MainForm.pMainForm.mapdoc.Ping("节点1", 2500,"已生成路由表！！");
                }
            }
            catch (Exception ex)
            {
                MainForm.pMainForm.mapdoc.WriteLog(ex.StackTrace);

            }
        }
        //将数据解析为文字列表，供分析
        static public List<string[]> Parse()
        {

                clear();
                index = 0;
                MapForm.SourceNode.Clear();
                //解块
                int blocknum = GetIntValueFromBit(6);
                AddtoList("0", "块数", blocknum.ToString(), "");
                for (int i = 0; i < blocknum; i++)
                {
                    string num = "块" + (i + 1).ToString();
                    AddtoList("0", num, (i + 1).ToString(), "");
                    //块定义
                    int blockid = GetIntValueFromBit(10);
                    AddtoList("1", "块标识", blockid.ToString(), "");
                    int blocklen = GetIntValueFromBit(12);
                    AddtoList("1", "块长", blocklen.ToString(), "");
                    int StartId = GetIntValueFromBit(6);
                    AddtoList("1", "起始源地址", StartId.ToString(), "");
                    optputdata += "源节点:" + StartId.ToString() + "\r\n";
                    MapForm.SourceNode.Add(StartId);
                    //通知地图，更改节点通信时间戳
                    if(isNodeTick)
                        MainForm.pMainForm.mapdoc.AddLastTimeMarker(StartId,DateTime.Now.ToString());
                    int EndId = GetIntValueFromBit(6);
                    AddtoList("1", "目的地址", EndId.ToString(), "");
                    int j = 34;//长度加两个地址长度
                    int Sector = 1;
                    while (j < blocklen)
                    {

                        num = "数据区" + Sector.ToString();
                        AddtoList("1", num, "", "");
                        optputdata += num + "\r\n";
                        Sector++;
                        //解析数据区
                        int sectorId = GetIntValueFromBit(8);
                        if (sectorId == 0)//结束标识
                        {
                            AddtoList("2", (string)WebId[sectorId], sectorId.ToString(), "结束");
                            j += 8;//只有8bit长
                        }
                        else if (sectorId==222)
                        {
                            AddtoList("2", "ID", sectorId.ToString(), "测试");
                            AddtoList("3", "数据长度", ((data.Count - 48)/8).ToString(), "");
                            byte[] b = GetByteValueFromBit(data.Count - 48);
                            AddtoList("3", "测试数据", CRCHelper.ConvertCharToHex(b, b.Length), "");//减去头和长度域
                            break;
                        }
                        else
                        {
                            //
                            AddtoList("2", "ID",sectorId.ToString(),(string)WebId[sectorId] );
                            optputdata += (string)WebId[sectorId] + "\r\n";
                            int len = GetIntValueFromBit(12);

                            j += len;
                            AddtoList("2", "数据区长", len.ToString(), "");
                            //AddtoList("2", "数据体", "", "");
                            if (len == 20)//命令
                                continue;
                            //有相同处理方法但ID不同的命令
                            switch (sectorId)
                            {
                                case 103:
                                    sectorId = 2;
                                    break;
                                case 104:
                                    sectorId = 2;
                                    break;
                                case 105:
                                    sectorId = 2;
                                    break;
                                case 106:
                                    sectorId = 2;
                                    break;
                                //case 107:
                                //    sectorId = 4;
                                //    break;
                                //case 108:
                                //    sectorId = 4;
                                //    break;
                                case 109:
                                    sectorId = 5;
                                    break;
                                case 110:
                                    sectorId = 5;
                                    break;
                                case 112:
                                    sectorId = 3;
                                    break;
                                case 113:
                                    sectorId = 6;
                                    break;
                                case 114:
                                    sectorId = 6;
                                    break;
                                case 116:
                                    sectorId = 61;
                                    break;
                                case 118:
                                    sectorId = 62;
                                    break;
                                case 122:
                                    sectorId = 63;
                                    break;
                                default:
                                    break;
                            }

                            switch (sectorId)
                            {

                                case 1://分段
                                    len = GetIntValueFromBit(4);
                                    AddtoList("3", "总段数", len.ToString(), "");
                                    len = GetIntValueFromBit(4);
                                    AddtoList("3", "当前段号", len.ToString(), "");
                                    break;
                                case 2:
                                    int nodes = GetIntValueFromBit(6);//节点数
                                    AddtoList("3", "节点数", nodes.ToString(), "");
                                    optputdata += "节点数:" + nodes.ToString()+ "\r\n";
                                    for (int n = 0; n < nodes; n++)
                                    {
                                        Nodeinfo ni = new Nodeinfo(GetIntValueFromBit(6),GetIntValueFromBit(1),GetIntValueFromBit(3),GetIntValueFromBit(8),
                                            GetIntValueFromBit(8),GetIntValueFromBit(3), GetIntValueFromBit(16),
                                            GetIntValueFromBit(28), GetIntValueFromBit(28), GetIntValueFromBit(14));
                                        AddtoList("3", "节点信息" + (n + 1).ToString(), "", "");
                                        optputdata += "节点信息" + (n + 1).ToString() + "\r\n";
                                        AddtoList("4", "节点ID", ni.NodeId.ToString(), ni.NodeID);
                                        optputdata += "节点ID:" + ni.NodeID + "\r\n";
                                        AddtoList("4", "节点类型", ni.NodeType.ToString(), ni.MoveType);
                                        optputdata += "节点类型:" + ni.MoveType + "\r\n";
                                        AddtoList("4", "节点接收换能器个数", ni.RecvNum.ToString(), ni.Receiver);
                                        optputdata += "节点接收换能器个数:" + ni.Receiver + "\r\n";
                                        AddtoList("4", "节点外挂设备1类型", ni.Set1type.ToString(), ni.Set1Type);
                                        optputdata += "节点外挂设备1类型:" + ni.Set1Type + "\r\n";
                                        AddtoList("4", "节点外挂设备2类型", ni.Set2type.ToString(), ni.Set2Type);
                                        optputdata += "节点外挂设备2类型:" + ni.Set2Type + "\r\n";
                                        AddtoList("4", "剩余能量", ni.NodePower.ToString(), ni.NodePW);
                                        optputdata += "剩余能量:" + ni.NodePW + "\r\n";
                                        AddtoList("4", "通信制式", ni.CommType.ToString(), ni.Type);
                                        optputdata += "通信制式:" + ni.Type + "\r\n";
                                        AddtoList("4", "经度", ni.Lang.ToString(), ni.Langtude);
                                        optputdata += "经度:" + ni.Langtude + "\r\n";
                                        AddtoList("4", "纬度", ni.Lat.ToString(), ni.Latitude);
                                        optputdata += "纬度:" + ni.Latitude + "\r\n";
                                        AddtoList("4", "深度", ni.depth.ToString(), ni.Depth);
                                        optputdata += "深度:" + ni.Depth + "\r\n";
                                        if ((ni.Set2type == 101)||(ni.Set1type == 101))//auv声学定位数据
                                        {
                                            PointLatLng p = new PointLatLng(((double)ni.Lat)/60/10000,((double)ni.Lang)/60/10000);
                                            MainForm.pMainForm.mapdoc.AddNewNodeToMap("节点" + ni.NodeId.ToString() + "(POS)", p, 1, -1);
                                        }
                                    }
                                    break;
                                case 3:
                                    nodes = GetIntValueFromBit(4);//邻节点数
                                    AddtoList("3", "邻节点数", nodes.ToString(), "");
                                    for (int n = 0; n < nodes; n++)
                                    {
                                        NeiborNodeinfo ni = new NeiborNodeinfo(GetIntValueFromBit(6), GetIntValueFromBit(16), GetIntValueFromBit(2));
                                        AddtoList("3", "邻节点信息" + (n + 1).ToString(), "", "");
                                        AddtoList("4", "邻节点ID", ni.NodeId.ToString(), ni.NodeID.ToString());
                                        AddtoList("4", "距离", ni.Nodedist.ToString(), ni.Distance.ToString());
                                        AddtoList("4", "评价", ni.ChannelEstimate.ToString(), ni.ChanEsti);
                                    }
                                    break;
                                case 4://主动上报网络表，重新路由
                                    int routers = GetIntValueFromBit(8);//路径条数
                                    Hashtable nodelist = new Hashtable();
                                    Hashtable distlist = new Hashtable();
                                    AddtoList("3", "路径条数", routers.ToString(), "");
                                    for (int n = 0; n < routers; n++)
                                    {
                                        NetworkList ni = new NetworkList(GetIntValueFromBit(6), GetIntValueFromBit(6), GetIntValueFromBit(16), GetIntValueFromBit(2));
                                        AddtoList("3", "路径信息" + (n + 1).ToString(), "", "");
                                        AddtoList("4", "源节点", ni.SourceNodeId.ToString(), ni.SourceNodeID.ToString());
                                        AddtoList("4", "目标节点", ni.DestinNodeId.ToString(), ni.DestinationNodeID.ToString());
                                        AddtoList("4", "路径距离", ni.Nodedist.ToString(), ni.Distance.ToString());
                                        AddtoList("4", "信道评价", ni.ChannelEstimate.ToString(), ni.ChanEsti);
                                        //添加到领接表中。
                                        if (SourceDataClass.isCommDepack == true)
                                        {
                                            string StartNodeName = "节点" + ni.SourceNodeID.ToString();
                                            string EndNodeName = "节点" + ni.DestinationNodeID.ToString();
                                            if (nodelist.ContainsKey(StartNodeName))//存在节点领接表
                                            {
                                                List<string> t = (List<string>)nodelist[StartNodeName];
                                                if (t.Contains(EndNodeName))
                                                {
                                                    continue;
                                                }
                                                else//新路由;
                                                {

                                                    t.Add(EndNodeName);
                                                }
                                            }
                                            else//if (nodelist.ContainsKey(StartNodeName))//存在节点领接表
                                            {
                                                //新路由
                                                List<string> newlst = new List<string> { };
                                                newlst.Add(EndNodeName);
                                                //新路由
                                                nodelist.Add(StartNodeName, newlst);

                                            }

                                            //更新另一节点领接表
                                            if (nodelist.ContainsKey(EndNodeName))//存在另一节点领接表
                                            {
                                                List<string> tt = (List<string>)nodelist[EndNodeName];
                                                if (tt.Contains(StartNodeName))
                                                {
                                                    continue;
                                                }
                                                else
                                                {
                                                    //新路由;
                                                    tt.Add(StartNodeName);
                                                }
                                            }
                                            else//if (nodelist.ContainsKey(EndNodeName))//存在另一节点领接表
                                            {
                                                //新路由
                                                List<string> newlst = new List<string> { };
                                                newlst.Add(StartNodeName);
                                                //新路由
                                                nodelist.Add(EndNodeName, newlst);
                                                
                                            }
                                            /////
                                            //更新距离表
                                            /////
                                            distlist.Add(StartNodeName + "-" + EndNodeName, ni.Distance.ToString());
                                            distlist.Add(EndNodeName + "-" +StartNodeName , ni.Distance.ToString());
                                        }
                                    }
                                    if (isCommDepack)
                                    {
                                        MainForm.pMainForm.mapdoc.NodeNetMap.Clear();
                                        MainForm.pMainForm.mapdoc.DistMap.Clear();
                                        MainForm.pMainForm.mapdoc.NodeRouteMap.Clear();
                                        UtilityClass.CopyHashTableStringList(nodelist, MainForm.pMainForm.mapdoc.NodeNetMap);
                                        UtilityClass.CopyHashTableString(distlist, MainForm.pMainForm.mapdoc.DistMap);
                                        MainForm.pMainForm.mapdoc.AddNet();//添加连接，删除未配置的点
                                        MainForm.pMainForm.mapdoc.SaveInit();
                                        Thread d = new Thread(ProcessRoute);
                                        info o = new info();
                                        o.distlist = distlist;
                                        o.nodelist = MainForm.pMainForm.mapdoc.NodeNetMap;
                                        d.Start((object)o);
                                        //pf.Activate();
                                        isCommDepack = false;
                                    }
                                    
                                    break;
                                case 108://被动返回网络表
                                    routers = GetIntValueFromBit(8);//路径条数
                                    AddtoList("3", "路径条数", routers.ToString(), "");
                                    for (int n = 0; n < routers; n++)
                                    {
                                        NetworkList ni = new NetworkList(GetIntValueFromBit(6), GetIntValueFromBit(6), GetIntValueFromBit(16), GetIntValueFromBit(2));
                                        AddtoList("3", "路径信息" + (n + 1).ToString(), "", "");
                                        AddtoList("4", "源节点", ni.SourceNodeId.ToString(), ni.SourceNodeID.ToString());
                                        AddtoList("4", "目标节点", ni.DestinNodeId.ToString(), ni.DestinationNodeID.ToString());
                                        AddtoList("4", "路径距离", ni.Nodedist.ToString(), ni.Distance.ToString());
                                        AddtoList("4", "信道评价", ni.ChannelEstimate.ToString(), ni.ChanEsti);
                                    }
                                    break;
                                case 5://网络简表
                                    routers = GetIntValueFromBit(8);//路由条数
                                    AddtoList("3", "路由条数", routers.ToString(), "");
                                    for (int n = 0; n < routers; n++)
                                    {
                                        int SourceNodeId = GetIntValueFromBit(6);
                                        int DestNodeId = GetIntValueFromBit(6);
                                        AddtoList("3", "路由信息" + (n + 1).ToString(), "", "");
                                        AddtoList("4", "源节点", SourceNodeId.ToString(), SourceNodeId.ToString());
                                        AddtoList("4", "目标节点", DestNodeId.ToString(), DestNodeId.ToString());

                                    }
                                    break;
                                case 6:
                                    routers = GetIntValueFromBit(6);//路由条数
                                    AddtoList("3", "路由条数", routers.ToString(), "");
                                    for (int n = 0; n < routers; n++)
                                    {
                                        RourteList ni = new RourteList(GetIntValueFromBit(6), GetIntValueFromBit(6), GetIntValueFromBit(4),
                                            GetIntValueFromBit(15), GetIntValueFromBit(2));
                                        AddtoList("3", "路由信息" + (n + 1).ToString(), "", "");
                                        AddtoList("4", "目标节点", ni.DNodeId.ToString(), ni.DestinationNodeID.ToString());
                                        AddtoList("4", "下一跳地址", ni.NextNodeId.ToString(), ni.NextNodeID.ToString());
                                        AddtoList("4", "跳数", ni.Hops.ToString(), ni.Hop.ToString());
                                        AddtoList("4", "目标节点序列号", ni.DestSerial.ToString(), ni.Serial.ToString());
                                        AddtoList("4", "路由状态", ni.RouteStatus.ToString(), ni.Status);
                                    }
                                    break;
                                case 7:
                                    nodes = (len - 26) / 6;//记录条数
                                    routers = GetIntValueFromBit(6);
                                    AddtoList("3", "起始地址", routers.ToString(), "");
                                    optputdata += "起始地址:" + routers.ToString() + "\r\n";
                                    for (int n = 0; n < nodes; n++)
                                    {
                                        routers = GetIntValueFromBit(6);
                                        AddtoList("3", "节点ID" + (n + 1).ToString(), routers.ToString(), "");
                                        optputdata += "节点" + (n + 1).ToString() + "ID号:" + routers.ToString() + "\r\n";
                                    }

                                    break;
                                case 8:
                                    nodes = (len - 32) / 6;//记录条数
                                    int sourceID = GetIntValueFromBit(6);
                                    AddtoList("3", "起始地址", sourceID.ToString(), "节点" + sourceID.ToString());
                                    for (int n = 0; n < nodes; n++)
                                    {
                                        AddtoList("3", "节点ID" + (n + 1).ToString(), GetIntValueFromBit(6).ToString(), "");
                                    }
                                    int dest = GetIntValueFromBit(6);
                                    AddtoList("3", "目的地址", dest.ToString(), "节点" + dest.ToString());
                                    break;
                                case 9:
                                    //nodes = (len - 20) / 12;//记录条数
                                    int sourceid, destid;
                                    AddtoList("3", "路径中断信息", "", "");
                                    sourceid = GetIntValueFromBit(6);
                                    destid = GetIntValueFromBit(6);
                                    AddtoList("4", "起始地址", sourceid.ToString(), sourceid.ToString());
                                    AddtoList("4", "目的地址", destid.ToString(), destid.ToString());
                                    //for (int n = 0; n < nodes; n++)
                                    //{
                                    //    AddtoList("3", "路径中断信息" + (n + 1).ToString(), "", "");
                                    //    sourceid = GetIntValueFromBit(6);
                                    //    destid = GetIntValueFromBit(6);
                                    //    AddtoList("4", "起始地址", sourceid.ToString(), sourceid.ToString());
                                    //    AddtoList("4", "目的地址", destid.ToString(), destid.ToString());
                                    //}

                                    break;
                                case 10:
                                    //nodes = (len - 20) / 16;//失败记录条数
                                    int blockd, source,errnum;
                               
                                    AddtoList("3", "转发失败信息", "", "");
                                    blockd = GetIntValueFromBit(10);
                                    source = GetIntValueFromBit(6);
                                    errnum = GetIntValueFromBit(3);
                                    AddtoList("4", "块标识", blockd.ToString(), blockd.ToString());
                                    AddtoList("4", "起始源地址", source.ToString(), source.ToString());
                                    AddtoList("4", "失败次数", errnum.ToString(), errnum.ToString());
                                    break;
                                case 61:
                                    if (len > 20)
                                    {
                                        int ID = GetIntValueFromBit(8);
                                        AddtoList("3", "设备类型", ID.ToString(), Enum.GetName(typeof(DeviceAddr), ID));
                                        optputdata += "设备类型=" + Enum.GetName(typeof(DeviceAddr), ID) + "\r\n";
                                        int comm = GetIntValueFromBit(8);
                                        AddtoList("3", "COM端口", comm.ToString(), "");
                                        optputdata += "COM端口=" + comm.ToString() + "\r\n";
                                        switch (ID)
                                        {
                                            case 16:
                                                int Type = GetIntValueFromBit(8);
                                                switch (Type)
                                                {
                                                    case 0x54://"T"
                                                        AddtoList("3", "状态", "TimeOut", "设备超时");
                                                        GetIntValueFromBit(32);
                                                        GetIntValueFromBit(16);//跳过48个bit
                                                        break;
                                                    case 0x4E://"N"
                                                        AddtoList("3", "状态", "No Device", "无设备");
                                                        GetIntValueFromBit(32);
                                                        GetIntValueFromBit(32);//跳过64个bit
                                                        break;
                                                    default:
                                                        index -= 8;//后退8个bit
                                                        byte[] b1 = GetByteValueFromBit(len - 36);
                                                        AddtoList("3", "设备数据", CRCHelper.ConvertCharToHex(b1, b1.Length), Encoding.Default.GetString(b1));
                                                        optputdata += "设备数据:" + Encoding.Default.GetString(b1) + "\r\n";
                                                        break;
                                                }
                                                break;
                                            case 17:
                                                Type = GetIntValueFromBit(8);
                                                switch (Type)
                                                {
                                                    case 0x54://"T"
                                                        AddtoList("3", "状态", "TimeOut", "设备超时");
                                                        optputdata += "设备超时" + "\r\n";
                                                        GetIntValueFromBit(32);
                                                        GetIntValueFromBit(16);//跳过48个bit
                                                        break;
                                                    case 0x4E://"N"
                                                        AddtoList("3", "状态", "No Device", "无设备");
                                                        optputdata += "无设备" + "\r\n";
                                                        GetIntValueFromBit(32);
                                                        GetIntValueFromBit(32);//跳过64个bit
                                                        break;
                                                    default:
                                                        index -= 8;//后退8个bit
                                                        byte[] b2 = GetByteValueFromBit(len - 36);
                                                        AddtoList("3", "设备数据", CRCHelper.ConvertCharToHex(b2, b2.Length), Encoding.Default.GetString(b2));
                                                        optputdata += "设备数据:" + Encoding.Default.GetString(b2) + "\r\n";
                                                        break;
                                                }
                                                break;
                                            case 18:
                                                Type = GetIntValueFromBit(8);
                                                switch (Type)
                                                {
                                                    case 0x54://"T"
                                                        AddtoList("3", "状态", "TimeOut", "设备超时");
                                                        optputdata += "设备超时" + "\r\n";
                                                        GetIntValueFromBit(32);
                                                        GetIntValueFromBit(16);//跳过48个bit
                                                        break;
                                                    case 0x4E://"N"
                                                        AddtoList("3", "状态", "No Device", "无设备");
                                                        optputdata += "无设备" + "\r\n";
                                                        GetIntValueFromBit(32);
                                                        GetIntValueFromBit(32);//跳过64个bit
                                                        break;
                                                    default:
                                                        index -= 8;//后退8个bit
                                                        AddtoList("3", "AUV_CTD采样数据", "", "");
                                                        optputdata += "AUV_CTD采样数据:" + "\r\n";
                                                        AddtoList("3", "数据体", "", "");
                                                        Int16 temp = (Int16)((GetIntValueFromBit(8) << 8) + GetIntValueFromBit(8));
                                                        AddtoList("4", "温度", temp.ToString(), (((double)temp) / 1000).ToString());
                                                        optputdata += "温度:" + (((double)temp) / 1000).ToString() + "\r\n";
                                                        int daolv = (GetIntValueFromBit(8) << 8) + GetIntValueFromBit(8);
                                                        AddtoList("4", "电导率", daolv.ToString(), daolv.ToString() + "μS/cm");
                                                        optputdata += "电导率:" + daolv.ToString() + "μS/cm" + "\r\n";
                                                        int pressure = (GetIntValueFromBit(8) << 8) + GetIntValueFromBit(8);
                                                        AddtoList("4", "压力", pressure.ToString(), (((double)pressure) / 1000).ToString() + "MPa");
                                                        optputdata += "压力:" + (((double)pressure) / 1000).ToString() + "MPa" + "\r\n";

                                                        break;
                                                }
                                                break;
                                            case 19:
                                                Type = GetIntValueFromBit(8);
                                                switch (Type)
                                                {
                                                    case 0x54://"T"
                                                        AddtoList("3", "状态", "TimeOut", "设备超时");
                                                        optputdata += "设备超时" + "\r\n";
                                                        GetIntValueFromBit(32);
                                                        GetIntValueFromBit(16);//跳过48个bit
                                                        break;
                                                    case 0x4E://"N"
                                                        AddtoList("3", "状态", "No Device", "无设备");
                                                        optputdata += "无设备" + "\r\n";
                                                        GetIntValueFromBit(32);
                                                        GetIntValueFromBit(32);//跳过64个bit
                                                        break;
                                                    default:
                                                        index -= 8;//后退8个bit

                                                        AddtoList("3", "ADCP数据", "", "");
                                                       string timestr = "20" + GetIntValueFromBit(8) + " "+ GetIntValueFromBit(8) + "-" +GetIntValueFromBit(8)
                                                            + " " +GetIntValueFromBit(8) + ":" + GetIntValueFromBit(8) + ":" + GetIntValueFromBit(8);
                                                        AddtoList("4", "系统时间", timestr, "");
                                                        int layers = GetIntValueFromBit(8);
                                                        AddtoList("4", "层数", layers.ToString(), layers.ToString());
                                                        int thickness = GetIntValueFromBit(16);
                                                        AddtoList("4", "层厚", thickness.ToString(), thickness.ToString());
                                                        for (int a = 0; a < layers; a++)
                                                        {
                                                            AddtoList("4", "层" + (a + 1).ToString() + "流速", "", "");
                                                            for (int layer = 0; layer < 4; layer++)
                                                            {
                                                                AddtoList("5", "流速" + (layer + 1).ToString(), GetIntValueFromBit(16).ToString(), "");
                                                            }
                                                        }
                                                        for (int a = 0; a < layers; a++)
                                                        {
                                                            AddtoList("4", "层" + (a + 1).ToString() + "回波强度", "", "");
                                                            for (int layer = 0; layer < 4; layer++)
                                                            {
                                                                AddtoList("5", "回波强度" + (layer + 1).ToString(), GetIntValueFromBit(8).ToString(), "");
                                                            }
                                                        }
                                                        for (int layer = 0; layer < 4; layer++)
                                                        {
                                                            AddtoList("4", "测底距离" + (layer + 1).ToString(), GetIntValueFromBit(16).ToString(), "");
                                                        }
                                                        for (int layer = 0; layer < 4; layer++)
                                                        {
                                                            AddtoList("4", "底速" + (layer + 1).ToString(), GetIntValueFromBit(16).ToString(), "");
                                                        }
                                                        break;
                                                }
                                                break;
                                            default:
                                                byte[] b = GetByteValueFromBit(len - 36);
                                                AddtoList("3", "数据内容", CRCHelper.ConvertCharToHex(b, b.Length), Encoding.Default.GetString(b));//减去头和长度域
                                                optputdata += "数据内容:" + CRCHelper.ConvertCharToHex(b, b.Length) + "(HEX)" + "\r\n";
                                                break;
                                        }
                                        if (TestForm.isTest)
                                        {
                                            if (TestForm.ACPacketHandle != null)
                                                TestForm.ACPacketHandle.Set();
                                        }

                                    }
                                    break;
                                case 62:
                                    int deviceid = GetIntValueFromBit(8);
                                    AddtoList("3", "设备类型", deviceid.ToString(), Enum.GetName(typeof(DeviceAddr), deviceid));
                                    optputdata += "设备类型:" + Enum.GetName(typeof(DeviceAddr), deviceid) + "\r\n";
                                    int com = GetIntValueFromBit(8);
                                    AddtoList("3", "COM端口", com.ToString(), "COM" + com.ToString());
                                    optputdata += "COM端口:" + com.ToString() + "\r\n";
                                    switch (deviceid)
                                    {
                                        case 101:
                                            AddtoList("3", "AUV运行状态", "","" );
                                            int Type = GetIntValueFromBit(8);
                                            switch (Type)
                                            {
                                                case 0x54://"T"
                                                    AddtoList("3", "状态", "TimeOut", "设备超时");
                                                    GetIntValueFromBit(32);
                                                    GetIntValueFromBit(16);//跳过48个bit
                                                    break;
                                                case 0x4E://"N"
                                                    AddtoList("3", "状态", "No Device", "无设备");
                                                    GetIntValueFromBit(32);
                                                    GetIntValueFromBit(32);//跳过64个bit
                                                    break;
                                                default:
                                                    int depth = (Type << 8) + GetIntValueFromBit(8); 

                                                    AddtoList("4", "深度", depth.ToString(), ((double)depth/10).ToString() + "米");
                                                    depth = (GetIntValueFromBit(8) << 8) + GetIntValueFromBit(8);
                                                    AddtoList("4", "高度", depth.ToString(), ((double)depth / 100).ToString() + "米");
                                                    AddtoList("4", "位置", "", "");
                                                    int byte1 = GetIntValueFromBit(8);
                                                    int byte2 = GetIntValueFromBit(8);
                                                    int byte3 = GetIntValueFromBit(8);
                                                    int byte4 = GetIntValueFromBit(8);
                                                    int lat = (byte1 << 24) + (byte2 << 16) + (byte3 << 8) + byte4;
                                                    if (lat >= 0)
                                                        AddtoList("5", "北纬", lat.ToString(), ((double)lat / 10000 / 60 ).ToString() + "度");
                                                    else
                                                        AddtoList("5", "南纬", lat.ToString(), ((double)-lat / 10000 / 60).ToString() + "度");
                                                    ///
                                                    int lng = (GetIntValueFromBit(8) << 24) + (GetIntValueFromBit(8) << 16) + (GetIntValueFromBit(8) << 8) + GetIntValueFromBit(8);
                                                    if (lng >= 0)
                                                        AddtoList("5", "东经", lng.ToString(), ((double)lng / 10000 / 60).ToString() + "度");
                                                    else
                                                        AddtoList("5", "西经", lng.ToString(), ((double)-lng / 10000 / 60).ToString() + "度");
                                                    
                                                    int v = (GetIntValueFromBit(8) << 8) + GetIntValueFromBit(8);
                                                    Int16 velocity = (Int16)v;
                                                    if(velocity>=0)
                                                        AddtoList("4", "航速", velocity.ToString(), "前进" + (((double)velocity)/1000).ToString() + "节");
                                                    else
                                                        AddtoList("4", "航速", velocity.ToString(), "后退" + (-((double)velocity) / 1000).ToString() + "节");
                                                    AddtoList("4", "电池数据", "", "");
                                                    int auvvol = GetIntValueFromBit(8);
                                                    AddtoList("5", "AUV动力电压", auvvol.ToString(), auvvol.ToString() + "V");
                                                    int AuvI = (GetIntValueFromBit(8) << 8) + GetIntValueFromBit(8);

                                                    AddtoList("5", "电流", AuvI.ToString(), AuvI.ToString() + "mA");
                                                    int vleft = GetIntValueFromBit(8);
                                                    AddtoList("5", "剩余电量", vleft.ToString(), vleft.ToString() + "%");
                                                    int alarm = GetIntValueFromBit(8);
                                                    if (alarm == 0)
                                                        AddtoList("4", "无报警", "", "");
                                                    else
                                                    {
                                                        AddtoList("4", "AUV报警", "", "");
                                                        if ((alarm & 0x01) == 1)
                                                        {
                                                            AddtoList("5", "漏水报警", "", "");
                                                        }
                                                        if ((alarm & 0x02) == 1)
                                                        {
                                                            AddtoList("5", "温度报警", "", "");
                                                        }
                                                        if ((alarm & 0x04) == 1)
                                                        {
                                                            AddtoList("5", "低压报警", "", "");
                                                        }
                                                        if ((alarm & 0x08) == 1)
                                                        {
                                                            AddtoList("5", "高度报警", "", "");
                                                        }
                                                        if ((alarm & 0x10) == 1)
                                                        {
                                                            AddtoList("5", "深度报警", "", "");
                                                        }
                                                        if ((alarm & 0x20) == 1)
                                                        {
                                                            AddtoList("5", "障碍报警", "", "");
                                                        }
                                                        if ((alarm & 0x40) == 1)
                                                        {
                                                            AddtoList("5", "保留", "", "");
                                                        }
                                                        if ((alarm & 0x80) == 1)
                                                        {
                                                            AddtoList("5", "保留", "", "");
                                                        }

                                                    }
                                                    ///attitude
                                                    int Roll = (GetIntValueFromBit(8) << 8) + GetIntValueFromBit(8);
                                                    AddtoList("4", "姿态角", "", "");
                                                    AddtoList("5", "横滚角", Roll.ToString(), ((double)Roll/100).ToString() + "度");
                                                    int pitch = (GetIntValueFromBit(8) << 8) + GetIntValueFromBit(8);

                                                    AddtoList("5", "纵倾角", pitch.ToString(), ((double)pitch / 100).ToString() + "度");
                                                    int head = (GetIntValueFromBit(8) << 8) + GetIntValueFromBit(8);

                                                    AddtoList("5", "航向角", head.ToString(), ((double)head / 100).ToString() + "度");
                                                    //转速
                                                    
                                                    PointLatLng p = new PointLatLng((double)lat/ 10000 / 60,(double)lng/ 10000 / 60);
                                                    if ((p != PointLatLng.Zero) && (p.Lat < 90) && (p.Lat > -90) && (p.Lng < 180) && (p.Lng > -180) )
                                                        MainForm.pMainForm.mapdoc.AddNewNodeToMap("节点" + StartId.ToString(), p, 1, (float)head / 100);
                                                    
                                                    int fanspeed = (GetIntValueFromBit(8) << 8) + GetIntValueFromBit(8);
                                                    if (fanspeed >= 0)
                                                        AddtoList("4", "正转", fanspeed.ToString(), fanspeed.ToString() + "转");
                                                    else
                                                        AddtoList("4", "反转", fanspeed.ToString(), (-fanspeed).ToString() + "转");
                                                    ///
                                                    break;
                                            }
                                            break;
                                                                            
                                        default:
                                            byte[] statusb = GetByteValueFromBit(len - 36);
                                            AddtoList("3", "状态数据", CRCHelper.ConvertCharToHex(statusb, statusb.Length), Encoding.Default.GetString(statusb));
                                            optputdata += "状态数据:" + CRCHelper.ConvertCharToHex(statusb, statusb.Length) + "(HEX)" + "\r\n";
                                            break;
                                        
                                    }
                                    
                                    if (TestForm.isTest)
                                    {
                                        if (TestForm.ACPacketHandle != null)
                                            TestForm.ACPacketHandle.Set();
                                    }
                                    break;
                                case 63:
                                    int id = GetIntValueFromBit(8);
                                    AddtoList("3", "通信机类型", id.ToString(), Enum.GetName(typeof(NodeType),id));
                                    optputdata += "通信机类型:" + Enum.GetName(typeof(NodeType), id)+"\r\n";
                                    AddtoList("3", "状态数据", "", "");
                                    
                                    if (id == 0)
                                    {
                                        string timestr = "20" + GetIntValueFromBit(8) + " " + GetIntValueFromBit(8) + "-" + GetIntValueFromBit(8)
                                                            + " " + GetIntValueFromBit(8) + ":" + GetIntValueFromBit(8) + ":" + GetIntValueFromBit(8);
                                        AddtoList("4", "系统时间", timestr, "");
                                        optputdata += "系统时间:" + timestr + "\r\n";
                                        
                                        int nn = GetIntValueFromBit(16);
                                        AddtoList("4", "3.3V电压", nn.ToString(), ((double)nn / 100).ToString() + "V");
                                        optputdata += "3.3V电压:" + ((double)nn / 100).ToString() + "V" + "\r\n";
                                        nn = GetIntValueFromBit(16);
                                        AddtoList("4", "48V电压", nn.ToString(), ((double)nn / 100).ToString() + "V");
                                        optputdata += "48V电压:" + ((double)nn / 100).ToString() + "V" + "\r\n";
                                        nn = GetIntValueFromBit(32);
                                        AddtoList("4", "3.3V剩余电量", nn.ToString(), ((double)nn / 100).ToString() + "mA*h");
                                        optputdata += "3.3V剩余电量:" + ((double)nn / 100).ToString() + "mA*h" + "\r\n";
                                        nn = GetIntValueFromBit(32);
                                        AddtoList("4", "48V剩余电量", nn.ToString(), ((double)nn / 100).ToString() + "mA*h");
                                        optputdata += "48V剩余电量:" + ((double)nn / 100).ToString() + "mA*h" + "\r\n";
                                        nn = GetIntValueFromBit(16);
                                        AddtoList("4", "温度", nn.ToString(), ((double)nn / 100).ToString() + "°C");
                                        optputdata += "温度:" + ((double)nn / 100).ToString() + "°C" + "\r\n";
                                        nn = GetIntValueFromBit(8);
                                        AddtoList("4", "漏水", nn.ToString(), (nn == 1) ? "漏水啦！" : "无漏水");
                                        optputdata += "漏水:" + ((nn == 1) ? "漏水啦！" : "无漏水") + "\r\n";
                                        if (len == 212)
                                        {
                                            nn = GetIntValueFromBit(1);
                                            AddtoList("4", "发射自动调节开关", nn.ToString(), (nn == 1) ? "自动调节" : "固定");
                                            optputdata += "发射自动调节开关:" + ((nn == 1) ? "自动调节" : "固定") + "\r\n";
                                            nn = GetIntValueFromBit(7);
                                            AddtoList("4", "发射幅度设置", nn.ToString(), ((double)nn / 100).ToString());
                                            optputdata += "发射幅度设置:" + ((double)nn / 100).ToString() + "\r\n";
                                            nn = GetIntValueFromBit(1);
                                            AddtoList("4", "接收自动调节开关", nn.ToString(), (nn == 1) ? "自动调节" : "固定");
                                            optputdata += "接收自动调节开关:" + ((nn == 1) ? "自动调节" : "固定") + "\r\n";
                                            nn = GetIntValueFromBit(7);
                                            AddtoList("4", "接收增益设置", nn.ToString(), nn.ToString() + "dB");
                                            optputdata += "接收增益设置:" + nn.ToString() + "dB" + "\r\n";
                                            
                                        }
                                    }
                                    else
                                    {
                                        byte[] b = GetByteValueFromBit(len - 28);
                                        AddtoList("4", "通信机状态", CRCHelper.ConvertCharToHex(b, b.Length), "");//减去头和长度域
                                        optputdata += "通信机状态:" + CRCHelper.ConvertCharToHex(b, b.Length) + "(HEX)" + "\r\n";
                                    }
                                    if (TestForm.isTest)
                                    {
                                        if (TestForm.ACPacketHandle != null)
                                            TestForm.ACPacketHandle.Set();
                                    }
                                    break;
                                case 101:
                                    byte[] tb = GetByteValueFromBit(len - 20);
                                    AddtoList("3", "回环数据（16进制）", CRCHelper.ConvertCharToHex(tb, tb.Length), Encoding.Default.GetString(tb));
                                    break;
                                case 102:
                                    byte[] bb = GetByteValueFromBit(len - 20);
                                    MainForm.pMainForm.mapdoc.pf.ShowData(StartId, System.Text.Encoding.Default.GetString(bb));
                                    AddtoList("3", "应答回环数据（16进制）", CRCHelper.ConvertCharToHex(bb, bb.Length), Encoding.Default.GetString(bb));
                                    optputdata += "应答回环数据（16进制）:" + CRCHelper.ConvertCharToHex(bb, bb.Length) + "(HEX)" + "\r\n";
                                    if (TestForm.isTest)
                                    {
                                        if (TestForm.ACPacketHandle != null)
                                            TestForm.ACPacketHandle.Set();
                                    }
                                    break;
                                case 107:
                                    AddtoList("3", "命令", (string)WebId[sectorId], "");
                                    int key107 = GetIntValueFromBit(1);
                                    AddtoList("3", "索取网络表开关", key107.ToString(), (key107.ToString() == "1") ? "重建网络表" : "直接返回当前网络表");
                                    break;
                                case 111:
                                    AddtoList("3", "命令", (string)WebId[sectorId], "");
                                    int key = GetIntValueFromBit(1);
                                    AddtoList("3", "重建邻节点表开关", key.ToString(), (key.ToString() == "1") ? "重建邻节点表" : "直接返回当前邻节点表");
                                    break;
                            
                                case 115://比一般命令多一个comm
                                    //AddtoList("3", "命令", (string)WebId[sectorId], "");
                                    AddtoList("3", "COM端口", GetIntValueFromBit(8).ToString(), "");
                                    break;
                                case 117:
                                    //AddtoList("3", "命令", (string)WebId[sectorId], "");
                                    AddtoList("3", "COM端口", GetIntValueFromBit(8).ToString(), "");
                                    break;
                                case 119:
                                    //AddtoList("3", "命令", (string)WebId[sectorId], "");
                                    AddtoList("3", "COM端口", GetIntValueFromBit(8).ToString(), "");
                                    byte[] parab = GetByteValueFromBit(len - 28);
                                    AddtoList("3", "设置参数", CRCHelper.ConvertCharToHex(parab, parab.Length), Encoding.Default.GetString(parab));
                                    break;
                                case 120:
                                    //AddtoList("3", "命令", (string)WebId[sectorId], "");
                                    int DeviceId = GetIntValueFromBit(8);
                                    AddtoList("3", "设备类型", DeviceId.ToString(), Enum.GetName(typeof(DeviceAddr), DeviceId));
                                    optputdata += "设备类型:" + Enum.GetName(typeof(DeviceAddr), DeviceId) + "\r\n";
                                    int mn = GetIntValueFromBit(8);
                                    AddtoList("3", "COM端口", mn.ToString(), "");
                                    optputdata += "COM端口:" + mn.ToString() + "\r\n";
                                    byte[] devicestatus = GetByteValueFromBit(len - 20-8-8);
                                    AddtoList("3", "响应", CRCHelper.ConvertCharToHex(devicestatus, devicestatus.Length), Encoding.Default.GetString(devicestatus));
                                    optputdata += "响应:" + Encoding.Default.GetString(devicestatus) + "\r\n";
                                    break;
                                case 140:
                                    //AddtoList("3", "命令", (string)WebId[sectorId], "");
                                    mn = GetIntValueFromBit(32);
                                    AddtoList("3", "设备数据定时回传间隔", mn.ToString(), "");
                                    optputdata += "设备数据定时回传间隔:" + mn.ToString() + "\r\n";
                                    break;
                                case 141:
                                    AddtoList("3", "发射自动调节开关", GetIntValueFromBit(1).ToString(), "");
                                    AddtoList("3", "发射幅度设置", GetIntValueFromBit(7).ToString(), "");
                                    AddtoList("3", "接收自动调节开关", GetIntValueFromBit(1).ToString(), "");
                                    AddtoList("3", "接收增益设置", GetIntValueFromBit(7).ToString(), "");
                                    break;
                                case 142:
                                    int CommType = GetIntValueFromBit(16);
                                    string comstr="";
                                    byte[] bbb = BitConverter.GetBytes((short)CommType);
                                    BitArray ba = new BitArray(bbb);
                                    for (int ij = 0; ij < ba.Count; ij++)
                                    {
                                        comstr += ba[ij] ? "1" : "0";
                                    }
                                    comstr.PadRight(16,'0');
                                    AddtoList("3", "通信制式开关", comstr, "");
                                    
                                    break;
                                default:
                                    if (len > 20)
                                    {
                                        byte[] b = GetByteValueFromBit(len - 20);
                                        AddtoList("3", "数据", CRCHelper.ConvertCharToHex(b, b.Length), Encoding.Default.GetString(b));//减去头和长度域
                                        optputdata += "数据:" + CRCHelper.ConvertCharToHex(b, b.Length) + "(HEX)" + "\r\n";
                                    }
                                    else
                                    {
                                        AddtoList("3", "命令", (string)WebId[sectorId], "");
                                        optputdata += "命令:" + (string)WebId[sectorId] + "\r\n";
                                    }
                                    break;
                            }
                        }



                    }


                }
                if (SourceDataClass.isCommDepack == true)
                {
                    MainForm.pMainForm.mapdoc.SendData(optputdata);
                }
            return parselist;


        }
        #endregion

        #region 读取bit流方法
        /// <summary>
        /// 从数组中读取一定长度的bit转成32bit的整型数
        /// </summary>
        /// <param name="bitlen">读取的bit长度</param>
        /// <returns>返回32bit的整型数</returns>
        static public int GetIntValueFromBit(int bitlen)
        {
            int[] value = new int[1];
            BitArray ba = new BitArray(bitlen);
            for (int i = 0; i < bitlen; i++)
            {
                ba[i] = data[index + i];
            }
            index += bitlen;
   
            ba.CopyTo(value,0);
            return value[0];
        }

        /// <summary>
        /// 从数组中读取一定长度的bit转成byte数组
        /// </summary>
        /// <param name="bitlen">读取的bit长度</param>
        /// <returns>返回byte数组</returns>
        static public Byte[] GetByteValueFromBit(int bitlen)
        {
            Byte[] value = new Byte[(int)Math.Ceiling((double)bitlen/8)];
            Array.Clear(value, 0, (int)Math.Ceiling((double)bitlen / 8));
            BitArray ba = new BitArray(bitlen);
            for (int i = 0; i < bitlen; i++)
            {
                ba[i] = data[index + i];
            }
            index += bitlen;

            ba.CopyTo(value, 0);
            return value;
        }
        /// <summary>
        /// 从数组中读取一定长度的bit转成32bit的整型数16进制字符串
        /// </summary>
        /// <param name="bitlen">读取的bit长度</param>
        /// <returns>返回8bit的整型数16进制字符串</returns>
        static public string GetHexValueFromBit(int bitlen)
        {
            string s = Convert.ToString(GetIntValueFromBit(bitlen), 16);
            if (s.Length == 1)
            {
                s = "0" + s;
            }
            return  s;
        }

        /// <summary>
        /// 从数组中读取一定长度的bit转成字符串
        /// </summary>
        /// <param name="bitlen">读取的bit长度</param>
        /// <returns>返回字符串</returns>
        static public string GetASCValueFromBit(int bitlen)
        {
            byte[] b = GetByteValueFromBit(bitlen);
            string str = Encoding.Default.GetString(b);
            return  str;
        }
        #endregion

        #region 写入比特流方法
        /// <summary>
        /// 将int型数据写入当前bit流
        /// </summary>
        /// <param name="?"></param>
        static public void OutPutIntBit(int[] dat, int bitlen)
        {
            BitArray ba;
           
            ba = new BitArray(dat);
            for (int j = 0; j < bitlen; j++)
            {
                packdata[packindex + j] = ba[j];
            }
            packindex += bitlen;
        }
       /// <summary>
       /// 将bit流写入当前比特流之后
       /// </summary>
       static public void OutPutArrayBit(BitArray ba)
       {
           for (int j = 0; j < ba.Length; j++)
           {
               packdata[packindex + j] = ba[j];
           }
           packindex += ba.Length;
       }

        #endregion

        #endregion


    }
       

}
