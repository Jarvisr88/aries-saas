namespace DevExpress.Xpf.Core
{
    using System;

    public class TabControlTabAddedEventArgs : TabControlCommonEventArgsBase
    {
        public TabControlTabAddedEventArgs(int index, object item) : base(index, item)
        {
        }
    }
}

