namespace DevExpress.XtraPrinting
{
    using System;
    using System.Threading;

    public class SynchronizedDelayer : IDelayer
    {
        private static readonly TimeSpan InfiniteTimeSpan = new TimeSpan(0, 0, 0, 0, -1);
        private readonly TimeSpan interval;
        private readonly object syncLock = new object();
        private readonly SynchronizationContext syncContext;
        private Timer timer;

        public SynchronizedDelayer(TimeSpan interval, SynchronizationContext syncContext)
        {
            this.interval = interval;
            this.syncContext = syncContext;
        }

        public void Abort()
        {
            object syncLock = this.syncLock;
            lock (syncLock)
            {
                if (this.timer != null)
                {
                    this.RemoveTimer();
                }
            }
        }

        public void Execute(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            object syncLock = this.syncLock;
            lock (syncLock)
            {
                if (this.timer != null)
                {
                    throw new InvalidOperationException();
                }
                this.timer = new Timer(new System.Threading.TimerCallback(this.TimerCallback), action, this.interval, InfiniteTimeSpan);
            }
        }

        private void ExecuteAction(Action action)
        {
            SendOrPostCallback d = x => action();
            if (this.syncContext != null)
            {
                this.syncContext.Post(d, null);
            }
            else
            {
                d(null);
            }
        }

        private void RemoveTimer()
        {
            this.timer.Dispose();
            this.timer = null;
        }

        private void TimerCallback(object state)
        {
            object syncLock = this.syncLock;
            lock (syncLock)
            {
                if (this.timer != null)
                {
                    this.RemoveTimer();
                    this.ExecuteAction((Action) state);
                }
            }
        }
    }
}

