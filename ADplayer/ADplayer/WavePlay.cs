using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace webnode.Sources
{
    class WavePlay:IDisposable
    {
        private WaveOutPlayer _player;
        private byte[] _playerBuffer;
        private FifoStream _stream;
        private WaveFormat _waveFormat;
        private int _audioSamplesPerSecond = 44100;
        private int _audioFrameSize = 65536;
        private int _audioBitsPerSample = 16;
        private int _audioChannels = 2;
        public WavePlay()
        {
            
        }
        public bool Initail()
        {
            if (WaveNative.waveInGetNumDevs() == 0)
            {
                MessageBox.Show(DateTime.Now.ToString() + " : 没有可用的音频设备\r\n");
                return false;
            }
            else
            {

                _stream = new FifoStream();
                Stop();
                _waveFormat = new WaveFormat(_audioFrameSize, _audioBitsPerSample, _audioChannels);
                _player = new WaveOutPlayer(-1, _waveFormat, _audioFrameSize * _audioBitsPerSample / 8, 3, new BufferFillEventHandler(Filler));
                return true;
            }
        }
        public int SamplesPerSecond
        {
            set 
            {
                _audioSamplesPerSecond = value;
            }
            get
            {
                return _audioSamplesPerSecond;
            }
        }
        public int FrameSize
        {
            set
            {
                _audioFrameSize = value;
            }
            get
            {
                return _audioFrameSize;
            }
        }
        public int BitsPerSample
        {
            set
            {
                _audioBitsPerSample = value;
            }
            get
            {
                return _audioBitsPerSample;
            }
        }
        public int Channels
        {
            set
            {
                _audioChannels = value;
            }
            get
            {
                return _audioChannels;
            }
        }

        public void PlayWave(byte[] ADWave)
        {
            _stream.Write(ADWave, 0, ADWave.Length);
        }
        private void Filler(IntPtr data, int size)
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
        private void Stop()
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
        public void Dispose()
        {
            Stop();
            GC.SuppressFinalize(this);
        }
    }
}
