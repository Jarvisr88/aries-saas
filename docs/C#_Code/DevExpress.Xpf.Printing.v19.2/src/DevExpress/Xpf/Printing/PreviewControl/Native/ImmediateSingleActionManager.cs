namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Threading;

    internal class ImmediateSingleActionManager
    {
        private readonly DispatcherTimer timer;
        private readonly Action action;

        public ImmediateSingleActionManager(Action action, double delayTime = 153.0)
        {
            this.action = action;
            DispatcherTimer timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromMilliseconds(delayTime);
            this.timer = timer1;
            this.timer.Tick += new EventHandler(this.OnTimerTick);
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            this.timer.Stop();
            this.action();
        }

        public void RaiseAction()
        {
            if (this.timer.IsEnabled)
            {
                this.timer.Stop();
            }
            this.timer.Start();
        }
    }
}

