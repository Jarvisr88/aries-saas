namespace DevExpress.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class WorkAsyncResult : IAsyncResult
    {
        public object AsyncState { get; set; }

        WaitHandle IAsyncResult.AsyncWaitHandle { get; }

        bool IAsyncResult.CompletedSynchronously { get; }

        public virtual bool IsCompleted { get; set; }
    }
}

