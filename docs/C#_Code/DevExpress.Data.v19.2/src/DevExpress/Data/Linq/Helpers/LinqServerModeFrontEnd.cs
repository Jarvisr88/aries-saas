namespace DevExpress.Data.Linq.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class LinqServerModeFrontEnd : IListServer, IList, ICollection, IEnumerable, IListServerHints, IBindingList, ITypedList, IDXCloneable
    {
        public readonly ILinqServerModeFrontEndOwner Owner;
        private IListServer _Wrapper;
        private Type _Type;
        private string _KeyExpression;
        private IQueryable _DataSource;
        private bool _IsReadyForTakeOff;
        private string _DefaultSorting;
        private CriteriaOperator _Successful_FilterCriteria;
        private ICollection<ServerModeOrderDescriptor[]> _Successful_sortInfo;
        private int _Successful_groupCount;
        private ICollection<ServerModeSummaryDescriptor> _Successful_summaryInfo;
        private ICollection<ServerModeSummaryDescriptor> _Successful_totalSummaryInfo;

        private event ListChangedEventHandler _ListChanged;

        public event EventHandler<ServerModeExceptionThrownEventArgs> ExceptionThrown;

        public event EventHandler<ServerModeInconsistencyDetectedEventArgs> InconsistencyDetected;

        public event ListChangedEventHandler ListChanged;

        public LinqServerModeFrontEnd(ILinqServerModeFrontEndOwner owner);
        private void _Wrapper_ExceptionThrown(object sender, ServerModeExceptionThrownEventArgs e);
        private void _Wrapper_InconsistencyDetected(object sender, ServerModeInconsistencyDetectedEventArgs e);
        public int Add(object value);
        public void Apply(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> summaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo);
        public void CatchUp();
        public void Clear();
        public bool Contains(object value);
        public void CopyTo(Array array, int index);
        protected virtual LinqServerModeFrontEnd CreateDXClone();
        protected virtual IListServer CreateRuntimeWrapper();
        private IListServer CreateWrapper();
        object IDXCloneable.DXClone();
        void IListServerHints.HintGridIsPaged(int pageSize);
        void IListServerHints.HintMaxVisibleRowsInGrid(int rowsInGrid);
        protected virtual LinqServerModeFrontEnd DXClone();
        public int FindIncremental(CriteriaOperator expression, string value, int startIndex, bool searchUp, bool ignoreStartRow, bool allowLoop);
        public virtual IList GetAllFilteredAndSortedRows();
        public IEnumerator GetEnumerator();
        public List<ListSourceGroupInfo> GetGroupInfo(ListSourceGroupInfo parentGroup);
        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors);
        public string GetListName(PropertyDescriptor[] listAccessors);
        public int GetRowIndexByKey(object key);
        public object GetRowKey(int index);
        public List<object> GetTotalSummary();
        public object[] GetUniqueColumnValues(CriteriaOperator valuesExpression, int maxCount, CriteriaOperator filterExpression, bool ignoreAppliedFilter);
        public int IndexOf(object value);
        public void Insert(int index, object value);
        protected void KillWrapper();
        public int LocateByExpression(CriteriaOperator expression, int startIndex, bool searchUp);
        public int LocateByValue(CriteriaOperator expression, object value, int startIndex, bool searchUp);
        protected virtual void OnListChanged(ListChangedEventArgs e);
        public virtual bool PrefetchRows(ListSourceGroupInfo[] groupsToPrefetch, CancellationToken cancellationToken);
        public void Refresh();
        public void Remove(object value);
        public void RemoveAt(int index);
        void IBindingList.AddIndex(PropertyDescriptor property);
        object IBindingList.AddNew();
        void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction);
        int IBindingList.Find(PropertyDescriptor property, object key);
        void IBindingList.RemoveIndex(PropertyDescriptor property);
        void IBindingList.RemoveSort();

        protected IListServer Wrapper { get; }

        bool IBindingList.AllowEdit { get; }

        bool IBindingList.AllowNew { get; }

        bool IBindingList.AllowRemove { get; }

        bool IBindingList.IsSorted { get; }

        ListSortDirection IBindingList.SortDirection { get; }

        PropertyDescriptor IBindingList.SortProperty { get; }

        bool IBindingList.SupportsChangeNotification { get; }

        bool IBindingList.SupportsSearching { get; }

        bool IBindingList.SupportsSorting { get; }

        public bool IsFixedSize { get; }

        public bool IsReadOnly { get; }

        public object this[int index] { get; set; }

        public int Count { get; }

        public bool IsSynchronized { get; }

        public object SyncRoot { get; }
    }
}

