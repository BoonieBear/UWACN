using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Collections;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using webnode.Helper;
using System.Xml;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
namespace webnode.Forms
{
    public partial class SetRouteForm : Office2007Form
    {
        // marker
        UserMarker NodeMarker;//gps位置
        GMapMarker CurrentMarker;//当前鼠标进入的节点
        GMapRoute CurrentRoute;//当前选择的路由
        string xmldoc;
        
        PointLatLng gtl;
        PointLatLng gbr;
        PointLatLng startp;
        PointLatLng endp;
        // layers
        internal GMapOverlay DistanceInfo;
        internal GMapOverlay objects;
        internal GMapOverlay routes;
        internal GMapOverlay Net;
        internal GMapOverlay rulers;
        internal GMapOverlay infolayers;
        internal GMapOverlay WebNodeLayer;
        Graphics g;
        public float bearing = 0.0F;
        PointLatLng GmapToGpsOffset = new PointLatLng(-0.002649654980715, -0.00476212229727);
        PointLatLng GpsToGmapOffset = new PointLatLng(0.002649654980715, 0.00476212229727);
        MapForm mf;
        List<string[]> NodeGrid = new List<string[]>();//单个节点路由表
        public Hashtable NewNetMap = new Hashtable();//当前节点连接表
        public Hashtable NewRouteMap = new Hashtable();//路由表
        public Hashtable DistMap = new Hashtable();//测量距离表
        bool iStartPoint = false;   //起点已选择标志
        //bool canSelectRoute = false;     //可以选择路由
        string StartNodeName;//起始点名称
        int GridIndex = -1;
        bool RouteisRunning = false;//路由动画标志
        Thread newthread = null;
        public SetRouteForm(MapForm mf)
        {
            InitializeComponent();
            this.mf = mf;
            objects = new GMapOverlay(routemap, "objects");
            routemap.Overlays.Add(objects);
            routes = new GMapOverlay(routemap, "routes");
            routemap.Overlays.Add(routes);
            Net = new GMapOverlay(routemap, "Net");
            routemap.Overlays.Add(Net);
            infolayers = new GMapOverlay(routemap, "Info");
            routemap.Overlays.Add(infolayers);

            WebNodeLayer = new GMapOverlay(routemap, "webnode");
            routemap.Overlays.Add(WebNodeLayer);
            g = routemap.CreateGraphics();
            routemap.Manager.Mode = AccessMode.ServerAndCache;
            string MyExecPath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            xmldoc = MyExecPath + "\\" + "config.xml"; 
        }

        

        private void SetRouteForm_Load(object sender, EventArgs e)
        {
            UtilityClass.CopyHashTableStringList(mf.NodeNetMap, NewNetMap);
            UtilityClass.CopyHashTableStringListArray(mf.NodeRouteMap, NewRouteMap);
            UtilityClass.CopyHashTableString(mf.DistMap, DistMap);
            double v = 0.5;
            // config map 
            string[] str = { "地图配置", "地图中心", "纬度" };
            double lat = double.Parse(XmlHelper.GetConfigValue(xmldoc, str));
            string[] lngstr = { "地图配置", "地图中心", "经度" };
            double lng = double.Parse(XmlHelper.GetConfigValue(xmldoc, lngstr));
            PointLatLng p = new PointLatLng(lat, lng);
            // set node marker

            p.Offset(GpsToGmapOffset);
            routemap.Position = p;
            NodeMarker = new UserMarker(p);
            NodeMarker.IsHitTestVisible = true;
            objects.Markers.Add(NodeMarker);
            NodeMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            CultureInfo ci = new CultureInfo("en-us");
            PointLatLng gpspos = NodeMarker.Position;
            gpspos.Offset(GmapToGpsOffset);
            NodeMarker.ToolTipText = "GPS\r\n{经度=" + gpspos.Lng.ToString("F08", ci) + "，纬度=" + gpspos.Lat.ToString("F08", ci) + "}";
            string[] offsetstr = { "地图配置", "偏移校准", "纬度" };
            string[] offsetstr1 = { "地图配置", "偏移校准", "经度" };
            string offset = XmlHelper.GetConfigValue(xmldoc, offsetstr);
            double latoffset = 0;
            double lngoffset = 0;
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

            routemap.MapType = mf.MainMap.MapType;
            routemap.MinZoom = 1;
            routemap.MaxZoom = 18;
            routemap.Zoom = 16;
            routemap.MapName = "路由设置";


            gtl = new PointLatLng(lat + v, lng - v);
            gtl.Offset(GpsToGmapOffset);
            gbr = new PointLatLng(lat - v, lng + v);
            gbr.Offset(GpsToGmapOffset);

            //读取节点
            XmlDocument xmlfile = new XmlDocument();
            xmlfile.Load(xmldoc);
            XmlNode xn = xmlfile.DocumentElement;
            xn = xn.SelectSingleNode("descendant::节点配置");
            foreach (XmlNode subnode in xn.ChildNodes)
            {
                NodeBox.Items.Add(subnode.Name);
                string[] sstr = { "节点配置", subnode.Name, "节点位置", "纬度" };
                double slat = double.Parse(XmlHelper.GetConfigValue(xmldoc, sstr));
                string[] lngsstr = { "节点配置", subnode.Name, "节点位置", "经度" };
                double slng = double.Parse(XmlHelper.GetConfigValue(xmldoc, lngsstr));
                //string[] desc = { "节点配置", subnode.Name, "节点描述" };
                //string strdes = XmlHelper.GetConfigValue(xmldoc, desc);
                PointLatLng pp = new PointLatLng(slat, slng);
                pp.Offset(GpsToGmapOffset);
                GMapMarkerGoogleGreen newnode = new GMapMarkerGoogleGreen(pp);
                newnode.Tag = subnode.Name;
                newnode.ToolTipMode = MarkerTooltipMode.Always;
                newnode.IsHitTestVisible = true;
                pp.Offset(GmapToGpsOffset);
                newnode.ToolTipText = newnode.Tag.ToString();
                WebNodeLayer.Markers.Add(newnode);
            }
            
            if (objects.Markers.Count > 0)
            {
                routemap.ZoomAndCenterMarkers(null);
                //ZoomSlider.Value = (int)MainMap.Zoom;
            }

            //AddRoutes();
            AddNet();
            RefreshNetGrid();//更新网络表
            //注册网络表改变事件
            Net.Routes.CollectionChanged += new GMap.NET.ObjectModel.NotifyCollectionChangedEventHandler(Net_CollectionChanged);

        }
        /*
        //添加预存的路由
        private void AddRoutes()
        {
            try
            {
                routes.Routes.Clear();
                if (NewRouteMap.Count == 0)
                    return;
                foreach (object obj in NewRouteMap.Keys)
                {
                    string node = (string)obj;

                    List<string[]> lst = (List<string[]>)NewRouteMap[node];
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
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                MessageBox.Show("路由表中节点与节点配置不一致，无法正常显示路由！");
            }

        }
         * */
        //添加预存的网络表
        private void AddNet()
        {
            try
            {
                Net.Routes.Clear();
                if (NewNetMap.Count == 0)
                    return;
                foreach (object obj in NewNetMap.Keys)
                {
                    string node = (string)obj;
                    if (PointLatLng.Zero == mf.GetNodeGPSPosition(node))//地图上没有不显示
                    {
                        continue;
                    }
                    bool isFindRoute = false;//找到路由标志。
                    List<string> lst = (List<string>)NewNetMap[node];
                    foreach (string endnode in lst)
                    {
                        isFindRoute = false;
                        if (PointLatLng.Zero == mf.GetNodeGPSPosition(endnode))//地图上没有不显示
                        {

                            continue;
                        }
                        
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
                            newroute.Stroke.Color = Color.WhiteSmoke;
                            newroute.Stroke.Width = 3;
                            Net.Routes.Add(newroute);
                        }
                    }
                }
                mf.AddNet();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message+":未成功添加网络表");
            }

        }

        //根据节点名取PointLatLng位置，未做偏移
        private PointLatLng GetGpsFromName(string name)
        {
            //读取节点

            string[] sstr = { "节点配置", name, "节点位置", "纬度" };
            double slat = double.Parse(XmlHelper.GetConfigValue(xmldoc, sstr));
            string[] lngsstr = { "节点配置", name, "节点位置", "经度" };
            double slng = double.Parse(XmlHelper.GetConfigValue(xmldoc, lngsstr));
            PointLatLng pp = new PointLatLng(slat, slng);
            return pp;


        }
        //更新路由表
        private void RefreshGrid(string startnode)
        {
            //路由表更新
            RouteGrid.Rows.Clear();
            if (startnode == null)//空字符
                return;
            if (!NewRouteMap.ContainsKey(startnode))//无路由记录
            {
                return;
            }
            NodeGrid = (List<string[]>)NewRouteMap[startnode];
           
            
            //List<string> tracelog = new List<string>();
            //NodeGrid.Clear();
            foreach (string[] log in NodeGrid)
            {

                string[] rowstr = { log[log.Length - 1].TrimStart('节', '点'), log[1].TrimStart('节', '点'), (log.Length - 1).ToString(), "1" };
                
                RouteGrid.Rows.Add(rowstr);
            }
            for (int i = 0; i < RouteGrid.Rows.Count; i++)
            {
                DataGridViewComboBoxCell Dcell  = (DataGridViewComboBoxCell)RouteGrid.Rows[i].Cells[4];
                Dcell.Value = Dcell.Items[0];
                Dcell.ToolTipText = Dcell.Value.ToString();
            }
            //RouteGrid.Sort(RouteGrid.Columns[0], ListSortDirection.Ascending);
            //

        }
        //领接节点表更新
        private void RefreshNeiborList(string startnode)
        {
            NeiborNodeLst.Rows.Clear();
            if (NewNetMap.Count == 0)
                return;
            IDictionaryEnumerator IdTor =  NewNetMap.GetEnumerator();
            while (IdTor.MoveNext())
            {
                DictionaryEntry de = (DictionaryEntry)IdTor.Current;
                if (startnode == (string)de.Key)
                {
                    string[] str = { "节点配置", (string)de.Key, "节点位置", "纬度" };
                    double lat = double.Parse(XmlHelper.GetConfigValue(xmldoc, str));
                    string[] lngstr = { "节点配置", (string)de.Key, "节点位置", "经度" };
                    double lng = double.Parse(XmlHelper.GetConfigValue(xmldoc, lngstr));
                    List<string> lst = (List<string>)NewNetMap[de.Key];
                    foreach (string name in lst)
                    {
                        string[] sstr = { "节点配置", name, "节点位置", "纬度" };
                        double slat = double.Parse(XmlHelper.GetConfigValue(xmldoc, sstr));
                        string[] lngsstr = { "节点配置", name, "节点位置", "经度" };
                        double slng = double.Parse(XmlHelper.GetConfigValue(xmldoc, lngsstr));
                        PointLatLng fr = new PointLatLng(lat, lng);
                        PointLatLng to = new PointLatLng(slat, slng);
                        double d = UtilityClass.CalcDistance(fr, to);
                        string[] rowstr = { ((string)de.Key).TrimStart('节', '点'), name.TrimStart('节', '点'), d.ToString("F01") };
                        NeiborNodeLst.Rows.Add(rowstr);
                    }
                }
            }
            NeiborNodeLst.Sort(NeiborNodeLst.Columns[0], ListSortDirection.Ascending);
            for (int i = 0; i < NeiborNodeLst.Rows.Count; i++)
            {
                DataGridViewComboBoxCell Dcell = (DataGridViewComboBoxCell)NeiborNodeLst.Rows[i].Cells[3];
                Dcell.Value = Dcell.Items[0];
                Dcell.ToolTipText = Dcell.Value.ToString();
            }
            //领接节点表更新
        }
        //网络表更新
        private void RefreshNetGrid()
        {
            NetGrid.Rows.Clear();
            IEnumerator<GMapRoute> ir =  Net.Routes.GetEnumerator();
            if (NewNetMap.Count == 0)
                return;
            //IDictionaryEnumerator IdTor = NewNetMap.GetEnumerator();
            while (ir.MoveNext())
            {
                double dist = 0;
                GMapRoute de = (GMapRoute)ir.Current;
                string[] name = de.Name.Split('-');
                if (DistMap.Contains(de.Name))
                {
                    dist = double.Parse((string)DistMap[de.Name]);
                }
                else
                {
                    dist = UtilityClass.CalcDistance(GetGpsFromName(name[0]),GetGpsFromName(name[1]));
                    DistMap.Add(de.Name, dist.ToString());
                }
                string[] rowstr = { (name[0]).TrimStart('节', '点'), name[1].TrimStart('节', '点'), (dist.ToString("F01")) };
                NetGrid.Rows.Add(rowstr);
                
            }
            NetGrid.Sort(NetGrid.Columns[0], ListSortDirection.Ascending);
            for (int i = 0; i < NetGrid.Rows.Count; i++)
            {
                DataGridViewComboBoxCell Dcell = (DataGridViewComboBoxCell)NetGrid.Rows[i].Cells[3];
                Dcell.Value = Dcell.Items[0];
                Dcell.ToolTipText = Dcell.Value.ToString();
            }
            
        }
        
        //网络表改变事件
        void Net_CollectionChanged(object sender, GMap.NET.ObjectModel.NotifyCollectionChangedEventArgs e)
        {
            string startnode = (string)NodeBox.SelectedItem;
            RefreshGrid(startnode);
            RefreshNeiborList(startnode);
            RefreshNetGrid();
            if (!NewNetMap.Equals(mf.NodeNetMap))
            {
                Reload.Enabled = true;
                BuildRouteBtn.Enabled = true;
            }
        }

        #region 操作事件
        //生产路由表
        private void BuildRouteBtn_Click(object sender, EventArgs e)
        {
            List<RouteBuildClass.SortPolicy> sp = new List<RouteBuildClass.SortPolicy>();
            sp.Add(RouteBuildClass.SortPolicy.MinHops);
            try
            {
                RouteBuildClass rbc = new RouteBuildClass(NewNetMap, DistMap, RouteBuildClass.FindPolicy.Recursion, sp, 2);
                NewRouteMap.Clear();
                NewRouteMap = rbc.FindRoutes();
                RefreshGrid(null);
                BuildRouteBtn.Enabled = false;
            }
            catch (Exception ee)
            {
                BuildRouteBtn.Enabled = true;
                MessageBox.Show(ee.Message);
         
            }
            
        }
        

       
        //保存配置为默认
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            mf.NodeRouteMap.Clear();
            mf.NodeNetMap.Clear();
            UtilityClass.CopyHashTableStringListArray(NewRouteMap, mf.NodeRouteMap);
            UtilityClass.CopyHashTableStringList(NewNetMap, mf.NodeNetMap);
            UtilityClass.CopyHashTableString(DistMap, mf.DistMap);
            //调用地图类的路由保存函数
            mf.SaveInit();
            mf.AddNet();
            //
            StatusLabel.Text = "路由表已保存！";
            Reload.Enabled = false;

        }
        //清除网络表，路由表
        private void ClearNetBtn_Click(object sender, EventArgs e)
        {
            string message = "确定清空？";
            string caption = "消息";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            MessageBoxIcon icon = MessageBoxIcon.Question;
            MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button2;
            // Show message box
            DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
            if (result == DialogResult.OK)
            {
                NewRouteMap.Clear();
                NewNetMap.Clear();
                DistMap.Clear();
                Net.Routes.Clear();
            }
        }
        private void Reload_Click(object sender, EventArgs e)
        {
            string message = "是否取消当前改变？";
            string caption = "消息";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            MessageBoxIcon icon = MessageBoxIcon.Question;
            MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button2;
            // Show message box
            DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
            if (result == DialogResult.OK)
            {
                UtilityClass.CopyHashTableStringListArray(mf.NodeRouteMap, NewRouteMap);
                UtilityClass.CopyHashTableStringList(mf.NodeNetMap, NewNetMap);
                Net.Routes.Clear();
                routes.Routes.Clear();
                AddNet();
                //AddRoutes();
                string startnode = (string)NodeBox.SelectedItem;
                RefreshGrid(startnode);
                RefreshNeiborList(startnode);
                RefreshNetGrid();
                Reload.Enabled = false;
                BuildRouteBtn.Enabled = false;
                StatusLabel.Text = "更改已撤销！";
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            if (Reload.Enabled)//未保存或未重置
            {
                string message = "是否保存当前改变？";
                string caption = "消息";
                MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                MessageBoxIcon icon = MessageBoxIcon.Question;
                MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button2;
                // Show message box
                DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
                if (result == DialogResult.OK)
                {
                    UtilityClass.CopyHashTableStringListArray(NewRouteMap, mf.NodeRouteMap);
                    UtilityClass.CopyHashTableStringList(NewNetMap, mf.NodeNetMap);
                    //调用地图类的路由保存函数
                    mf.SaveInit();
                    //
                    StatusLabel.Text = "路由表已保存！";
                    Reload.Enabled = false;
                    BuildRouteBtn.Enabled = false;
                    RouteisRunning = false;
                    if (newthread != null)
                    {
                        newthread.Abort();
                        newthread.Join();
                        //newthread = null;
                    }
                    this.Close();
                }
            }
            else
            {
                RouteisRunning = false;
                if (newthread != null)
                {
                    newthread.Abort();
                    newthread.Join();
                    //newthread = null;
                }
                this.Close();
            }
        }

        
        //选择节点事件
        private void NodeBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string startnode = (string)NodeBox.SelectedItem;
            RefreshNeiborList(startnode);
            RefreshGrid(startnode);

        }

        //判断是否选择了路由路线。
        private void routemap_MouseClick(object sender, MouseEventArgs e)
        {
            if (CurrentMarker != null)//点击节点时不响应
                return;
            PointLatLng p = routemap.FromLocalToLatLng(e.X, e.Y);
            foreach (GMapRoute r in Net.Routes)
            {

                double d = UtilityClass.CalcDistance(p, (PointLatLng)r.From) + UtilityClass.CalcDistance(p, (PointLatLng)r.To) - r.Distance * 1000;
                if (d < 10)
                {
                    if (CurrentRoute != null)
                        CurrentRoute.Stroke.Color = Color.WhiteSmoke;//将之前选择的路由颜色还原
                    if (!r.Equals(CurrentRoute))
                    {
                        CurrentRoute = r;//
                        r.Stroke.Color = Color.DeepPink;
                        break;//选择了另一节点，跳出
                    }
                    //又选择同一节点，说明是放弃选择
                    CurrentRoute = null;
                }
            }
        }
        
        private void routemap_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if(CurrentRoute != null)
                {
                    //将hash表中数据删除
                    string[] name = CurrentRoute.Name.Split('-');
                    if(NewNetMap.ContainsKey(name[0]))
                    {
                        List<string> t = (List<string>)NewNetMap[name[0]];
                        t.Remove(name[1]);
                        if (t.Count == 0)//没有任何连接
                        {
                            NewNetMap.Remove(name[0]);
                        }
                    }
                    //删除另一边记录
                    if (NewNetMap.ContainsKey(name[1]))
                    {
                        List<string> t = (List<string>)NewNetMap[name[1]];
                        t.Remove(name[0]);
                        if (t.Count == 0)//没有任何连接
                        {
                            NewNetMap.Remove(name[1]);
                        }
                    }
                    //删除路由记录
                    Net.Routes.Remove(CurrentRoute);
                    CurrentRoute = null;
                }
                
            }
        }
        
        //当前鼠标下的节点选择事件
        private void routemap_OnMarkerEnter(GMapMarker item)
        {
            CurrentMarker = item;
        }
        
        private void routemap_OnMarkerLeave(GMapMarker item)
        {
            CurrentMarker = null;
        }

        //节点选择事件
        private void routemap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (item.GetType().Equals(typeof(UserMarker)))
                return;
            if (!iStartPoint)//未确定起始点
            {
                iStartPoint = true;
                startp = item.Position;
                StartNodeName = item.Tag.ToString();
                this.Cursor = Cursors.Hand;
            }
            else//确定了起点
            {
                endp = item.Position;

                string EndNodeName = item.Tag.ToString();
                if (EndNodeName.Equals(StartNodeName))//相同节点无路由
                {
                    this.Cursor = Cursors.Default;
                    return;
                }
                //是否新路由？
                if (NewNetMap.ContainsKey(StartNodeName))//存在节点领接表
                {
                    List<string> t = (List<string>)NewNetMap[StartNodeName];
                    if (t.Contains(EndNodeName))
                    {
                        //存在路由，返回
                        this.Cursor = Cursors.Default;
                        iStartPoint = false;
                        return;
                    }
                    else//新路由;
                    {
                        
                        t.Add(EndNodeName);
                    }
                }
                else//if (NewNetMap.ContainsKey(StartNodeName))//存在节点领接表
                {
                    //新路由
                    List<string> newlst = new List<string> { };
                    newlst.Add(EndNodeName);
                    //新路由
                    NewNetMap.Add(StartNodeName, newlst);

                }

                //更新另一节点领接表
                if (NewNetMap.ContainsKey(EndNodeName))//存在另一节点领接表
                {
                    List<string> tt = (List<string>)NewNetMap[EndNodeName];
                    if (tt.Contains(StartNodeName))
                    {
                        //存在路由，返回，不可能发生
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    else
                    {
                        //新路由;
                        tt.Add(StartNodeName);
                    }
                }
                else//if (NewNetMap.ContainsKey(EndNodeName))//存在另一节点领接表
                {
                    //新路由
                    List<string> newlst = new List<string> { };
                    newlst.Add(StartNodeName);
                    //新路由
                    NewNetMap.Add(EndNodeName, newlst);
                }
                
                //
                iStartPoint = false;
                this.Cursor = Cursors.Default;
                List<PointLatLng> route = new List<PointLatLng>();
                route.Add(startp);
                route.Add(endp);
                GMapRoute newroute = new GMapRoute(route, string.Concat(StartNodeName, "-", EndNodeName));
                newroute.Stroke.Color = Color.WhiteSmoke;
                newroute.Stroke.Width = 3;
                Net.Routes.Add(newroute);
            }
        }

        //选择路由显示
        private void RouteGrid_SelectionChanged(object sender, EventArgs e)
        {
            RouteisRunning = false;
            if (newthread != null)
            {
                if (newthread.IsAlive)
                {
                    newthread.Abort();
                    newthread.Join();
                    //newthread = null;
                }
            }
            if ((NodeGrid.Count > 0) && (RouteGrid.SelectedRows.Count > 0))
            {
                DataGridViewRow Dr = RouteGrid.SelectedRows[0];
                GridIndex = Dr.Index;
                List<PointLatLng> plist = new List<PointLatLng> ();
                string[] tracemap = NodeGrid[GridIndex];
                foreach (string node in tracemap)
                {
                    string[] str = { "节点配置", node, "节点位置", "纬度" };
                    double lat = double.Parse(XmlHelper.GetConfigValue(xmldoc, str));
                    string[] lngstr = { "节点配置", node, "节点位置", "经度" };
                    double lng = double.Parse(XmlHelper.GetConfigValue(xmldoc, lngstr));
                    PointLatLng p = new PointLatLng(lat, lng);
                    p.Offset(GpsToGmapOffset);  
                    plist.Add(p);
                }
                //GMapRoute r = new GMapRoute(plist,"");
                //r.Stroke.Color = Color.Blue;
                //WebNodeLayer.Routes.Clear();//先清除之前显示的路由
                //WebNodeLayer.Routes.Add(r);
                if (ShowRouteAni.Checked)
                {
                    RouteisRunning = true;
                    newthread = new Thread(ShowLineAni);
                    newthread.Start((object)plist);
                }
            }

        }

        private void RouteGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (GridIndex > -1)
                {
                    string message = "是否删除当前选择路由？";
                    string caption = "询问";
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                    MessageBoxIcon icon = MessageBoxIcon.Question;
                    MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button2;
                    // Show message box
                    DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
                    if (result == DialogResult.OK)
                    {
                        NodeGrid.RemoveAt(GridIndex);//在当前选择时删除这次选定的路由，重新选择节点后恢复
                        RouteGrid.Rows.RemoveAt(GridIndex);//在列表删除之后执行，显示新的路由
                        
                    }
                }
            }
        }

        private void ShowLineAni(object obj)
        {
            List<PointLatLng> plist = (List<PointLatLng>)obj;
            PointLatLng lastp = new PointLatLng();
            while (RouteisRunning)//循环演示
            {
                foreach (PointLatLng p in plist)
                {
                    if (!lastp.IsZero)
                    {
                        int time = (int)(UtilityClass.CalcDistance(lastp, p) * 1000 / 1500);
                        mf.LineEmitAnimation(ref routemap, lastp, p, time,"");
                        Thread.Sleep(time);
                    }
                    lastp = p;

                }
                //遍历完清零
                lastp.Lat = 0;
                lastp.Lng = 0;
            }
        }

        private void ShowRouteAni_CheckedChanged(object sender, EventArgs e)
        {
            if (!ShowRouteAni.Checked)
            {
                RouteisRunning = false;
                if (newthread != null)
                {
                    newthread.Abort();
                    newthread.Join();
                    //newthread = null;
                }
            }
        }


        private void AddRouteToList_Click(object sender, EventArgs e)
        {
            int nodenum = RouteGrid.RowCount;
            int[] dat = new int[1];
            if(RouteGrid.RowCount>0)
            {
                SourceDataClass.InitForPack(nodenum * 33 + 6 + 20);
                dat[0] = 6;
                SourceDataClass.OutPutIntBit(dat,8);
                dat[0] = nodenum * 33 + 6 + 20;
                SourceDataClass.OutPutIntBit(dat, 12);
                dat[0] = nodenum;
                SourceDataClass.OutPutIntBit(dat, 6);//路由条数
                for (int i = 0; i < nodenum; i++)
                {
                    dat[0] = int.Parse(RouteGrid.Rows[i].Cells[0].Value.ToString());
                    SourceDataClass.OutPutIntBit(dat, 6);//目标节点
                    dat[0] = int.Parse(RouteGrid.Rows[i].Cells[1].Value.ToString());
                    SourceDataClass.OutPutIntBit(dat, 6);//下一跳地址
                    dat[0] = int.Parse(RouteGrid.Rows[i].Cells[2].Value.ToString());//跳数
                    SourceDataClass.OutPutIntBit(dat, 4);//跳数
                    dat[0] = int.Parse(RouteGrid.Rows[i].Cells[3].Value.ToString());
                    SourceDataClass.OutPutIntBit(dat, 15);
                    int value = Convert.ToInt32(Enum.Parse(typeof(SourceDataClass.RouteStatus), RouteGrid.Rows[i].Cells[4].Value.ToString()));
                    dat[0] = value;
                    SourceDataClass.OutPutIntBit(dat, 2);
                }

                //加入列表
                MainForm.pMainForm.comlistwin.AddCmd(NodeBox.Text,"路由表",SourceDataClass.packdata) ;
                StatusLabel.Text = "路由表已加入命令列表";

                MainForm.pMainForm.RefreshListStat();
                MessageBox.Show("路由表已加入命令列表!");
            }
        }
        
        private void AddNeiborToList_Click(object sender, EventArgs e)
        {
            
                int nodenum = NeiborNodeLst.RowCount;
                int[] dat = new int[1];
                if (nodenum > 0)
                {

                    SourceDataClass.InitForPack(nodenum * 24 + 4 + 20);
                    dat[0] = 3;
                    SourceDataClass.OutPutIntBit(dat, 8);
                    dat[0] = nodenum * 24 + 4 + 20;
                    SourceDataClass.OutPutIntBit(dat, 12);
                    dat[0] = nodenum;
                    SourceDataClass.OutPutIntBit(dat, 4);//邻节点数
                    for (int i = 0; i < nodenum; i++)
                    {
                        dat[0] = int.Parse(NeiborNodeLst.Rows[i].Cells[1].Value.ToString());
                        SourceDataClass.OutPutIntBit(dat, 6);//邻节点
                        dat[0] = (int)(double.Parse(NeiborNodeLst.Rows[i].Cells[2].Value.ToString()) * 10);
                        SourceDataClass.OutPutIntBit(dat, 16);//距离
                        int value = Convert.ToInt32(Enum.Parse(typeof(SourceDataClass.ChannlValue), NeiborNodeLst.Rows[i].Cells[3].Value.ToString()));
                        dat[0] = value;
                        SourceDataClass.OutPutIntBit(dat, 2);//评价
                    }

                    //加入列表
                    MainForm.pMainForm.comlistwin.AddCmd(NodeBox.Text, "邻节点表", SourceDataClass.packdata);
                    StatusLabel.Text = "邻节点表已加入命令列表";
                    MainForm.pMainForm.RefreshListStat();
                    MessageBox.Show("邻节点表已加入命令列表!");
                }
           

        }

        
        private void AddAllToCmd_Click(object sender, EventArgs e)
        {
            string message = "要广播网络表吗？";
            string caption = "网络表命令";
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            MessageBoxIcon icon = MessageBoxIcon.Question;
            MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button1;
            // Show message box
            DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
            if (result == DialogResult.Yes)
            {

                int nodenum = NetGrid.RowCount;
                int[] dat = new int[1];
                if (nodenum > 0)
                {

                    SourceDataClass.InitForPack(nodenum * 30 + 4 + 20);
                    dat[0] = 4;
                    SourceDataClass.OutPutIntBit(dat, 8);
                    dat[0] = nodenum * 30 + 4 + 20;
                    SourceDataClass.OutPutIntBit(dat, 12);
                    dat[0] = nodenum;
                    SourceDataClass.OutPutIntBit(dat, 4);//邻节点数
                    for (int i = 0; i < nodenum; i++)
                    {
                        dat[0] = int.Parse(NetGrid.Rows[i].Cells[1].Value.ToString());
                        SourceDataClass.OutPutIntBit(dat, 6);//邻节点
                        dat[0] = (int)(double.Parse(NetGrid.Rows[i].Cells[2].Value.ToString()) * 10);
                        SourceDataClass.OutPutIntBit(dat, 16);//距离
                        int value = Convert.ToInt32(Enum.Parse(typeof(SourceDataClass.ChannlValue), NetGrid.Rows[i].Cells[3].Value.ToString()));
                        dat[0] = value;
                        SourceDataClass.OutPutIntBit(dat, 2);//评价
                    }

                    //加入列表
                    MainForm.pMainForm.comlistwin.AddCmd("节点00", "网络表", SourceDataClass.packdata);
                    StatusLabel.Text = "网络表已加入命令列表";
                    MessageBox.Show("网络表已加入命令列表!");
                }
            }
            else
            {
                if (result == DialogResult.No)
                {
                    if (NodeBox.Text == "")
                    {
                        MessageBox.Show("请选择目标节点");
                        return;
                    }
                    int nodenum = NetGrid.RowCount;
                    int[] dat = new int[1];
                    if (nodenum > 0)
                    {
                        SourceDataClass.InitForPack(nodenum * 30 + 4 + 20);
                        dat[0] = 3;
                        SourceDataClass.OutPutIntBit(dat, 8);
                        dat[0] = nodenum * 30 + 4 + 20;
                        SourceDataClass.OutPutIntBit(dat, 12);
                        dat[0] = nodenum;
                        SourceDataClass.OutPutIntBit(dat, 4);//邻节点数
                        for (int i = 0; i < nodenum; i++)
                        {
                            dat[0] = int.Parse(NetGrid.Rows[i].Cells[1].Value.ToString());
                            SourceDataClass.OutPutIntBit(dat, 6);//邻节点
                            dat[0] = (int)(double.Parse(NetGrid.Rows[i].Cells[2].Value.ToString()) * 10);
                            SourceDataClass.OutPutIntBit(dat, 16);//距离
                            int value = Convert.ToInt32(Enum.Parse(typeof(SourceDataClass.ChannlValue), NetGrid.Rows[i].Cells[3].Value.ToString()));
                            dat[0] = value;
                            SourceDataClass.OutPutIntBit(dat, 2);//评价
                        }

                        //加入列表
                        MainForm.pMainForm.comlistwin.AddCmd(NodeBox.Text, "网络表", SourceDataClass.packdata);
                        StatusLabel.Text = "网络表已加入命令列表";
                        MessageBox.Show("网络表已加入命令列表!");
                    }
                }
            }
        }

        private void AddRoute_Click(object sender, EventArgs e)
        {
            foreach (string startnode in NewRouteMap.Keys)
            {
                NodeGrid = (List<string[]>)NewRouteMap[startnode];

                int nodenum = NodeGrid.Count;
                int[] dat = new int[1];
                SourceDataClass.InitForPack(nodenum * 33 + 6 + 20);
                dat[0] = 6;
                SourceDataClass.OutPutIntBit(dat,8);
                dat[0] = nodenum * 33 + 6 + 20;
                SourceDataClass.OutPutIntBit(dat, 12);
                dat[0] = nodenum;
                SourceDataClass.OutPutIntBit(dat, 6);//路由条数
                foreach (string[] log in NodeGrid)
                {
                    dat[0] = int.Parse(log[log.Length - 1].TrimStart('节', '点'));
                    SourceDataClass.OutPutIntBit(dat, 6);
                    dat[0] = int.Parse(log[1].TrimStart('节', '点'));
                    SourceDataClass.OutPutIntBit(dat, 6);
                    dat[0] = log.Length - 1;
                    SourceDataClass.OutPutIntBit(dat,4);
                    dat[0] = 0 ;//节点序列号
                    SourceDataClass.OutPutIntBit(dat,15);
                    dat[0] = 0 ;//路由状态
                    SourceDataClass.OutPutIntBit(dat, 2);
                    
                }
                MainForm.pMainForm.comlistwin.AddCmd(startnode, "路由表", SourceDataClass.packdata);
            }
            StatusLabel.Text = "全路由表已加入命令列表";

            MainForm.pMainForm.RefreshListStat();
            MessageBox.Show("全路由表已加入命令列表!");

        }
        #endregion

       

       

        

        

       


        

   

    }
}
