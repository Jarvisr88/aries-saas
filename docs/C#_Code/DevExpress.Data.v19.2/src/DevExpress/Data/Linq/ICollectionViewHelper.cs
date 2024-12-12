namespace DevExpress.Data.Linq
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Data;

    public class ICollectionViewHelper : IListServer, IList, ICollection, IEnumerable, IDisposable, IDataSync, IListServerCaps, ISupportEditableCollectionView, IEditableCollectionView, IListWrapper, IWeakEventListener, IItemProperties
    {
        private object newItemPlaceHolder;
        private ICollectionView collection;
        private Type elementType;
        private CriteriaOperator FilterCriteria;
        private Func<object, bool> fitPredicate;
        private CollectionViewListSourceGroupInfo rootGroup;
        private IList<ServerModeSummaryDescriptor> GroupSummaryInfo;
        private Func<IEnumerable, object>[] GroupSummaryEvaluators;
        private IList<ServerModeSummaryDescriptor> TotalSummaryInfo;
        private Predicate<object> lastFilter;
        private List<GroupDescription> lastGroupDescriptions;
        private List<SortDescription> lastSortDescriptions;
        private bool isInitialized;
        private bool isFilterSortGroupLocked;
        private CollectionViewFilterSortGroupInfoChangedEventHandler filterSortGroupInfoChanged;

        public event EventHandler<ServerModeExceptionThrownEventArgs> ExceptionThrown;

        public event CollectionViewFilterSortGroupInfoChangedEventHandler FilterSortGroupInfoChanged;

        public event EventHandler<ServerModeInconsistencyDetectedEventArgs> InconsistencyDetected;

        public ICollectionViewHelper(ICollectionView collection, object newItemPlaceHolder);
        public ICollectionViewHelper(Type elementType, ICollectionView collection, object newItemPlaceHolder);
        public int Add(object value);
        private static bool alwaysFalse(object dummy);
        public virtual void Apply(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo_new, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo);
        public void Clear();
        public bool Contains(object value);
        public void CopyTo(Array array, int index);
        private CollectionViewListSourceGroupInfo CreateListSourceGroupInfo(CollectionViewGroup group, int level);
        internal ServerModeOrderDescriptor CreateServerModeOrderDescriptor(SortDescription sd);
        internal List<ListSortInfo> CreateSortInfo();
        private CollectionViewListSourceGroupInfo CreateTopGroup();
        bool IDataSync.ResetCache();
        public void Dispose();
        private void FilterCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        private bool filterPredicate(object obj);
        private PropertyGroupDescription FindGroupDescriptionByName(string name);
        public int FindIncremental(CriteriaOperator expression, string value, int startIndex, bool searchUp, bool ignoreStartRow, bool allowLoop);
        private SortDescription? FindSortDescriptionByName(string name);
        public IList GetAllFilteredAndSortedRows();
        public IEnumerator GetEnumerator();
        public object GetGridSource();
        public object GetGridSourceAsync();
        public List<ListSourceGroupInfo> GetGroupInfo(ListSourceGroupInfo parentGroup);
        private string GetOperandPropertyName(ServerModeOrderDescriptor od);
        public int GetRowIndexByKey(object key);
        public object GetRowKey(int index);
        public List<object> GetTotalSummary();
        private ICollectionView GetUnderlyingView();
        public object[] GetUniqueColumnValues(CriteriaOperator valuesExpression, int maxCount, CriteriaOperator filterExpression, bool ignoreAppliedFilter);
        public int IndexOf(object value);
        public void Initialize();
        public void Insert(int index, object value);
        public int LocateByExpression(CriteriaOperator expression, int startIndex, bool searchUp);
        public int LocateByValue(CriteriaOperator expression, object value, int startIndex, bool searchUp);
        private Func<IEnumerable, object>[] MakeSummaryEvaluators(IEnumerable<ServerModeSummaryDescriptor> descriptors);
        private bool NeedSortGroupRefresh(ICollection<ServerModeOrderDescriptor> sortInfo, int groupCount);
        public virtual bool PrefetchRows(ListSourceGroupInfo[] groupsToPrefetch, CancellationToken cancellationToken);
        private void RaiseFilterSortGroupInfoChanged(bool needRefresh);
        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e);
        public void Refresh();
        public void Remove(object value);
        public void RemoveAt(int index);
        private void Reset();
        private void ResetCaches();
        private void SyncLastCollections();
        object IEditableCollectionView.AddNew();
        void IEditableCollectionView.CancelEdit();
        void IEditableCollectionView.CancelNew();
        void IEditableCollectionView.CommitEdit();
        void IEditableCollectionView.CommitNew();
        void IEditableCollectionView.EditItem(object item);
        void IEditableCollectionView.Remove(object item);
        void IEditableCollectionView.RemoveAt(int index);

        protected Type ElementType { get; set; }

        public object NewItemPlaceHolder { get; set; }

        protected IList List { get; }

        protected INotifyCollectionChanged CollectionSortDescriptions { get; }

        protected INotifyCollectionChanged CollectionGroupDescriptions { get; }

        public ICollectionView Collection { get; }

        private CollectionViewListSourceGroupInfo RootGroup { get; }

        public bool IsFixedSize { get; }

        public bool IsReadOnly { get; }

        public object this[int index] { get; set; }

        public int Count { get; }

        public bool IsSynchronized { get; }

        public object SyncRoot { get; }

        int IDataSync.GroupCount { get; }

        bool IDataSync.HasFilter { get; }

        List<ListSortInfo> IDataSync.Sort { get; }

        public bool AllowSyncSortingAndGrouping { get; set; }

        bool IListServerCaps.CanFilter { get; }

        bool IListServerCaps.CanGroup { get; }

        bool IListServerCaps.CanSort { get; }

        public bool IsSupportEditableCollectionView { get; }

        private IEditableCollectionView EditableView { get; }

        bool IEditableCollectionView.CanAddNew { get; }

        bool IEditableCollectionView.CanCancelEdit { get; }

        bool IEditableCollectionView.CanRemove { get; }

        object IEditableCollectionView.CurrentAddItem { get; }

        object IEditableCollectionView.CurrentEditItem { get; }

        bool IEditableCollectionView.IsAddingNew { get; }

        bool IEditableCollectionView.IsEditingItem { get; }

        NewItemPlaceholderPosition IEditableCollectionView.NewItemPlaceholderPosition { get; set; }

        Type IListWrapper.WrappedListType { get; }

        public ReadOnlyCollection<ItemPropertyInfo> ItemProperties { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ICollectionViewHelper.<>c <>9;
            public static Func<ServerModeOrderDescriptor[], bool> <>9__42_0;
            public static Func<ServerModeOrderDescriptor[], ServerModeOrderDescriptor> <>9__42_1;
            public static Func<IEnumerable, object> <>9__59_1;
            public static Func<object, object> <>9__63_0;
            public static Func<object, bool> <>9__95_0;
            public static Func<object, bool> <>9__95_1;

            static <>c();
            internal bool <Apply>b__42_0(ServerModeOrderDescriptor[] sis);
            internal ServerModeOrderDescriptor <Apply>b__42_1(ServerModeOrderDescriptor[] sis);
            internal bool <filterPredicate>b__95_0(object x);
            internal bool <filterPredicate>b__95_1(object x);
            internal object <GetUniqueColumnValues>b__63_0(object x);
            internal object <MakeSummaryEvaluators>b__59_1(IEnumerable rows);
        }
    }
}

