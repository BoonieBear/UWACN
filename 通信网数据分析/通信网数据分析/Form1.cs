using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Globalization;
namespace 通信网数据分析
{
    public partial class Form1:Form
    {
        private bool isCommType = false;//
        private List<string> TimeLst = new List<string>();
        private List<string[]> DetailLstOfData = new List<string[]>();//数据解析后形成的列表
        //string[15] 内容：节点ID,收到数据时间,路径记录,节点类型,节点状态数据(16进制),3.3V电压,48V电压,3.3V剩余电量,48V剩余电量,节点温度,漏水报警,发射自动调节开关,发射幅度设置,接收自动调节开关,接收增益设置
        private List<string[]> DetailLstOfStatus = new List<string[]>();//数据解析后形成的状态列表
        //string[8] 内容：
        private Hashtable TimeData = new Hashtable();//与收到数据对应的数据hash表，用于打开特定的文件
        delegate void ParseDirectoryCallback();
        string MyExecPath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        DirectoryInfo ADPathInfo;
        public Form1()
        {
            InitializeComponent();
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
            SourceDataClass.WebId.Add(200, "全网复位");
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
            SourceDataClass.DataId.Add(170, "DSP回传数据命令");
            SourceDataClass.DataId.Add(171, "上位机下达DSP命令");
        }

        private void OpenNewWindows()
        {
            NodedataForm ndf = new NodedataForm(TimeLst,TimeData, DetailLstOfStatus, DetailLstOfData);
            ndf.MdiParent = this;
            ndf.Show();
            ndf.WindowState = FormWindowState.Maximized;
        }

        private void about_Click(object sender, EventArgs e)
        {
 
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
            ab.Dispose();
        }

        
        private void opensinglefile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    BinaryReader br = new BinaryReader(openFileDialog.OpenFile());
                    int a = br.PeekChar();
                    if ((char)a == 0x45)
                        isCommType = true;
                    else
                        isCommType = false;//默认网络数据
                    byte[] b = br.ReadBytes((int)openFileDialog.OpenFile().Length);
                    if (isCommType)
                    {
                        int NodeId;
                        string time;
                        byte[] data;
                        if (DepackData(b, out NodeId, out time, out data))
                        {
                            
                                DataViewForm dvf = new DataViewForm();
                                dvf.str = CRCHelper.ConvertCharToHex(data, data.Length);
                                dvf.Text = openFileDialog.FileName;
                                dvf.MdiParent = this;
                                dvf.ShowData();
                                dvf.WindowState = FormWindowState.Maximized;

                        }
                        else
                        {
                            throw new Exception("数据校验错误！");
                        }
                    }
                    else
                    {
                        DataViewForm dvf = new DataViewForm();

                        dvf.str = CRCHelper.ConvertCharToHex(b, b.Length);
                        dvf.Text = openFileDialog.FileName;
                        dvf.MdiParent = this;
                        dvf.ShowData();
                        dvf.WindowState = FormWindowState.Maximized;
                    }
                    br.Close();
                }
                catch (Exception MyEx)
                {
                    MessageBox.Show("数据格式错误！" + MyEx.Message);
                }
            }
        }
        //解EB90协议，读出源节点，串口收到时间，转发类型id及解开协议后的数据体。
        private bool DepackData(byte[] indata, out int NodeId, out string time, out byte[] data)
        {
            try
            {
                string strcmd = Encoding.ASCII.GetString(indata);//字符形式的命令，用于读取EB90协议的数据，内部数据用cmd存取。
                string[] str = strcmd.Split(',');
                NodeId = int.Parse(str[2]);
                int id;
                if (!SourceDataClass.DepackCommData(indata, out time, out id, out data))
                {
                    
                    throw new Exception("数据校验错误");
                    
                }
                return true;
               
            }
            catch (Exception e)
            {
                NodeId = 0;
                time = null;
                data = null;
                Debug.WriteLine(e.ToString());
                return false;
            }
        }

        private void quit_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }
            Application.Exit();
        }

        private void closeallWindows_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }
            
        }
        //根据目标节点对了路由表进行排序
        private class DataTimeComparer : IComparer<FileInfo>
        {
            public int Compare(FileInfo x, FileInfo y)
            {

                return DateTime.Compare(x.LastWriteTime, y.LastWriteTime); 

            }
        }
        private void opendirectory_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string directorypath = folderBrowserDialog.SelectedPath;
                DirectoryInfo di = new DirectoryInfo(directorypath);
                FileInfo[] fi =  di.GetFiles("MSPPackageData*.dat");
                if(fi.Length==0)
                {
                    MessageBox.Show("当前文件夹没有符合格式MSPPackageData*.dat的文件！");
                    return;
                }
                List<FileInfo> lf = new List<FileInfo>(fi);
                
                
                isCommType = true;
                TimeData.Clear();
                TimeLst.Clear();
                DetailLstOfData.Clear();
                DetailLstOfStatus.Clear();
                FilesWorker.RunWorkerAsync(lf);
                
                
            }
        }

        private void FilesWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<FileInfo> lf = (List<FileInfo>)e.Argument;
            DataTimeComparer dc = new DataTimeComparer();
            lf.Sort(dc);
            BackgroundWorker worker = sender as BackgroundWorker;
            
            for (int i = 0; i < lf.Count; i++)
            {
                using (BinaryReader br = new BinaryReader(lf[i].OpenRead()))
                {   
                    byte[] b = br.ReadBytes((int)lf[i].Length);
                    
                    int NodeId;
                    string time;
                    byte[] data;

                    if (isCommType)//串口数据
                    {
                        if (DepackData(b, out NodeId, out time, out data))
                        {
                            if (NodeId != 0)//收到的节点号
                            {
                                try
                                {
                                    SourceDataClass.GetData(data);
                                    SourceDataClass.ParseToLst();
                                    if (SourceDataClass.datalist.Count > 0)
                                    {
                                        foreach (string[] s in SourceDataClass.datalist)
                                        {
                                            s[2] = time;
                                        }
                                        DetailLstOfData.AddRange(SourceDataClass.datalist);
                                    }
                                    if (SourceDataClass.statuslist.Count>0)
                                    {
                                        foreach (string[] s in SourceDataClass.statuslist)
                                        {
                                            s[1] = time;
                                        }

                                        DetailLstOfStatus.AddRange(SourceDataClass.statuslist);
                                    }
                                    TimeLst.Add(time);
                                    TimeData.Add(time, data);
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine(ex.Message);
                                    worker.ReportProgress((i+1) * 100 / lf.Count);
                                    continue;

                                }


                            }
                        }
                        else
                        {
                            FileInfo f = new FileInfo(lf[i].FullName);
                            Debug.WriteLine(lf[i].FullName + "解析错误");
                        }
                    }
                    else//网络数据
                    {
                        try
                        {
                            SourceDataClass.GetData(b);
                            int ret = SourceDataClass.ParseToLst();
                            time = lf[i].LastWriteTime.ToString();
                            if (ret == 1)
                            {
                                time += "(回环应答)";
                            }
                            if (SourceDataClass.datalist.Count > 0)
                            {
                                foreach (string[] s in SourceDataClass.datalist)
                                {
                                    s[2] = time;
                                }
                                DetailLstOfData.AddRange(SourceDataClass.datalist);
                            }
                            if (SourceDataClass.statuslist.Count>0)
                            {
                                foreach (string[] s in SourceDataClass.statuslist)
                                {
                                    s[1] = time;
                                }

                                DetailLstOfStatus.AddRange(SourceDataClass.statuslist);
                            }
                            TimeLst.Add(time);
                            TimeData.Add(time, b);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.StackTrace);
                            worker.ReportProgress((i+1) * 100 / lf.Count);
                            continue;

                        }
                    }

                }
                worker.ReportProgress((i+1) * 100 / lf.Count);
            }
            
            
        }

        private void FilesWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FileProgress.Visible = true;
            FileProgress.Value = e.ProgressPercentage;
            StatusLabel.Text = "已处理" + e.ProgressPercentage.ToString() + "%";
        }

        private void FilesWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {

                StatusLabel.Text = "数据未正确处理，错误：" + e.Error.ToString();
            }
            else
            {

                StatusLabel.Text = "数据处理完毕！";
                ParseDirectoryCallback caller = new ParseDirectoryCallback(OpenNewWindows);
                
                caller.Invoke();
            }
            
            FileProgress.Visible = false;
        }

        private void opennetfile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    BinaryReader br = new BinaryReader(openFileDialog.OpenFile());
                    
                    byte[] b = br.ReadBytes((int)openFileDialog.OpenFile().Length);

                    DataViewForm dvf = new DataViewForm();

                    dvf.str = CRCHelper.ConvertCharToHex(b, b.Length);
                    dvf.Text = openFileDialog.FileName;
                    dvf.MdiParent = this;
                    dvf.ShowData();
                    dvf.WindowState = FormWindowState.Maximized;

                    br.Close();
                }
                catch (Exception MyEx)
                {
                    MessageBox.Show("数据格式错误！" + MyEx.Message);
                }
            }
        }

        private void opennetdirectory_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string directorypath = folderBrowserDialog.SelectedPath;
                DirectoryInfo di = new DirectoryInfo(directorypath);
                FileInfo[] fi = di.GetFiles("NetRecvData*.dat");
                if (fi.Length == 0)
                {
                    MessageBox.Show("当前文件夹没有符合格式NetRecvData*.dat的文件！");
                    return;
                }
                List<FileInfo> lf = new List<FileInfo>(fi);
                isCommType = false;
                TimeData.Clear();
                TimeLst.Clear();
                DetailLstOfData.Clear();
                DetailLstOfStatus.Clear();
                FilesWorker.RunWorkerAsync(lf);
            }
            
        }

        private void Translate_Click(object sender, EventArgs e)
        {
            try
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string directorypath = folderBrowserDialog.SelectedPath;
                    DirectoryInfo di = new DirectoryInfo(directorypath);
                    FileInfo[] fi = di.GetFiles("NetRecvData*.dat");
                    if (fi.Length == 0)
                    {
                        MessageBox.Show("当前文件夹没有符合格式NetRecvData*.dat的文件！");
                        return;
                    }
                    List<FileInfo> lf = new List<FileInfo>(fi);
                    DataTimeComparer dc = new DataTimeComparer();
                    lf.Sort(dc);
                    string log = MyExecPath + "\\Msp_LOG";
                    Directory.CreateDirectory(log);
                    string LogPath = log + "\\" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString();
                    ADPathInfo = Directory.CreateDirectory(LogPath);

                    TranslateWorker.RunWorkerAsync(lf);
                }
            }
            catch (Exception xe)
            {
                MessageBox.Show(xe.Message);
            }
        }

        private void TranslateWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<FileInfo> lf = (List<FileInfo>)e.Argument;
            BackgroundWorker worker = sender as BackgroundWorker;

            for (int i = 0; i < lf.Count; i++)
            {
                using (BinaryReader br = new BinaryReader(lf[i].OpenRead()))
                {
                    byte[] b = br.ReadBytes((int)lf[i].Length);

                    try
                    {
                        byte[] outcmd = SourceDataClass.CommPackage(170,b);
                        string timestring = lf[i].Name.Replace("NetRecvData", "");
                        timestring = timestring.TrimEnd('.', 'd', 'a', 't');
                        string filename = ADPathInfo.FullName + "\\MSPPackageData" + timestring + ".dat";
                        using (BinaryWriter bw = new BinaryWriter(File.Open(filename, FileMode.OpenOrCreate)))
                        {
                            bw.Write(outcmd);
                        }
                        worker.ReportProgress(i * 100 / (lf.Count - 1));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        worker.ReportProgress(i * 100 / (lf.Count - 1));
                        continue;

                    }
                }
            }

        }

        private void TranslateWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FileProgress.Visible = true;
            FileProgress.Value = e.ProgressPercentage;
            StatusLabel.Text = "已处理" + e.ProgressPercentage.ToString() + "%";
        }

        private void TranslateWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {

                StatusLabel.Text = "数据未正确转换，错误：" + e.Error.ToString();
            }
            else
            {

                StatusLabel.Text = "数据处理完毕！";
                MessageBox.Show("数据已经批量处理完毕！");
            }

            FileProgress.Visible = false;
        }
    }
}
