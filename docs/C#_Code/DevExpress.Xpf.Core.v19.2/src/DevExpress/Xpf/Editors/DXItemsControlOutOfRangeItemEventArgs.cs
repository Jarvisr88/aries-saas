namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class DXItemsControlOutOfRangeItemEventArgs : EventArgs
    {
        public DXItemsControlOutOfRangeItemEventArgs(int index)
        {
            this.Index = index;
        }

        public int Index { get; private set; }

        public object Item { get; set; }

        public bool Handled { get; set; }
    }
}

