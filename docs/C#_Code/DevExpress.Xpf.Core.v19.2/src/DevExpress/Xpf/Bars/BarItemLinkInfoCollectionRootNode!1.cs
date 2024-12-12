namespace DevExpress.Xpf.Bars
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;

    public class BarItemLinkInfoCollectionRootNode<TElement> : BarItemLinkInfoCollectionExpandableNode<TElement>, INotifyLockableChanged, ILockable where TElement: class, IBarItemLinkInfo
    {
        private Locker updateLocker;
        private List<Tuple<int, TElement, NotifyCollectionChangedAction>> changes;
        private readonly bool expandInplaceLinkHolders;
        [CompilerGenerated]
        private NotifyCollectionChangedEventHandler ItemsChanged;
        private int lockCount;
        [CompilerGenerated]
        private EventHandler onBeginUpdate;
        [CompilerGenerated]
        private EventHandler onEndUpdate;

        event EventHandler INotifyLockableChanged.OnBeginUpdate;

        event EventHandler INotifyLockableChanged.OnEndUpdate;

        public event NotifyCollectionChangedEventHandler ItemsChanged;

        private event EventHandler onBeginUpdate;

        private event EventHandler onEndUpdate;

        public BarItemLinkInfoCollectionRootNode(bool expandInplaceLinkHolders, IList<BarItemLinkBase> source, BaseBarItemLinkInfoFactory<TElement> factory, bool allowRecycling);
        private void CreateChange(BarItemLinkInfoCollectionNode<TElement> node, NotifyCollectionChangedAction action);
        void ILockable.BeginUpdate();
        void ILockable.EndUpdate();
        public int GetIndex(TElement barItemLinkInfo);
        public IDisposable Lock();
        public void OnElementAdded(BarItemLinkInfoCollectionNode<TElement> node, BarItemLinkBase element);
        public void OnElementRemoved(BarItemLinkInfoCollectionNode<TElement> node, BarItemLinkBase element);
        private void OnUnlocked(object sender, EventArgs e);
        public void Unlock();

        private Locker UpdateLocker { get; }

        private List<Tuple<int, TElement, NotifyCollectionChangedAction>> Changes { get; }

        public bool ExpandInplaceLinkHolders { get; }

        bool ILockable.IsLockUpdate { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarItemLinkInfoCollectionRootNode<TElement>.<>c <>9;
            public static Func<Tuple<int, TElement, NotifyCollectionChangedAction>, bool> <>9__15_0;
            public static Func<Tuple<int, TElement, NotifyCollectionChangedAction>, bool> <>9__15_1;

            static <>c();
            internal bool <OnUnlocked>b__15_0(Tuple<int, TElement, NotifyCollectionChangedAction> x);
            internal bool <OnUnlocked>b__15_1(Tuple<int, TElement, NotifyCollectionChangedAction> x);
        }
    }
}

