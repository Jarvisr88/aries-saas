namespace DevExpress.Xpf.Core
{
    using System;

    public class TabControlTabInsertedEventArgs : TabControlCommonEventArgsBase
    {
        public TabControlTabInsertedEventArgs(int index, object item) : base(index, item)
        {
        }
    }
}

