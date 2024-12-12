namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class LockHelper : IDisposable
    {
        private LockHelperDelegate UnlockDelegate;
        private int lockCount;
        private readonly Stack<LockHelperDelegate> actions;

        public LockHelper()
        {
            this.actions = new Stack<LockHelperDelegate>();
        }

        public LockHelper(LockHelperDelegate unlockDelegate)
        {
            this.actions = new Stack<LockHelperDelegate>();
            this.UnlockDelegate = unlockDelegate;
        }

        public void AddUnlockAction(LockHelperDelegate action)
        {
            if (this.IsLocked && ((this.actions != null) && !this.actions.Contains(action)))
            {
                this.actions.Push(action);
            }
        }

        public void DoWhenUnlocked(LockHelperDelegate action)
        {
            if (this.IsLocked)
            {
                this.AddUnlockAction(action);
            }
            else
            {
                action();
            }
        }

        public LockHelper Lock()
        {
            this.lockCount++;
            return this;
        }

        public LockHelper LockOnce()
        {
            if (!this.IsLocked)
            {
                this.Lock();
            }
            return this;
        }

        public static implicit operator bool(LockHelper locker) => 
            locker.IsLocked;

        public void Reset()
        {
            this.actions.Clear();
            this.UnlockDelegate = null;
            this.Unlock();
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
                    while (true)
                    {
                        if (this.actions.Count <= 0)
                        {
                            if (this.UnlockDelegate != null)
                            {
                                this.UnlockDelegate();
                            }
                            break;
                        }
                        LockHelperDelegate delegate2 = this.actions.Pop();
                        delegate2();
                    }
                }
            }
        }

        public bool IsLocked =>
            this.lockCount > 0;

        public delegate void LockHelperDelegate();
    }
}

