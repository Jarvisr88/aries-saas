namespace DevExpress.Xpf.Core
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    [DebuggerStepThrough]
    public class Locker : IDisposable
    {
        private int lockCount;

        public event EventHandler Unlocked;

        public void DoIfNotLocked(Action action)
        {
            if (!this.IsLocked)
            {
                action();
            }
        }

        public void DoIfNotLocked(Action action, Action lockedAction = null)
        {
            if (!this.IsLocked)
            {
                action();
            }
            else if (lockedAction != null)
            {
                lockedAction();
            }
        }

        public T DoIfNotLocked<T>(Func<T> action, Func<T> lockedAction) => 
            !this.IsLocked ? action() : lockedAction();

        public void DoLockedAction(Action action)
        {
            this.Lock();
            try
            {
                action();
            }
            finally
            {
                this.Unlock();
            }
        }

        public T DoLockedAction<T>(Func<T> action)
        {
            T local;
            this.Lock();
            try
            {
                local = action();
            }
            finally
            {
                this.Unlock();
            }
            return local;
        }

        public void DoLockedActionIfNotLocked(Action action)
        {
            this.DoIfNotLocked(() => this.DoLockedAction(action));
        }

        public Locker Lock()
        {
            this.lockCount++;
            return this;
        }

        public Locker LockOnce()
        {
            if (!this.IsLocked)
            {
                this.Lock();
            }
            return this;
        }

        public static implicit operator bool(Locker locker) => 
            locker.IsLocked;

        private void RaiseOnUnlock()
        {
            if (this.Unlocked != null)
            {
                this.Unlocked(this, EventArgs.Empty);
            }
        }

        public void Reset()
        {
            this.lockCount = 0;
        }

        void IDisposable.Dispose()
        {
            this.Unlock();
        }

        public void Unlock()
        {
            if (this.IsLocked)
            {
                this.lockCount--;
                if (!this.IsLocked)
                {
                    this.RaiseOnUnlock();
                }
            }
        }

        public bool IsLocked =>
            this.lockCount > 0;
    }
}

