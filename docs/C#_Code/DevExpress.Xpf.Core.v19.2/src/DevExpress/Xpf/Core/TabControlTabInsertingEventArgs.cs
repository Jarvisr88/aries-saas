namespace DevExpress.Xpf.Core
{
    using System;

    public class TabControlTabInsertingEventArgs : TabControlCommonCancelEventArgsBase
    {
        public TabControlTabInsertingEventArgs(int index, object item) : base(index, item)
        {
        }
    }
}

