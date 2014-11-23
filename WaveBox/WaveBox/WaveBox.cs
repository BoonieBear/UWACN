//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE.
//
//  This material may not be duplicated in whole or in part, except for 
//  personal use, without the express written consent of the author. 
//
//  Email:  buptfx@gmail.com
//
//  Copyright (C) 2012 Fu Xiang. ReWrite  the SoundCatcher code writed by  Jeff Morton (jeffrey.raymond.morton@gmail.com)
//  All Rights Reserved.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WaveBox
{
    public partial class WaveBox : UserControl,IDisposable
    {
        private string title = "波形显示";
        private WaveInRecorder _recorder;
        private byte[] _recorderBuffer;
        private WaveOutPlayer _player;
        private byte[] _playerBuffer;
        private FifoStream _stream;
        private WaveFormat _waveFormat;
        private AudioFrame _audioFrame;
        private int _audioSamplesPerSecond = 64000;
        private int _displayFrequecyMax = 16000;
        private int _displayAmpMax = 32767;
        private int _TimeDomainLen = 131072;
        private int _audioFrameSize = 1024;
        private int _audioBitsPerSample = 16;
        private int _audioChannels = 2;
        private BinaryReader br;
        private bool _isPlayer = false;

        public WaveBox()
        {
            InitializeComponent();
            _audioFrame = new AudioFrame(_audioSamplesPerSecond, _displayFrequecyMax, _TimeDomainLen, _displayAmpMax, _audioBitsPerSample);
            //Initail();
            
        }
         ~WaveBox()
        {
            Stop();
            GC.SuppressFinalize(this);
        }
        public string Title
        {
            set
            {
                title = value;
            }
            get { return title; }
        }
        public int SamlesPerSecond
        {
            set
            {
                _audioSamplesPerSecond = value;
            }
            get { return _audioSamplesPerSecond; }
        }
        public int MaxFrequecyShow
        {
            set
            {
                _displayFrequecyMax = value;
            }
            get { return _displayFrequecyMax; }
        }
        public int MaxAmpShow
        {
            set
            {
                _displayAmpMax = value;
            }
            get { return _displayAmpMax; }
        }
        public int TimeWindowSamples
        {
            set
            {
                _TimeDomainLen = value;
            }
            get { return _TimeDomainLen; }
        }
        public int AudioFrameSize
        {
            set
            {
                _audioFrameSize = value;
            }
            get { return _audioFrameSize; }
        }

        public int BitsPerSample
        {
            set
            {
                _audioBitsPerSample = value;
            }
            get { return _audioBitsPerSample; }
        }
        public int Channel
        {
            set
            {
                _audioChannels = value;
            }
            get { return _audioChannels; }
        }
        public bool isPlaying
        {
            set
            {
                _isPlayer = value;
            }
            get { return _isPlayer; }
        }

        private void WaveBox_Load(object sender, EventArgs e)
        {

            if (isPlaying)
            {
                if (WaveNative.waveInGetNumDevs() == 0)
                {
                    MessageBox.Show(DateTime.Now.ToString() + " : 没有可用的音频设备\r\n");
                    isPlaying = false;
                }
                else
                {
                    _stream = new FifoStream();
                }
            }
            

        }
        public void Initail()
        {
            Stop();
            timer1.Enabled = true;
            timer2.Enabled = true;
            //Test();
            try
            {
                _waveFormat = new WaveFormat(_audioFrameSize, _audioBitsPerSample, _audioChannels);
               // _recorder = new WaveInRecorder(0, _waveFormat, _audioFrameSize * 2, 3, new BufferDoneEventHandler(DataArrived));
                if(_isPlayer)
                    _player = new WaveOutPlayer(-1, _waveFormat, _audioFrameSize * 2, 3, new BufferFillEventHandler(Filler));
      
            }
            catch (Exception ex)
            {
                MessageBox.Show(DateTime.Now + " : 错误：\r\n" + ex.ToString() + "\r\n");
            }
        }
        public void Clear()
        {
            Graphics g = FrequencyDomainBox.CreateGraphics();
            g.Clear(Color.Black);
            g = TimeDomainBox.CreateGraphics();
            g.Clear(Color.Black);
            g.Dispose();
        }
        private void Stop()
        {
            if (_recorder != null)
                try
                {
                    _recorder.Dispose();
                }
                finally
                {
                    _recorder = null;
                }
            if (_isPlayer == true)
            {
                if (_player != null)
                    try
                    {
                        _player.Dispose();
                    }
                    finally
                    {
                        _player = null;
                    }
                _stream.Flush(); // clear all pending data
            }
        }

        private void Filler(IntPtr data, int size)
        {
            if (_isPlayer == true)
            {
                if (_playerBuffer == null || _playerBuffer.Length < size)
                    _playerBuffer = new byte[size];
                if (_stream.Length >= size)
                    _stream.Read(_playerBuffer, 0, size);
                else
                    for (int i = 0; i < _playerBuffer.Length; i++)
                        _playerBuffer[i] = 0;
                System.Runtime.InteropServices.Marshal.Copy(_playerBuffer, 0, data, size);
            }
        }

        private void DataArrived(IntPtr data, int size)
        {
            if (_recorderBuffer == null || _recorderBuffer.Length < size)
                _recorderBuffer = new byte[size];
            if (_recorderBuffer != null)
            {
                System.Runtime.InteropServices.Marshal.Copy(data, _recorderBuffer, 0, size);
                if (_isPlayer == true)
                    _stream.Write(_recorderBuffer, 0, _recorderBuffer.Length);
                byte[] b = new byte[size];
                System.Runtime.InteropServices.Marshal.Copy(data,b,0, size);
                try
                {
                    //_audioFrame.Process(ref b);
                    _audioFrame.SetYAxis(ref YSection);
                    _audioFrame.RenderTimeDomain(ref TimeDomainBox);
                    _audioFrame.RenderSpectrogram(ref FrequencyDomainBox);
                }
                catch (Exception MyEx)
                {
                    //do nothing
                }
            }
        }
        public void Test()
        {

            //int h = 0;
            //for (int i = 0; i < buf.Length; i += 4)
            //{
            //    _waveLeft[h] = (double)BitConverter.ToInt16(buf, i);
            //    h++;
            //}
            FileStream fs = new FileStream("Ch1AD20121210102223.dat", FileMode.Open);
            br = new BinaryReader(fs);
            timer.Enabled = true;
            // Generate frequency domain data in decibels


        }
        public void Display(byte[] buf)
        {
            if (_isPlayer == true)
                _stream.Write(buf, 0, buf.Length);
            if (buf.Length <= _audioFrameSize)
            {
                _audioFrame.AddData(buf);
            }
            else
            {
                int size = 0;
                for (int i = 0; i < Math.Floor((double)buf.Length / _audioFrameSize); i++)
                {
                    
                    byte[] newbuf = new byte[_audioFrameSize];
                    Array.Copy(buf, i * _audioFrameSize, newbuf, 0, _audioFrameSize);
                    _audioFrame.AddData(newbuf);
                    size += _audioFrameSize;
                }
                //最后一包
                if (buf.Length - size!=0)
                {
                    byte[] lastbuf = new byte[buf.Length - size];
                    Array.Copy(buf, size, lastbuf, 0, buf.Length - size);
                    _audioFrame.AddData(lastbuf);
                }

            }
            

        }
        private void WaveBox_Resize(object sender, EventArgs e)
        {

            if (_audioFrame != null)
            {
                _audioFrame.SetYAxis(ref YSection);
                
                _audioFrame.RenderTimeDomain(ref TimeDomainBox);       
                _audioFrame.RenderSpectrogram(ref FrequencyDomainBox);

            }
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            byte[] _wave = br.ReadBytes(4096);
            if (_wave.Length != 4096)//读到头了
                br.BaseStream.Position = 0;
            if (_wave.Length!=0)
                Display(_wave);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //_audioFrame.SetTitle(ref TitleSection, title);
            //_audioFrame.SetYAxis(ref YSection);
            _audioFrame.RenderSpectrogram(ref FrequencyDomainBox);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            _audioFrame.SetYAxis(ref YSection);
            _audioFrame.RenderTimeDomain(ref TimeDomainBox);
        }

        private void YSection_Paint(object sender, PaintEventArgs e)
        {
            //_audioFrame.SetYAxis(ref YSection);
        }

        

        
    }
}
