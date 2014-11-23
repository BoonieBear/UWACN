using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Net.NetworkInformation;
/*和dsp通信命令
#define NET_DA_START								0xAC03
#define NET_DA_STOP									0xAC02
#define NET_DA_DATA									0xAC01
#define NET_ANSWER									0xACFF
#define NET_FPGA_DATA								0xAC04
#define NET_FPGA_PROGRAM							0xAC05
*/
namespace WaveGenarator
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            Sendbutton.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            filepathbox.Enabled = false;
            textBox6.Enabled = false;
            button1.Enabled = false;
            sendbtn.Enabled = false;
            radioButton1.Checked = true;
            DloadFPGABtn.Enabled = false;
        }
    
        //负责与节点交换数据
        TcpClient client;
        NetworkStream stream;
        BinaryReader filereader;

        //一些参数
        int port = 8080;
        double amp = 1;
        int DloadType = 0;//下载数据类型，0：发射数据，1：FPGA数据
        //网络消息委托
        public delegate void InvokeDelegate();
        private void Form1_Load(object sender, EventArgs e)
        {
            NetworkChange.NetworkAvailabilityChanged += new
            NetworkAvailabilityChangedEventHandler(AvailabilityChangedCallback);
        }
        public void InvokeMethod()
        {
            Connbtn.Text = "连接";
        }

        private void AvailabilityChangedCallback(object sender, EventArgs e)
        {
            NetworkAvailabilityEventArgs myEg = (NetworkAvailabilityEventArgs)e;
            if (!myEg.IsAvailable)
            {
                MessageBox.Show("网络中断");
                if (client.Connected)
                {
                    stream.Close();
                    client.Close();
                }
                else
                {
                    client.Close();
                }
                StatusLabel.Text = "未连接节点";
                Connbtn.BeginInvoke(new InvokeDelegate(InvokeMethod));
            }
        }

        #region connect
        /// <summary>
        /// 连接节点，使用nodelinker在后台连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectNode(object sender, EventArgs e)
        {
            //开始连接
            if (Connbtn.Text == "连接")
            {
                Connbtn.Text = "取消";
                try
                {
                    IPAddress Nodeip = IPAddress.Parse(IpAddrBox.Text);
                    client = new TcpClient();//每次close后都要重写new一个新的对象，因为close后源对象已释放
                    client.ReceiveTimeout = 5000;
                    client.SendTimeout = 5000;
                    NodeLinker.RunWorkerAsync(Nodeip);
                    StatusLabel.Text = "连接中...";


                }

                catch (Exception MyEx)
                {
                    
                    MessageBox.Show(MyEx.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            //取消连接
            else
            {

                this.NodeLinker.CancelAsync();
                if (client.Connected)
                {
                    stream.Close();
                    client.Close();
                }
                else
                {
                    client.Close();
                }
                StatusLabel.Text = "未连接节点";
                Connbtn.Text = "连接";

            }

        }

        private void connect(IPAddress ipaddr, BackgroundWorker MyWorker, DoWorkEventArgs e)
        {
            if (MyWorker.CancellationPending)
            {
                e.Cancel = true;
            }
            try
            {
                client.Connect(ipaddr, port);
                stream = client.GetStream();

            }
            catch (SocketException myEx)
            {
                e.Result = myEx.ErrorCode;
                


            }
            finally
            {
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
            if (e.Result != null)
            {

                Connbtn.Text = "连接";
                sendbtn.Enabled = false;
                DloadFPGABtn.Enabled = false;
                Sendbutton.Enabled = false;
                StatusLabel.Text = "未连接节点";
                string errstring = " 操作失败！" + "错误号：" + e.Result.ToString();
                MessageBox.Show(errstring, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                client.Close();
                
            }
            else
            {
                Connbtn.Text = "断开";
                StatusLabel.Text = "已连接节点";
                sendbtn.Enabled = true;
                DloadFPGABtn.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "波形文件(*.wav)|*.wav|所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filepathbox.Text = openFileDialog.FileName;

            }

        }
        #endregion connect

        #region radiochange
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                comboBox1.Enabled = true;
                lenBox.Enabled = true;
                textBox3.Enabled = true;

            }
            else
            {
                comboBox1.Enabled = false;
                lenBox.Enabled = false;
                textBox3.Enabled = false;

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                comboBox2.Enabled = true;
                textBox2.Enabled = true;
                textBox4.Enabled = true;
                textBox6.Enabled = true;

            }
            else
            {
                comboBox2.Enabled = false;
                textBox2.Enabled = false;
                textBox4.Enabled = false;
                textBox6.Enabled = false;

            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                filepathbox.Enabled = true;
                button1.Enabled = true;
            }
            else
            {
                filepathbox.Enabled = false;
                button1.Enabled = false;
            }
        }
        #endregion radiochange

        //下发数据文件
        private void sendbtn_Click(object sender, EventArgs e)
        {

            byte[] sendbuf;
            //判断时间长度是否合法
            //if (Convert.ToInt32(lenBox.Text) <= 0 || Convert.ToInt32(lenBox.Text) > 3000)
            //{
            //    MessageBox.Show(" 长度设置错误（0 - 3000）", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //if (Convert.ToInt32(textBox6.Text) <= 0 || Convert.ToInt32(textBox6.Text) > 3000)
            //{
            //    MessageBox.Show(" 长度设置错误（0 - 3000）", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            ////判断幅度是否合法
            try
            {
                amp = Convert.ToDouble(ampBox.Text);
                if (amp >= 0.1 && amp <= 1)
                    AmpValidate.SetError(ampBox, "");
                else
                {
                    AmpValidate.SetError(ampBox, "范围有误！(0.1 - 1)");
                    MessageBox.Show(" 幅度设置错误", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception myEx)
            {
                AmpValidate.SetError(ampBox, "格式有误！(0.1 - 1)");
                ampBox.Focus();
            }

            //禁止下发数据时重新发数据
            sendbtn.Enabled = false;

            if (radioButton1.Checked)
            {
                CosineWave wavedata = new CosineWave();
                try
                {

                    wavedata.Set(Convert.ToDouble(textBox3.Text), Math.PI,
                        Convert.ToInt32(comboBox1.Text), amp, Convert.ToDouble(lenBox.Text));
                    wavedata.Genarate();
                    sendbuf = new byte[(int)(wavedata.Length * wavedata.SampleRate) * 2];
                    wavedata.copy(sendbuf);
                    toolProgressBar.Visible = true;
                    DloadType = 0;//下载数据
                    NetSender.RunWorkerAsync(sendbuf);

                }
                catch (Exception myEx)
                {
                    MessageBox.Show(myEx.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            if (radioButton2.Checked)
            {
                ChirpWave wavedata = new ChirpWave();
                try
                {

                    wavedata.Set(Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox4.Text),
                        Convert.ToInt32(comboBox2.Text), Convert.ToDouble(textBox6.Text), amp);
                    wavedata.Genarate();
                    sendbuf = new byte[(int)(wavedata.Length * wavedata.SampleRate) * 2];
                    wavedata.copy(sendbuf);
                    NetSender.RunWorkerAsync(sendbuf);

                }
                catch (Exception myEx)
                {
                    MessageBox.Show(myEx.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    wavedata.Dispose();
                }

            }
            if (radioButton3.Checked)
            {
                try
                {
                    FileStream fs = new FileStream(filepathbox.Text, FileMode.Open, FileAccess.Read);
                    filereader = new BinaryReader(fs);
                    sendbuf = new byte[fs.Length];
                    filereader.Read(sendbuf, 0, (int)fs.Length);
                    NetSender.RunWorkerAsync(sendbuf);

                }
                catch (Exception myEx)
                {
                    MessageBox.Show(myEx.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }


        }



        private void NetSender_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (DloadType == 0)
            {
                StatusLabel.Text = "发射数据下载...";
                sendsignal((byte[])e.Argument, worker, e);
            }
            else if (DloadType == 1)
            {
                StatusLabel.Text = "FPGA数据下载...";
                sendfpgadata((byte[])e.Argument, worker, e);
            }
            
        }

        private void sendsignal(byte[] buf, BackgroundWorker MyWorker, DoWorkEventArgs e)
        {
            try
            {
                //发命令清除dsp标志
                UInt16[] pkg_clean_head = { 0xAC02, 16, 0, 0, 0, 0, 0, 0 };
                byte[] pkg_clean = new byte[16];//16byte包头，16bit×8
                Buffer.BlockCopy(pkg_clean_head, 0, pkg_clean, 0, 16);

                if (stream.CanWrite)
                {
                    stream.Write(pkg_clean, 0, 16);
                    byte[] responseData = new byte[16];

                    stream.Read(responseData, 0, responseData.Length);
                    while (BitConverter.ToUInt32(responseData, 0) != 0xac02acff)//收不到响应
                    {
                        stream.Read(responseData, 0, responseData.Length);

                    }


                }
                //包头初始化
                Int32 sum = 0;//已发送数据量
                UInt16[] pkg_head = { 0xAC01, 1016, 0, 0, 0, 0, 0, 0 };
                for (int i = 0; i < buf.Length / 2032; i++)//前n-1包，长度都为2032byte
                {


                    byte[] pkg = new byte[2048];//16byte包头，16bit×8
                    Buffer.BlockCopy(pkg_head, 0, pkg, 0, 16);
                    Buffer.BlockCopy(buf, i * 2032, pkg, 16, 2032);
                    if (stream.CanWrite)
                    {
                        stream.Write(pkg, 0, 2048);
                        sum += 2032;
                        byte[] responseData = new byte[16];
                        stream.Read(responseData, 0, responseData.Length);

                        while (BitConverter.ToUInt32(responseData, 0) != 0xac01acff)//收不到响应
                        {
                            stream.Read(responseData, 0, responseData.Length);

                        }
                        MyWorker.ReportProgress((sum / (buf.Length/100)));
                    }
                    else
                    {
                        MyWorker.CancelAsync();
                    }
                }
                //最后一包
                Int32 lastlength = ((Int32)buf.Length - (Int32)sum);
                UInt16[] pkg_tailhead = { 0xAC01, (UInt16)(lastlength/2), 0, 0, 0, 0, 0, 0 };
                byte[] pkg_tail = new byte[lastlength + 16];
                Buffer.BlockCopy(pkg_tailhead, 0, pkg_tail, 0, 16);
                Buffer.BlockCopy(buf, (Int32)sum, pkg_tail, 16, lastlength);
                if (stream.CanWrite)
                {
                    stream.Write(pkg_tail, 0, lastlength +16);
                    byte[] responseData = new byte[16];
                    stream.Read(responseData, 0, responseData.Length);

                    while (BitConverter.ToUInt32(responseData, 0) != 0xac01acff)//收不到响应
                    {
                        stream.Read(responseData, 0, responseData.Length);
                        
                    }
                    MyWorker.ReportProgress(100);
                }
                else
                {
                    MyWorker.CancelAsync();
                }

            }
            catch (Exception myEx)
            {
                SocketException sockex = (SocketException)myEx.InnerException;
                e.Result = sockex.ErrorCode;
                sendbtn.Enabled = false;
                MessageBox.Show(myEx.Message, "数据传输错误", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void sendfpgadata(byte[] buf, BackgroundWorker MyWorker, DoWorkEventArgs e)
        {
            try
            {
                //发命令清除flash
                UInt16[] pkg_clean_head = { 0xAC05, 16, 0, 0, 0, 0, 0, 0 };
                byte[] pkg_clean = new byte[16];//16byte包头，16bit×8
                Buffer.BlockCopy(pkg_clean_head, 0, pkg_clean, 0, 16);

                if (stream.CanWrite)
                {
                    stream.Write(pkg_clean, 0, 16);
                    byte[] responseData = new byte[16];

                    stream.Read(responseData, 0, responseData.Length);
                    while (BitConverter.ToUInt32(responseData, 0) != 0xac05acff)//收不到响应
                    {
                        stream.Read(responseData, 0, responseData.Length);

                    }


                }
                //包头初始化
                Int32 sum = 0;//已发送数据量
                UInt16[] pkg_head = { 0xAC04, 1016, 0, 0, 0, 0, 0, 0 };
                for (int i = 0; i < buf.Length / 2032; i++)//前n-1包，长度都为2032byte
                {


                    byte[] pkg = new byte[2048];//16byte包头，16bit×8
                    Buffer.BlockCopy(pkg_head, 0, pkg, 0, 16);
                    Buffer.BlockCopy(buf, i * 2032, pkg, 16, 2032);
                    if (stream.CanWrite)
                    {
                        stream.Write(pkg, 0, 2048);
                        sum += 2032;
                        byte[] responseData = new byte[16];
                        stream.Read(responseData, 0, responseData.Length);

                        while (BitConverter.ToUInt32(responseData, 0) != 0xac04acff)//收不到响应
                        {
                            stream.Read(responseData, 0, responseData.Length);

                        }
                        MyWorker.ReportProgress(sum * 100 / buf.Length);
                    }
                    else
                    {
                        MyWorker.CancelAsync();
                    }
                }
                //最后一包
                Int32 lastlength = ((Int32)buf.Length - (Int32)sum);
                UInt16[] pkg_tailhead = { 0xAC05, (UInt16)(lastlength / 2), 0, 0, 0, 0, 0, 0 };
                byte[] pkg_tail = new byte[lastlength + 16];
                Buffer.BlockCopy(pkg_tailhead, 0, pkg_tail, 0, 16);
                Buffer.BlockCopy(buf, (Int32)sum, pkg_tail, 16, lastlength);
                if (stream.CanWrite)
                {
                    stream.Write(pkg_tail, 0, lastlength + 16);
                    byte[] responseData = new byte[16];
                    stream.Read(responseData, 0, responseData.Length);

                    while (BitConverter.ToUInt32(responseData, 0) != 0xac05acff)//收不到响应
                    {
                        stream.Read(responseData, 0, responseData.Length);

                    }
                    MyWorker.ReportProgress(100);
                }
                else
                {
                    MyWorker.CancelAsync();
                }

            }
            catch (Exception myEx)
            {
                SocketException sockex = (SocketException)myEx.InnerException;
                e.Result = sockex.ErrorCode;
                MessageBox.Show(myEx.Message, "数据传输错误", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }


        private string Createfilename(string type, string ext)
        {
            string filename = type + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() +
                DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "." + ext;

            return filename;
        }

        #region testfunc
        private void testbtn_Click(object sender, EventArgs e)
        {
            try
            {
                amp = Convert.ToDouble(ampBox.Text);
                if (amp >= 0.1 && amp <= 1)
                    AmpValidate.SetError(ampBox, "");
                else
                {
                    AmpValidate.SetError(ampBox, "范围有误！(0.1 - 1)");
                    MessageBox.Show(" 幅度设置错误", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception myEx)
            {
                AmpValidate.SetError(ampBox, "格式有误！(0.1 - 1)");
                ampBox.Focus();
            }
            if (radioButton1.Checked)
            {
                CosineWave wavedata = new CosineWave();
                try
                {

                    wavedata.Set(Convert.ToDouble(textBox3.Text), Math.PI,
                        Convert.ToInt32(comboBox1.Text), amp, Convert.ToDouble(lenBox.Text));
                    wavedata.amp = Convert.ToDouble(ampBox.Text);
                    wavedata.Genarate();

                    wavedata.SaveFile(Createfilename("single", "wav"));
                    MessageBox.Show(" 文件已保存", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception myEx)
                {
                    MessageBox.Show(myEx.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {

                    wavedata.Dispose();
                }

            }
            if (radioButton2.Checked)
            {
                ChirpWave wavedata = new ChirpWave();
                try
                {

                    wavedata.Set(Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox4.Text),
                        Convert.ToInt32(comboBox2.Text), Convert.ToDouble(textBox6.Text), amp);
                    wavedata.Genarate();

                    wavedata.SaveFile(Createfilename("chirp", "wav"));
                    MessageBox.Show(" 文件已保存", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception myEx)
                {
                    MessageBox.Show(myEx.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {

                    wavedata.Dispose();
                }

            }

        }
        #endregion testfunc

        private void NetSender_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolProgressBar.Visible = false;
            if (e.Cancelled)
            {
                // The user canceled the operation.
                MessageBox.Show("操作已取消！");
                sendbtn.Enabled = true;
                Sendbutton.Enabled = false;
                StatusLabel.Text = "操作已取消！";
                if (client.Connected)
                {
                    stream.Close();
                    client.Close();
                }
                else
                {
                    client.Close();
                }
               
                StatusLabel.Text = "未连接节点";
                Connbtn.Text = "连接";
            }
            else if (e.Result != null)
            {
                // There was an error during the operation.
                string msg = String.Format("信号发射错误: {0}", e.Result);
                StatusLabel.Text = msg;
                
                sendbtn.Enabled = true;
                Sendbutton.Enabled = false;
                MessageBox.Show(msg);
            }
            else
            {
                // The operation completed normally.
                
                StatusLabel.Text = "波形下发成功，可以发射";
                sendbtn.Enabled = true;
                Sendbutton.Enabled = true;
            }

        }

        private void NetSender_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolProgressBar.Value = e.ProgressPercentage;
            string msg = String.Format("波形下发...{0}%", e.ProgressPercentage);
            StatusLabel.Text = msg;
        }

        /// <summary>
        /// 开始发射命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sendbutton_Click(object sender, EventArgs e)
        {
            try
            {
                UInt16[] pkg_head = { 0xAC03, 16, 0, 0, 0, 0, 0, 0 };
                byte[] pkg = new byte[16];//16byte包头，16bit×8
                Buffer.BlockCopy(pkg_head, 0, pkg, 0, 16);

                if (stream.CanWrite)
                {
                    stream.Write(pkg, 0, 16);
                    byte[] responseData = new byte[16];

                    stream.Read(responseData, 0, responseData.Length);
                    while (BitConverter.ToUInt32(responseData, 0) != 0xac03acff)//收不到响应
                    {
                        stream.Read(responseData, 0, responseData.Length);

                    }
                    StatusLabel.Text = "发射中……";


                }


            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message, "数据传输错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (client.Connected)
                {
                    stream.Close();
                    client.Close();
                }
                else
                {
                    client.Close();
                }
                
                StatusLabel.Text = "未连接节点";
                Connbtn.Text = "连接";
            }

        }

        private void DloadFPGABtn_Click(object sender, EventArgs e)
        {
            char[] sendbuf;//下发数据缓存
            openFileDialog.Filter = "FPGA文件(*.data)|*.data|所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FPGA_filename.Text = openFileDialog.FileName;

            }
            if (!File.Exists(FPGA_filename.Text))
            {
                try
                {
                    Encoding ascii = Encoding.ASCII;
                    string readText = File.ReadAllText(FPGA_filename.Text, ascii);
                    sendbuf = new char[readText.Length];
                    readText.CopyTo(0, sendbuf, 0, readText.Length);
                    DloadType = 1;
                    NetSender.RunWorkerAsync(sendbuf);

                }

                catch (Exception myEx)
                {
                    MessageBox.Show(myEx.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("文件不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
