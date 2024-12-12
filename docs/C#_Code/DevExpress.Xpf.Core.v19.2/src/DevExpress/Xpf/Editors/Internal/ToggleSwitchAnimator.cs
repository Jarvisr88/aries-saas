namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media.Animation;
    using System.Windows.Threading;

    public class ToggleSwitchAnimator
    {
        private readonly int interval = 30;
        private readonly int frameTime = 40;

        public ToggleSwitchAnimator(double from, double to, int duration, Action tickCallback)
        {
            ExponentialEase ease = new ExponentialEase {
                EasingMode = EasingMode.EaseIn,
                Exponent = 2.0
            };
            this.Value = from;
            this.From = from;
            this.To = to;
            this.Duration = duration;
            this.Callback = tickCallback;
        }

        private void OnTick(object sender, EventArgs e)
        {
            this.Value += this.Increment;
            if (((Math.Sign(this.Increment) == 1) && (this.Value > this.To)) || ((Math.Sign(this.Increment) == -1) && (this.Value < this.To)))
            {
                this.Value = this.To;
                this.Stop();
            }
            this.Callback();
        }

        public void Start()
        {
            if (this.InProcess)
            {
                this.Stop();
            }
            this.InProcess = true;
            DispatcherTimer timer1 = new DispatcherTimer();
            timer1.Interval = new TimeSpan(0, 0, 0, 0, this.interval);
            this.Timer = timer1;
            double num = this.To - this.Value;
            this.Increment = num / ((double) (this.Duration / this.frameTime));
            this.Timer.Tick += new EventHandler(this.OnTick);
            this.Timer.Start();
        }

        public void Stop()
        {
            this.InProcess = false;
            if (this.Timer != null)
            {
                this.Timer.Stop();
                this.Timer.Tick -= new EventHandler(this.OnTick);
                this.Timer = null;
            }
        }

        public double Value { get; private set; }

        public double From { get; private set; }

        public double To { get; private set; }

        public int Duration { get; private set; }

        public bool InProcess { get; private set; }

        private double Increment { get; set; }

        private DispatcherTimer Timer { get; set; }

        private Action Callback { get; set; }
    }
}

