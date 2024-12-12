namespace DevExpress.Data.TreeList
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Data.TreeList.DataHelpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class TreeListDataControllerBase : IEvaluatorDataAccess, IDisposable
    {
        public static bool OptimizeSummaryCalculation;
        private int currentControllerRow;
        private int lockUpdate;
        private int lockUpdateSummary;
        private List<string> changedProperties;
        private object dataSource;
        private int lockNodeCollectionChanged;
        private CriteriaOperator filterCriteria;
        private Func<object, bool> filterFitPredicateCore;
        private bool isFilterCriteriaChanged;
        private bool currentRowEditing;
        private int lockEndEdit;
        private bool isIncrementalSummaryCalculation;

        static TreeListDataControllerBase();
        public TreeListDataControllerBase(IDataProvider dataProvider);
        public virtual TreeListNodeBase AddNewNode(TreeListNodeBase parentNode);
        public TreeListNodeBase AddNewRow();
        public virtual void AfterListChanged(object sender, ListChangedEventArgs e);
        public virtual bool BeforeListChanged(object sender, ListChangedEventArgs e);
        public virtual void BeginCurrentRowEdit();
        protected void BeginDataRowEdit(TreeListNodeBase node);
        protected void BeginLockEndEdit();
        public virtual void BeginSummaryUpdate();
        public virtual void BeginUpdate();
        public virtual void BeginUpdateCore();
        protected internal virtual bool CalcNodeVisibility(TreeListNodeBase node, Func<object, bool> customFilterFitPredicate = null);
        protected virtual void CalcSummary(IEnumerable<TreeListSummaryItem> summaryItems, Dictionary<TreeListNodeBase, SummaryDataItem> summaryData, IEnumerable<TreeListNodeBase> nodes, IEnumerable<TreeListNodeBase> selectedNodes = null);
        public virtual bool CanCalculateSummaries(IEnumerable<TreeListSummaryItem> summaries);
        protected virtual bool CanCalculateSummary(TreeListSummaryItem item, TreeListNodeBase node);
        protected virtual bool CanCalculateSummaryForItem(TreeListSummaryItem item);
        protected virtual bool CanCalculateSummaryForNode(TreeListNodeBase node);
        public virtual void CancelCurrentRowEdit();
        protected void CancelDataRowEdit(TreeListNodeBase node);
        public virtual void CancelNewNodeEdit();
        public void CancelUpdate();
        protected virtual bool CanContinueCalculation(TreeListNodeBase node, TreeListSummaryItem item, Dictionary<TreeListNodeBase, SummaryDataItem> summaryData);
        protected virtual void ClearSummaryData(IEnumerable<TreeListSummaryItem> changedItems, Dictionary<TreeListNodeBase, SummaryDataItem> summaryData);
        protected virtual TreeListDataHelperBase CreateDataHelper();
        public virtual ExpressionEvaluator CreateExpressionEvaluator(CriteriaOperator criteriaOperator, out Exception e);
        protected internal virtual ExpressionEvaluator CreateExpressionEvaluator(string criteria, out Exception e);
        protected virtual Func<object, bool> CreateFilterFitPredicate();
        public virtual Func<object, bool> CreateFilterFitPredicate(CriteriaOperator criteria);
        protected virtual TreeListFilterHelper CreateFilterHelper();
        protected virtual TreeListNodeComparerBase CreateNodesComparer();
        protected virtual TreeListNodesInfo CreateNodesInfo();
        protected virtual TreeListSummaryValue CreateSummaryValue(TreeListSummaryItem item);
        protected virtual DevExpress.Data.ValueComparer CreateValueComparer();
        protected internal int? CustomNodeSort(TreeListNodeBase node1, TreeListNodeBase node2, object value1, object value2, DataColumnInfo column, ColumnSortOrder sortOrder);
        public virtual void DeleteNode(TreeListNodeBase node, bool deleteChildren, bool modifySource);
        public virtual Action DeleteNodeWithChildrenAndSource(TreeListNodeBase node, bool allowRollback);
        object IEvaluatorDataAccess.GetValue(PropertyDescriptor descriptor, object theObject);
        public void Dispose();
        protected virtual void DoCalcTotalSummary();
        protected virtual bool DoFilterNode(TreeListNodeBase node);
        public void DoFilterNodes();
        public virtual void DoFilterNodes(TreeListNodeBase parentNode);
        protected void DoFilterNodesCore(TreeListNodeBase parent);
        public virtual void DoRefresh(bool keepNodesState);
        public bool DoSortNodes();
        public virtual bool DoSortNodes(TreeListNodeBase parentNode);
        protected internal virtual void DoSortNodesCore(TreeListNodeBase parentNode, bool recursive = true);
        private void DoSortNodesCore(ITreeListNodeCollection nodes, TreeListNodeComparerBase comparer, bool recursive);
        public virtual void DoUpdate(bool sortNodes = true);
        public virtual bool EndCurrentRowEdit();
        protected void EndDataRowEdit(TreeListNodeBase node);
        protected void EndLockEndEdit();
        public void EndSortUpdate();
        public virtual void EndSummaryUpdate();
        public virtual void EndUpdate();
        public virtual void EndUpdateCore();
        public virtual IList ExtractListSource(object dataSource);
        public TreeListNodeBase FindNodeById(int id);
        public virtual TreeListNodeBase FindNodeByValue(object value);
        public virtual TreeListNodeBase FindNodeByValue(string fieldName, object value);
        protected IEditableObject GetEditableObject(TreeListNodeBase node);
        protected virtual PropertyDescriptorCollection GetFilterDescriptorCollection();
        public virtual TreeListNodeBase GetNodeByRowHandle(int rowHandle);
        protected internal virtual IEnumerable<TreeListNodeBase> GetNodeEnumerator(ITreeListNodeCollection nodes, bool onlyExpanded = false);
        protected internal virtual IEnumerable<TreeListNodeBase> GetNodeEnumerator(TreeListNodeBase node, bool onlyExpanded = false);
        public virtual int GetRowHandleByNode(TreeListNodeBase node);
        public virtual int GetRowHandleByVisibleIndex(int visibleIndex);
        protected internal virtual IEnumerable<TreeListNodeBase> GetSelectedNodeEnumerator(TreeListNodeBase nodes);
        protected virtual IEnumerable<TreeListSummaryItem> GetSummarySelectionSummary(IEnumerable<TreeListSummaryItem> summaryItems);
        public object GetSummaryValue(TreeListNodeBase node, TreeListSummaryItem item);
        protected virtual object GetSummaryValueCore(TreeListNodeBase node, TreeListSummaryItem item);
        protected internal virtual object GetSummaryValueCore(TreeListNodeBase node, TreeListSummaryItem item, Dictionary<TreeListNodeBase, SummaryDataItem> summaryData);
        public object GetTotalSummaryValue(TreeListSummaryItem item);
        protected internal UnboundColumnInfoCollection GetUnboundColumns();
        protected internal object GetUnboundData(TreeListNodeBase node, string propertyName, object value);
        public virtual object GetValue(TreeListNodeBase node, string fieldName);
        public virtual object GetValue(int rowHandle, string fieldName);
        public virtual int GetVisibleIndexByNode(TreeListNodeBase node);
        private void IntializeNewNode(TreeListNodeBase node);
        protected virtual bool? IsCustomNodeFilter(TreeListNodeBase node);
        protected bool IsCustomSummaryItem(TreeListSummaryItem item);
        protected bool IsMinMaxSummaryItem(SummaryItem item);
        public bool IsNullValue(object value);
        protected virtual bool IsServiceSummaryItem(TreeListSummaryItem item);
        protected internal bool IsUnboundColumn(DataColumnInfo column);
        protected bool IsUnboundWithExpression(DataColumnInfo ci);
        public bool IsValidRowHandle(int rowHandle);
        public bool IsValidVisibleIndex(int visibleIndex);
        protected internal void LockNodeCollectionChanged();
        protected virtual bool NotifyOnEndCurrentRowEdit();
        protected virtual void OnAfterToggleExpandAllNodes(TreeListNodeBase parent, bool expand);
        protected virtual void OnBeforeToggleExpandAllNodes(TreeListNodeBase parent, bool expand);
        protected virtual void OnBeginNewNodeEdit();
        protected virtual void OnCancelNewNodeEdit();
        protected virtual void OnCurrentControllerRowChanged();
        protected virtual void OnCurrentRowCancelled(TreeListNodeBase node);
        protected virtual void OnCurrentRowUpdated(TreeListNodeBase node);
        protected internal virtual void OnDataHelperDisposing();
        protected internal virtual void OnDataSourceChanged();
        protected virtual void OnEndCurrentRowEdit();
        protected virtual void OnEndNewNodeEdit(NodeChangeType changeType);
        protected virtual void OnFilterCriteriaChanged();
        protected internal virtual void OnLoaded();
        protected internal virtual void OnNodeAddedRemoved(TreeListNodeBase node, NodeChangeType changeType);
        public virtual void OnNodeCollectionChanged(TreeListNodeBase node, NodeChangeType changeType, string changedPropertyName = null, bool raiseNodeChangedEvent = true, bool sourceIsUpdated = false);
        public virtual void OnNodeCollectionChanging(TreeListNodeBase node, NodeChangeType changeType);
        protected internal virtual void OnNodeExpandedOrCollapsed(TreeListNodeBase node);
        protected internal virtual bool OnNodeExpandingOrCollapsing(TreeListNodeBase node);
        protected internal virtual void OnNodeIsVisibleChanged(TreeListNodeBase treeListNodeBase);
        protected void OnNodeSummaryCollectionChanged();
        protected void OnSortInfoCollectionChanged();
        protected void OnTotalSummaryCollectionChanged();
        protected internal virtual void OnVisibleIndexChanged(TreeListNodeBase node);
        protected internal virtual void OnVisibleRowCountChanged();
        public virtual void PopulateColumns();
        private bool PrepareDeleteNode(TreeListNodeBase node);
        private void ProcessSummaryDataItemValues(SummaryDataItem item, Func<TreeListSummaryValue, bool> summaryItemSelector, Action<TreeListSummaryValue> updateAction);
        protected internal virtual object RaiseAddingNew(TreeListNodeBase parentNode);
        protected virtual void RaiseInitNewNode(TreeListNodeBase node);
        protected internal virtual void RePopulateColumnsIfNeeded();
        public virtual void ResetCurrentPosition();
        private void ResetFilterFitPredicate();
        public virtual void RestoreFocusState();
        public virtual void RestoreNodesState();
        protected internal virtual void ResubscribeToBindingList(IBindingList oldValue, IBindingList newValue);
        public virtual void SaveFocusState();
        public virtual void SaveNodesState();
        protected internal void SetUnboundData(TreeListNodeBase node, string propertyName, object value);
        public void SetValue(TreeListNodeBase node, string fieldName, object value);
        public void SetValueCore(TreeListNodeBase node, string fieldName, object value);
        protected virtual bool ShouldRecalculateSummaryOnNodeChanged(TreeListNodeBase node, NodeChangeType changeType);
        protected void StopCurrentRowEdit();
        public virtual void ToggleExpandedAllChildNodes(TreeListNodeBase parent, bool expand);
        protected internal void UnlockNodeCollectionChanged();
        public void UnlockUpdate();
        protected void UpdateCalculatedTotalSummary(TreeListNodeBase node, NodeChangeType changeType, IEnumerable<TreeListSummaryItem> calculatedSummaryItems, Dictionary<TreeListNodeBase, SummaryDataItem> summaryData);
        protected virtual void UpdateCurrentControllerRow(TreeListNodeBase node);
        protected virtual void UpdateCurrentControllerRowOnEndCurrentEdit(TreeListNodeBase currentNode);
        public virtual void UpdateDataHelper();
        protected void UpdateDataProvider();
        protected virtual void UpdateFocusedNode();
        protected void UpdateMinMaxTotalSummary(TreeListNodeBase node, NodeChangeType changeType, IEnumerable<TreeListSummaryItem> summaryItems, Dictionary<TreeListNodeBase, SummaryDataItem> summaryData);
        protected virtual void UpdateNodeAncestorsAndDescendantsVisibilityOnFiltering(TreeListNodeBase node);
        protected virtual void UpdateNodeAncestorsExpandStateOnFiltering(TreeListNodeBase node);
        internal void UpdateNodeId(TreeListNodeBase node);
        public virtual void UpdateNodeSummary();
        public virtual void UpdateNodeSummary(IEnumerable<TreeListSummaryItem> changedItems);
        protected virtual void UpdateSummaryCore(IEnumerable<TreeListSummaryItem> changedItems, IEnumerable<TreeListSummaryItem> summaryItems);
        private void UpdateSummaryData(Dictionary<TreeListNodeBase, SummaryDataItem> summaryData);
        protected virtual void UpdateSummaryProperties(IEnumerable<TreeListSummaryItem> changedItems);
        protected void UpdateSummaryValue(TreeListNodeBase summaryOwner, Dictionary<TreeListNodeBase, SummaryDataItem> summaryData, TreeListSummaryItem item, TreeListNodeBase node, bool useVisibleParent = false, bool checkSelection = false, bool initOnly = false);
        public virtual void UpdateTotalAndNodeSummary(IEnumerable<TreeListSummaryItem> changedItems);
        public virtual void UpdateTotalSummary();
        public virtual void UpdateTotalSummary(IEnumerable<TreeListSummaryItem> changedItems);
        protected virtual void UpdateTotalSummaryIncrementally(TreeListNodeBase node, NodeChangeType changeType, IEnumerable<TreeListSummaryItem> changedItems, Dictionary<TreeListNodeBase, SummaryDataItem> summaryData);
        protected virtual void UpdateTotalSummaryOnNodeCollectionChanged(TreeListNodeBase node, NodeChangeType changeType, string changedPropertyName);
        protected virtual void UpdateTotalSummaryOnNodeCollectionChanged(TreeListNodeBase node, NodeChangeType changeType, IEnumerable<TreeListSummaryItem> changedItems, Dictionary<TreeListNodeBase, SummaryDataItem> summaryData);
        protected virtual void UpdateUnboundColumns();
        protected virtual void UpdateUnlocked();
        protected internal virtual bool UseFirstRowTypeWhenPopulatingColumns(Type itemType);

        public ITreeListNodeCollection Nodes { get; }

        public IDataProvider DataProvider { get; private set; }

        public DataColumnInfoCollection Columns { get; private set; }

        public Type ItemType { get; }

        public int TotalNodesCount { get; }

        public int TotalVisibleNodesCount { get; }

        public bool IsSelfReferenceMode { get; }

        public bool IsUpdateLocked { get; }

        public bool IsReady { get; }

        public DevExpress.Data.ValueComparer ValueComparer { get; private set; }

        public TreeListNodeBase RootNode { get; private set; }

        public bool IsDisposed { get; private set; }

        public virtual TreeListNodesInfo NodesInfo { get; protected internal set; }

        public TreeListDataHelperBase DataHelper { get; private set; }

        protected bool IsSummaryUpdateLocked { get; }

        public bool SupportNotifications { get; }

        public object DataSource { get; set; }

        public virtual bool CanUseFastPropertyDescriptors { get; }

        public int CurrentControllerRow { get; set; }

        public TreeListNodeBase CurrentNode { get; }

        public bool IsCurrentRowEditing { get; }

        public bool AllowEdit { get; }

        protected bool IsLockEndEdit { get; }

        public CriteriaOperator FilterCriteria { get; set; }

        public TreeListFilterHelper FilterHelper { get; private set; }

        protected bool IsFiltered { get; private set; }

        protected bool HasFilter { get; }

        public Func<object, bool> FilterFitPredicate { get; }

        public TreeListDataColumnSortInfoCollection SortInfo { get; private set; }

        protected bool IsSorted { get; private set; }

        public TreeListSummaryItemCollection NodeSummary { get; private set; }

        public TreeListSummaryItemCollection TotalSummary { get; private set; }

        protected Dictionary<TreeListNodeBase, SummaryDataItem> SummaryData { get; private set; }

        protected internal bool IsNodeCollectionChangedLocked { get; }

        public bool IsNewNodeEditing { get; private set; }

        public bool IsAddingNewNode { get; }

        protected internal virtual bool CanRaiseAddingNew { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TreeListDataControllerBase.<>c <>9;
            public static Func<TreeListNodeBase, bool> <>9__132_0;
            public static Func<TreeListNodeBase, bool> <>9__132_1;
            public static Action<TreeListNodeBase> <>9__132_2;
            public static Func<TreeListNodeBase, bool> <>9__132_3;
            public static Action<TreeListNodeBase> <>9__132_4;
            public static Func<TreeListNodeBase, bool> <>9__132_5;
            public static Func<TreeListNodeBase, bool> <>9__133_0;
            public static Action<TreeListNodeBase> <>9__133_1;
            public static Func<KeyValuePair<TreeListSummaryItem, TreeListSummaryValue>, TreeListSummaryValue> <>9__176_1;

            static <>c();
            internal TreeListSummaryValue <CalcSummary>b__176_1(KeyValuePair<TreeListSummaryItem, TreeListSummaryValue> k);
            internal bool <UpdateNodeAncestorsAndDescendantsVisibilityOnFiltering>b__132_0(TreeListNodeBase child);
            internal bool <UpdateNodeAncestorsAndDescendantsVisibilityOnFiltering>b__132_1(TreeListNodeBase parent);
            internal void <UpdateNodeAncestorsAndDescendantsVisibilityOnFiltering>b__132_2(TreeListNodeBase parent);
            internal bool <UpdateNodeAncestorsAndDescendantsVisibilityOnFiltering>b__132_3(TreeListNodeBase parent);
            internal void <UpdateNodeAncestorsAndDescendantsVisibilityOnFiltering>b__132_4(TreeListNodeBase parent);
            internal bool <UpdateNodeAncestorsAndDescendantsVisibilityOnFiltering>b__132_5(TreeListNodeBase child);
            internal bool <UpdateNodeAncestorsExpandStateOnFiltering>b__133_0(TreeListNodeBase parent);
            internal void <UpdateNodeAncestorsExpandStateOnFiltering>b__133_1(TreeListNodeBase parent);
        }
    }
}

