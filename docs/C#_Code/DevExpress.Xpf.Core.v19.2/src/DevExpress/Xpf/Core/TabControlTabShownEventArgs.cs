namespace DevExpress.Xpf.Core
{
    using System;

    public class TabControlTabShownEventArgs : TabControlCommonEventArgsBase
    {
        public TabControlTabShownEventArgs(int index, object item) : base(index, item)
        {
        }
    }
}

