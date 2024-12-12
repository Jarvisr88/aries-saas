namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public class TabControlTabRemovingEventArgs : TabControlCommonCancelEventArgsBase
    {
        public TabControlTabRemovingEventArgs(int index, object item, bool isDragDrop) : base(index, item)
        {
            this.IsDragDrop = isDragDrop;
        }

        public bool IsDragDrop { get; private set; }
    }
}

