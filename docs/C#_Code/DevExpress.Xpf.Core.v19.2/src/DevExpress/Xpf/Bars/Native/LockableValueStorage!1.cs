namespace DevExpress.Xpf.Bars.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class LockableValueStorage
    {
        private TValue currentValue;
        private TValue lockedValue;
        private bool hasLockedValue;
        private readonly bool setLastOnUnlock;
        private readonly Locker locker;
        [CompilerGenerated]
        private ValueChangedEventHandler<TValue> ValueChanged;
        [CompilerGenerated]
        private ValueChangedEventHandler<TValue> ValueChanging;

        public event ValueChangedEventHandler<TValue> ValueChanged;

        public event ValueChangedEventHandler<TValue> ValueChanging;

        public LockableValueStorage();
        public LockableValueStorage(bool setLastOnUnlock);
        public TValue GetValue();
        public IDisposable Lock();
        private void OnUnlocked(object sender, EventArgs e);
        public void SetValue(TValue value, bool silent = false);
        public void Unlock();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LockableValueStorage<TValue>.<>c <>9;
            public static ValueChangedEventHandler<TValue> <>9__5_0;
            public static ValueChangedEventHandler<TValue> <>9__5_1;

            static <>c();
            internal void <.ctor>b__5_0(object o, ValueChangedEventArgs<TValue> a);
            internal void <.ctor>b__5_1(object o, ValueChangedEventArgs<TValue> a);
        }
    }
}

