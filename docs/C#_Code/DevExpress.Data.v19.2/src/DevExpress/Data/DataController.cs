namespace DevExpress.Data
{
    using DevExpress.Data.Details;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.Selection;
    using DevExpress.Utils;
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class DataController : DataControllerBase
    {
        private Dictionary<int, bool> detailEmptyHash;
        private DevExpress.Data.Helpers.FilterHelper filterHelper;
        private bool allowRestoreSelection;
        private bool notifyClientOnNextChange;
        private string lastErrorText;
        private IClassicRowKeeper rowsKeeper;
        private SelectionController selection;
        private IDataControllerVisualClient visualClient;
        private IDataControllerRelationSupport relationSupport;
        private DataColumnSortInfoCollection sortInfo;
        private VisibleListSourceRowCollection visibleListSourceRows;
        private VisibleListSourceRowCollection visibleListSourceRowsFilterCache;
        private VisibleIndexCollection visibleIndexes;
        private GroupRowInfoCollection groupInfo;
        private SummaryItemCollection groupSummary;
        private TotalSummaryItemCollection totalSummary;
        private MasterRowInfoCollection expandedMasterRowCollection;
        private SummarySortInfoCollection summarySortInfo;
        private CriteriaOperator filterCriteria;
        private bool autoUpdateTotalSummary;
        private bool immediateUpdateRowPosition;
        private DataController.FilterRowStub _FilterStub;
        public const int InvalidRow = -2147483648;
        public const int OperationInProgress = -2147483638;
        private CustomSummaryEventArgs summaryCalculateArgs;
        private bool isEndUpdate;
        private bool collapsingRows;
        private DataController.IFindRowByValueCache findRowByValueCache;
        private bool dataSyncInProgress;
        private int refreshInProgress;
        private int prevSelectionCount;
        private bool prevGrouped;
        private int sortGroupUpdate;
        [Browsable(false)]
        public bool ByPassFilter;
        private SubstituteSortInfoEventArgs sortInfoInterceptor;
        private string[] lastGroupDescriptors;

        public event CustomSummaryEventHandler CustomSummary;

        public event CustomSummaryExistEventHandler CustomSummaryExists;

        public event EventHandler Refreshed;

        public event RowDeletedEventHandler RowDeleted;

        public event RowDeletingEventHandler RowDeleting;

        public event SelectionChangedEventHandler SelectionChanged;

        public DataController();
        private void AddControllerRowToDeletedHashtable(Dictionary<int, int> list, int controllerRow);
        private void AddPropertyDescriptorToDictionary(Dictionary<string, PropertyDescriptor> dictionary, PropertyDescriptor pd);
        protected virtual void BeginInvoke(Delegate method);
        public void BeginSortUpdate();
        public override void BeginUpdate();
        protected virtual void BuildVisibleIndexes();
        public virtual CriteriaOperator CalcColumnFilterCriteriaByValue(string fieldName, object columnValue, bool equal, bool roundDateTime, IFormatProvider provider);
        protected virtual void CalcGroupSummary();
        protected virtual void CalcGroupSummaryItem(SummaryItem summary);
        protected virtual void CalcGroupSummaryItem(SummaryItem summary, GroupRowInfo groupRow);
        protected virtual void CalcListBasedSummary(SummaryItem summaryItem, CustomSummaryEventArgs e);
        public void CalcSummary();
        protected virtual object CalcSummaryCountValue(GroupRowInfo groupRow, SummaryItem summaryItem);
        protected virtual object CalcSummaryInfo(GroupRowInfo groupRow, SummaryItem summaryItem, ref bool validResult);
        protected virtual object CalcSummaryValue(SummaryItem summaryItem, GroupRowInfo groupRow);
        protected virtual object CalcSummaryValue(SummaryItem summaryItem, SummaryItemType summaryType, bool ignoreNullValues, Type valueType, IEnumerable valuesEnumerable, Func<string[]> exceptionAuxInfoGetter, GroupRowInfo groupRow);
        protected virtual void CalcTotalSummary();
        protected virtual void CalcTotalSummaryItem(SummaryItem summary);
        public virtual void CancelFindIncremental();
        public virtual void CancelWeakFindIncremental();
        protected virtual bool CanFilterAddedRow(int listSourceRow);
        public virtual bool CanFindColumn(DataColumnInfo column);
        protected virtual bool CanFindUnboundColumn(DataColumnInfo column);
        public bool CanSortColumn(DataColumnInfo column);
        public bool CanSortColumn(int column);
        public bool CanSortColumn(string fieldName);
        protected virtual bool CanSortColumnCore(DataColumnInfo column);
        protected virtual void ChangeAllExpanded(bool expanded);
        protected virtual void ChangeExpanded(int groupRowHandle, bool expanded, bool recursive);
        protected virtual void ChangeExpandedLevel(int groupLevel, bool expanded, bool recursive);
        public void ChangeGroupSorting(int groupLevel);
        protected internal virtual bool CheckImmediateUpdateRowPosition(ListChangedEventArgs e);
        protected virtual void CheckRaiseVisibleCountChanged(int prevVCount);
        protected void CheckUpdateTotalSummary();
        protected override void ClearClients();
        protected virtual void ClearFilterCache();
        public virtual void ClearInvalidRowsCache();
        protected virtual void ClearVisibleInfoOnRefresh();
        public CriteriaOperator ClientUserSubstituteFilter(CriteriaOperator criterion);
        public void CollapseAll();
        public void CollapseDetail(DetailInfo detail);
        public void CollapseDetailRow(int controllerRow);
        public void CollapseDetailRow(int controllerRow, int relationIndex);
        public void CollapseDetailRowByKey(object rowKey);
        public void CollapseDetailRowByOwner(object detailOwner);
        public override void CollapseDetailRows();
        public void CollapseLevel(int groupLevel, bool recursive);
        public void CollapseRow(int groupRowHandle);
        public void CollapseRow(int groupRowHandle, bool recursive);
        private bool CompareValueDefault(int controllerRow, object val, string text);
        public IClassicRowKeeper CreateControllerRowsKeeper();
        public virtual IClassicRowKeeper CreateControllerRowsKeeperClassic();
        protected virtual IClassicRowKeeper CreateControllerRowsKeeperCore();
        protected virtual CustomSummaryEventArgs CreateCustomSummaryEventArgs();
        protected virtual CustomSummaryExistEventArgs CreateCustomSummaryExistEventArgs(GroupRowInfo groupRow, object item);
        protected virtual DevExpress.Data.Helpers.FilterHelper CreateFilterHelper();
        protected virtual DataController.FilterRowStub CreateFilterRowStub(out Exception exception);
        protected virtual GroupRowInfoCollection CreateGroupRowInfoCollection();
        protected virtual MasterRowInfoCollection CreateMasterRowCollection();
        protected virtual SelectionController CreateSelectionController();
        protected virtual VisibleIndexCollection CreateVisibleIndexCollection();
        public virtual void DeleteRow(int controllerRow);
        public virtual void DeleteRows(int[] controllerRows);
        public virtual void DeleteSelectedRows();
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void DestroyFindRowByValueCache();
        public override void Dispose();
        protected virtual void DoFilterRows();
        protected void DoGroupRows();
        protected virtual void DoGroupRowsCore();
        protected override void DoRefresh(bool useRowsKeeper);
        protected void DoRefreshCore(bool useRowsKeeper);
        protected virtual void DoRefreshDataOperations();
        public virtual void DoSortGroupRefresh();
        protected virtual void DoSortRows();
        protected virtual void DoSortSummary();
        protected virtual void DoSortSummary(GroupRowInfoCollection groups, GroupRowInfo parentGroup);
        public void EndSortUpdate();
        protected override void EndUpdateCore(bool sortUpdate);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void EnsureFindRowByValueCache(DataColumnInfo column, int capacity);
        public virtual void EnsureRowLoaded(int controllerRow, OperationCompleted completed);
        public void ExpandAll();
        public DetailInfo ExpandDetailRow(int controllerRow, int relationIndex);
        public void ExpandLevel(int groupLevel, bool recursive);
        public void ExpandRow(int groupRowHandle);
        public void ExpandRow(int groupRowHandle, bool recursive);
        protected virtual bool ExtendedSummaryEquals(SummaryItem groupSummary, SummaryItem totalSummary);
        [IteratorStateMachine(typeof(DataController.<FilterRows>d__71))]
        internal IEnumerable<int> FilterRows(CriteriaOperator filter, IEnumerable<int> rowsToFit);
        private int FindControllerRowForInsert(int listSourceRow, int? oldControllerRow = new int?());
        public virtual int FindIncremental(string text, int columnHandle, int startRowHandle, bool down, bool ignoreStartRow, bool allowLoop, CompareIncrementalValue compareValue, params OperationCompleted[] completed);
        [IteratorStateMachine(typeof(DataController.<FindMatchingGroupSummaries>d__450))]
        private IEnumerable<SummaryItem> FindMatchingGroupSummaries(SummaryItem summaryItem);
        public virtual int FindRowByBeginWith(string columnName, string text);
        public virtual int FindRowByRowValue(object value, int tryListSourceIndex = -1);
        public virtual int FindRowByValue(string columnName, object value, params OperationCompleted[] completed);
        private int FindRowByValueCore(object value, DataColumnInfo column, int startIndex, int count);
        public virtual int FindRowByValues(Dictionary<DataColumnInfo, object> values);
        public int FindRowByValues(Dictionary<string, object> columnValues);
        public MasterRowInfo FindRowDetailInfo(object rowKey);
        public IList GetAllFilteredAndSortedRows();
        public virtual IList GetAllFilteredAndSortedRows(Function<bool> callbackMethod);
        private int GetColumnByGroupLevel(GroupRowInfo group, int column);
        public override int GetControllerRow(int listSourceRow);
        public int GetControllerRowByGroupRow(int controllerRow);
        public int GetControllerRowHandle(int visibleIndex);
        public int GetControllerRowHandle(int visibleIndex, int visibleCount);
        public IList GetDetailList(int controllerRow, int relationIndex);
        public virtual ErrorInfo GetErrorInfo(int controllerRow);
        public virtual ErrorInfo GetErrorInfo(int controllerRow, int column);
        public virtual string GetErrorText(int controllerRow);
        public string GetErrorText(int controllerRow, DataColumnInfo column);
        public virtual string GetErrorText(int controllerRow, int column);
        public string GetErrorText(int controllerRow, string columnName);
        public ErrorType GetErrorType(int controllerRow);
        public virtual ErrorType GetErrorType(int controllerRow, int column);
        private DataColumnSortInfo[] GetExpandedAndInterceptedSortInfo();
        protected internal override PropertyDescriptorCollection GetFilterDescriptorCollection();
        protected internal string[] GetGroupDescriptors();
        protected internal override object GetGroupRowKeyValueInternal(GroupRowInfo group);
        public virtual object GetGroupRowValue(GroupRowInfo group);
        public object GetGroupRowValue(int controllerRow);
        protected virtual object GetGroupRowValue(GroupRowInfo group, int column);
        public object GetGroupRowValue(int controllerRow, DataColumnInfo column);
        public virtual object[] GetGroupRowValues(GroupRowInfo group);
        public Hashtable GetGroupSummary(int groupRowHandle);
        protected virtual Hashtable GetGroupSummaryCore(GroupRowInfo group);
        internal DataColumnSortInfo[] GetInterceptSortInfo();
        public object GetListSourceRow(int controllerRow);
        public virtual int GetListSourceRowIndex(int controllerRow);
        private int[] GetListSourceRows(int[] controllerRows);
        public object GetListSourceRowValue(int listSourceRowIndex, DataColumnInfo column);
        public object GetListSourceRowValue(int listSourceRowIndex, int column);
        public object GetListSourceRowValue(int listSourceRowIndex, string columnName);
        private object GetListSourceRowValueDetail(int listSourceRowIndex, DataColumnInfo column);
        public int GetNextSibling(int controllerRow);
        public GroupRowInfo GetParentGroupRow(int controllerRow);
        public int GetParentRowHandle(int controllerRow);
        public int GetPrevSibling(int controllerRow);
        public int GetRelationCount(int controllerRow);
        public string GetRelationDisplayName(int controllerRow, int relationIndex);
        public int GetRelationIndex(int controllerRow, string relationName);
        public string GetRelationName(int controllerRow, int relationIndex);
        public object GetRow(int controllerRow);
        public virtual object GetRow(int controllerRow, OperationCompleted completed);
        public object GetRowByListSourceIndex(int listSourceRow);
        public MasterRowInfo GetRowDetailInfo(int controllerRow);
        public string GetRowDisplayText(int controllerRow, int column);
        protected virtual IDXDataErrorInfo GetRowDXErrorInfo(int controllerRow);
        protected virtual IDataErrorInfo GetRowErrorInfo(int controllerRow);
        protected virtual IEnumerable<int> GetRowHandlesForSummary(int controllerRow, int rowCount, SummaryItem summaryItem, GroupRowInfo groupRow);
        public object GetRowKey(int controllerRow);
        public int GetRowLevel(int controllerRow);
        public object GetRowValue(int controllerRow, DataColumnInfo column);
        public object GetRowValue(int controllerRow, int column);
        public object GetRowValue(int controllerRow, string columnName);
        public object GetRowValue(int controllerRow, DataColumnInfo column, OperationCompleted completed);
        public virtual object GetRowValue(int controllerRow, int column, OperationCompleted completed);
        public object GetRowValue(int controllerRow, string columnName, OperationCompleted completed);
        protected virtual object GetRowValueDetail(int controllerRow, DataColumnInfo column);
        private IEnumerable<object> GetShortcutSummaryEnumerable(SummaryItem summaryItem, GroupRowInfo groupRow);
        [IteratorStateMachine(typeof(DataController.<GetSummaryAuxInfo>d__444))]
        private static IEnumerable<string> GetSummaryAuxInfo(SummaryItem summaryItem, bool ignoreNullValues);
        protected virtual object GetSummaryShortcut(GroupRowInfo groupRowInfo, SummaryItem summaryItem, out bool isValid);
        public Func<object> GetThreadSafeRowValueGetter(int controllerRow, int column);
        public object[] GetUniqueColumnValues(string fieldName, ColumnValuesArguments args, OperationCompleted completed);
        public object[] GetUniqueColumnValues(string fieldName, int maxCount, bool includeFilteredOut, bool roundDataTime, OperationCompleted completed);
        public object[] GetUniqueColumnValues(string fieldName, int maxCount, bool includeFilteredOut, bool roundDataTime, OperationCompleted completed, bool implyNullLikeEmptyStringWhenFiltering);
        public object[] GetUniqueColumnValues(string fieldName, int maxCount, CriteriaOperator filter, bool ignoreAppliedFilter, bool roundDataTime, OperationCompleted completed, bool implyNullLikeEmptyStringWhenFiltering);
        public object GetValueEx(int controllerRow, string columnName);
        public virtual object GetValueEx(int controllerRow, string columnName, OperationCompleted completed);
        public int GetVisibleIndex(int controllerRowHandle);
        public int GetVisibleIndexChecked(int controllerRowHandle);
        public VisibleIndexCollection GetVisibleIndexes();
        public virtual bool HasCellError(int controllerRow, DataColumnInfo column);
        public bool HasCellError(int controllerRow, int column);
        public virtual bool HasErrors(int controllerRow, out ErrorType errorType);
        protected virtual bool IsAssociativeSummary(SummaryItemType summaryType);
        public bool IsControllerCellValid(int controllerRow, int column);
        public virtual bool IsControllerRowValid(int controllerRow);
        public bool IsDetailRow(int controllerRow);
        public bool IsDetailRowEmpty(int controllerRow, int relationIndex);
        public bool IsDetailRowEmptyCached(int controllerRow);
        public bool IsDetailRowEmptyCached(int controllerRow, int relationIndex);
        public bool IsDetailRowExpanded(int controllerRow);
        public bool IsDetailRowExpanded(int controllerRow, int relationIndex);
        private bool IsRowEquals(Dictionary<DataColumnInfo, object> values, int listSourceIndex);
        public bool IsRowExpanded(int groupRowHandle);
        protected virtual bool IsRowFit(int listSourceRow);
        public virtual bool IsRowLoaded(int controllerRow);
        private bool? IsRowUserFit(int listSourceRow, bool fit);
        public bool IsRowVisible(int controllerRowHandle);
        protected bool IsSummaryExists(GroupRowInfo groupRow, SummaryItem summaryItem);
        protected virtual bool IsSummaryShortcutable(SummaryItemType summaryType);
        public virtual bool IsValidControllerRowHandle(int controllerRowHandle);
        public virtual void LoadRow(int controllerRow);
        public virtual void LoadRowHierarchy(int rowHandle, OperationCompleted completed);
        public virtual void LoadRows(int startFrom, int count);
        public void MakeRowVisible(int controllerRowHandle);
        private static void MakeVisibleIndexesDirty(VisibleIndexCollection visibleIndexes);
        protected virtual void OnActionItemAdded(int index);
        protected virtual void OnActionItemDeleted(int index, bool filterChange = false);
        protected virtual void OnActionItemMoved(int oldIndex, int newIndex);
        protected override void OnBindingListChanged(ListChangedEventArgs e);
        protected virtual void OnBindingListChangedCore(ListChangedEventArgs e);
        protected virtual void OnBindingListChangingEnd();
        protected virtual void OnBindingListChangingStart();
        protected virtual void OnDataSync_FilterSortGroupInfoChanged(object sender, CollectionViewFilterSortGroupInfoChangedEventArgs e);
        private bool OnFilteredItemChanged(int listSourceRow, DataControllerChangedItemCollection changedItems);
        protected virtual void OnFilterExpressionChanged();
        protected internal override void OnGroupDeleted(GroupRowInfo groupInfo);
        protected internal override void OnGroupsDeleted(List<GroupRowInfo> groups, bool addedToSameGroup);
        protected virtual void OnGroupSummaryCollectionChanged(object sender, CollectionChangeEventArgs e);
        private void OnItemAdded(ListChangedEventArgs e, DataControllerChangedItemCollection changedItems);
        protected virtual void OnItemAddedCore(int listSourceRow, DataControllerChangedItemCollection changedItems, bool rowInserted);
        protected virtual void OnItemChanged(ListChangedEventArgs e, DataControllerChangedItemCollection changedItems);
        private void OnItemDeleted(ListChangedEventArgs e, DataControllerChangedItemCollection changedItems);
        protected virtual void OnItemDeletedCore(int controllerRow, DataControllerChangedItemCollection changedItems);
        protected virtual void OnItemMoved(ListChangedEventArgs e, DataControllerChangedItemCollection changedItems);
        protected override void OnListSourceChangeClear();
        protected virtual void OnMasterDetailChanged();
        protected virtual void OnPostRefresh(bool useRowsKeeper);
        protected void OnPostRefreshUpdateSelection();
        protected virtual void OnPreRefresh(bool useRowsKeeper);
        protected virtual void OnRefresh(bool useRowsKeeper);
        protected virtual void OnRefreshed();
        protected void OnSortInfoCollectionChanged(object sender, CollectionChangeEventArgs e);
        protected void OnSortSummaryCollectionChanged(object sender, CollectionChangeEventArgs e);
        protected virtual void OnTotalSummaryCollectionChanged(object sender, CollectionChangeEventArgs e);
        protected virtual void OnVisibleClient_VisibleRangeChanged(object sender, EventArgs e);
        protected virtual void OnVisibleIndexesUpdated();
        public virtual bool PrefetchAllData(Function<bool> callbackMethod);
        private List<object> ProcessListBasedSummary(VisibleListSourceRowCollection list, SummaryItem summaryItem);
        private List<object> ProcessListBasedSummaryDupUni(VisibleListSourceRowCollection list, SummaryItem summaryItem);
        protected override void RaiseOnBeforeListChanged(ListChangedEventArgs e);
        protected internal override void RaiseRowDeleted(int controllerRow, int listSourceRowIndex, object row);
        protected internal override bool RaiseRowDeleting(int listSourceRowIndex);
        protected internal void RaiseSelectionChanged(SelectionChangedEventArgs e);
        private void RebuildVisibleListSourceRows(GroupRowInfoCollection groupInfo);
        public void RefreshRow(int rowHandle);
        public override void RePopulateColumns(bool allowRefresh);
        protected override void RequestDataSyncInitialize();
        protected virtual GroupRowInfo RequestSummary(GroupRowInfo rowInfo);
        protected override void Reset();
        private void ResetFilterStub();
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetRowsKeeper();
        protected void ResetRowsKeeperEx();
        protected internal virtual void RestoreGroupExpanded(GroupRowInfo group);
        public void RestoreRowState();
        public void RestoreRowState(IClassicRowKeeper keeper);
        public void RestoreRowState(Stream stream);
        protected virtual void SaveFilterCache();
        public void SaveRowState();
        public void SaveRowState(IClassicRowKeeper keeper);
        public void SaveRowState(Stream stream);
        public virtual void ScrollingCancelAllGetRows();
        public virtual void ScrollingCheckRowLoaded(int rowHandle);
        public void SetRowValue(int controllerRow, DataColumnInfo column, object val);
        public void SetRowValue(int controllerRow, int column, object val);
        public void SetRowValue(int controllerRow, string columnName, object val);
        protected virtual void SetRowValueCore(int controllerRow, int column, object val);
        private int ShowRow(int dataIndex, DataControllerChangedItemCollection changedItems, bool rowInserted);
        public static bool StringStartsWith(string source, string part);
        protected override void SubscribeDataSync();
        public int UnsafeGetListSourceRowIndex(int controllerRow);
        protected override void UnsubscribeDataSync();
        public void UpdateGroupSummary();
        public virtual void UpdateGroupSummary(List<SummaryItem> changedItems);
        public override void UpdateGroupSummary(GroupRowInfo groupRow, DataControllerChangedItemCollection changedItems);
        public virtual void UpdateSortGroup(DataColumnSortInfo[] sortInfo, int groupCount, DevExpress.Data.Helpers.SummarySortInfo[] summaryInfo);
        public override void UpdateTotalSummary(List<SummaryItem> changedItems);
        protected virtual void UpdateTotalSummaryOnItemAdded(int listSourceRow);
        protected virtual void UpdateTotalSummaryOnItemChanged(int listSourceRow, string propertName);
        protected virtual void UpdateTotalSummaryOnItemDeleted(int controllerRow);
        protected virtual void UpdateTotalSummaryOnItemFilteredOut(int listSourceRow);
        private void ValidateSortInfos(DataColumnSortInfo[] infos);
        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ValidateSortInfosAfterIntercept(DataColumnSortInfo[] patched);
        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ValidateSortInfosBeforeIntercept(DataColumnSortInfo[] rv);
        protected virtual void VisibleListSourceRowMove(int oldControllerRow, ref int newControllerRow, DataControllerChangedItemCollection changedItems, bool isAdding);
        protected override void VisualClientNotifyTotalSummary();
        protected internal override void VisualClientRequestSynchronization();
        protected internal override void VisualClientUpdateLayout();

        public bool MaintainVisibleRowBindingOnFilterChange { get; set; }

        protected internal CustomSummaryEventArgs SummaryCalculateArgs { get; }

        public Dictionary<int, bool> DetailEmptyHash { get; }

        public DevExpress.Data.Helpers.FilterHelper FilterHelper { get; }

        public IDataControllerRelationSupport RelationSupport { get; set; }

        public bool AutoExpandAllGroups { get; set; }

        public override bool AllowPartialGrouping { get; set; }

        public virtual bool AutoUpdateTotalSummary { get; set; }

        public virtual bool ImmediateUpdateRowPosition { get; set; }

        public bool IsImmediateUpdateRowPosition { get; }

        public IDataControllerVisualClient2 VisualClient2 { get; }

        public IDataControllerVisualClient VisualClient { get; set; }

        public IDataControllerDetailClient DetailClient { get; }

        public bool AllowRestoreSelection { get; set; }

        public bool IsInitializing { get; }

        public SelectionController Selection { get; }

        protected internal IClassicRowKeeper RowsKeeper { get; }

        protected DataController.FilterRowStub FilterStub { get; }

        [Obsolete("Use FilterDelegate instead", true)]
        protected ExpressionEvaluator FilterExpressionEvaluator { get; }

        protected bool HasUserFilter { get; }

        public bool IsFiltered { get; }

        public CriteriaOperator FilterCriteria { get; set; }

        public string FilterExpression { get; set; }

        public virtual bool AllowNew { get; }

        public bool AllowEdit { get; }

        public bool AllowRemove { get; }

        public bool IsSupportMasterDetail { get; }

        public DataColumnSortInfoCollection SortInfo { get; }

        public SummaryItemCollection GroupSummary { get; }

        public SummarySortInfoCollection SummarySortInfo { get; }

        public TotalSummaryItemCollection TotalSummary { get; }

        public int VisibleListSourceRowCount { get; }

        public int GroupRowCount { get; }

        public int RelationCount { get; }

        public virtual bool CanSort { get; }

        public virtual bool CanGroup { get; }

        public virtual bool CanFilter { get; }

        public int GroupedColumnCount { get; }

        public bool IsSorted { get; }

        public bool IsGrouped { get; }

        protected virtual bool IsDataSyncInProgress { get; }

        protected bool IsRefreshInProgress { get; }

        public GroupRowInfoCollection GroupInfo { get; }

        protected virtual bool AllowRebuildVisubleIndexesOnRefresh { get; }

        private bool InSortGroupUpdate { get; }

        public string LastErrorText { get; set; }

        public int VisibleCount { get; }

        public MasterRowInfoCollection ExpandedMasterRowCollection { get; }

        protected internal VisibleListSourceRowCollection VisibleListSourceRows { get; }

        protected internal VisibleIndexCollection VisibleIndexes { get; }

        public virtual bool CollapseDetailRowsOnReset { get; set; }

        protected bool NotifyClientOnNextChange { get; set; }

        public virtual bool IsVirtualQuery { get; }

        protected internal string[] LastGroupDescriptors { get; }

        protected virtual bool IsSortBySummary { get; }

        public virtual bool UseClassicRowKeeper { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataController.<>c <>9;
            public static Func<DataColumnSortInfo, int> <>9__205_0;
            public static Func<DataColumnSortInfo, bool> <>9__404_0;
            public static Func<GroupRowInfo, byte> <>9__441_0;
            public static Func<GroupRowInfo, int> <>9__441_1;
            public static Func<object, object> <>9__459_0;
            public static Func<IGrouping<object, object>, bool> <>9__459_1;
            public static Func<IGrouping<object, object>, object> <>9__459_2;
            public static Func<object, object> <>9__459_3;
            public static Func<IGrouping<object, object>, bool> <>9__459_4;
            public static Func<IGrouping<object, object>, object> <>9__459_5;

            static <>c();
            internal byte <CalcGroupSummaryItem>b__441_0(GroupRowInfo g);
            internal int <CalcGroupSummaryItem>b__441_1(GroupRowInfo g);
            internal bool <GetExpandedAndInterceptedSortInfo>b__404_0(DataColumnSortInfo si);
            internal int <GetGroupRowKeyValueInternal>b__205_0(DataColumnSortInfo ssi);
            internal object <ProcessListBasedSummaryDupUni>b__459_0(object x);
            internal bool <ProcessListBasedSummaryDupUni>b__459_1(IGrouping<object, object> g);
            internal object <ProcessListBasedSummaryDupUni>b__459_2(IGrouping<object, object> g);
            internal object <ProcessListBasedSummaryDupUni>b__459_3(object x);
            internal bool <ProcessListBasedSummaryDupUni>b__459_4(IGrouping<object, object> g);
            internal object <ProcessListBasedSummaryDupUni>b__459_5(IGrouping<object, object> g);
        }

        [CompilerGenerated]
        private sealed class <FilterRows>d__71 : IEnumerable<int>, IEnumerable, IEnumerator<int>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private int <>2__current;
            private int <>l__initialThreadId;
            private CriteriaOperator filter;
            public CriteriaOperator <>3__filter;
            private IEnumerable<int> rowsToFit;
            public IEnumerable<int> <>3__rowsToFit;
            private DataController.FilterRowStub <stub>5__1;
            public DataController <>4__this;
            private IEnumerator<int> <>7__wrap1;

            [DebuggerHidden]
            public <FilterRows>d__71(int <>1__state);
            private void <>m__Finally1();
            private void <>m__Finally2();
            private void <>m__Finally3();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<int> IEnumerable<int>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            int IEnumerator<int>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

        [CompilerGenerated]
        private sealed class <FindMatchingGroupSummaries>d__450 : IEnumerable<SummaryItem>, IEnumerable, IEnumerator<SummaryItem>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private SummaryItem <>2__current;
            private int <>l__initialThreadId;
            public DataController <>4__this;
            private SummaryItem summaryItem;
            public SummaryItem <>3__summaryItem;
            private IEnumerator <>7__wrap1;

            [DebuggerHidden]
            public <FindMatchingGroupSummaries>d__450(int <>1__state);
            private void <>m__Finally1();
            private void <>m__Finally2();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<SummaryItem> IEnumerable<SummaryItem>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            SummaryItem IEnumerator<SummaryItem>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }


        public sealed class FilterRowStub : BaseRowStub
        {
            private readonly Func<BaseRowStub, bool> FilterDelegate;

            public FilterRowStub(DataController _DC, Func<BaseRowStub, bool> _FilterDelegate, Action additionalCleanUp);
            public static DataController.FilterRowStub Create(DataController _DC, out Exception exception);
            public static DataController.FilterRowStub Create(DataController _DC, CriteriaOperator filter, out Exception exception);
            public bool Filter();

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DataController.FilterRowStub.<>c <>9;
                public static Func<BaseRowStub, bool> <>9__2_0;
                public static Func<BaseRowStub, bool> <>9__2_1;

                static <>c();
                internal bool <Create>b__2_0(BaseRowStub x);
                internal bool <Create>b__2_1(BaseRowStub x);
            }
        }

        protected abstract class FindIndexCache<TMember> : DataController.IFindIndexCache<TMember>, IDisposable where TMember: class
        {
            private int index;
            private readonly TMember valueMember;
            private readonly IDictionary<object, int> indices;
            private readonly Func<TMember, object, int, int> findIndex;
            protected const int DefaultMagicNumberCacheThreshold = 0x80;

            protected FindIndexCache(TMember valueMember, Func<TMember, object, int, int> findIndex, int capacity);
            protected static bool CanCreateCache(Type valueType, int capacity);
            int DataController.IFindIndexCache<TMember>.Get(TMember member, object value, int indexFrom);
            void DataController.IFindIndexCache<TMember>.Put(TMember member, object value, int index);
            void IDisposable.Dispose();
        }

        protected sealed class FindRowByValueCache : DataController.FindIndexCache<DataColumnInfo>, DataController.IFindRowByValueCache, DataController.IFindIndexCache<DataColumnInfo>, IDisposable
        {
            private FindRowByValueCache(DataColumnInfo column, Func<DataColumnInfo, object, int, int> findRowByValue, int capacity);
            public static DataController.IFindRowByValueCache Create(DataColumnInfo column, Func<DataColumnInfo, object, int, int> findRowByValue, int capacity);
        }

        protected sealed class FindValueIndexCache : DataController.FindIndexCache<string>, DataController.IFindValueIndexCache, DataController.IFindIndexCache<string>, IDisposable
        {
            private FindValueIndexCache(string valueMember, Func<string, object, int, int> findValueIndex, int capacity);
            public static DataController.IFindValueIndexCache Create(string valueMember, Type valueType, Func<string, object, int, int> findValueIndex, int capacity);
        }

        protected interface IFindIndexCache<TMember> : IDisposable
        {
            int Get(TMember member, object obj, int indexFrom);
            void Put(TMember member, object obj, int index);
        }

        protected interface IFindRowByValueCache : DataController.IFindIndexCache<DataColumnInfo>, IDisposable
        {
        }

        protected interface IFindValueIndexCache : DataController.IFindIndexCache<string>, IDisposable
        {
        }
    }
}

