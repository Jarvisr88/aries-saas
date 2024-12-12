namespace DevExpress.Office.Services.Implementation
{
    using DevExpress.Office.Utils;
    using System;

    public class ThreadSyncService : IThreadSyncService
    {
        [ThreadStatic]
        private static IThreadSyncService service;
        private readonly MessageQueue messageQueue = new MessageQueue();

        public static IThreadSyncService Create()
        {
            service ??= new ThreadSyncService();
            return service;
        }

        public void EnqueueInvokeInUIThread(Action action)
        {
            this.messageQueue.EnsureHandleCreated();
            this.messageQueue.BeginInvoke(action);
        }
    }
}

