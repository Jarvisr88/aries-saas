namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;

    public class ItemCancelEventArgs : ItemEventArgs
    {
        public ItemCancelEventArgs(BaseLayoutItem item) : this(false, item)
        {
        }

        public ItemCancelEventArgs(bool cancel, BaseLayoutItem item) : base(item)
        {
            this.Cancel = cancel;
        }

        public bool Cancel { get; set; }
    }
}

