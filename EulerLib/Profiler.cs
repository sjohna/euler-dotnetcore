using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Euler
{
    // TODO: refactor Profiler
    public interface IProfileTimerDealy : IDisposable { }

    public interface IProfiler
    {
        // TODO: had to update lang version to 8.0 and targetframework to .NEt standard 2.1 for this work work. Do this for all projects
        public static IProfiler Default = new DefaultProfiler();

        IProfileTimerDealy Time(string timerName);
        void Inline(string timer, Action a);
        void Print();
    }

    public class ProfileTimerDealy : IProfileTimerDealy
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

    public class DefaultProfileTimerDealy : IProfileTimerDealy
    {
        internal static DefaultProfileTimerDealy Instance = new DefaultProfileTimerDealy();

        private DefaultProfileTimerDealy()
        {
            // no-op
        }

        public void Dispose()
        {
            // no-op
        }
    }

    public class Profiler : IProfiler
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

        public IProfileTimerDealy Time(string timerName)
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

    public class DefaultProfiler : IProfiler
    {
        public void Inline(string timer, Action a)
        {
            a();
        }

        public void Print()
        {
            //no-op
        }

        public IProfileTimerDealy Time(string timerName)
        {
            return DefaultProfileTimerDealy.Instance;
        }
    }
}