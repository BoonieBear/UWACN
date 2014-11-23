using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace WaveGenarator
{
    /// <summary>
    /// 生成设定的余弦波形，并提供保存到本地的功能
    /// </summary>
    public class CosineWave : IDisposable
    {
        /// <summary>
        /// 波形数据
        /// </summary>
        public Int16[] data;
        public double _freq = 10000;
        public double _phase = Math.PI*0.5;//相位从零开始
        public double _srate = 64000;
        public double length = 0.1;
        public double amp = 1;
        public void Dispose()
        {
            GC.Collect();
        }

        public CosineWave()
        {

        }
        public CosineWave(double freq, double phase, double sample_rate, double Amp,double len)
        {
            Set(freq, phase, sample_rate, Amp, len);
        }
        /// <summary>
        /// 中心频率
        /// </summary>
        public double Freq
        {
            get
            {
                double f  = _freq;
                return f;

            }
            set
            {
                _freq = value ;
            }
        }

        /// <summary>
        /// 初始相位
        /// </summary>
        public double Phase
        {
            get
            {
                double p = _phase;
                return p;

            }
            set
            {
                _phase = value;
            }
        }

        /// <summary>
        /// 采样率
        /// </summary>
        public double SampleRate
        {
            get
            {
                double s = _srate;
                return s;
            }
            set
            {
                _srate = value;
            }
        }

        /// <summary>
        /// 波形长度
        /// </summary>
        public double Length
        {
            get
            {
                double l = length;
                return l;
            }
            set
            {
                length = value/1000;
            }
        }

   

        /// <summary>
        /// 设定频率，相位，采样率，信号长度参数
        /// </summary>
        public virtual void Set(double freq, double phase, double sample_rate, double Amp,double len)
        {
            Freq = freq;
            Phase = phase;
            SampleRate = sample_rate;
            amp = Amp;
            Length = len;
            data = new Int16[(int)(sample_rate * Length)];

        }

        /// <summary>
        /// 产生波形，使用virtual关键字以便继承类重写
        /// </summary>
        public virtual void Genarate()
        {
            try
            {
                double fAngle = Phase;
                int i;
                for (i = 0; i < SampleRate * Length; i++)
                {
                    data[i] = (short)(amp * 32767 * Math.Cos(fAngle));
                    fAngle += 2 * Math.PI * Freq / SampleRate;
                    if (fAngle > 2 * Math.PI)
                        fAngle -= 2 * Math.PI;
                }
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message,"信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 复制波形
        /// </summary>
        public void copy(byte[] Darray)
        {
            try
            {
                Buffer.BlockCopy(data, 0, Darray, 0, data.Length * 2);
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        /// <param name="filepath">保存波形</param>
        public void SaveFile(string filepath)
        {
            try
            {
                FileStream fs = new FileStream(filepath, FileMode.CreateNew);
                // Create the writer for data.
                BinaryWriter w = new BinaryWriter(fs);
                // Write data to Test.data.
                foreach (short dat in data)
                {
                    w.Write(dat);
                }
                w.Close();
                fs.Close();
                

            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
