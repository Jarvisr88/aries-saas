namespace DevExpress.Printing.Core
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Threading;

    [Obsolete("Use the SynchronizedDelayer class instead.")]
    public class ThreadingTimerDelayer : IDelayer
    {
        private readonly TimeSpan interval;
        private Timer timer;

        public ThreadingTimerDelayer(TimeSpan interval)
        {
            this.interval = interval;
        }

        public void Abort()
        {
            this.timer.Dispose();
        }

        public void Execute(Action action)
        {
            this.timer = new Timer(result => this.Tick((Action) result), action, this.interval, new TimeSpan(-1L));
        }

        private void Tick(Action action)
        {
            this.timer.Dispose();
            action();
        }
    }
}

