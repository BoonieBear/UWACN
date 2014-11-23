using System;
using System.Collections.Generic;
using System.Text;
using MSPTracer.Helper;
namespace MSPTracer.Helper
{
    public class EventsClass
    {
        /// <summary>
        /// 主对话框中状态栏文字更改事件
        /// </summary>
        public class StatusEventArgs : EventArgs
        {
            private readonly string statuslabel;
            public StatusEventArgs(string statuslabel)
            {
                this.statuslabel = statuslabel;
            }
            public string label
            {
                get
                {
                    return statuslabel;
                }
            }
        }
        /// <summary>
        /// 传递给主窗口的经纬度事件消息
        /// </summary>
        public class lnglatEventArgs : EventArgs
        {
            private readonly double lng;
            private readonly double lat;
            private readonly float bearing;
            public lnglatEventArgs(double lng, double lat, float bearing)
            {
                this.lng = lng;
                this.lat = lat;
                this.bearing = bearing;
            }
            public double position_lng
            {
                get
                {
                    return lng;
                }
            }
            public double position_lat
            {
                get
                {
                    return lat;
                }
            }
            public float direction_bearing
            {
                get
                {
                    return bearing;
                }
            }
        }
        public class WaveEventArgs : EventArgs
        {
            private readonly byte[] buf ;
            private readonly int length;
            public WaveEventArgs(byte[] netpacket, int length)
            {
                this.buf = new byte[length];
                this.length = length;
                Buffer.BlockCopy(netpacket, 0, buf, 0, length);
            }
            public byte[] WaveBuffer
            {
                get
                {
                    return buf;
                }
            }
            public int WaveBufferLength
            {
                get
                {
                    return length;
                }
            }
        }
        public class UtcTimeEventArgs : EventArgs
        {
            private readonly string GpsTime;
            public UtcTimeEventArgs(string UtcTime)
            {
                this.GpsTime = UtcTime;

            }
            public string GpsUtcTime
            {
                get
                {
                    return GpsTime;
                }
            }
        }
        public class GpsEventArgs : EventArgs
        {
            private readonly string gpslog;
            public GpsEventArgs(string buffer)
            {
                this.gpslog = buffer;
            }
            public string gpsdata
            {
                get
                {
                    return gpslog;
                }
            }
        }

    }
}
