using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using webnode.Helper;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace WebWatcher
{
    class Program
    {
        static LogFile LogFile = new LogFile("WatcherLog");
        static private Mutex gMu;
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
            Log("Webnode watcher start!!");
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
            //start listenning
            //TBD
            //
            Console.ReadLine();
        }
    }
}
