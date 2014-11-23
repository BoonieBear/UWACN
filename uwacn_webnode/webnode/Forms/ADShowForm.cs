using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Rendering;
namespace webnode.Forms
{
    public partial class ADShowForm : Office2007Form
    {
        public delegate void AddDataHandle(byte[] buf);
        public ADShowForm()
        {
            InitializeComponent();
            ADwaveBox.Initail();
        }

        private void ADShowForm_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
        }

        private void ADShowForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.ShowInTaskbar = false;
            if (e.CloseReason != CloseReason.ApplicationExitCall)//非系统关闭，只是关闭本窗口
                e.Cancel = true;
        }
        public void AddToBox(byte[] buf)
        {
            if (ADwaveBox.InvokeRequired)
            {
                AddDataHandle a = new AddDataHandle(AddToBox);
                this.Invoke(a, new object[] { buf });
            }
            else
            {
                ADwaveBox.Display(buf);
                
            }
        }

        private void ADShowForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void 通道选择ToolStripMenuItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((通道选择ToolStripMenuItem.SelectedIndex > -1)&&(通道选择ToolStripMenuItem.SelectedIndex<4))
            {
               CommLineForm.ADchannel = 通道选择ToolStripMenuItem.SelectedIndex;
            }
        }
    }
}
