namespace DevExpress.XtraPrinting
{
    using System;
    using System.Windows.Threading;

    [Obsolete("Use the SynchronizedDelayer class instead.")]
    public class DispatcherTimerDelayer : IDelayer
    {
        private readonly DispatcherTimer timer;
        private Action action;

        public DispatcherTimerDelayer(TimeSpan interval)
        {
            DispatcherTimer timer1 = new DispatcherTimer();
            timer1.Interval = interval;
            this.timer = timer1;
        }

        public void Abort()
        {
            this.StopTimer();
        }

        public void Execute(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            if (this.timer.IsEnabled)
            {
                throw new InvalidOperationException("Another execute request is pending already");
            }
            this.action = action;
            this.timer.Tick += new EventHandler(this.timer_Tick);
            this.timer.Start();
        }

        private void StopTimer()
        {
            this.timer.Stop();
            this.timer.Tick -= new EventHandler(this.timer_Tick);
        }

        private void timer_Tick(object sender, object e)
        {
            this.StopTimer();
            this.action();
        }
    }
}

