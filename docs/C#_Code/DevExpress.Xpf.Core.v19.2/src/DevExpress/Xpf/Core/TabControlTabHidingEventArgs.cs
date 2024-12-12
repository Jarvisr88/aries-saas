namespace DevExpress.Xpf.Core
{
    using System;

    public class TabControlTabHidingEventArgs : TabControlCommonCancelEventArgsBase
    {
        public TabControlTabHidingEventArgs(int index, object item) : base(index, item)
        {
        }
    }
}

