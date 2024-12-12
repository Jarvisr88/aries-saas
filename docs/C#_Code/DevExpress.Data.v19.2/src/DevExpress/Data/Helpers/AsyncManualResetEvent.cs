namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    public class AsyncManualResetEvent
    {
        private volatile bool state;
        private readonly ManualResetEventSlim stateEvent;
        private SpinLock syncLock;
        private readonly Queue<TaskCompletionSource<object>> waitingTasks;

        public AsyncManualResetEvent(bool initialState);
        public void Reset();
        public void Set();
        private void SetTaskCompleted(object taskCompletionSource);
        public void WaitOne();
        public bool WaitOne(int milliseconds);
        public Task WaitOneAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}

