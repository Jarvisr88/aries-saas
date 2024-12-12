namespace DevExpress.Data.Helpers
{
    using System;
    using System.Threading;

    public class RealTimeSourceThrottler
    {
        private readonly SynchronizationContext Context;
        private readonly System.Action Action;
        private readonly System.Threading.Timer Timer;
        private int Disposed;

        private RealTimeSourceThrottler(SynchronizationContext context, int throttleTime, System.Action action);
        private void Do();
        private void DoCore();
        private void OnIdle(object sender, EventArgs e);
        public static void Throttle(SynchronizationContext context, int throttleTime, System.Action action);
    }
}

