namespace DevExpress.Xpo.DB
{
    using System;
    using System.Threading;

    public class DataStoreForkMultipleReadersSingleWriter : DataStoreFork
    {
        private readonly IDataStore changesProvider;
        private readonly ReaderWriterLockSlim rwl;

        public DataStoreForkMultipleReadersSingleWriter(IDataStore changesProvider, params IDataStore[] readProviders) : base(changesProvider.AutoCreateOption, readProviders)
        {
            this.rwl = new ReaderWriterLockSlim();
            this.changesProvider = changesProvider;
        }

        public override IDataStore AcquireChangeProvider()
        {
            this.rwl.EnterWriteLock();
            return this.changesProvider;
        }

        public override IDataStore AcquireReadProvider()
        {
            this.rwl.EnterReadLock();
            return base.AcquireReadProvider();
        }

        public override void ReleaseChangeProvider(IDataStore provider)
        {
            this.rwl.ExitWriteLock();
        }

        public override void ReleaseReadProvider(IDataStore provider)
        {
            try
            {
                base.ReleaseReadProvider(provider);
            }
            finally
            {
                this.rwl.ExitReadLock();
            }
        }
    }
}

