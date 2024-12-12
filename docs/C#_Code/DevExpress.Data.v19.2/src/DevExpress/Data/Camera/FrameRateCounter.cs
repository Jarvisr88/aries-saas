namespace DevExpress.Data.Camera
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Timers;

    internal class FrameRateCounter
    {
        private Timer timer;
        private int count;

        public FrameRateCounter();
        public void Free();
        public void Inc();
        public void Start();
        public void Stop();
        private void timer_Elapsed(object sender, ElapsedEventArgs e);

        public int FrameRate { get; private set; }
    }
}

