namespace DevExpress.Xpo.DB.Helpers
{
    using System;
    using System.Threading;

    internal class DataCacheWriterLock : IDisposable
    {
        public readonly ReaderWriterLockSlim RWL;

        public DataCacheWriterLock(ReaderWriterLockSlim rwl)
        {
            this.RWL = rwl;
            this.RWL.EnterWriteLock();
        }

        public void Dispose()
        {
            this.RWL.ExitWriteLock();
        }
    }
}

