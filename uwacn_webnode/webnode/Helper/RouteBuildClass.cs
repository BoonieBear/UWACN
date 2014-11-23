using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using GMap.NET;
using System.Windows.Forms;
using System.Collections;
namespace webnode.Helper
{
    class RouteBuildClass
    {
        #region 枚举常量
        //路由排序策略
        public enum SortPolicy
        {
            
            MinHops =1,//最小跳数
            MinMaxDist =2,//最小最大跳距准则
            TotalDist=3,//路由距离

        }

        public enum FindPolicy
        { 
            Recursion = 0,//递归
        }

        #endregion

        #region 私有成员
        
        
        Hashtable netgrid = new Hashtable();//网络表
        Hashtable dist = new Hashtable();//网络表
        FindPolicy findtype = FindPolicy.Recursion;//路由生产方法
        static List<SortPolicy> sorttype = new List<SortPolicy>();//排序方法
        int reserveRoute = 2;//每点保留路径数
        static Hashtable SortFuncHandle = new Hashtable();//排序算法hash表
        string xmldoc;
        private void CopyHashTable(Hashtable src, Hashtable dst)
        {
            dst.Clear();
            foreach (object obj in src.Keys)
            {
                string nodename = (string)obj;
                List<string> lst = (List<string>)src[nodename];
                List<string> newlst = new List<string>(lst.Capacity);
                foreach (string str in lst)
                {
                    string newstr = str;
                    newlst.Add(newstr);
                }
                dst.Add(nodename, newlst);

            }
        }
        #endregion

        #region 公有成员
        public string progress;
        int progressint = 0;
        delegate int SortFunc(string[] x, string[] y);
        #endregion

        #region 公共成员函数
        /// <summary>
        /// 路由生成构造函数
        /// </summary>
        /// <param name="bp">生成路由策略</param>
        /// <param name="sp">路由排序策略</param>
        /// <param name="reserve">保留路径数</param>
        public RouteBuildClass(Hashtable netlist, Hashtable dist,FindPolicy bp, List<SortPolicy> sp, int reserve)
        {
            
            CopyHashTable(netlist,netgrid );//不修改内容，用引用即可
            UtilityClass.CopyHashTableString( dist,this.dist);
            findtype = bp;
            if(sp!=null)
                sorttype = sp;
            InitTable();
            reserveRoute = reserve;
            string MyExecPath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            xmldoc = MyExecPath + "\\" + "config.xml"; 

        }

        //默认构造
        public RouteBuildClass()
        {
            InitTable();
            findtype = FindPolicy.Recursion;
            
        }

        //初始化排序方法列表
        public void InitTable()
        {
            if (sorttype.Count == 0)
            {
                sorttype.Add(SortPolicy.MinHops);
                SortFunc MinHops = SortByMinHops;
                SortFuncHandle.Add(SortPolicy.MinHops, MinHops);
            }
            else
            {
                foreach (SortPolicy sp in sorttype)
                {
                    switch (sp)
                    {
                        case SortPolicy.MinHops:
                            SortFunc MinHops = SortByMinHops;
                            if (!SortFuncHandle.ContainsKey(SortPolicy.MinHops))
                                SortFuncHandle.Add(SortPolicy.MinHops, MinHops);
                            break;
                        case SortPolicy.MinMaxDist:
                            SortFunc MinMax = SortByMinMaxDist;
                            if (!SortFuncHandle.ContainsKey(SortPolicy.MinMaxDist))
                                SortFuncHandle.Add(SortPolicy.MinMaxDist, MinMax);
                            break;
                        case SortPolicy.TotalDist:
                            SortFunc TotalDist = SortByTotalDist;
                            if (!SortFuncHandle.ContainsKey(SortPolicy.TotalDist)) 
                                SortFuncHandle.Add(SortPolicy.TotalDist, TotalDist);
                            break;
                        default:
                            MinHops = SortByMinHops;
                            if (!SortFuncHandle.ContainsKey(SortPolicy.MinHops))
                                SortFuncHandle.Add(SortPolicy.MinHops, MinHops); 
                            break;
                    } 
                }
            }
        }

        /// <summary>
        /// 生成路由表并排序
        /// </summary>
        public Hashtable FindRoutes()
        {
            Hashtable routes = new Hashtable();
            List<string> tracelog = new List<string>();
            
            
            foreach (string startnode in netgrid.Keys)
            {
                List<string[]> grid = new List<string[]>();
                progress = "正在自动生成"+startnode+"路由……";
                FindByRecursion(startnode,ref tracelog,ref grid);
                tracelog.Clear();//删除节点记录
                
                Hashtable ht =  GroupRouteList(ref grid);
                DelRoutesByreserveRouteAndNext(ref ht);
                grid.Clear();
                foreach (string s in ht.Keys)
                {
                    grid.AddRange((List<string[]>)ht[s]);
                }
                //加入路由表中
                routes.Add(startnode, grid);
                
            }

            progress = "路由表生成完毕！";
            return routes;
            
        }

        /// <summary>
        /// 将某点路由按终点存放在hash表中待排序
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        private Hashtable GroupRouteList(ref List<string[]>grid)
        {
            Hashtable ht = new Hashtable();
            for (int i = 0; i < grid.Count; i++)
            {
                string[] g = grid[i];
                if (ht.ContainsKey(g[g.Length - 1]))
                {
                    List<string[]> l = (List<string[]>)ht[g[g.Length - 1]];
                    l.Add(g);
                }
                else
                {
                    List<string[]> l = new List<string[]>();
                    l.Add(g);
                    ht.Add(g[g.Length - 1], l);
                }
            }
            return ht;
        }
        #endregion

        #region 路由删除算法
        /// <summary>
        /// 根据保留路由数以及下一跳是否一致来删除到某终点路由表中的路由
        /// </summary>
        /// <param name="grid"></param>
        private void DelRoutesByreserveRouteAndNext(ref Hashtable grid)
        {
            foreach (string node in grid.Keys)
            {
                List<string[]> l = (List<string[]>)grid[node];
                GridStringComparer sc = new GridStringComparer();
                l.Sort(sc);
                if (l.Count > reserveRoute)//大于保留数的路由删掉
                    l.RemoveRange(2, l.Count - 2);
                if (l.Count >1)//还剩至少2条路由
                {
                    while ((l[l.Count - 1][1] == l[l.Count - 2][1]))//下一跳一样
                    {
                        l.RemoveAt(l.Count - 1);
                        if (l.Count == 1)
                            break;
                    }
                }
            }
            
        }
        #endregion


        #region 路由生成算法

        /// <summary>
        /// 递归生成路由表
        /// </summary>
        /// <param name="startnode">节点</param>
        /// <param name="tracelog">路径记录</param>
        /// <param name="grid">当前点路由</param>
        private void FindByRecursion(string startnode,ref List<string> tracelog, ref List<string[]> grid)
        {
            if (netgrid.ContainsKey(startnode))
            {
                tracelog.Add(startnode);
                if ((int.Parse(startnode.TrimStart('节','点')) != 1) || (tracelog.Count<=1))//不以节点1为起始点
                {
                    //Debug.WriteLine("add node to tracelog:{0}", startnode);
                    List<string> lst = (List<string>)netgrid[startnode];
                    //查找是否领接点都在log中
                    foreach (string node in lst)
                    {
                        if (!tracelog.Contains(node))//不存在log中
                        {
                            FindByRecursion(node, ref tracelog, ref grid);//继续找
                        }
                    }
                }
                //找完所有领接点了
                //Debug.WriteLine("end search!");
                if (tracelog.Count > 1)//排除只有唯一节点的记录，即起始节点自己
                {
                    string[] newroute = new string[tracelog.Count];
                    tracelog.CopyTo(newroute);
                    tracelog.RemoveAt(tracelog.Count - 1);//删掉最后一个记录，供上一次调用使用
                    grid.Add(newroute);

                }
            }
            
        }
        #endregion

        #region 各种排序算法
        //根据目标节点对了路由表进行排序
        //按跳数排序
        private int SortByMinHops(string[] x, string[] y)
        {
            int retval = 0;
            if (x.Length < y.Length)
                retval = -1;
            else if (x.Length > y.Length)
            {
                retval = 1;
            }
            else
                retval =0;
            return retval;
        }
        /// <summary>
        /// 按最小最大间距原则排序
        /// </summary>
        private int SortByMinMaxDist(string[] x, string[] y)
        {

            double max_x = 0;
            double max_y = 0;
            for (int i = 0; i < x.Length - 1; i++)//只运行x.Length-1次，算节点间隔次数
            {
                double distance = 0;
                string lenname = x[i] + "-" + x[i + 1];
                string lenname1 = x[i+1] + "-" + x[i];
                if (dist.Contains(lenname))
                {
                    distance = double.Parse((string)dist[lenname]);
                }
                else if (dist.Contains(lenname1))
                {
                    distance = double.Parse((string)dist[lenname1]);
                }
                else//正常来说不可能的
                {
                    distance = 0;
                }
                if (distance > max_x)
                    max_x = distance;
            }
            for (int i = 0; i < y.Length - 1; i++)//只运行y.Length-1次，算节点间隔次数
            {
                double distance = 0;
                string lenname = y[i] + "-" + y[i + 1];
                string lenname1 = y[i + 1] + "-" + y[i];
                if (dist.Contains(lenname))
                {
                    distance = double.Parse((string)dist[lenname]);
                }
                else if (dist.Contains(lenname1))
                {
                    distance = double.Parse((string)dist[lenname1]);
                }
                else//正常来说不可能的
                {
                    distance = 0;
                }
                if (distance > max_y)
                    max_y = distance;
            }
            if (max_x < max_y)
                return -1;
            else if (max_x > max_y)
                return 1;
            else 
                return 0;
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

        /// <summary>
        /// 按总距离排序
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int SortByTotalDist(string[] x, string[] y)
        {
            double total_x = 0;
            double total_y = 0;
            for (int i = 0; i < x.Length - 1; i++)//只运行x.Length-1次，算节点间隔次数
            {
                double distance = 0;
                string lenname = x[i] + "-" + x[i + 1];
                string lenname1 = x[i + 1] + "-" + x[i];
                if (dist.Contains(lenname))
                {
                    distance = double.Parse((string)dist[lenname]);
                }
                else if (dist.Contains(lenname1))
                {
                    distance = double.Parse((string)dist[lenname1]);
                }
                else//正常来说不可能的
                {
                    distance = 0;
                }
                total_x += distance;
            }
            for (int i = 0; i < y.Length - 1; i++)//只运行y.Length-1次，算节点间隔次数
            {
                double distance = 0;
                string lenname = x[i] + "-" + x[i + 1];
                string lenname1 = x[i + 1] + "-" + x[i];
                if (dist.Contains(lenname))
                {
                    distance = double.Parse((string)dist[lenname]);
                }
                else if (dist.Contains(lenname1))
                {
                    distance = double.Parse((string)dist[lenname1]);
                }
                else//正常来说不可能的
                {
                    distance = 0;
                }
                total_y += distance;
            }
            if (total_x < total_y)
                return -1;
            else if (total_x > total_y)
                return 1;
            else
                return 0;
        }

        #region 排序用比较器
        
        public class GridStringComparer : IComparer<string[]>
        {
            
            public int Compare(string[] x, string[] y)
            {
                int index = 0;
                int retval=0;
                while ((retval == 0)&&(index < sorttype.Count))//如果比较相等且比较函数未到最后一个
                {
                    SortFunc func = (SortFunc)SortFuncHandle[sorttype[index]];
                    retval = func(x, y);
                    index++;
                }
                return retval;

            }
        }

        #endregion
        
        #endregion
 

    }
}
