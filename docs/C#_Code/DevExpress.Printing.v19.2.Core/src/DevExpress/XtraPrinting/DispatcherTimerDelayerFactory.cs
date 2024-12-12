namespace DevExpress.XtraPrinting
{
    using System;

    [Obsolete("Use the SynchronizedDelayerFactory class instead.")]
    public class DispatcherTimerDelayerFactory : IDelayerFactory
    {
        public IDelayer Create(TimeSpan interval) => 
            new DispatcherTimerDelayer(interval);
    }
}

