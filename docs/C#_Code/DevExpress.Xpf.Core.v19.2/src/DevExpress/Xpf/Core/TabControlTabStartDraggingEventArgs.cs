namespace DevExpress.Xpf.Core
{
    using System;

    public class TabControlTabStartDraggingEventArgs : TabControlCommonCancelEventArgsBase
    {
        public TabControlTabStartDraggingEventArgs(int index, object item) : base(index, item)
        {
        }
    }
}

