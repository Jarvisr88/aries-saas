namespace DevExpress.Data.Helpers
{
    using DevExpress.Data.Utils;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class BindingListAdapterBase : IBindingList, IList, ICollection, IEnumerable, IDisposable
    {
        private static readonly PropertyChangedEventArgs EmptyEventArgs;
        private readonly ItemPropertyNotificationMode itemPropertyNotificationMode;
        protected readonly IList source;
        private PropertyChangedWeakEventHandler<BindingListAdapterBase> propertyChangedHandler;
        private CollectionChangedWeakEventHandler<BindingListAdapterBase> collectionChangedHandler;
        private int lastChangedItemIndex;
        private PropertyDescriptorCollection itemProperties;

        public event ListChangedEventHandler ListChanged;

        static BindingListAdapterBase();
        public BindingListAdapterBase(IList source);
        public BindingListAdapterBase(IList source, ItemPropertyNotificationMode itemPropertyNotificationMode);
        protected BindingListAdapterBase(IList source, ItemPropertyNotificationMode itemPropertyNotificationMode, bool doSubsribe);
        public int Add(object value);
        public void AddIndex(PropertyDescriptor property);
        protected virtual void AddInternal(object obj);
        private void AddListener(INotifyPropertyChanged notifyPropertyChanged);
        public object AddNew();
        protected virtual void AddNewInternal();
        public void ApplySort(PropertyDescriptor property, ListSortDirection direction);
        public void Clear();
        protected virtual void ClearInternal();
        public bool Contains(object value);
        public void CopyTo(Array array, int index);
        public static BindingListAdapterBase CreateFromList(IList list);
        public static BindingListAdapterBase CreateFromList(IList list, ItemPropertyNotificationMode itemPropertyNotificationMode);
        public int Find(PropertyDescriptor property, object key);
        public IEnumerator GetEnumerator();
        private Type GetRowType();
        public int IndexOf(object value);
        public void Insert(int index, object value);
        protected virtual void InsertInternal(int index, object obj);
        private bool IsChangedItem(object item, int offset);
        protected virtual bool IsItemLoaded(int index);
        protected virtual void NotifyChanged(ListChangedEventArgs e);
        protected virtual void NotifyOnItemRemove(int oldIndex, int newIndex, object item);
        protected virtual void NotifyOnItemReplace(int startingIndex, object oldItem);
        protected virtual void OnIndexAccessed(int index);
        protected virtual void OnObjectPropertyChanged(object sender, PropertyChangedEventArgs e);
        protected virtual void OnObjectPropertyChangedCore(object sender, PropertyChangedEventArgs e);
        protected virtual void OnSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        protected void RaiseChangedIfNeeded(object sender, string propertyName, Action<int, PropertyDescriptor> raiseEvent, Action unsubscribe);
        public void Remove(object value);
        public void RemoveAt(int index);
        protected virtual void RemoveAtInternal(int index);
        public void RemoveIndex(PropertyDescriptor property);
        protected virtual void RemoveInternal(object obj);
        private void RemoveListener(INotifyPropertyChanged notifyPropertyChanged);
        public void RemoveSort();
        protected virtual void RemovingAtInternal(int index);
        protected void SubscribeAll(IList source);
        private void SubscribeAllItemsPropertyChangedEvent();
        protected virtual void SubscribeItemPropertyChangedEvent(object item);
        private void SubscribeItemsPropertyChangedEvent(int startIndex, Func<int> getCount);
        private void SubscribeNewItemsPropertyChangedEvent(NotifyCollectionChangedEventArgs e, int startingIndex);
        void IDisposable.Dispose();
        protected virtual void UnsubscribeItemPropertyChangedEvent(object item);
        private void UnsubscribeItemsPropertyChangedEvent(IList oldItems, bool needCheckItemLoading);

        private bool ShouldSubscribePropertyChanged { get; }

        protected virtual bool ShouldSubscribePropertiesChanged { get; }

        private INotifyCollectionChanged NotifyCollectionChanged { get; }

        private PropertyChangedWeakEventHandler<BindingListAdapterBase> PropertyChangedHandler { get; }

        private CollectionChangedWeakEventHandler<BindingListAdapterBase> CollectionChangedHandler { get; }

        public bool RaisesItemChangedEvents { get; set; }

        public object OriginalDataSource { get; set; }

        public bool SupportsSearching { get; }

        public Func<object> AddingNew { get; set; }

        public Func<bool> CanAddingNew { get; set; }

        public virtual bool AllowNew { get; }

        public bool SupportsSorting { get; }

        public bool IsSorted { get; }

        public ListSortDirection SortDirection { get; }

        public PropertyDescriptor SortProperty { get; }

        public virtual bool AllowRemove { get; }

        public virtual bool AllowEdit { get; }

        public virtual bool SupportsChangeNotification { get; }

        public bool IsFixedSize { get; }

        public bool IsReadOnly { get; }

        public object this[int index] { get; set; }

        public int Count { get; }

        public bool IsSynchronized { get; }

        public object SyncRoot { get; }

        protected bool CanRaiseListChanged { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BindingListAdapterBase.<>c <>9;
            public static Action<BindingListAdapterBase, object, PropertyChangedEventArgs> <>9__13_0;
            public static Action<BindingListAdapterBase, object, NotifyCollectionChangedEventArgs> <>9__16_0;

            static <>c();
            internal void <get_CollectionChangedHandler>b__16_0(BindingListAdapterBase owner, object o, NotifyCollectionChangedEventArgs e);
            internal void <get_PropertyChangedHandler>b__13_0(BindingListAdapterBase owner, object o, PropertyChangedEventArgs e);
        }
    }
}

