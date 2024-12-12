namespace DevExpress.Office.Services.Implementation
{
    using System;
    using System.Threading;

    public interface IThreadPoolService
    {
        void QueueJob(WaitCallback job);
        void Reset();
    }
}

