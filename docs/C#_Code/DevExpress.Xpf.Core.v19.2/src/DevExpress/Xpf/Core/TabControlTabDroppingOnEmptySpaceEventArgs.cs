namespace DevExpress.Xpf.Core
{
    using System;

    public class TabControlTabDroppingOnEmptySpaceEventArgs : TabControlCommonCancelEventArgsBase
    {
        public TabControlTabDroppingOnEmptySpaceEventArgs(int index, object item) : base(index, item)
        {
        }
    }
}

