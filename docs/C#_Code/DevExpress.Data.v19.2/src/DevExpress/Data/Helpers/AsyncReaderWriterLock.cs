namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    public class AsyncReaderWriterLock : IDisposable
    {
        private AsyncOperationIdentifier writeLockOwnerId;
        private AsyncOperationIdentifier upgradeableReadLockOwherId;
        private readonly HashSet<AsyncOperationIdentifier> readerIds;
        private int readWaitersCount;
        private int writeWaitersCount;
        private int writeAfterUpgradeWaitersCount;
        private int upgradeableReadWaitersCount;
        private readonly SemaphoreSlim readLockSemaphore;
        private readonly SemaphoreSlim writeLockSemaphore;
        private readonly SemaphoreSlim writeAfterUpgradeLockSemaphore;
        private readonly SemaphoreSlim upgradeableReadLockSemaphore;
        private SpinLock spinLock;

        public AsyncReaderWriterLock();
        private IDisposable ContinueEnterReadLock(AsyncOperationIdentifier asyncOperationId);
        private IDisposable ContinueEnterUpgradeableReadLock(AsyncOperationIdentifier asyncOperationId);
        public void Dispose();
        public IDisposable EnterReadLock();
        [AsyncStateMachine(typeof(AsyncReaderWriterLock.<EnterReadLockAsync>d__13))]
        public Task<IDisposable> EnterReadLockAsync(AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken = new CancellationToken());
        public IDisposable EnterUpgradeableReadLock();
        [AsyncStateMachine(typeof(AsyncReaderWriterLock.<EnterUpgradeableReadLockAsync>d__16))]
        public Task<IDisposable> EnterUpgradeableReadLockAsync(AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken = new CancellationToken());
        public IDisposable EnterWriteLock();
        [AsyncStateMachine(typeof(AsyncReaderWriterLock.<EnterWriteLockAsync>d__19))]
        public Task<IDisposable> EnterWriteLockAsync(AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken = new CancellationToken());
        private void ExitReadLock(AsyncOperationIdentifier asyncOperationId);
        private void ExitUpgradeableReadLock(AsyncOperationIdentifier asyncOperationId);
        private void ExitWriteLock(AsyncOperationIdentifier asyncOperationId);
        private IDisposable TryEnterReadLockSimple(AsyncOperationIdentifier asyncOperationId);
        private IDisposable TryEnterUpgradeableReadLockSimple(AsyncOperationIdentifier asyncOperationId);
        private IDisposable TryEnterWriteLockSimple(AsyncOperationIdentifier asyncOperationId);

        [CompilerGenerated]
        private struct <EnterReadLockAsync>d__13 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<IDisposable> <>t__builder;
            public AsyncReaderWriterLock <>4__this;
            public AsyncOperationIdentifier asyncOperationId;
            public CancellationToken cancellationToken;
            private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext();
            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine);
        }

        [CompilerGenerated]
        private struct <EnterUpgradeableReadLockAsync>d__16 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<IDisposable> <>t__builder;
            public AsyncReaderWriterLock <>4__this;
            public AsyncOperationIdentifier asyncOperationId;
            public CancellationToken cancellationToken;
            private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext();
            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine);
        }

        [CompilerGenerated]
        private struct <EnterWriteLockAsync>d__19 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<IDisposable> <>t__builder;
            public AsyncReaderWriterLock <>4__this;
            public AsyncOperationIdentifier asyncOperationId;
            public CancellationToken cancellationToken;
            private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext();
            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine);
        }

        private class ReadLockHolder : IDisposable
        {
            private readonly AsyncReaderWriterLock owner;
            private readonly AsyncOperationIdentifier asyncOperationId;

            public ReadLockHolder(AsyncReaderWriterLock owner, AsyncOperationIdentifier asyncOperationId);
            public void Dispose();
        }

        private class UpgradeableReadLockHolder : IDisposable
        {
            private readonly AsyncReaderWriterLock owner;
            private readonly AsyncOperationIdentifier asyncOperationId;

            public UpgradeableReadLockHolder(AsyncReaderWriterLock owner, AsyncOperationIdentifier asyncOperationId);
            public void Dispose();
        }

        private class WriteLockHolder : IDisposable
        {
            private readonly AsyncReaderWriterLock owner;
            private readonly AsyncOperationIdentifier asyncOperationId;

            public WriteLockHolder(AsyncReaderWriterLock owner, AsyncOperationIdentifier asyncOperationId);
            public void Dispose();
        }
    }
}

