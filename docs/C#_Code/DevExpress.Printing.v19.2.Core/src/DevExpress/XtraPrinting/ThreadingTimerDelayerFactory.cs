namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing.Core;
    using System;

    [Obsolete("Use the SynchronizedDelayerFactory class instead.")]
    public class ThreadingTimerDelayerFactory : IDelayerFactory
    {
        public IDelayer Create(TimeSpan interval) => 
            new ThreadingTimerDelayer(interval);
    }
}

