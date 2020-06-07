using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Euler
{
    public class ProfileTimerDealy : IDisposable
    {
        private Profiler m_profiler;
        private string m_timer;
        private Stopwatch m_stopwatch;

        internal ProfileTimerDealy(string timer, Profiler profiler)
        {
            m_timer = timer;
            m_profiler = profiler;
            m_stopwatch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            m_stopwatch.Stop();
            m_profiler.UpdateTimer(m_timer,m_stopwatch.ElapsedTicks);
        }
    }

    public class Profiler
    {
        private Dictionary<string,long> m_times = new Dictionary<string,long>();

        internal void UpdateTimer(string name, long time)
        {
            if (!m_times.ContainsKey(name))
            {
                m_times.Add(name,time);
            }
            else
            {
                m_times[name] += time;
            }
        }

        public ProfileTimerDealy Time(string timerName)
        {
            return new ProfileTimerDealy(timerName, this);
        }

        public void Inline(string timer, Action a)
        {
            using(Time(timer)) a();
        }

        public void Print()
        {
            long totalTicks = m_times.Select(t => t.Value).Sum();

            foreach (var time in m_times)
            {
                Console.WriteLine($"{time.Key}: {time.Value} ({time.Value*100.0/totalTicks})%");
            }
        }
    }
}