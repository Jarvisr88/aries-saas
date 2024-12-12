namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal class ComparativeDataSourceCore : IBindingList, IList, ICollection, IEnumerable, ITypedList
    {
        private ComparativeSource owner;
        private IBindingList dataSourceAdapter;
        private bool isDisposed;
        private ComparativePropertyDescriptorCollection propertyCollection;
        private string listName;
        private DevExpress.Data.ShowValues showValuesCore;
        private CalculatedColumnCollection calculatedFieldsCore;

        public event ListChangedEventHandler ListChanged;

        public ComparativeDataSourceCore(object dataSource, ComparativeSource owner);
        public int Add(object value);
        public void AddIndex(PropertyDescriptor property);
        public object AddNew();
        public void ApplySort(PropertyDescriptor property, ListSortDirection direction);
        public void Clear();
        public bool Contains(object value);
        public void CopyTo(Array array, int index);
        private void dataSourceAdapter_ListChanged(object sender, ListChangedEventArgs e);
        public int Find(PropertyDescriptor property, object key);
        public IEnumerator GetEnumerator();
        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors);
        public string GetListName(PropertyDescriptor[] listAccessors);
        public int IndexOf(object value);
        public void Insert(int index, object value);
        private void InvalidateDataSource();
        internal void RaiseDataSourceChanged();
        public void Remove(object value);
        public void RemoveAt(int index);
        public void RemoveIndex(PropertyDescriptor property);
        public void RemoveSort();
        private void SubscribeListChanged();
        private void UnSubscribeListChanged();
        private void UpdateInnerProperties();

        private IBindingList DataSourceAdapter { get; set; }

        public object DataSource { get; set; }

        internal PropertyDescriptorCollection BasePropertyDescriptors { get; }

        internal DevExpress.Data.ShowValues ShowValues { get; set; }

        internal CalculatedColumnCollection CalculatedColumns { get; }

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

