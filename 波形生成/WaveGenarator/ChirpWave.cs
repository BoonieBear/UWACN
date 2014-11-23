using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WaveGenarator
{
    public class ChirpWave: CosineWave
    {
        private double _strfreq;
        private double _stpfreq;
        
        public ChirpWave(double startfreq, double stopfreq, double sample_rate, double length,double Amp)
        {
            Set(startfreq, stopfreq, sample_rate, length, Amp);
        }

        public ChirpWave()
        {
            
        }

        public override void  Genarate()
        {
            try
            {

                double fAngle = 0;

                for (int i = 0; i < SampleRate * Length; i++)
                {
                    data[i] = (short)(amp * 32767 * Math.Cos(2 * Math.PI * fAngle + Phase));
                    fAngle += ((_stpfreq - _strfreq) / Length / SampleRate * i + _strfreq) / SampleRate;
                    if (fAngle > 1)
                        fAngle -= 1;
                    if (fAngle < -1)
                        fAngle += 1;
                }
            }
            catch (Exception myEx)
            {
                MessageBox.Show(myEx.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 设置波形参数
        /// </summary>
        /// <param name="startfreq"></param>
        /// <param name="stopfreq"></param>
        /// <param name="sample_rate"></param>
        /// <param name="length"></param>
        public  override void Set(double startfreq, double stopfreq, double sample_rate, double length, double Amp)
        {

            _strfreq = startfreq;
            _stpfreq = stopfreq;
            SampleRate = sample_rate;
            Length = length;
            amp = Amp;
            data = new Int16[(int)(sample_rate * Length)];
        }

    }
}
