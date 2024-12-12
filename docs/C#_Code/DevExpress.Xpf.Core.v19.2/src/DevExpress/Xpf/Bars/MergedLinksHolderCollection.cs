namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;

    public class MergedLinksHolderCollection : ObservableCollection<ILinksHolder>
    {
        private readonly ILinksHolder owner;
        private readonly BarItemLinkMergeHelper mergeHelper;
        private int previousCount;

        public event ValueChangedEventHandler<BarItemLinkCollection> ActualLinksChanged;

        public MergedLinksHolderCollection(ILinksHolder owner);
        protected override void ClearItems();
        protected override void InsertItem(int index, ILinksHolder item);
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e);
        private void RaiseActualLinksChanged();
        public void Remerge();

        public ILinksHolder Owner { get; }

        public BarItemLinkCollection MergedLinks { get; private set; }

        public BarItemLinkCollection ActualLinks { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MergedLinksHolderCollection.<>c <>9;
            public static Action<IMergingSupport> <>9__14_0;
            public static Action<IMergingSupport> <>9__14_1;

            static <>c();
            internal void <OnCollectionChanged>b__14_0(IMergingSupport x);
            internal void <OnCollectionChanged>b__14_1(IMergingSupport x);
        }
    }
}

