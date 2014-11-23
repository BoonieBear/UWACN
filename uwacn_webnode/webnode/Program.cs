using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace webnode
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); 
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionHandler);
            Application.Run(new MainForm());
        }
        static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {

            try
            {

                //

            }

            catch
            {
                MessageBox.Show(e.ExceptionObject.ToString());
            }
            finally
            { }
        }
    }
}
