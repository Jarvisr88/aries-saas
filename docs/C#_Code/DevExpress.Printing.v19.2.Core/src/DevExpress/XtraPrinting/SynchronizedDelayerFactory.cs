namespace DevExpress.XtraPrinting
{
    using System;
    using System.Threading;

    public class SynchronizedDelayerFactory : IDelayerFactory
    {
        private readonly SynchronizationContext syncContext;

        public SynchronizedDelayerFactory() : this(SynchronizationContext.Current)
        {
        }

        public SynchronizedDelayerFactory(SynchronizationContext syncContext)
        {
            this.syncContext = syncContext;
        }

        public IDelayer Create(TimeSpan interval) => 
            new SynchronizedDelayer(interval, this.syncContext);
    }
}

