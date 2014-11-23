using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Rendering;
using MSPTracer.Helper;
using System.Globalization;
using System.IO;
namespace MSPTracer
{
    public partial class Form1 : Form
    {
        public delegate void SyncEventHandler(object sender, EventsClass.GpsEventArgs e);//主窗口状态栏委托
        public static event SyncEventHandler SyncLogEvent;
        delegate void LogBoxCallback(object sender, EventsClass.GpsEventArgs e);
        Thread readThread;
        public static System.IO.Ports.SerialPort SyncSerialPort;
        public static bool isContinue = true;
        public int wrongnumber = 0;
        public Form1()
        {
            InitializeComponent();
            SyncSerialPort = new System.IO.Ports.SerialPort();
            SyncSerialPort.Encoding = Encoding.UTF8;
            SerialPort.Text = "COM12";
            Serialbaud.Text = "9600";
            CircleTime.Text = "15";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SyncLogEvent += new SyncEventHandler(GPSForm_GpsLogEvent);
            String input;
            try
            {
                csFile fl = new csFile("command.txt");
                while ((input = fl.readLine()) != null)
                {
                    Commandlist.Items.Add(input);
                }
                
            }
            catch (Exception MyEx)
            {
                MessageBox.Show(MyEx.Message + ":" + "请确认数据文件在commands文件夹中且当前路径下有command文件！");
                Application.Exit();
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
                LogBoxCallback d = new LogBoxCallback(GPSForm_GpsLogEvent);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                string comming = e.gpsdata;
                comming = comming.Substring(comming.LastIndexOf("EB90"));
                string[] str = comming.Split(',');
                if (str[1] == "03")
                {
                    GpsLog.AppendText("430-->>" + e.gpsdata + ",END" + "\r\n");
                    GpsLog.ScrollToCaret();
                    if (str[2] == "Y")
                    {
                        GpsLog.AppendText("430回复ACK正确" + "\r\n");
                        GpsLog.ScrollToCaret();
                    }
                    else
                    {
                        GpsLog.AppendText("430回复ACK错误" + "\r\n");
                        GpsLog.ScrollToCaret();
                    }
                }
                else
                {

                    string oldstr = comming;

                    
                    oldstr = oldstr.Remove(oldstr.Length - 2);
                    try
                    {
                        string crcnew = CRCHelper.CRC16(oldstr);

                        string newstr = CRCHelper.ConvertCharToHex(comming) + CRCHelper.ConvertCharToHex(",END");
                    GpsLog.AppendText("430-->>" + newstr + "\r\n");
                    GpsLog.ScrollToCaret();
                    string crc = newstr.Substring(newstr.Length-12, 4);
                    
                    if (crc == crcnew)
                    {
                        string ack = "EB90,03,Y,";
                        ack += CRCHelper.CRC16(ack);
                        ack += ",END";
                        ack = CRCHelper.ConvertCharToHex(ack);
                        WriteCommand(ack);
                        GpsLog.AppendText("上位机回复ACK正确" + "\r\n");
                        GpsLog.ScrollToCaret();
                    }
                    else
                    {
                        string ack = "EB90,03,N,";
                        ack += CRCHelper.CRC16(ack);
                        ack += ",END";
                        ack = CRCHelper.ConvertCharToHex(ack);
                        WriteCommand(ack);
                        GpsLog.AppendText("上位机回复ACK错误" + "\r\n");
                        GpsLog.ScrollToCaret();
                        wrongnumber++;
                        numberlabel.Text = "上位机校验错误次数：" + wrongnumber.ToString() +" 双击清零";
                    }
                    }
                    catch (Exception MyEx)
                    {
                        MessageBox.Show(MyEx.Message + ":" + MyEx.StackTrace);
                    }
                }
                

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
                    if (SyncSerialPort.IsOpen)
                    {
                        string message = SyncSerialPort.ReadTo(",END");

                        EventsClass.GpsEventArgs e = new EventsClass.GpsEventArgs(message);
                        SyncEventHandler handler = SyncLogEvent;
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

        private void WriteCommand(string str)
        {
            if (str == string.Empty)
                return;
            if (SyncSerialPort.IsOpen)
            {
                byte[] c = new byte[str.Length / 2];
                for (int i = 0; i < str.Length / 2; i++)
                {
                    string s = str.Substring(i * 2, 2);

                    int d = Convert.ToByte(s, 16);
                    c[i] = (byte)d;
                    SyncSerialPort.Write(c, i, 1);
                }
                
                //SyncSerialPort.Write(str.ToCharArray(),0,str.Length);
                GpsLog.AppendText("<<--上位机" + str);
                GpsLog.AppendText("\r\n");
                GpsLog.ScrollToCaret();
            }
        }

        public void StartWork()
        {

            try
            {
                SyncSerialPort.Open();
                if (SyncSerialPort.IsOpen)
                {
                    readThread = new Thread(Read);
                    readThread.Start();
                }
            }
            catch (Exception MyEx)
            {
                MessageBox.Show(MyEx.Message);
            }
        }

        private void OpenSerial_CheckedChanged(object sender, EventArgs e)
        {
            if (OpenSerial.Checked)
            {
                if ((SerialPort.FindString(SerialPort.Text) != -1) && (Serialbaud.FindString(Serialbaud.Text) != -1))
                {
                    SyncSerialPort.PortName = SerialPort.Text;
                    SyncSerialPort.BaudRate = int.Parse(Serialbaud.Text);
                    StartWork();
                    if (!SyncSerialPort.IsOpen)
                    {
                        OpenSerial.Checked = false;
                    }
                    else
                    {
                        SerialPort.Enabled = false;
                        Serialbaud.Enabled = false;
                        buttonX1.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("请选择正确的配置！");
                   
                }
            }
            else
            {
                SyncSerialPort.Close();
                SerialPort.Enabled = true;
                Serialbaud.Enabled = true;
            }
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            string str;
            Stream myStream = null;
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            Byte[] buffer;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            if (myStream.Length > 2 * 1024)
                            {
                                MessageBox.Show("文件有点大！");
                                myStream.Close();
                            }
                            buffer = new Byte[myStream.Length];
                            myStream.Read(buffer, 0, (int)myStream.Length);
                            str = System.Text.Encoding.Default.GetString(buffer);
                            //str.Replace(" ",String.Empty);
                            string[] newstr =  str.Split(' ');
                            str = String.Join(String.Empty, newstr);
                            CommandBox.Clear();
                            CommandBox.Text = str;

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: 无法打开指定文件. Original error: " + ex.Message);
                }
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            Stream myStream;

            Byte[] buffer = new Byte[CommandBox.Text.Length];
            buffer = System.Text.Encoding.Default.GetBytes(CommandBox.Text);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog.OpenFile()) != null)
                {
                    myStream.Write(buffer, 0, CommandBox.Text.Length);
                    myStream.Flush();
                    myStream.Close();
                }
            }
        }

        private void Commandlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            Stream myStream = null;
            Byte[] buffer;
            string filename = "commands/";
            string str = Commandlist.Items[Commandlist.SelectedIndex].ToString();
            string[] newstr = str.Split(' ');
            filename += newstr[0];
            filename += ".txt";
            try
            {
                myStream = File.OpenRead(filename);
                using (myStream)
                {
                    
                    if (myStream.Length > 2 * 1024)
                    {
                        MessageBox.Show("文件有点大，换个小点的吧！");
                        myStream.Close();
                    }
                    buffer = new Byte[myStream.Length];
                    myStream.Read(buffer, 0, (int)myStream.Length);
                    str = System.Text.Encoding.Default.GetString(buffer);
                    string[] strarray = str.Split(' ');
                    str = String.Join(String.Empty, strarray);
                    CommandBox.Clear();
                    CommandBox.Text = str;

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: 无法打开指定文件. Original error: " + ex.Message);
                
            }
        }

        private void CircleCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (CircleCheck.Checked)
            {
                CircleTime.Enabled = true;
            }
            else
            {
                CircleTime.Enabled = true;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (!CircleCheck.Checked)
            {
                //string ans = CRCHelper.ConvertHexToChar(CommandBox.Text);
                //string str = CommandBox.Text;
                //byte[] c = new byte[1];
                //for (int i = 0; i < str.Length / 2; i++)
                //{
                //    string s = str.Substring(i * 2, 2);
                    
                //    int d = Convert.ToByte(s, 16);
                //    c[0] = (byte)d;
                //}
                //SyncSerialPort.Write(c, 0, 1);
                WriteCommand(CommandBox.Text);
            
                
            }
            else
            {
                if(buttonX1.Text == "发送")
                {
                    buttonX1.Text = "停止";
                    timer.Interval = (int)float.Parse(CircleTime.Text) * 1000;
                    string ans = CRCHelper.ConvertHexToChar(CommandBox.Text);
                    WriteCommand(ans);
                    timer.Start();
                    CircleTime.Enabled = false;
                    CircleCheck.Enabled = false;
                    CommandBox.Enabled = false;
                    Commandlist.Enabled = false;
                }
                else
                {
                    buttonX1.Text = "发送";
                    timer.Stop();
                    CircleTime.Enabled = true;
                    CircleCheck.Enabled = true;
                    CommandBox.Enabled = true;
                    Commandlist.Enabled = true;

                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //string ans = CRCHelper.ConvertHexToChar(CommandBox.Text);
            WriteCommand(CommandBox.Text);
        }

        private void numberlabel_DoubleClick(object sender, EventArgs e)
        {
            wrongnumber = 0;
            numberlabel.Text = "上位机校验错误次数：" + wrongnumber.ToString() + " 双击清零";
        }



    }
}
