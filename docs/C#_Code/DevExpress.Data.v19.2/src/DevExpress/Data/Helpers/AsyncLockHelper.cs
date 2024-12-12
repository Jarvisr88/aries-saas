namespace DevExpress.Data.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public class AsyncLockHelper : IDisposable
    {
        private AsyncOperationIdentifier currentAsyncOperationId;
        private int threadId;
        private int recursionDepth;
        private readonly SemaphoreSlim lockSemaphore;

        public AsyncLockHelper();
        public void Dispose();
        private void Free();
        public IDisposable Lock();
        [AsyncStateMachine(typeof(AsyncLockHelper.<LockAsync>d__5))]
        public Task<IDisposable> LockAsync(AsyncOperationIdentifier asyncOperationId);

        [CompilerGenerated]
        private struct <LockAsync>d__5 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<IDisposable> <>t__builder;
            public AsyncOperationIdentifier asyncOperationId;
            public AsyncLockHelper <>4__this;
            private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext();
            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine);
        }

        private class AsyncLockHolder : IDisposable
        {
            private readonly AsyncLockHelper lockObject;
            private readonly int recursionDepth;
            private readonly AsyncOperationIdentifier currentAsyncOperationId;
            private readonly int threadId;

            public AsyncLockHolder(AsyncLockHelper lockObject);
            public void Dispose();
        }
    }
}

