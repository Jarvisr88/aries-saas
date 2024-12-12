namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class RealTimeSourceCore : IRealTimeListChangeProcessor, IBindingList, IList, ICollection, IEnumerable, ITypedList, IDisposable
    {
        private bool isDisposed;
        private readonly SynchronizationContext synchronizationContext;
        private RealTimeQueue _Worker;
        private List<RealTimeProxyForObject> cache;
        private string listName;
        private RealTimePropertyDescriptorCollection propertyCollection;
        private IBindingList dataSourceAdapter;
        private string displayableProperties;
        private DateTime? lastProcessedCommandCreationTime;
        private readonly object syncObject;
        private readonly ItemPropertyNotificationMode notificationMode;
        private bool waitReset;
        private bool isCatchUp;

        public event ListChangedEventHandler ListChanged;

        public RealTimeSourceCore(object source, SynchronizationContext context, string displayableProperties, bool ignoreItemEvents, bool useWeakEventHandler);
        public int Add(object value);
        public void AddIndex(PropertyDescriptor property);
        public object AddNew();
        public void ApplySort(PropertyDescriptor property, ListSortDirection direction);
        internal void CatchUp();
        public void Clear();
        public bool Contains(object value);
        public void CopyTo(Array array, int index);
        private void DataSourceChanged();
        void IRealTimeListChangeProcessor.NotifyLastProcessedCommandCreationTime(DateTime sent);
        public void Dispose();
        protected virtual void Dispose(bool disposing);
        protected override void Finalize();
        public int Find(PropertyDescriptor property, object key);
        public IEnumerator GetEnumerator();
        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors);
        public string GetListName(PropertyDescriptor[] listAccessors);
        internal TimeSpan GetQueueDelay();
        public int IndexOf(object value);
        public void Insert(int index, object value);
        private void InvalidateDataSource();
        public void Remove(object value);
        public void RemoveAt(int index);
        public void RemoveIndex(PropertyDescriptor property);
        public void RemoveSort();
        private void SourceListChanged(object sender, RealTimeEventBase command);

        private object SyncObject { get; }

        public string DisplayableProperties { get; set; }

        public object DataSource { get; set; }

        public bool UseWeakEventHandler { get; set; }

        private RealTimeQueue Worker { get; }

        List<RealTimeProxyForObject> IRealTimeListChangeProcessor.Cache { get; set; }

        ListChangedEventHandler IRealTimeListChangeProcessor.ListChanged { get; }

        RealTimePropertyDescriptorCollection IRealTimeListChangeProcessor.PropertyDescriptorsCollection { get; set; }

        bool IRealTimeListChangeProcessor.IsCatchUp { get; set; }

        public bool AllowEdit { get; }

        public bool AllowNew { get; }

        public bool AllowRemove { get; }

        public bool IsSorted { get; }

        public ListSortDirection SortDirection { get; }

        public PropertyDescriptor SortProperty { get; }

        public bool SupportsChangeNotification { get; }

        public bool SupportsSearching { get; }

        public bool SupportsSorting { get; }

        public bool IsFixedSize { get; }

        public bool IsReadOnly { get; }

        public object this[int index] { get; set; }

        public int Count { get; }

        public bool IsSynchronized { get; }

        public object SyncRoot { get; }
    }
}

