namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Collections.Generic;

    internal class FloatingWindowLock
    {
        private Dictionary<LockerKey, LockHelper> lockers = new Dictionary<LockerKey, LockHelper>();

        public LockHelper GetLocker(LockerKey key)
        {
            LockHelper helper;
            if (!this.lockers.TryGetValue(key, out helper))
            {
                helper = new LockHelper();
                this.lockers[key] = helper;
            }
            return helper;
        }

        public bool IsLocked(LockerKey key) => 
            (bool) this.GetLocker(key);

        public IDisposable Lock(LockerKey key) => 
            this.GetLocker(key).Lock();

        public IDisposable LockOnce(LockerKey key) => 
            this.GetLocker(key).LockOnce();

        public void Unlock(LockerKey key)
        {
            this.GetLocker(key).Unlock();
        }

        public enum LockerKey
        {
            WindowState,
            FloatingBounds,
            Maximize,
            Minimize,
            NativeBounds,
            RestoreBounds,
            ParentOpening,
            CheckFloatBounds,
            ThemeChanging,
            Focus,
            Restore,
            ResetMaximized
        }
    }
}

