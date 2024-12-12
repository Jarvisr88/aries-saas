namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;

    public class BarItemLinkInfoCollectionGeneric<TElement> : ObservableCollection<TElement>, IBarItemLinkInfoCollection, IList, ICollection, IEnumerable where TElement: class, IBarItemLinkInfo
    {
        private readonly IList<BarItemLinkBase>[] sourceCollection;
        private readonly Locker hasVisibleInfosLocker;
        private readonly Dictionary<BarItemLinkInfoCollectionRootNode<TElement>, Tuple<IList<BarItemLinkBase>, ObservableCollection<TElement>>> rootNodes;
        private bool haveVisibleInfos;
        [CompilerGenerated]
        private EventHandler HaveVisibleInfosChanged;
        private CollectionComposerHelper<ObservableCollection<TElement>, TElement> composerHelper;
        private readonly BaseBarItemLinkInfoFactory<TElement> factory;

        public event EventHandler HaveVisibleInfosChanged;

        public BarItemLinkInfoCollectionGeneric(bool expandInplaceLinkHolders, IList<BarItemLinkBase>[] sourceCollection, BaseBarItemLinkInfoFactory<TElement> factory, bool allowRecycling);
        public void Destroy();
        protected virtual void HasVisibleInfosLockerUnlocked(object sender, EventArgs e);
        private void OnCompositionHelperItemsChanged(object sender, NotifyCollectionChangedEventArgs e);
        protected virtual void OnElementActualIsVisibleChanged(object sender, ValueChangedEventArgs<bool> e);
        protected virtual void OnHaveVisibleInfosChanged(bool oldValue);
        private void OnSourceBeginUpdate(object sender, EventArgs e);
        private void OnSourceEndUpdate(object sender, EventArgs e);
        private void OnSourceItemsChanged(object sender, NotifyCollectionChangedEventArgs e);
        private void ProcessItemsChanged(NotifyCollectionChangedEventArgs e, BarItemLinkInfoCollectionRootNode<TElement> node, ObservableCollection<TElement> collection);
        private void SubscribeElement(TElement element);
        private void UnsubscribeElement(TElement element);

        protected BaseBarItemLinkInfoFactory<TElement> Factory { get; }

        public bool HaveVisibleInfos { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarItemLinkInfoCollectionGeneric<TElement>.<>c <>9;
            public static Func<object, object> <>9__17_0;

            static <>c();
            internal object <OnCompositionHelperItemsChanged>b__17_0(object x);
        }
    }
}

