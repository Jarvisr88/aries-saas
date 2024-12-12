namespace DevExpress.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class AsyncWorker
    {
        private Thread thread;
        private WorkAsyncResult result;

        public IAsyncResult BeginWork(Action work, CancellationToken token);
        private void ExecCore(object work);

        public System.Exception Exception { get; private set; }
    }
}

