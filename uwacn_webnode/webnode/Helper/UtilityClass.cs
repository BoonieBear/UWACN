using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Globalization;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Data;
using System.IO;
using System.Drawing;
//using System.Threading;
using System.Windows.Forms;
using System.Collections;
using webnode.Forms;
namespace webnode.Helper
{   
    /// <summary>
    /// 共用类，包括各种结构体定义，共用函数，及其他类需要的一切东东
    /// </summary>
    class UtilityClass
    {
        static double R = 6378137; // WGS-84;
        public struct UtcTime
        {
            public UInt16 year;
            public UInt16 mon;
            public UInt16 day;
            public UInt16 hour;
            public UInt16 min;
            public UInt16 sec;
            public override string ToString()
            {

                return year.ToString() + "-" + mon.ToString("00", CultureInfo.InvariantCulture) + "-" + day.ToString("00", CultureInfo.InvariantCulture) + " " + hour.ToString("00", CultureInfo.InvariantCulture) + ":" +
                  min.ToString("00", CultureInfo.InvariantCulture) + ":" + sec.ToString("00", CultureInfo.InvariantCulture);
            }
        }
        /// <summary>
        /// 将DataTable行列转换
        /// </summary>
        /// <param name="src">要转换的DataTable</param>
        /// <param name="columnHead">要作为Column的列</param>
        /// <returns></returns>
        public static DataTable Col2Row(DataTable src, int columnHead)
        {
            DataTable result = new DataTable();
            DataColumn myHead = src.Columns[columnHead];
            result.Columns.Add(myHead.ColumnName);
            for (int i = 0; i < src.Rows.Count; i++)
            {
                result.Columns.Add(src.Rows[i][myHead].ToString());
            }
            //
            foreach (DataColumn col in src.Columns)
            {
                if (col == myHead)
                    continue;
                object[] newRow = new object[src.Rows.Count + 1];
                newRow[0] = col.ColumnName;
                for (int i = 0; i < src.Rows.Count; i++)
                {
                    newRow[i + 1] = src.Rows[i][col];
                }
                result.Rows.Add(newRow);
            }
            return result;
        }


        /// <summary>
        /// 拷贝hash表内容，string类型
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static void CopyHashTableString(Hashtable src, Hashtable dst)
        {
            dst.Clear();
            foreach (object obj in src.Keys)
            {
                string nodename = (string)obj;
                string lst = (string)src[nodename];

                dst.Add(nodename, lst);

            }
        }
        /// <summary>
        /// 拷贝hash表内容，List<string>类型
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static void CopyHashTableStringList(Hashtable src, Hashtable dst)
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
        /// <summary>
        /// 拷贝hash表内容，List<string[]>类型
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static void CopyHashTableStringListArray(Hashtable src, Hashtable dst)
        {
            dst.Clear();
            foreach (object obj in src.Keys)
            {
                string nodename = (string)obj;
                List<string[]> lst = (List<string[]>)src[nodename];
                List<string[]> newlst = new List<string[]>(lst.Capacity);
                foreach (string[] str in lst)
                {
                    string[] newstr = new string[str.Length];
                    Array.Copy(str, newstr, str.Length);
                    newlst.Add(newstr);
                }
                dst.Add(nodename, newlst);

            }
        }
        public static DataTable Col2Row(DataTable src, string columnHead)
        {
            for (int i = 0; i < src.Columns.Count; i++)
            {
                if (src.Columns[i].ColumnName.ToUpper() == columnHead.ToUpper())
                    return Col2Row(src, i);
            }
            return new DataTable();
        }
        public static byte[] RawSerialize(object anything)
        {
            int rawsize = Marshal.SizeOf(anything);
            IntPtr buffer = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(anything, buffer, false);
            byte[] rawdatas = new byte[rawsize];
            Marshal.Copy(buffer, rawdatas, 0, rawsize);
            Marshal.FreeHGlobal(buffer);
            return rawdatas;
        }


        public static object RawDeserialize(byte[] rawdatas, Type anytype)
        {
            int rawsize = Marshal.SizeOf(anytype);
            //if (rawsize > rawdatas.Length)
            if (rawsize != rawdatas.Length)
                return null;
            IntPtr buffer = Marshal.AllocHGlobal(rawsize);
            Marshal.Copy(rawdatas, 0, buffer, rawsize);
            object retobj = Marshal.PtrToStructure(buffer, anytype);
            Marshal.FreeHGlobal(buffer);
            return retobj;
        }



        public static double CalcDistance(PointLatLng start, PointLatLng end)
        {
            double pidiv180 = Math.PI / 180;
            double a = Math.Pow(Math.Sin((start.Lat - end.Lat) / 2 * pidiv180), 2) 
                + Math.Cos(start.Lat * pidiv180) * Math.Cos(end.Lat*pidiv180)
                * Math.Pow(Math.Sin((start.Lng - end.Lng) / 2 * pidiv180), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a),Math.Sqrt(1-a));
            return R * c ;
        }

        /// <summary>
        /// 吸附窗体
        /// </summary>
        /// <param name="frm">要吸附边缘的窗体</param>
        /// <param name="frmHeight">窗体的高度</param>
        /// <param name="timer">定时器控件</param>
        //用法：在对应窗体timer控件的Tick事件中写代码 int height = this.Height; hide_show(this, ref height, timer1);
        public static void hide_show(Form frm, ref int frmHeight, Timer timer)
        {
            if (frm.WindowState != FormWindowState.Minimized)
            {
                timer.Interval = 100;
                if (Cursor.Position.X > frm.Left - 1 && Cursor.Position.X < frm.Right && Cursor.Position.Y > frm.Top - 1 && Cursor.Position.Y < frm.Bottom)
                {
                    if (frm.Top <= 0 && frm.Left > 5 && frm.Left < Screen.PrimaryScreen.WorkingArea.Width - frm.Width)
                    {
                        frm.Top = 0;
                    }
                    else if (frm.Left <= 0)
                    {
                        frm.Left = 0;
                    }
                    else if (frm.Left + frm.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    {
                        frm.Left = Screen.PrimaryScreen.WorkingArea.Width - frm.Width;
                    }
                    else if (frm.Top + frm.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    {
                        frm.Top = Screen.PrimaryScreen.WorkingArea.Height - frm.Height;
                    }
                    else
                    {
                        if (frmHeight > 0)
                        {
                            frm.Height = frmHeight;
                            frmHeight = 0;
                        }
                    }
                }
                //else
                //{
                //    if (frmHeight < 1)
                //    {
                //        frmHeight = frm.Height;
                //    }
                //    if (frm.Top <= 4 && frm.Left > 5 && frm.Left < Screen.PrimaryScreen.WorkingArea.Width - frm.Width)
                //    {
                //        frm.Top = 3 - frm.Height;
                //        if (frm.Left <= 4)
                //        {
                //            frm.Left = -5;
                //        }
                //        else if (frm.Left + frm.Width >= Screen.PrimaryScreen.WorkingArea.Width - 4)
                //        {
                //            frm.Left = Screen.PrimaryScreen.WorkingArea.Width - frm.Width + 5;
                //        }
                //    }
                //    else if (frm.Left <= 4)
                //    {
                //        frm.Left = 3 - frm.Width;
                //    }
                //    else if (frm.Left + frm.Width >= Screen.PrimaryScreen.WorkingArea.Width - 4)
                //    {
                //        frm.Left = Screen.PrimaryScreen.WorkingArea.Width - 3;
                //    }
                //}
            }
        }
        /// <summary>
        ///动画效果参数 
        /// </summary>
        public class AniParameter
        {
            Graphics g;
            MapCustom.Map map;
            PointLatLng p;
            int maxRadius;//最大半径
            int ElaspseTime;
            int radius;//当前半径
            string text;
            public AniParameter(ref MapCustom.Map Map, Graphics tg, PointLatLng tp, int tRadius, int elap,string str)
            {
                this.map = Map;
                g = tg;
                p = tp;
                maxRadius = tRadius;
                ElaspseTime = elap;
                radius = 1;
                text = str;
            }
            public string AniText
            {
                get
                {
                    return text;
                }
                set
                {
                    text = value;
                }
            }
            public MapCustom.Map mainmap
            {
                get
                {
                    return map;
                }
                set
                {
                    map = value;
                }
            }
            public Graphics Gp
            {
                get
                {
                    return g;
                }
                set 
                {
                    g = value;
                }
            }
            public PointLatLng AniPoint
            {
                get
                {
                    return p;
                }
                set
                {
                    p = value;
                }
            }
            public int R
            {
                get
                {
                    return maxRadius;
                }
                set
                {
                    maxRadius = value;
                }
            }
            public int r
            {
                get
                {
                    return radius;
                }
                set
                {
                    radius = value;
                }
            }
            public int TimeUp
            {
                get
                {
                    return ElaspseTime;
                }
                set
                {
                    ElaspseTime = value;
                }
            }
            
        }

        public class LineParameter
        {
            Graphics g;
            MapCustom.Map map;
            PointLatLng startp;
            PointLatLng endp;
            int ElaspseTime;
            string text;
            double length;//当前长度
            public LineParameter(ref MapCustom.Map Map,Graphics tg, PointLatLng sp, PointLatLng ep, int elap,string anitext)
            {
                this.map = Map;
                g = tg;
                startp = sp;
                endp = ep;
                ElaspseTime = elap;
                length = 0;
                text = anitext;
            }
            public MapCustom.Map mainmap
            {
                get
                {
                    return map;
                }
                set
                {
                    map = value;
                }
            }
            public double factor
            {
                get
                {
                    return length;
                }
                set
                {
                    length = value;
                }
            }
            public Graphics Gp
            {
                get
                {
                    return g;
                }
                set 
                {
                    g = value;
                }
            }
            public PointLatLng startAniPoint
            {
                get
                {
                    return startp;
                }
                set
                {
                    startp = value;
                }
            }
            public PointLatLng endAniPoint
            {
                get
                {
                    return endp;
                }
                set
                {
                    endp = value;
                }
            }
           
            public int TimeUp
            {
                get
                {
                    return ElaspseTime;
                }
                set
                {
                    ElaspseTime = value;
                }
            }
            public string AniText
            {
                get
                {
                    return text;
                }
                set
                {
                    text = value;
                }
            }
        }
    }
    /// <summary>
    /// 在picturebox中绘制波形图案，调用一次程序绘制一次
    /// </summary>
    class DrawWave
    {


        public Bitmap mybitmap;//用于双缓冲的位图，和画布等大
        public Bitmap psdbitmap;//用于双缓冲的位图，和画布等大
        public float ykedu;//给定的最大刻度与实际像素的比例关系
        public float xkedu;
        public PointF[] pdata;//实际显示的点
        public DrawWave(ref PictureBox picbox)
        {
            //length应等于picturebox宽度
            pdata = new PointF[picbox.Width];
            psdbitmap = new Bitmap(picbox.Width, picbox.Height);//设定位图大小
            Graphics psdbufferg = Graphics.FromImage(psdbitmap);//从位图上获取“画布”
            Rectangle rect = new Rectangle(0, 0, picbox.Width, picbox.Height);
            psdbufferg.FillRectangle(new SolidBrush(Color.Black), rect);

        }

        //ptlist长度必须等于xmax值,ymax要与ptlist中最大值一致
        public void DrawLineS(Color color, int Xmax, int Ymax, ref PictureBox picbox, ref short[] ptlist)
        {
            mybitmap = new Bitmap(picbox.Width, picbox.Height);//设定位图大小
            Graphics doublebufferg = Graphics.FromImage(mybitmap);//从位图上获取“画布”


            Rectangle rect = new Rectangle(0, 0, picbox.Width, picbox.Height);
            doublebufferg.FillRectangle(new SolidBrush(Color.Black), rect);


            //画X和Y轴
            DrawXY(ref doublebufferg, picbox);
            //Y轴上的刻度
            SetYAxis(ref doublebufferg, picbox, Ymax);
            //X轴上的刻度
            //SetXAxis(ref doublebufferg, picbox, Xmax);
            xkedu = (float)Xmax / (picbox.Width);
            for (int i = 0; i < pdata.Length; i++)
            {
                pdata[i].Y = (ptlist[i * (int)xkedu] + 32768) / ykedu;
                pdata[i].X = i;

            
                //doublebufferg.FillEllipse(new SolidBrush(Color.White), pdata[i].X, pdata[i].Y,2,2);
            }
            doublebufferg.DrawLines(new Pen(color), pdata);
            //将缓冲中的位图绘制到我们的窗体上
            using (Graphics g1 = picbox.CreateGraphics())//创建 PictureBox窗体的画布
            {
                g1.DrawImage(mybitmap, 0, 0);
                doublebufferg.Dispose();
            }

        }

        /// <summary>
        /// 绘制PSD图
        /// </summary>
        /// <param name="nfft">fft窗大小</param>
        /// <param name="ncount">psd缓冲前新数据长度</param>
        /// <param name="Ymax">纵轴值域</param>
        /// <param name="picbox">波形绘制所在picturebox</param>
        /// <param name="ptlist">psd缓冲</param>
        public void DrawPSD(int nfft, int ncount, int Ymax, PictureBox picbox, ref double[] ptlist)
        {
            Graphics psdbufferg = Graphics.FromImage(psdbitmap);//从位图上获取“画布”
            mybitmap = new Bitmap(picbox.Width, picbox.Height);//设定位图大小
            Graphics doublebufferg = Graphics.FromImage(mybitmap);//从位图上获取“画布”

            

            using (Graphics g1 = picbox.CreateGraphics())//创建 PictureBox窗体的画布
            {
                //搬移新数据
                if (ncount > 0)
                {
                    //Point np = new Point(ncount / nfft, 0);
                    //Point p = new Point(0, 0);
                    //Size sz = new Size(picbox.Width - ncount/nfft,picbox.Height);
                    RectangleF destRect = new RectangleF(0.0F, 0.0F, picbox.Width - ncount/nfft, picbox.Height);

                    // Create rectangle for source image.
                    RectangleF srcRect = new RectangleF(ncount / nfft, 0.0F, picbox.Width - ncount/nfft, picbox.Height);
                    GraphicsUnit units = GraphicsUnit.Pixel;

                    doublebufferg.DrawImage(psdbitmap,destRect, srcRect, units);

                    //Buffer.BlockCopy(ptlist, ncount, ptlist, 0, ptlist.Length - ncount);
                    //Buffer.BlockCopy(data, 0, ptlist, ptlist.Length - ncount, ncount);
                    float yfactor = (float)picbox.Height / nfft;

                    for (int i = 0; i < ncount / nfft; i++)
                    {
                        for (int j = 0; j < nfft; j++)
                        {
                            Color cr = Color.FromArgb(112,0,(int)ptlist[i * nfft + j]%255);
                            //因为从x为左边画椭圆，所以在画点时往左一像素，以免横移时出现空隙
                            doublebufferg.FillEllipse(new SolidBrush(cr), picbox.Width - ncount / nfft + i-1, j * yfactor, 2, 2 * yfactor);
                        }
                    }                
                    psdbufferg.DrawImage(mybitmap,0,0);
                    //画X和Y轴
                    DrawXY(ref doublebufferg, picbox);
                    //Y轴上的刻度
                    SetYAxis(ref doublebufferg, picbox, Ymax);
                    g1.DrawImage(mybitmap, 0, 0);
                }
                else
                {
                    doublebufferg.DrawImage(psdbitmap,0,0);
                    DrawXY(ref doublebufferg, picbox);
                    SetYAxis(ref doublebufferg, picbox, Ymax);
                    g1.DrawImage(mybitmap, 0, 0);
                }
                

           }

        }
           

        //完成X轴和Y轴的基本部分
        public void DrawXY(ref Graphics g, PictureBox picbox)
        {
            Pen pen = new Pen(Color.SlateGray, 1);//画笔
            SolidBrush sb = new SolidBrush(Color.White);//话刷

            //X轴的箭头，实际上是绘制了一个三角形
            Point[] xpts = new Point[3] { 
                new Point(picbox.Width - 15, picbox.Height / 2 + 2),
                new Point(picbox.Width - 15, picbox.Height / 2 - 2), 
                new Point(picbox.Width - 10, picbox.Height / 2 ) 
                                        };

            g.DrawLine(pen, 10, picbox.Height / 2, picbox.Width - 10, picbox.Height / 2);
            g.DrawPolygon(pen, xpts);
            //g.DrawString("X", new Font("宋体", 9), sb, picbox.Width - 5, picbox.Height / 2 - 2);



            //Y轴的箭头，实际上是绘制了一个三角形
            //Point[] ypts = new Point[3] { 
            //         new Point(28, 35), 
            //         new Point(30, 30), 
            //         new Point(32, 35) };

            //g.DrawLine(pen, 30, picbox.Height - 30, 30, 30);
            //g.DrawPolygon(pen, ypts);
            //g.DrawString("Y", new Font("宋体", 9), sb, 15, 30);

        }


        //绘制Y轴上的刻度
        public void SetYAxis(ref Graphics g, PictureBox picbox, int YMAX)
        {
            Pen p1 = new Pen(Color.White, 1);
            Pen p2 = new Pen(Color.White, 2);
            SolidBrush sb = new SolidBrush(Color.White);

            ykedu = (float)YMAX / (picbox.Height);//给定的最大刻度与实际像素的比例关系

            //第一个刻度的两个端点
            float xl = 10, yl = picbox.Height - 10, xr = 12, yr = picbox.Height - 10;

            for (int j = 0; j < picbox.Height ; j += 20)
            {

                if (j % 100 == 0)//一个大的刻度，黑色，每隔50像素一个
                {
                    g.DrawLine(p2, xl, yl - j, xr, yl - j);//刻度线
                    string tempy = (j * ykedu - YMAX/2).ToString("0");
                    g.DrawString(tempy, new Font("宋体", 7), sb, xl - 5, yl - j );
                }
                else//小刻度，金黄色，10像素一个
                { g.DrawLine(p1, xl, yl - j, xr, yl - j); }
            }
        }



        //绘制x轴上的刻度
        public void SetXAxis(ref Graphics g, PictureBox picbox, int XMAX)
        {
            Pen p1 = new Pen(Color.White, 1);
            Pen p2 = new Pen(Color.White, 2);
            SolidBrush sb = new SolidBrush(Color.White);

            xkedu = (float)XMAX / (picbox.Width);
            float xt = 10, yt = picbox.Height - 13, xb = 10, yb = picbox.Height - 8;

            for (int i = 0; i < picbox.Width; i += 10)
            {

                if (i % 100 == 0)
                {
                    g.DrawLine(p2, xt + i, yt, xb + i, yb);
                    string tempx = (i * xkedu).ToString();
                    g.DrawString(tempx, new Font("宋体", 7), sb, xt + i - 7, picbox.Height - 7);
                }
                else { g.DrawLine(p1, xt + i, yt, xb + i, yb); }
            }
        }



    }

    /// <summary>
    /// 快速傅立叶变换(Fast Fourier Transform)。 
    /// </summary>
    public class Transform
    {
        static private int n, nu;

        static private int BitReverse(int j)
        {
            int j2;
            int j1 = j;
            int k = 0;
            for (int i = 1; i <= nu; i++)
            {
                j2 = j1 / 2;
                k = 2 * k + j1 - 2 * j2;
                j1 = j2;
            }
            return k;
        }

        static public double[] FFTDb(ref double[] x,ref double[] y)
        {
            // Assume n is a power of 2
            n = x.Length;
            nu = (int)(Math.Log(n) / Math.Log(2));
            int n2 = n / 2;
            int nu1 = nu - 1;
            double[] xre = new double[n];
            double[] xim = new double[n];
            double[] decibel = new double[n];
            double tr, ti, p, arg, c, s;
            for (int i = 0; i < n; i++)
            {
                xre[i] = x[i];
                xim[i] = y[i];
            }
            int k = 0;
            for (int l = 1; l <= nu; l++)
            {
                while (k < n)
                {
                    for (int i = 1; i <= n2; i++)
                    {
                        p = BitReverse(k >> nu1);
                        arg = 2 * (double)Math.PI * p / n;
                        c = (double)Math.Cos(arg);
                        s = (double)Math.Sin(arg);
                        tr = xre[k + n2] * c + xim[k + n2] * s;
                        ti = xim[k + n2] * c - xre[k + n2] * s;
                        xre[k + n2] = xre[k] - tr;
                        xim[k + n2] = xim[k] - ti;
                        xre[k] += tr;
                        xim[k] += ti;
                        k++;
                    }
                    k += n2;
                }
                k = 0;
                nu1--;
                n2 = n2 / 2;
            }
            k = 0;
            int r;
            while (k < n)
            {
                r = BitReverse(k);
                if (r > k)
                {
                    tr = xre[k];
                    ti = xim[k];
                    xre[k] = xre[r];
                    xim[k] = xim[r];
                    xre[r] = tr;
                    xim[r] = ti;
                }
                k++;
            }
            for (int i = 0; i < n; i++)
                decibel[i] = 10.0 * Math.Log10((float)(Math.Sqrt((xre[i] * xre[i]) + (xim[i] * xim[i]))));
            return decibel;
        }
    }

    
    //深度数据管理类
    public class DepthInfoClass
    {
        public bool HasData;//是否有数据
        public int GridSizeX;//经度方向的矩阵维数
        public int GridSizeY;//纬度方向的矩阵维数
        public double StepX;//经度方向的步长，单位：度
        public double StepY;//纬度方向的步长，单位：度
        public double Lon0;//左下角的经度，单位：度
        public double Lat0;//左下角的纬度，单位：度
        BinaryReader Depthbr;//深度数据文件
        float subline = 0;//数据文件测量时水位-当前水位
        byte[] depth = null;//深度数据数组
        public DepthInfoClass()
        {
            HasData = false;
        }
        public void GetFileData(Stream depthfile)
        {

            if (!HasData)//没有数据
                Depthbr = new BinaryReader(depthfile);
            else
            {
                Depthbr.Close();
                depth = null;
                Depthbr = new BinaryReader(depthfile);
            }
            try
            {
                WaterLevelForm wf = new WaterLevelForm();
                if (wf.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        float dataline = float.Parse(wf.DataFileLine.Text);
                        float currentline = float.Parse(wf.CurrentLine.Text);
                        subline = dataline - currentline;
                    }
                    catch
                    {
                        throw new ArgumentNullException("输入数据格式不正确！");
                    }
                    
                }
                GridSizeX = Depthbr.ReadInt32();
                GridSizeY = Depthbr.ReadInt32();
                StepX = Depthbr.ReadDouble();
                StepY = Depthbr.ReadDouble();
                Lon0 = Depthbr.ReadDouble();
                Lat0 = Depthbr.ReadDouble();
                depth = Depthbr.ReadBytes(GridSizeX * GridSizeY * 4);
                HasData = true;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                HasData = false;
                depth = null;
                if (Depthbr != null)
                    Depthbr.Close();
            }
        }
        public float GetDepth(PointLatLng point)
        {
            try
            {
                if (HasData)//有数据
                {
                    double x = Math.Floor((point.Lng - Lon0) / StepX);//经度索引
                    
                    double y = Math.Floor((point.Lat - Lat0) / StepY);//纬度索引
                    if (x < 0 || x > GridSizeX - 1)
                        return 0;
                    if (y < 0 || y > GridSizeY - 1)
                        return 0;
                    float deep = BitConverter.ToSingle(depth, (int)(x * GridSizeY + y) * 4);
                    if (deep < 0.01)//无效数据
                        return 0;
                    else
                    {
                        deep -= subline;//减去水位差
                        if (deep > 0)
                            return deep;
                        else
                            return 0;
                    }
                }
                else
                {
                    return 0;//无效数据
                }
            }
            catch
            {
                MessageBox.Show("解析深度数据错误!");
                return 0;
            }
        }
    }

    //自定义图片地图类
    public class UserMap
    {
        private bool bLoaded;
        private string CfgName;
        private string PicName;
        private string FilePath;
        private string MapName;
        private Point RefPoint1;
        private Point RefPoint2;
        private PointLatLng RefPosition1;
        private PointLatLng RefPosition2;
        public UserMap()
        {
            Clean();
            
        }
        private void Clean()
        {
            bLoaded = false;
            MapName = "实验区图";
            CfgName = "";
            PicName = "";
            FilePath = "";
            RefPoint1 = Point.Empty;
            RefPoint2 = Point.Empty;
            RefPosition1 = PointLatLng.Zero;
            RefPosition2 = PointLatLng.Zero;
        }
        public bool LoadFile(string sFileName)
        {
            try
            {
                if(bLoaded)
                {
                    Clean();
                    bLoaded = false;
                }
                FileInfo fi = new FileInfo(sFileName);
                PicName = fi.Name;
                FilePath = fi.DirectoryName;
                if (HasCfg())
                {
                    if (ReadCfg())
                        bLoaded = true;
                }
                else
                {
                    if (CreateCfg(PicName + ".cfg"))
                    {
                        bLoaded = true;
                    }
                }

                return bLoaded;
            }
            catch
            {
                bLoaded = false;
                MessageBox.Show("文件无法正确载入！");
                return bLoaded;
            }
        }
        private bool ReadCfg()
        {
            if (CfgName != "")
            {
                try
                {
                    string[] str = { "自定义地图配置", "地图名称" };
                    if ((MapName = XmlHelper.GetConfigValue(CfgName, str)) != null)
                    {
                        string[] strname = { "自定义地图配置", "参考点1坐标" };
                        string point;
                        if ((point = XmlHelper.GetConfigValue(CfgName, strname)) != null)
                        {
                            return true;
                        }
                    }
                    return false;
                }
                catch
                {
                    MessageBox.Show("配置文件无法正确读出！");
                    return false;
                }
                
            }
            else
            {
                return false;
            }
        }
        private bool CreateCfg(string filename)
        {
            bool CrtOK = false;
            try
            {
                CrtOK = XmlHelper.CreateXmlDocument(filename, "自定义地图配置", "gb2312", FilePath);
                if(CrtOK)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("地图名称","实验区图");
                    ht.Add("地图文件名",PicName);
                    ht.Add("参考点1坐标",Point.Empty.ToString());
                    ht.Add("参考点2坐标", Point.Empty.ToString());
                    ht.Add("参考点1GPS", PointLatLng.Zero.ToString());
                    ht.Add("参考点2GPS", PointLatLng.Zero.ToString());
                    CrtOK = XmlHelper.InsertNode(filename, "Img文件名", false, "自定义地图配置", null, ht);
                    if(CrtOK)
                        CfgName = filename;
                }

            }
            catch(Exception e)
            {
                MessageBox.Show("创建.cfg文件错误:"+e.Message);
                CrtOK = false;
            }

            return CrtOK;
        }

        private bool HasCfg()
        {
            bool HasFile = false;
            try
            {
                DirectoryInfo di = new DirectoryInfo(FilePath);
                FileInfo[] fi = di.GetFiles("*.cfg");
                string[] str = { "自定义地图配置", "地图文件名" };
                foreach (FileInfo f in fi)
                {

                    if (XmlHelper.GetConfigValue(f.FullName, str)==PicName)
                    {
                        HasFile = true;
                        CfgName = f.FullName;
                        break; 
                    }
                    else
                    {
                        HasFile = false;
                        continue;
                    }
                    
                }
            }
            catch
            {
                MessageBox.Show("查找.cfg文件错误！");
                HasFile = false;
            }
            return HasFile;
        }
    }
	
}