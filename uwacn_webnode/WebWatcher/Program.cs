using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using webnode.Helper;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;

namespace WebWatcher
{
    class Program
    {
        static LogFile LogFile = new LogFile("WatcherLog");
        static private Mutex gMu;
        static private DateTime Tip;
        static private System.Threading.Timer Feeder;
        static private System.Threading.Timer Cleaner;
        private static void FeedTimeOut(object state)
        {
            if (DateTime.Now.Subtract(Tip).TotalSeconds >10)
            {
                Log("时间到，重启水声通信网程序");

                stopProcess("webnode");
                Process.Start(exepath + "\\webnode.exe");
                Log("启动水声通信网程序");
                Tip = DateTime.Now.AddSeconds(5);
            }
            Debug.WriteLine("DateTime.Now.Subtract(Tip).TotalSeconds=", DateTime.Now.Subtract(Tip).TotalSeconds.ToString());
        }

        static private NetworkStream streams;
        static private TcpListener dog;
        static DirectoryInfo exepath = new DirectoryInfo(System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName));
        static void Log(string msg)
        {
            Console.WriteLine(msg);
            if (LogFile.logfile.ws == null)//还未创建文件
                LogFile.OpenFile(exepath);
            LogFile.writeLine(msg+"\n");
            if (LogFile.length >= 100 * 1024)//不允许大于100K
            {
                LogFile.close();
            }
        }
        public static void stopProcess(string name)
        {
            foreach (Process p in System.Diagnostics.Process.GetProcessesByName(name))
            {
                try
                {
                    p.Kill();
                    p.WaitForExit();
                    Log("水声通信网程序关闭。");
                }
                catch (Exception exp)
                {
                    Log(exp.Message);
                    MessageBox.Show("无法关闭水声通信网程序。", "watcher");
                    Log("无法关闭水声通信网程序,Webwatcher退出!");
                    Environment.Exit(0);
                    //System.Diagnostics.EventLog.WriteEntry("AlchemySearch:KillProcess", exp.Message, System.Diagnostics.EventLogEntryType.Error);
                }
            }
        }
        static void Main(string[] args)
        {
            var createdNew = false;
            gMu = new Mutex(true, "Watcher", out createdNew);
            if (!createdNew)
            {
                MessageBox.Show("请不要同时运行两个Watcher实例。", "watcher");
                //Environment.Exit(0);
                return;
            }
            Log("Watcher启动!!");
            foreach (Process p in System.Diagnostics.Process.GetProcessesByName("webnode"))
            {
                MessageBox.Show("请先关闭水声通信网程序再开启Webwatcher。", "watcher");
                Log("发现水声通信网程序，退出！");
                return;
            }
            //无webnode，启动
            if (!File.Exists(exepath + "\\webnode.exe"))
            {
                Log("找不到水声通信网程序,watcher退出！");
                return;
            }
            Process.Start(exepath+"\\webnode.exe");
            Log("启动水声通信网程序");
            Feeder = new System.Threading.Timer(FeedTimeOut, null, 5000, 3000);
            Tip = DateTime.Now.AddSeconds(3);
            //start listenning
            dog = new TcpListener(IPAddress.Any,32100);
            dog.Start();
            Log("开始监听");
            // 后台线程1：用于接收tcp连接请求，并将网络流加入列表。随主线程的退出而退出。
            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(100);// 可以根据需要设置时间
                    if (!dog.Pending())
                    {
                        continue;
                    }
                    var client = dog.AcceptTcpClient();

                    if (!client.Connected)
                    {
                        continue;
                    }
                    streams = client.GetStream();
                }
            })
            { IsBackground = true }.Start();

            // 后台线程2：用于接收请求，并作出响应。随主线程的退出而退出。
            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(100);// 可以根据需要设置时间
                    if (streams == null || !streams.CanRead)
                    {
                        continue;
                    }
                    var buffer = new byte[2];
                    try
                    {
                        var a = streams.Read(buffer, 0, buffer.Length);
                    }
                    catch(Exception e)
                    {
                        streams.Close();
                    }
                    
                    if (BitConverter.ToUInt16(buffer, 0) == 0xFE01)
                    {
                        Tip = DateTime.Now;
                    }
                }
            })
            { IsBackground = true }.Start();
            //
            Console.ReadLine();
        }
    }
}
