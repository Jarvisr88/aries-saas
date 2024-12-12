namespace DevExpress.Xpf.Grid.Hierarchy
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class HierarchyChangedEventArgs : EventArgs
    {
        public static readonly HierarchyChangedEventArgs Default = new HierarchyChangedEventArgs(HierarchyChangeType.Invalidated, null);

        public HierarchyChangedEventArgs(HierarchyChangeType changeType, IItem item = null)
        {
            this.ChangeType = changeType;
            this.Item = item;
        }

        public HierarchyChangeType ChangeType { get; private set; }

        public IItem Item { get; private set; }
    }
}

