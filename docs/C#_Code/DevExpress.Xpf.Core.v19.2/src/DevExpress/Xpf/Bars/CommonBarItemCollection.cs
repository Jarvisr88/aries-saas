namespace DevExpress.Xpf.Bars
{
    using DevExpress.Mvvm;
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;

    [ContentWrapper(typeof(IBarItem))]
    public class CommonBarItemCollection : ObservableCollection<IBarItem>, ILockable
    {
        private int lockCount;

        public CommonBarItemCollection();
        public CommonBarItemCollection(ILinksHolder logicalParent);
        protected override void ClearItems();
        void ILockable.BeginUpdate();
        void ILockable.EndUpdate();
        protected override void InsertItem(int index, IBarItem item);
        private void OnInsertItem(int index, IBarItem item);
        private void OnParentIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e);
        private void OnRemoveItem(IBarItem barItem);
        protected override void RemoveItem(int index);
        protected override void SetItem(int index, IBarItem item);

        private DevExpress.Xpf.Bars.BarItemLinkCollection BarItemLinkCollection { get; set; }

        internal ILogicalChildrenContainer LogicalParent { get; set; }

        bool ILockable.IsLockUpdate { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CommonBarItemCollection.<>c <>9;
            public static Func<BarItemLinkBase, bool> <>9__15_0;

            static <>c();
            internal bool <OnRemoveItem>b__15_0(BarItemLinkBase l);
        }
    }
}

