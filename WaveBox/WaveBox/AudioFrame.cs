/* Copyright (C) 2012 Fu Xiang (buptfx@gmail.com)

   This program is free software; you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation; either version 2 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program; if not, write to the Free Software
   Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA */

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
namespace WaveBox
{
    class AudioFrame
    {
        private double[] _waveLeft;
        private double[] wave;
        Point[] p;
        private int SamplesPerSecond;
        //private FifoStream _streamMemory;
        private double[] _fftLeft;
        double min = double.MaxValue;
        double max = double.MinValue;
        //private SignalGenerator _signalGenerator;
        public int Ymax = 32767;
        public int Fmax = 64000;
        public int _bitsPerSample = 16;
        public int _SampleByte = 2;
        public ArrayList _fftLeftSpect = new ArrayList();
        
        public AudioFrame(int audioSamplesPerSecond,int maxFrequecy, int timedomainlen, int amp, int BitsPerSample)
        {
            SamplesPerSecond = audioSamplesPerSecond;
            wave = new double[timedomainlen];
            Buffer.SetByte(wave, 0, 0);
            Ymax = amp;
            Fmax = maxFrequecy;
            _bitsPerSample = BitsPerSample;
            _SampleByte = _bitsPerSample / 8;
           
        }

        /// <summary>
        /// Process 16 bit sample
        /// </summary>
        /// <param name="wave"></param>
        
        public void AddData(byte[] buf)
        {
            _waveLeft = new double[buf.Length / _SampleByte];
            int h = 0;
            for (int i = 0; i < buf.Length; i += _SampleByte)
            {
                _waveLeft[h] = (double)BitConverter.ToInt16(buf, i);
                h++;
            }
            _fftLeft = FourierTransform.FFT(ref _waveLeft);
            Array.Copy(wave, _waveLeft.Length, wave, 0, wave.Length - _waveLeft.Length);
            Array.Copy(_waveLeft, 0, wave, wave.Length - _waveLeft.Length, _waveLeft.Length);
            Monitor.Enter(_fftLeftSpect);
            _fftLeftSpect.Add(_fftLeft);

            //if (_fftLeftSpect.Count > 20)
            //    _fftLeftSpect.RemoveAt(0);
            Monitor.Exit(_fftLeftSpect);
        }
        //给功率谱加窗，以免下采样显示时丢失谱线，但会造成谱分辨率下降
         private void AddWindows(double[] b)
         {

         }
        /// <summary>
        /// Render time domain to PictureBox
        /// </summary>
        /// <param name="pictureBox"></param>
        public void RenderTimeDomain(ref PictureBox pictureBox)
        {
            if (wave == null)
                return;
            // Set up for drawing
            Bitmap canvas = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics offScreenDC = Graphics.FromImage(canvas);
            Pen pen = new System.Drawing.Pen(Color.WhiteSmoke);
            p = new Point[pictureBox.Width];
            // Determine channnel boundries
            int width = canvas.Width;
            int height = canvas.Height;
            double center = height / 2;

            // Draw left channel
            double scale = 0.5 * height / Ymax;  // a 16 bit sample has values from -32768 to 32767
            //int xPrev = 0, yPrev = 0;
            double distance = (double)wave.Length / (double)width;
            for (int x = 0; x < width; x++)
            {
                int y = (int)(center - (wave[(int)(distance * x)] * scale));
                p[x] = new Point(x, y);
                
            }
            offScreenDC.DrawLines(pen,p);
            // Clean up
            pictureBox.Image = canvas;
            offScreenDC.Dispose();
            
            
        }
 

        /// <summary>
        /// Render waterfall spectrogram to PictureBox
        /// </summary>
        /// <param name="pictureBox"></param>
        public void RenderSpectrogram(ref PictureBox pictureBox)
        {
            if (pictureBox.Image == null)
            {
                Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
                Graphics g = Graphics.FromImage(bitmap);
                g.Clear(Color.Black);
                pictureBox.Image = bitmap;
                g.Dispose();
                return;
            }
            Bitmap canvas = new Bitmap(pictureBox.Width,pictureBox.Height);
            Graphics offScreenDC = Graphics.FromImage(canvas);
            
            int width = canvas.Width;
            int height = canvas.Height;
            //offScreenDC.CopyFromScreen(0, 0, 0, 0, new Size(width,height),CopyPixelOperation.SourceCopy);
            Monitor.Enter(_fftLeftSpect);
            int len = _fftLeftSpect.Count;
            //Debug.WriteLine(len, "_fftLeftSpect.Count=");
            Rectangle destRect = new Rectangle(0, 0, pictureBox.Width - len, pictureBox.Height);
            GraphicsUnit units = GraphicsUnit.Pixel;
            Rectangle srcRect = new Rectangle(len, 0, pictureBox.Width - len, pictureBox.Height);
            offScreenDC.DrawImage(pictureBox.Image, destRect,srcRect , units);
            double range = 0;

            for (int y = 0; y < _fftLeftSpect.Count; y++)
            {
                for (int x = 0; x < ((double[])_fftLeftSpect[_fftLeftSpect.Count - y - 1]).Length; x++)
                {
                    double amplitude = ((double[])_fftLeftSpect[_fftLeftSpect.Count - y - 1])[x];
                    if (min > amplitude)
                    {
                        
                        min = amplitude;
                    }
                    if (max < amplitude)
                    {
                        max = amplitude;
                    }
                }
            }

            // get range
            if (min < 0 || max < 0)
                if (min < 0 && max < 0)
                    range = max - min;
                else
                    range = Math.Abs(min) + max;
            else
                range = max - min;

            // lock image
            PixelFormat format = canvas.PixelFormat;
            BitmapData data = canvas.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, format);
            int stride = data.Stride;
            int offset = stride - width * 4;

            try
            {
                unsafe
                {

                    for (int x = 0; x < _fftLeftSpect.Count; x++)
                    {
                        double distance = ((double)((double[])_fftLeftSpect[_fftLeftSpect.Count - x - 1]).Length * Fmax/SamplesPerSecond*2/ (double)(height));
                        byte* pixel = (byte*)data.Scan0.ToPointer();
                        pixel += stride - 4 * (x + 1);
                        for (int y = 0; y < height; y++, pixel += offset)
                        {
                            
                            double amplitude = ((double[])_fftLeftSpect[_fftLeftSpect.Count - x - 1])[(int)(distance * (height - 1 - y))];
                            
                            int color = GetColor(min, max, range, amplitude);
                            if (color> 255)//最强db点
                            {
 
                            }
                            pixel[0] = (byte)color;
                            pixel[1] = (byte)0;
                            pixel[2] = (byte)color;
                            pixel[3] = (byte)color;
                            pixel += stride;
                        }
    
                    }
                    _fftLeftSpect.Clear();
                   
                    Monitor.Exit(_fftLeftSpect);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
           
            // unlock image
            canvas.UnlockBits(data);
            
            // Clean up
            
            pictureBox.Image = canvas;
            offScreenDC.Dispose();
            
        }


        /// <summary>
        /// Get color in the range of 0-255 for amplitude sample
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="range"></param>
        /// <param name="amplitude"></param>
        /// <returns></returns>
        private static int GetColor(double min, double max, double range, double amplitude)
        {
            double color;
            if (min != double.NegativeInfinity && min != double.MaxValue & max != double.PositiveInfinity && max != double.MinValue && range != 0)
            {
                if (min < 0 || max < 0)
                    if (min < 0 && max < 0)
                        color = (255 / range) * (Math.Abs(min) - Math.Abs(amplitude));
                    else
                        if (amplitude < 0)
                            color = (255 / range) * (Math.Abs(min) - Math.Abs(amplitude));
                        else
                            color = (255 / range) * (amplitude + Math.Abs(min));
                else
                    color = (255 / range) * (amplitude - min*2)*1.2 ;
            }
            else
                color = 0;
            if (color > 255)
                color = 255;
            if (color < 0)
                color = 0;
            return (int)color;
        }
        public void SetTitle(ref PictureBox picbox,string title)
        {
            Bitmap bitmap = new Bitmap(picbox.Width, picbox.Height);
            Graphics g = Graphics.FromImage(bitmap);
            SolidBrush sb = new SolidBrush(Color.Black);
            g.DrawString(title, new Font("Arial Regular", 20), sb, picbox.Width/2-20,5);
            picbox.Image = bitmap;
            g.Dispose();

        }
        //绘制Y轴上的刻度
        public void SetYAxis(ref PictureBox picbox)
        {
            Pen p1 = new Pen(Color.Black, 2);
            Pen p2 = new Pen(Color.Black, 1);
            SolidBrush sb = new SolidBrush(Color.Black);
            Bitmap bitmap = new Bitmap(picbox.Width, picbox.Height);
            Graphics g = Graphics.FromImage(bitmap);
            double scale = (double)Ymax * 2 / (picbox.Height/2);//给定的最大刻度与实际像素的比例关系
            //开始画时域
            //第一个刻度的两个端点
            int xl = 3, yl = picbox.Height - 1, xr = 6, yr = picbox.Height - 1;
            for (int j = 0; j < 9; j++)
            {

                g.DrawLine(p1, xl, yl - j * picbox.Height / 16, xr, yl - j * picbox.Height / 16);//刻度线
                if((j>0)&&(j<8))
                {
                    string tempy = (- Ymax + 0.25*j*Ymax).ToString("0");
                    g.DrawString(tempy, new Font("Arial Regular", 8), sb, xl + 5, yl - j * picbox.Height / 16 - 5);
                }
               
            }
            g.DrawString("smpl", new Font("Arial Regular", 8), sb, xl + 1, picbox.Height / 2 + 2);
            //开始画频域
            for (int j = 0; j < 10; j++)
            {
                yl = picbox.Height / 2 - 1;
                yr = picbox.Height / 2 - 1;
                g.DrawLine(p1, xl, yl - j * picbox.Height / 20, xr, yl - j * picbox.Height / 20);//刻度线
                if (j > 0) 
                {
                    string tempy = (j * Fmax / 10).ToString("0");
                    g.DrawString(tempy, new Font("Arial Regular", 8), sb, xl + 5, yl - j * picbox.Height / 20 - 6);
                }


            }
            g.DrawString("Hz", new Font("Arial Regular", 8), sb, xl + 1, 2);
            picbox.Image = bitmap;
            g.Dispose();

        }

    }
}
