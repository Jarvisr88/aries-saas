namespace DevExpress.Data
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Data.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class DataControllerBase : IDisposable, IEvaluatorDataAccess
    {
        private DataControllerNotificationProviders notificationProviders;
        private bool disposed;
        private bool allowNotifications;
        private bool summariesIgnoreNullValues;
        private bool keepGroupRowsExpandedOnRefresh;
        private int prevVisibleCount;
        private BaseDataControllerHelper helper;
        private DataColumnInfoCollection columns;
        private DataColumnInfoCollection detailColumns;
        private DevExpress.Data.ValueComparer valueComparer;
        private IDataControllerThreadClient threadClient;
        private IList listSource;
        private IDataControllerData dataClient;
        protected IDataControllerSort fSortClient;
        private Type forcedDataRowType;
        private int lockUpdate;
        private int listSourceChanging;
        private int suspedVCount;
        public static bool AllowFindNonStringTypesServerMode;
        public static bool CatchRowUpdatedExceptions;
        private bool ignoreNextReset;

        public event ListChangedEventHandler BeforeListChanged;

        public event EventHandler BeforePopulateColumns;

        public event ListChangedEventHandler ListChanged;

        public event EventHandler ListSourceChanged;

        public event EventHandler VisibleRowCountChanged;

        static DataControllerBase();
        public DataControllerBase();
        public void AddThreadClient(IDataControllerThreadClient client);
        public virtual void BeginUpdate();
        public void CancelUpdate();
        protected virtual void CheckInvalidCurrentRow();
        protected virtual void ClearClients();
        public virtual void CollapseDetailRows();
        protected internal virtual int CompareGroupColumnRows(DataColumnSortInfoCollection sortInfo, GroupRowInfoCollection groupInfo, int controllerRow1, int controllerRow2);
        public bool ContainsColumn(string fieldName);
        protected virtual DataControllerChangedItemCollection CreateDataControllerChangedItemCollection();
        public virtual DataControllerBase.EvalRowStub CreateEvalRowStub(CriteriaOperator expression, out Exception ex);
        public virtual ExpressionEvaluator CreateExpressionEvaluator(CriteriaOperator criteriaOperator, bool setDataAccess, out Exception e);
        protected virtual BaseDataControllerHelper CreateHelper();
        protected virtual DevExpress.Data.ValueComparer CreateValueComparer();
        object IEvaluatorDataAccess.GetValue(PropertyDescriptor descriptor, object theObject);
        public virtual void Dispose();
        protected void DoGroupColumn(DataColumnSortInfoCollection sortInfo, GroupRowInfoCollection groupInfo, int controllerRow, int rowCount, GroupRowInfo parentGroup);
        private void DoGroupColumn(DataColumnSortInfoCollection sortInfo, GroupRowInfoCollection groupInfo, int controllerRow, int rowCount, GroupRowInfo parentGroup, List<Func<int, int, bool>> areEqualsCache);
        public void DoRefresh();
        protected virtual void DoRefresh(bool useRowsKeeper);
        public virtual void EndUpdate();
        protected virtual void EndUpdateCore(bool sortUpdate);
        private static DataColumnSortInfo[] ExtractReallyGroupingSortInfos(DataColumnSortInfo colSortInfoMono);
        public DataColumnInfo FindColumn(string fieldName);
        private static int FindFirstSureInvalid(int _firstMayBeValid, int _firstSureInvalid, Func<int, bool> testIndexForValid);
        protected internal virtual TypeConverter GetActualTypeConverter(TypeConverter converter, PropertyDescriptor property);
        protected int GetChangedListSourceRow(ListChangedEventArgs e);
        protected int GetColumnIndex(DataColumnInfo column);
        public virtual int GetControllerRow(int listSourceRow);
        protected internal virtual PropertyDescriptorCollection GetFilterDescriptorCollection();
        protected internal virtual object GetGroupRowKeyValueInternal(GroupRowInfo group);
        private Func<int, int, bool> GetGroupRowsAreEquals(int level, DataColumnSortInfo columnInfoMono, List<Func<int, int, bool>> areEqualsCache);
        private Func<int, int, bool> GetGroupRowsPivotCompatibleEqualizer(DataColumnInfo dataColumnInfo);
        [IteratorStateMachine(typeof(DataControllerBase.<GetGroupsBoundaries>d__166))]
        private IEnumerable<KeyValuePair<int, int>> GetGroupsBoundaries(int handlesCount, Func<int, int> handleToSourceIndex, Func<int, int, bool> areEquals);
        protected int GetListSourceFromVisibleListSourceRowCollection(VisibleListSourceRowCollection visibleListSourceRowCollection, int controllerRow);
        public int GetListSourceRowIndex(GroupRowInfoCollection groupInfo, int controllerRow);
        public DataControllerNotificationProviders GetNotificationProviders();
        public int GetVisibleListSourceRowCount(VisibleListSourceRowCollection visibleListSourceRows);
        public virtual bool IsColumnValid(int column);
        public virtual bool IsControllerRowValid(VisibleListSourceRowCollection visibleListSourceRows, int controllerRow);
        public virtual bool IsDetailColumnValid(int column);
        public bool IsEqualGroupValues(object val1, object val2);
        protected internal virtual bool IsEqualGroupValues(object val1, object val2, int listSourceRow1, int listSourceRow2, DataColumnInfo columnInfo);
        protected virtual bool IsEqualNonNullValues(object val1, object val2);
        public virtual bool IsGroupRowHandle(int controllerRowHandle);
        protected internal virtual void MakeGroupRowVisible(GroupRowInfo groupRow);
        private Func<int, int, bool> MakeMergedGroupRowsEqualizerFromSingleColumnsEqualizers(DataColumnSortInfo[] dataColumnSortInfoS, Func<DataColumnInfo, Func<int, int, bool>> singleColumnEqualizerMaker);
        protected virtual void OnBindingListChanged(ListChangedEventArgs e);
        protected void OnBindingListChanged(object sender, ListChangedEventArgs e);
        protected internal virtual void OnColumnPopulated(DataColumnInfo info);
        protected internal virtual void OnEndNewItemRow();
        protected internal virtual void OnGroupDeleted(GroupRowInfo groupRow);
        protected internal virtual void OnGroupsDeleted(List<GroupRowInfo> groups, bool addedToSameGroup);
        protected internal virtual void OnItemDeleted(int listSourceRow);
        protected internal virtual void OnItemDeleting(int listSourceRow);
        protected virtual void OnListSourceChangeClear();
        protected virtual void OnListSourceChanged();
        protected internal virtual void OnStartNewItemRow();
        public virtual void PopulateColumns();
        internal void RaiseBeforePopulateColumns(EventArgs e);
        protected virtual void RaiseListSourceChanged();
        protected virtual void RaiseOnBeforeListChanged(ListChangedEventArgs e);
        protected internal virtual void RaiseOnBindingListChanged(ListChangedEventArgs e);
        protected void RaiseOnListChanged(ListChangedEventArgs e);
        protected internal virtual void RaiseRowDeleted(int controllerRow, int listSourceRowIndex, object row);
        protected internal virtual bool RaiseRowDeleting(int listSourceRowIndex);
        protected internal virtual void RaiseVisibleRowCountChanged();
        public virtual void RefreshData();
        public void RemoveThreadClient(IDataControllerThreadClient client);
        public void RePopulateColumns();
        public virtual void RePopulateColumns(bool allowRefresh);
        protected virtual void RequestDataSyncInitialize();
        protected virtual void Reset();
        protected void ResetSortInfoCollectionCore(DataColumnSortInfoCollection sortInfo);
        private void ResetValidateBindingListChanged();
        protected void ResumeVisibleRowCountChanged();
        protected virtual void SetListSource(IList value);
        protected virtual void SetListSourceCore(IList value);
        protected void SetVisibleListSourceCollectionCore(VisibleListSourceRowCollection visibleListSourceRowCollection, int[] list, int count);
        protected virtual bool StorePrevVisibleCount(int visibleCount);
        protected virtual void SubscribeDataSync();
        protected internal virtual void SubscribeListChanged(INotificationProvider provider, object list);
        protected void SuspendVisibleRowCountChanged();
        protected virtual void UnsubscribeDataSync();
        protected internal virtual void UnsubscribeListChanged(INotificationProvider provider, object list);
        public virtual void UpdateGroupSummary(GroupRowInfo groupRow, DataControllerChangedItemCollection changedItems);
        public void UpdateTotalSummary();
        public virtual void UpdateTotalSummary(List<SummaryItem> changedItems);
        protected internal virtual bool UseFirstRowTypeWhenPopulatingColumns(Type rowType);
        private void ValidateBindingListChanged(ListChangedEventArgs e);
        public virtual void ValidateExpression(CriteriaOperator op);
        protected virtual void VisualClientNotifyTotalSummary();
        protected internal virtual void VisualClientRequestSynchronization();
        protected internal virtual void VisualClientUpdateLayout();
        public virtual void WaitForAsyncEnd();

        public DataControllerNotificationProviders NotificationProviders { get; set; }

        public virtual bool AllowPartialGrouping { get; set; }

        public bool KeepGroupRowsExpandedOnRefresh { get; set; }

        public Type ForcedDataRowType { get; set; }

        public bool IsDisposed { get; }

        public bool SummariesIgnoreNullValues { get; set; }

        public bool AllowNotifications { get; set; }

        protected internal virtual bool IsDetailDescriptorAllow { get; }

        public IList ListSource { get; }

        public IDataControllerData DataClient { get; set; }

        public IDataControllerData2 DataClient2 { get; }

        public IDataControllerSort SortClient { get; set; }

        public DataColumnInfoCollection Columns { get; protected set; }

        public DataColumnInfoCollection DetailColumns { get; }

        public int ListSourceRowCount { get; }

        public virtual IDataControllerThreadClient ThreadClient { get; set; }

        public int LockUpdate { get; }

        public virtual bool IsUpdateLocked { get; }

        public virtual bool IsReady { get; }

        public virtual bool AlwaysUsePrimitiveDataSource { get; }

        public DevExpress.Data.ValueComparer ValueComparer { get; }

        public virtual bool IsServerMode { get; }

        protected internal virtual IDataSync DataSync { get; }

        public BaseDataControllerHelper Helper { get; }

        protected bool IsListSourceChanging { get; }

        protected int PrevVisibleCount { get; }

        protected internal virtual bool DoGroupRowsProcessingPivotCompatible { get; }

        public bool AllowIEnumerableDetails { get; set; }

        public bool ComplexUseLinqDescriptors { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataControllerBase.<>c <>9;
            public static Func<DataColumnSortInfo, DataColumnInfo> <>9__175_0;

            static <>c();
            internal DataColumnInfo <CompareGroupColumnRows>b__175_0(DataColumnSortInfo si);
        }


        public sealed class EvalRowStub : BaseRowStub
        {
            private readonly Delegate TypedDelegate;
            private Func<BaseRowStub, object> UnTypedDelegate;

            public EvalRowStub(DataControllerBase _DC, Delegate typedDelegate, Action additionalCleanUp);
            public static DataControllerBase.EvalRowStub Create(DataControllerBase _DC, CriteriaOperator op, out Exception exception);
            public object Evaluate(int forRowIndex);

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DataControllerBase.EvalRowStub.<>c <>9;
                public static Func<BaseRowStub, object> <>9__2_0;

                static <>c();
                internal object <Create>b__2_0(BaseRowStub stub);
            }
        }
    }
}

