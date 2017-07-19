using System;
using System.Collections.Generic;
using System.Text;

namespace AU.Common.Utility
{
    public sealed class NetworkSpeed
    {
        private struct Speed
        {
            public DateTime Time;
            public long Size;
        }

        List<Speed> SpeedList = new List<Speed>();

        public void Increment(long value)
        {
            lock (this)
            {
                SpeedList.Add(new Speed() { Size = value, Time = DateTime.Now });
                if (this.SpeedList.Count > 3000)
                {
                    if (this.SpeedList.Count > 0)
                    {
                        this.SpeedList.RemoveAt(0);
                    }
                }
            }
        }

        DateTime LastGetSpeedTime = DateTime.Now;

        public string GetSpeed()
        {
            lock (this)
            {
                if (this.SpeedList.Count > 0 && DateTime.Now.Subtract(this.SpeedList[this.SpeedList.Count - 1].Time).TotalSeconds > 1)
                {
                    this.SpeedList.Clear();
                    this.LastGetSpeedTime = DateTime.Now;
                }
                this.LastGetSpeedTime = DateTime.Now;
                if (this.SpeedList.Count == 0) return "0 B/s";
                long total = 0;
                for (int i = 0; i < this.SpeedList.Count; i++)
                {
                    total += this.SpeedList[i].Size;
                }
                double speed = total / (DateTime.Now - this.SpeedList[0].Time).TotalSeconds;
                return string.Format("{0}/s", GetText(speed));
            }
        }

        private string GetText(double size)
        {
            if (size < 1024)
                return string.Format("{0} B", size.ToString("0.0"));
            else if (size < 1024 * 1024)
                return string.Format("{0} KB", (size / 1024.0f).ToString("0.0"));
            else
                return string.Format("{0} MB", (size / (1024.0f * 1024.0f)).ToString("0.0"));
        }
    }
}
