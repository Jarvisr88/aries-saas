namespace DevExpress.Xpo.Logger
{
    using System;
    using System.Diagnostics;

    public class LogMessageTimer : IDisposable
    {
        private Stopwatch sw = Stopwatch.StartNew();

        public void Dispose()
        {
            this.Stop();
        }

        public void Restart()
        {
            this.sw.Stop();
            this.sw.Reset();
            this.sw.Start();
        }

        public void Start()
        {
            this.sw.Start();
        }

        public TimeSpan Stop()
        {
            TimeSpan elapsed = this.sw.Elapsed;
            this.sw.Stop();
            return elapsed;
        }

        public TimeSpan Elapsed =>
            this.sw.Elapsed;
    }
}

