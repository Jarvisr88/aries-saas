namespace DevExpress.Xpf.Core.Native
{
    using System;

    public abstract class FreezableRenderObject
    {
        private static readonly object Locker;
        private bool isFrozen;

        static FreezableRenderObject();
        protected FreezableRenderObject();
        public void Freeze();
        protected abstract void FreezeOverride();
        protected void SetProperty<T>(ref T container, T value);

        protected bool IsFrozen { get; }
    }
}

