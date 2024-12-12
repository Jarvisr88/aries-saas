namespace DevExpress.Office.Services.Implementation
{
    using DevExpress.Office.Utils;
    using System;
    using System.Threading;

    public class OfficeThreadPoolService : IThreadPoolService, IDisposable
    {
        private OfficeThreadPool threadPool;

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (this.threadPool != null))
            {
                this.threadPool.Dispose();
                this.threadPool = null;
            }
        }

        public void QueueJob(WaitCallback job)
        {
            this.threadPool ??= new OfficeThreadPool();
            this.threadPool.QueueJob(job);
        }

        public void Reset()
        {
            if (this.threadPool != null)
            {
                this.threadPool.Reset();
            }
        }
    }
}

