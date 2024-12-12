namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Runtime.CompilerServices;

    internal class DelayedActivation
    {
        private readonly WeakReference itemRef;

        public DelayedActivation(BaseLayoutItem item, bool focus, bool delayed)
        {
            this.itemRef = new WeakReference(item);
            this.Focus = focus;
            this.Delayed = delayed;
        }

        public bool Delayed { get; private set; }

        public bool Focus { get; private set; }

        public BaseLayoutItem Item =>
            this.itemRef.Target as BaseLayoutItem;
    }
}

