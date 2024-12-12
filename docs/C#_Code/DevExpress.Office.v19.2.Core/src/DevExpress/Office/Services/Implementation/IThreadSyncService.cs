namespace DevExpress.Office.Services.Implementation
{
    using System;

    public interface IThreadSyncService
    {
        void EnqueueInvokeInUIThread(Action action);
    }
}

