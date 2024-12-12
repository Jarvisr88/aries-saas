namespace DevExpress.Xpf.Core
{
    using System;

    public class TabControlTabShowingEventArgs : TabControlCommonCancelEventArgsBase
    {
        public TabControlTabShowingEventArgs(int index, object item) : base(index, item)
        {
        }
    }
}

