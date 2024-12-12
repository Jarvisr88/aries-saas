namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class DesignTimeDataSource : IList, ICollection, IEnumerable, ITypedList, IListSource, IBindingList, ICollectionViewFactory
    {
        private readonly List<ExpandoObject> internalList;
        protected readonly Type dataObjectType;
        private readonly int rowCount;
        private PropertyDescriptorCollection properties;
        protected readonly bool useDistinctValues;
        protected readonly List<DesignTimePropertyInfo> Properties;

        public event ListChangedEventHandler ListChanged;

        public DesignTimeDataSource(IEnumerable<DesignTimePropertyInfo> columns, int rowCount, bool useDistinctValues = false);
        protected DesignTimeDataSource(Type dataObjectType, int rowCount, bool useDistinctValues);
        public DesignTimeDataSource(Type dataObjectType, int rowCount, bool useDistinctValues = false, object originalDataSource = null, IEnumerable<DesignTimePropertyInfo> defaultColumns = null, List<DesignTimePropertyInfo> properties = null);
        public int Add(object value);
        public void AddIndex(PropertyDescriptor property);
        public object AddNew();
        public void ApplySort(PropertyDescriptor property, ListSortDirection direction);
        public void Clear();
        public bool Contains(object value);
        public void CopyTo(Array array, int index);
        protected virtual DXGridDataController CreateDataController();
        private void CreateProperties(IEnumerable<DesignTimePropertyInfo> columns, int rowCount, bool useDistinctValues);
        protected virtual DesignTimeDataSource.DesignTimePropertyDescriptor CreatePropertyDescriptor(bool useDistinctValues, DesignTimePropertyInfo propertyInfo);
        public int Find(PropertyDescriptor property, object key);
        internal IEnumerable<DesignTimePropertyInfo> GetColumns(Type dataObjectType, object originalDataSource, IEnumerable<DesignTimePropertyInfo> defaultColumns);
        private object GetDataSource(Type dataObjectType, object originalDataSource);
        [IteratorStateMachine(typeof(DesignTimeDataSource.<GetDesignTimePropertyInfo>d__24))]
        private IEnumerable<DesignTimePropertyInfo> GetDesignTimePropertyInfo(DataColumnInfoCollection columns);
        [IteratorStateMachine(typeof(DesignTimeDataSource.<GetEnumerator>d__47))]
        public IEnumerator GetEnumerator();
        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors);
        public IList GetList();
        public string GetListName(PropertyDescriptor[] listAccessors);
        public int IndexOf(object value);
        protected void Initialize(Type dataObjectType, int rowCount, bool useDistinctValues, object originalDataSource, IEnumerable<DesignTimePropertyInfo> defaultColumns);
        public void Insert(int index, object value);
        protected virtual void PopulateColumns(DXGridDataController dataController);
        private void PopulateInternalList(int rowCount);
        private void RaiseListChanged();
        public void Remove(object value);
        public void RemoveAt(int index);
        public void RemoveIndex(PropertyDescriptor property);
        public void RemoveSort();
        ICollectionView ICollectionViewFactory.CreateView();

        internal static DateTime Today { get; }

        public bool AreRealColumnsAvailable { get; private set; }

        public object SyncRoot { get; }

        public bool IsSynchronized { get; }

        public bool IsFixedSize { get; }

        public bool IsReadOnly { get; }

        public int Count { get; }

        public object this[int index] { get; set; }

        public bool ContainsListCollection { get; }

        public bool AllowEdit { get; }

        public bool AllowNew { get; }

        public bool AllowRemove { get; }

        public bool IsSorted { get; }

        public ListSortDirection SortDirection { get; }

        public PropertyDescriptor SortProperty { get; }

        public bool SupportsChangeNotification { get; }

        public bool SupportsSearching { get; }

        public bool SupportsSorting { get; }

        [CompilerGenerated]
        private sealed class <GetDesignTimePropertyInfo>d__24 : IEnumerable<DesignTimePropertyInfo>, IEnumerable, IEnumerator<DesignTimePropertyInfo>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private DesignTimePropertyInfo <>2__current;
            private int <>l__initialThreadId;
            private DataColumnInfoCollection columns;
            public DataColumnInfoCollection <>3__columns;
            private IEnumerator <>7__wrap1;

            [DebuggerHidden]
            public <GetDesignTimePropertyInfo>d__24(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<DesignTimePropertyInfo> IEnumerable<DesignTimePropertyInfo>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            DesignTimePropertyInfo IEnumerator<DesignTimePropertyInfo>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

        [CompilerGenerated]
        private sealed class <GetEnumerator>d__47 : IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            public DesignTimeDataSource <>4__this;
            private List<ExpandoObject>.Enumerator <>7__wrap1;

            [DebuggerHidden]
            public <GetEnumerator>d__47(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            object IEnumerator<object>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

        protected class DesignTimePropertyDescriptor : PropertyDescriptor
        {
            private readonly object[] values;
            private readonly object[][] distinctValues;
            private readonly Type propertyType;
            private readonly bool useDistinctValues;
            private readonly bool isReadonly;

            public DesignTimePropertyDescriptor(string name, Type propertyType, bool useDistinctValues, bool isReadonly);
            public override bool CanResetValue(object component);
            protected virtual object[][] CreateDistinctValues();
            protected virtual object[] CreateValues();
            public override object GetValue(object component);
            public override void ResetValue(object component);
            public override void SetValue(object component, object value);
            public override bool ShouldSerializeValue(object component);

            public override Type ComponentType { get; }

            public override bool IsReadOnly { get; }

            public override Type PropertyType { get; }
        }

        private class DummyDataClient : IDataControllerData, IDataControllerData2
        {
            public ComplexColumnInfoCollection GetComplexColumns();
            public UnboundColumnInfoCollection GetUnboundColumns();
            public object GetUnboundData(int listSourceRow1, DataColumnInfo column, object value);
            public bool? IsRowFit(int listSourceRow, bool fit);
            public PropertyDescriptorCollection PatchPropertyDescriptorCollection(PropertyDescriptorCollection collection);
            public void SetUnboundData(int listSourceRow1, DataColumnInfo column, object value);
            public void SubstituteFilter(SubstituteFilterEventArgs args);

            public bool CanUseFastProperties { get; }

            public bool HasUserFilter { get; }
        }
    }
}

