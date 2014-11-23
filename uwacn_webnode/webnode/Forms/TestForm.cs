using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Globalization;
using System.IO;
using webnode.Helper;
using System.Threading;
namespace webnode.Forms
{
    public partial class TestForm : Office2007Form
    {
        private int linkid = 1;
        private string[] testid;
        private int currentid;
        private string[] testip;
        private int idindex = 0;
        private int times = 0;
        private string command;
        private int interval = 300;
        private int tick = 0;
        private static int timeout = 60;
        private static LogFile SystemLog = new LogFile("Test");
        public static EventWaitHandle  ACPacketHandle;
        delegate void WriteLogCallback(string str);
        delegate void ChangeStateCallback(bool state);
        public delegate void WriteLogEventHandler(string str);//串口数据处理委托
        public static event WriteLogEventHandler WriteLogEvent;
        public static bool isTest = false;
        public TestForm()
        {
            ACPacketHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                if (OpenTestFile.ShowDialog() == DialogResult.OK)
                {
                    string str;
                    StreamReader sr = new StreamReader(OpenTestFile.OpenFile(), Encoding.GetEncoding(936));
                    str = sr.ReadLine();  //标题
                    string[] title = str.Split('=');
                    TaskBox.AppendText(title[0]);
                    TaskBox.AppendText(title[1]);
                    TaskBox.AppendText("\r\n");
                    str = sr.ReadLine();  //说明
                    title = str.Split('=');
                    TaskBox.AppendText(title[0]);
                    TaskBox.AppendText(title[1]);
                    TaskBox.AppendText("\r\n");
                    str = sr.ReadLine();//节点
                    title = str.Split('=');
                    TaskBox.AppendText(title[0]);
                    TaskBox.AppendText(title[1]);
                    linkid = int.Parse(title[1]);
                    TaskBox.AppendText("\r\n");
                    str = sr.ReadLine();
                    title = str.Split('=', '{', '}');//测试点

                    TaskBox.AppendText(title[0]);
                    TaskBox.AppendText(title[1]);
                    TaskBox.AppendText(title[2]);
                    testid = title[2].Split(' ');
                    TaskBox.AppendText("\r\n");

                    str = sr.ReadLine();
                    title = str.Split('=', '{', '}');//测试点

                    TaskBox.AppendText(title[0]);
                    TaskBox.AppendText(title[1]);
                    TaskBox.AppendText(title[2].Replace(" ", ","));
                    testip = title[2].Split(' ');
                    TaskBox.AppendText("\r\n");

                    str = sr.ReadLine();  //
                    title = str.Split('=');
                    TaskBox.AppendText(title[0]);
                    TaskBox.AppendText(title[1]);
                    command = title[1].Replace(" ","");
                    TaskBox.AppendText("\r\n");

                    str = sr.ReadLine();  //
                    title = str.Split('=');
                    TaskBox.AppendText(title[0]);
                    TaskBox.AppendText(title[1]);
                    TaskBox.AppendText("秒");
                    interval = int.Parse(title[1]);
                    TaskBox.AppendText("\r\n");
                    str = sr.ReadLine();  //
                    title = str.Split('=');
                    TaskBox.AppendText(title[0]);
                    TaskBox.AppendText(title[1]);
                    TaskBox.AppendText("秒");
                    timeout = int.Parse(title[1]);
                    TaskBox.AppendText("\r\n");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void writelog(string str)
        {
            try
            {
                if (testlog.InvokeRequired)//不是同一线程调用，调用控件的委托
                {

                    WriteLogCallback d = new WriteLogCallback(writelog);
                    this.Invoke(d, new object[] { str });

                }
                else
                {
                    string time = "(" + DateTime.Now.Month.ToString("00", CultureInfo.InvariantCulture) + "/" + DateTime.Now.Day.ToString("00", CultureInfo.InvariantCulture) + " " + DateTime.Now.Hour.ToString("00", CultureInfo.InvariantCulture)
                                + ":" + DateTime.Now.Minute.ToString("00", CultureInfo.InvariantCulture) + ":" + DateTime.Now.Second.ToString("00", CultureInfo.InvariantCulture) + ")";
                    testlog.AppendText(time + str + "\r\n");
                    testlog.ScrollToCaret();
                    if (SystemLog.logfile.ws == null)//还未创建文件
                        SystemLog.OpenFile(MainForm.pMainForm.RecordInfo);
                    SystemLog.writeLine(time + str);
                    if (SystemLog.length >= 1024 * 1024)//不允许大于1M
                    {
                        SystemLog.close();
                    }
                }
            }
            catch
            { }
        }
        private void teststart_Click(object sender, EventArgs e)
        {
            isTest = true;
            SendMsg();
            timer.Enabled = true;
            teststart.Enabled = false;
            StopTest.Enabled = true;
            buttonX1.Enabled = false;
            
        }
        private void SendMsg()
        {
            try
            {

                int[] b = new int[1];
                int blocklen = 34 + 6+20;//
                SourceDataClass.InitForPack(blocklen);

                b[0] = 1;
                SourceDataClass.OutPutIntBit(b, 6);
                BitArray blockba = new BitArray(blocklen);
                b[0] = ComListForm.PackageIndex;
                ComListForm.PackageIndex++;
                SourceDataClass.OutPutIntBit(b, 10);
                b[0] = blocklen - 6;//仅包括块头定义,块长度不包括块数长度
                SourceDataClass.OutPutIntBit(b, 12);
                b[0] = linkid;
                SourceDataClass.OutPutIntBit(b, 6);
                currentid = int.Parse(testid[idindex]);
                idindex++;
                if (idindex == testid.Length)
                    idindex = 0;
                b[0] = currentid;
                SourceDataClass.OutPutIntBit(b, 6);


                if (command == "获取节点信息")
                {
                    b[0] = 103;
                    SourceDataClass.OutPutIntBit(b, 8);
                    b[0] = 20;
                    SourceDataClass.OutPutIntBit(b, 12);


                }
                else if (command == "获取路由信息")
                {
                    b[0] = 113;
                    SourceDataClass.OutPutIntBit(b, 8);
                    b[0] = 20;
                    SourceDataClass.OutPutIntBit(b, 12);


                }
                else if (command == "获取设备数据")
                {
                    b[0] = 115;
                    SourceDataClass.OutPutIntBit(b, 8);
                    b[0] = 28;
                    SourceDataClass.OutPutIntBit(b, 12);
                    b[0] = 2;
                    SourceDataClass.OutPutIntBit(b, 8);
                    blocklen += 8;

                }
                else if (command == "获取邻节点信息")
                {
                    b[0] = 111;
                    SourceDataClass.OutPutIntBit(b, 8);
                    b[0] = 20;
                    SourceDataClass.OutPutIntBit(b, 12);


                }
                else if (command == "获取网络简表")
                {
                    b[0] = 109;
                    SourceDataClass.OutPutIntBit(b, 8);
                    b[0] = 20;
                    SourceDataClass.OutPutIntBit(b, 12);


                }
                else if (command == "获取节点信息表")
                {
                    b[0] = 105;
                    SourceDataClass.OutPutIntBit(b, 8);
                    b[0] = 20;
                    SourceDataClass.OutPutIntBit(b, 12);


                }
                else if (command == "获取网络表")
                {
                    b[0] = 107;
                    SourceDataClass.OutPutIntBit(b, 8);
                    b[0] = 20;
                    SourceDataClass.OutPutIntBit(b, 12);

                }
                else if (command == "回环测试")
                {
                    b[0] = 101;
                    SourceDataClass.OutPutIntBit(b, 8);
                    b[0] = 20;
                    SourceDataClass.OutPutIntBit(b, 12);

                }
                else if (command == "获取节点状态")
                {
                    b[0] = 119;
                    SourceDataClass.OutPutIntBit(b, 8);
                    b[0] = 20;
                    SourceDataClass.OutPutIntBit(b, 12);

                }
                byte[] cmd = new byte[(int)Math.Ceiling(((double)blocklen) / 8)];
                SourceDataClass.packdata.CopyTo(cmd, 0);
                byte[] Serialcmd = SourceDataClass.CommPackage(171, cmd);
                //BinaryWriter bw = new BinaryWriter(new FileStream("ping.dat", FileMode.Create));
                //bw.Write(cmd);
                //bw.Close();
                if (MainForm.pMainForm.mapdoc.WriteMSPCommand(Serialcmd))
                {
                    MainForm.pMainForm.mapdoc.MSPCmdFile.OpenFile(MainForm.pMainForm.SerialCmdPathInfo);
                    MainForm.pMainForm.mapdoc.MSPCmdFile.BinaryWrite(Serialcmd);
                    MainForm.pMainForm.mapdoc.MSPCmdFile.close();
                    writelog("向节点" + currentid.ToString() + "发送串口命令：" + command);
                    times++;
                    Thread wait = new Thread(waitforans);
                    wait.Start();
                    testwait.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                writelog(ex.Message);
                testwait.Enabled = true;
            }
        }
        private static void  waitforans()
        {
            try
            {
                if (!ACPacketHandle.WaitOne(timeout * 1000))//等待信号超时
                {
                    Exception MyEx = new Exception("接收应答数据超时！\r\n注意此处出错啦-------我是分割线-------");
                    throw MyEx;

                }
                else
                {
                    if (isTest)
                    {
                        Exception MyEx = new Exception("接收到应答！");
                        throw MyEx;
                    }
                    else
                    {
                        Exception MyEx = new Exception("测试中止！");
                        throw MyEx;
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                if (WriteLogEvent != null)
                    WriteLogEvent(str);

            }
            
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (tick == interval)
            {
                //执行任务
                SendMsg();
                //
                tick = 0;
            }
            else
            {
                label.Text = "已进行了" + times.ToString() + "次测试";
                this.TitleText = " 距离下次发送命令还有" + (interval - tick).ToString() + "秒";
                tick++;
            }
        }

        private void TestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isTest)
            {
                string message = "是否停止当前测试？";
                string caption = "消息";
                MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                MessageBoxIcon icon = MessageBoxIcon.Question;
                MessageBoxDefaultButton defaultResult = MessageBoxDefaultButton.Button2;
                // Show message box
                DialogResult result = MessageBox.Show(message, caption, buttons, icon, defaultResult);
                if (result == DialogResult.OK)
                {
                    isTest = false;
                    ACPacketHandle.Set();
                    ACPacketHandle.Close();
                    SystemLog.close();
                }
            }
            ACPacketHandle.Set();
            ACPacketHandle.Close();
            SystemLog.close();
            
        }

        private void StopTest_Click(object sender, EventArgs e)
        {
            isTest = false;
            ACPacketHandle.Set();
            timer.Enabled = false;
            idindex = 0;
            testwait.Enabled = false;
            teststart.Enabled = true;
            this.TitleText = "自动测试";
            buttonX1.Enabled = true;
        }

        private void testwait_Click(object sender, EventArgs e)
        {
 
            timer.Stop();
            teststart.Enabled = true;
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            WriteLogEvent += new WriteLogEventHandler(TestForm_WriteLogEvent);
        }
        private void TestForm_WriteLogEvent(string str)
        {
            writelog(str);
            changestate(true);
            
        }
        private void changestate(bool state)
        {
            if (testwait.InvokeRequired)//不是同一线程调用，调用控件的委托
            {

                ChangeStateCallback d = new ChangeStateCallback(changestate);
                this.Invoke(d, new object[] { state });

            }
            else
            {
                testwait.Enabled = state;
            }

        }

    }
}
