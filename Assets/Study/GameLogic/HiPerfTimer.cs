using System;
using System.Runtime.InteropServices;
using System.ComponentModel;

#if UNITY_STANDALONE_WIN
namespace Win32
{
    internal class HiPerfTimer
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(
            out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(
            out long lpFrequency);

        private long startTime;
        private long freq;

        // 构造函数
        public HiPerfTimer()
        {
            startTime = 0;
            bool ret = QueryPerformanceFrequency(out freq);
            if (ret == false)
            {
                throw new Win32Exception();
            }
        }

        // 开始计时器
        public Int64 GetNowTime()
        {
            QueryPerformanceCounter(out startTime);
            return startTime;
        }

        public double GetDuration(Int64 n64StopTime, Int64 n64Start)
        {
            return (double)(n64StopTime - n64Start) * 1000 / (double)freq;
        }
    }
}
#endif