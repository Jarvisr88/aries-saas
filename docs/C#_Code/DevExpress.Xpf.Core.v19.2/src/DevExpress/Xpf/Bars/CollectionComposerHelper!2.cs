namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;

    public class CollectionComposerHelper<TCollection, TItem> : IDisposable where TCollection: class, IList<TItem>, INotifyCollectionChanged where TItem: class, IAltitudeSupport
    {
        private IEnumerable<TCollection> sourceItems;
        private ObservableCollection<TItem> unorderedResultItems;
        private ObservableCollection<TItem> orderedResultItems;
        private ReadOnlyObservableCollection<TItem> readOnlyOrderedResultItems;
        private Dictionary<TItem, int> itemsAndOrder;

        public CollectionComposerHelper(IEnumerable<TCollection> sourceItems);
        private void Clear();
        public void Dispose();
        private void InsertItem(TItem element, int index);
        private void OnElementAltitudeChanged(object sender, ValueChangedEventArgs<int> e);
        private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        private void OnUnorderedItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        private void Populate();
        private void RemoveItem(TItem element, int index);

        public ReadOnlyObservableCollection<TItem> Items { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CollectionComposerHelper<TCollection, TItem>.<>c <>9;
            public static Func<TCollection, bool> <>9__7_0;
            public static Func<TCollection, int> <>9__7_1;
            public static Func<KeyValuePair<TItem, int>, int> <>9__14_0;

            static <>c();
            internal bool <.ctor>b__7_0(TCollection x);
            internal int <.ctor>b__7_1(TCollection x);
            internal int <OnUnorderedItemsCollectionChanged>b__14_0(KeyValuePair<TItem, int> x);
        }
    }
}

