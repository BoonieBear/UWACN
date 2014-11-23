using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Rendering;
using System.IO;
using WaveBox;
using webnode.Sources;
namespace ADplayer
{
    public partial class Form1 :Office2007Form
    {
        BinaryReader br;
        public Form1()
        {
            InitializeComponent(); 

        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            
           
                br = new BinaryReader(openFileDialog.OpenFile());
                timer.Enabled = true;
        }

        private void play_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            byte[] b = br.ReadBytes(4096);
            if (b.Length < 4096)
                br.BaseStream.Position = 0;
            waveBox.Display(b);
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
                timer.Enabled = false;
            waveBox.Clear();
        }
    }
}
