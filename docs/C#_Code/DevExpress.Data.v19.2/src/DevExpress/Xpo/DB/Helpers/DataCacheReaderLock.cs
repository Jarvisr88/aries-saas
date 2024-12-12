namespace DevExpress.Xpo.DB.Helpers
{
    using System;
    using System.Threading;

    internal class DataCacheReaderLock : IDisposable
    {
        public readonly ReaderWriterLockSlim RWL;

        public DataCacheReaderLock(ReaderWriterLockSlim rwl)
        {
            this.RWL = rwl;
            this.RWL.EnterReadLock();
        }

        public void Dispose()
        {
            this.RWL.ExitReadLock();
        }
    }
}

