using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
namespace CRCBuilder
{
    public partial class Form1 : Form
    {
 
        public Form1()
        {
            InitializeComponent();
        }
        //清除文字
        private void clearbutton_Click(object sender, EventArgs e)
        {
            sourcebox.Clear();
            modecheck.Checked = false;
            
        }
        //清除文字
        private void clrbtn_Click(object sender, EventArgs e)
        {
            databox.Clear();
            
        }
        //从剪贴板粘贴
        private void pastebtn_Click(object sender, EventArgs e)
        {
            string str = Clipboard.GetText();
            sourcebox.Clear();
            sourcebox.Text = str;
            
            
        }
        //复制到剪贴板
        private void copytobtn_Click(object sender, EventArgs e)
        {
            databox.SelectAll();
            databox.Copy();
        }
        //加载文件
        private void loadbtn_Click(object sender, EventArgs e)
        {
            string str;
            Stream myStream = null;
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*" ;
            openFileDialog.FilterIndex = 2 ;
            Byte[] buffer;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            if (myStream.Length > 1024 * 1024)
                            {
                                MessageBox.Show("文件有点大，换个小点的吧！");
                                myStream.Close();
                            }
                            buffer = new Byte[myStream.Length];
                            myStream.Read(buffer, 0, (int)myStream.Length);
                            str = System.Text.Encoding.Default.GetString(buffer);
                            sourcebox.Clear();
                            sourcebox.Text = str;
                           
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: 无法打开指定文件. Original error: " + ex.Message);
                }
            }

        }
        //保存文件
        private void savefile_Click(object sender, EventArgs e)
        {
            Stream myStream;
            Byte[] buffer = new Byte[databox.Text.Length];
            buffer = System.Text.Encoding.Default.GetBytes(databox.Text);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog.OpenFile()) != null)
                {
                    myStream.Write(buffer, 0, databox.Text.Length);
                    myStream.Flush();
                    myStream.Close();
                }
            }

        }
        //计算CRC
        private void crcbtn_Click(object sender, EventArgs e)
        {
            string str;
            char ls;
            crctext.Text = string.Empty;
            if (modecheck.Checked)
            {
                sourcebox.Text.Replace(" ","");
                str = CRCHelper.ConvertAsciiToChar(sourcebox.Text);
                if (str == null)
                    return ;
                ushort crcnum = CRCHelper.CRC16(str);
                str = crcnum.ToString("X", NumberFormatInfo.InvariantInfo);
                str = str.PadLeft(4, '0');
                crctext.Text = str;
                databox.Clear();
                databox.Text = sourcebox.Text + crctext.Text;
            }
            else
            {
                ushort crcnum = CRCHelper.CRC16(sourcebox.Text);
                
                ls = (char)((crcnum & 0xFF00)>>8);
                crctext.Text += ls.ToString();
                ls = (char)(crcnum & 0xFF);
                crctext.Text += ls.ToString();
                databox.Clear();
                databox.Text = sourcebox.Text + crctext.Text;

            }
            
            
            
        }
        //16进制显示
        private void modecheck_CheckedChanged(object sender, EventArgs e)
        {
           string str;

            if (modecheck.Checked)
            {
                str = CRCHelper.ConvertCharToAscii(sourcebox.Text);
                sourcebox.Clear();
                sourcebox.Text = str;
                str = "";
                str = CRCHelper.ConvertCharToAscii(crctext.Text);
                str = str.PadLeft(4,'0');
                crctext.Clear();
                crctext.Text = str;
                str = "";
                


            }
            else 
            {
                str = CRCHelper.ConvertAsciiToChar(sourcebox.Text);
                sourcebox.Clear();
                sourcebox.Text = str;
                str = "";
                str = CRCHelper.ConvertAsciiToChar(crctext.Text);
                str = str.PadLeft(2,'0');
                crctext.Clear();
                crctext.Text = str;
                str = "";

            }
            databox.Text = sourcebox.Text + crctext.Text;
        }
        

    }
}
