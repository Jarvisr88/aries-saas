namespace DevExpress.Data
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public abstract class BaseGridController : BaseListSourceDataController, IColumnsServerActions
    {
        private ControllerRowValuesKeeper valuesKeeper;
        internal int lastGroupedColumnCount;
        private bool keepFocusedRowOnUpdate;
        private bool allowCurrentControllerRow;
        private bool allowCurrentRowObjectForGroupRow;
        internal bool currentRowEditing;
        private int currentControllerRow;
        private BaseGridController.RowObjectInfo currentControllerRowInfo;
        private int currentControllerRowObjectLevel;
        private CacheRowValuesMode valuesCacheMode;
        private IDataControllerValidationSupport validationClient;
        private IDataControllerCurrentSupport currentClient;
        internal object dataSource;
        internal string dataMember;
        internal const string CrossThreadExceptionMessage = "A multithreading issue is detected. The DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection option allows you to disable the exception, but it does not resolve the underlying issue.";
        internal static bool _DisableThreadingProblemsDetection;
        private DataControllerVirtualQuery virtualQuery;
        private int _lockEndEdit;

        public event InternalExceptionEventHandler CatchException;

        protected BaseGridController();
        protected virtual bool AllowServerAction(string fieldName, ColumnServerActionType action);
        public virtual void BeginCurrentRowEdit();
        protected override void BeginInvoke(Delegate method);
        protected void BeginLockEndEdit();
        public virtual void CancelCurrentRowEdit();
        internal bool CheckCrossThread(Action<object> method, bool ignoreThreadWarning = false);
        protected internal void CheckCurrentControllerRowObject();
        protected virtual void CheckCurrentControllerRowObject(ListChangedEventArgs e);
        protected virtual void CheckCurrentControllerRowObjectOnRefresh();
        protected override void CheckInvalidCurrentRow();
        protected virtual bool CompareBindingContext(System.Windows.Forms.BindingContext context);
        protected virtual bool CompareDataSource(object dataSource, string dataMember);
        private ListChangedEventArgs ConvertListArgsToControllerArgs(ListChangedEventArgs e);
        public override IClassicRowKeeper CreateControllerRowsKeeperClassic();
        protected override IClassicRowKeeper CreateControllerRowsKeeperCore();
        protected override FilterHelper CreateFilterHelper();
        protected virtual DataControllerVirtualQuery CreateVirtualQuery();
        bool IColumnsServerActions.AllowAction(string fieldName, ColumnServerActionType action);
        public override void Dispose();
        protected override void DoGroupRowsCore();
        protected override void DoRefreshDataOperations();
        public bool EndCurrentRowEdit();
        public virtual bool EndCurrentRowEdit(bool force);
        protected void EndLockEndEdit();
        protected int FindRowLevel(int controllerDataRow, int requiredLevel);
        public override int GetControllerRow(int listSourceRow);
        protected virtual object GetCurrentControllerRowObject();
        public object GetCurrentRowValue(DataColumnInfo column);
        public object GetCurrentRowValue(int column);
        public object GetCurrentRowValue(string columnName);
        protected virtual IList GetList(object dataSource);
        protected virtual IList GetListSource(object dataSource);
        internal void InternalSetControllerRow(int newCurrentRow);
        public override bool IsGroupRowHandle(int controllerRowHandle);
        protected override void OnBindingListChangedCore(ListChangedEventArgs e);
        protected virtual void OnCurrentControllerRowChanged();
        protected virtual void OnCurrentControllerRowChanging(int oldControllerRow, int newControllerRow);
        protected virtual void OnCurrentControllerRowObjectChanging(object oldObject, object newObject, int level, int sourceIndex);
        protected virtual bool OnCurrentRowUpdated(bool prevEditing, int controllerRow, object row);
        protected virtual bool OnCurrentRowValidating();
        protected abstract void OnDataSourceChanged();
        protected override void OnListSourceChangeClear();
        protected virtual void OnPostRowCellException(int controllerRow, int column, object row, Exception exception);
        protected virtual ExceptionAction OnPostRowException(Exception exception);
        protected override void OnRefreshed();
        protected override void OnVisibleClient_VisibleRangeChanged(object sender, EventArgs e);
        public virtual void QueryMoreRows(int suggestedRowCount = 0);
        protected void RaiseCurrentRowChanged();
        protected virtual void RaiseCurrentRowChanged(bool prevEditing);
        protected virtual void RaiseOnCatchException(InternalExceptionEventArgs e);
        public override void RefreshData();
        protected override void Reset();
        public virtual void ResetCurrentPosition();
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool SetAutoSearchDataSource(object dataSource, CriteriaOperator filter, System.Windows.Forms.BindingContext context = null);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool SetAutoSuggestDataSource(ICollection suggestions, CriteriaOperator filter, System.Windows.Forms.BindingContext context = null);
        protected virtual void SetBindingContextCore(System.Windows.Forms.BindingContext context);
        protected virtual void SetCurrentControllerRowObject(object value, int level, int sourceIndex);
        public void SetCurrentRowValue(DataColumnInfo column, object val);
        public void SetCurrentRowValue(int column, object val);
        public void SetCurrentRowValue(string columnName, object val);
        public override void SetDataSource(object dataSource);
        public virtual void SetDataSource(System.Windows.Forms.BindingContext context, object dataSource, string dataMember);
        protected bool SetDataSourceCore(System.Windows.Forms.BindingContext context, object dataSource, string dataMember);
        protected override void SetRowValueCore(int controllerRow, int column, object val);
        protected virtual void StopCurrentRowEditCore();
        public virtual void SyncCurrentRow();
        internal void SyncCurrentRowObject(ListChangedEventArgs e);
        internal void ThrowCrossThreadExceptionToTheUIThreadIfPossible();
        public override void UpdateTotalSummary(List<SummaryItem> changedItems);
        protected override void VisibleListSourceRowMove(int oldControllerRow, ref int newControllerRow, DataControllerChangedItemCollection changedItems, bool isAdding);
        protected internal override void VisualClientRequestSynchronization();

        public bool AllowCurrentControllerRow { get; set; }

        protected ControllerRowValuesKeeper ValuesKeeper { get; }

        public int LastGroupedColumnCount { get; }

        public CacheRowValuesMode ValuesCacheMode { get; set; }

        public virtual IDataControllerValidationSupport ValidationClient { get; set; }

        protected internal DataControllerVirtualQuery VirtualQuery { get; set; }

        public override bool IsVirtualQuery { get; }

        public override bool CanGroup { get; }

        public override bool CanSort { get; }

        public override bool CanFilter { get; }

        public virtual bool IsBusy { get; }

        protected bool ValidationClientIsAllowBeginInvoke { get; }

        public virtual IDataControllerCurrentSupport CurrentClient { get; set; }

        public bool KeepFocusedRowOnUpdate { get; set; }

        public int CurrentListSourceIndex { get; set; }

        public object CurrentControllerRowObject { get; }

        internal BaseGridController.RowObjectInfo CurrentControllerRowInfo { get; set; }

        public virtual int CurrentControllerRow { get; set; }

        protected virtual System.Windows.Forms.BindingContext BindingContext { get; set; }

        public object DataSource { get; set; }

        public string DataMember { get; set; }

        public virtual bool AllowCurrentRowObjectForGroupRow { get; set; }

        public bool IsCurrentRowModified { get; }

        public virtual bool IsCurrentRowEditing { get; }

        protected virtual bool RequireEndEditOnGroupRows { get; }

        protected bool IsLockEndEdit { get; }

        internal class RowObjectInfo
        {
            public int SourceIndex;
            public object Row;
        }
    }
}

