namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using DevExpress.Data.Selection;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class CurrencyDataController : BaseGridController
    {
        private System.Windows.Forms.BindingContext bindingContext;
        private System.Windows.Forms.CurrencyManager currencyManager;
        private int lockPositionChange;
        private int lockCurrencyResetEvent;
        private bool refreshFired;
        private bool fieldRequested;
        private FieldInfo suspendPushDataInCurrentChangedInfo;
        private bool syncPositionOnItemChanged;
        private CurrencyDataController.CurrencyManagerSyncReason? requireSyncCurrencyManager;
        private int deleteProgress;
        private int deleteSavedVisibleIndex;
        private int tempDeatchedListSourceRow;
        internal bool finishingNewItemRowEdit;
        private int cancelingRowEdit;

        public CurrencyDataController();
        public override void AddNewRow();
        private void BeginDelete();
        public override void CancelCurrentRowEdit();
        protected override bool CanFilterAddedRow(int listSourceRow);
        protected override void CheckCurrentControllerRowObject(ListChangedEventArgs e);
        private void CheckEndNewItemRow();
        private bool CheckStackFrame(string methodName, Type type);
        private int CheckSyncReason(int newRow);
        protected override bool CompareBindingContext(System.Windows.Forms.BindingContext context);
        protected override SelectionController CreateSelectionController();
        protected virtual void CurrencyManagerEndEdit();
        public override void DeleteRow(int controllerRow);
        public override void DeleteRows(int[] controllerRows);
        public override void Dispose();
        protected override void DoRefresh(bool useRowsKeeper);
        public override bool EndCurrentRowEdit(bool force);
        private void EndDelete();
        protected System.Windows.Forms.CurrencyManager GetCurrencyManager();
        protected IList GetCurrencyManagerListSource();
        protected bool GetInListItemChanged();
        public override int GetListSourceRowIndex(int controllerRow);
        private void InvokeSyncWithCurrencyManager();
        public override bool IsControllerRowValid(int controllerRow);
        private bool IsFromCancelEdit();
        private bool IsFromListChanged();
        public override bool IsValidControllerRowHandle(int controllerRowHandle);
        protected void LockCurrencyResetEvent();
        protected void LockPositionChange();
        protected override void OnActionItemAdded(int index);
        protected override void OnActionItemDeleted(int index, bool filterChange);
        protected override void OnActionItemMoved(int oldIndex, int newIndex);
        protected override void OnBindingListChangingEnd();
        protected override void OnBindingListChangingStart();
        private void OnCurrencyManager_CurrentChanged(object sender, EventArgs e);
        private void OnCurrencyManager_ItemChanged(object sender, ItemChangedEventArgs e);
        private void OnCurrencyManager_PositionChanged(object sender, EventArgs e);
        protected virtual void OnCurrencyManagerAddNew();
        protected virtual void OnCurrencyManagerChanged();
        protected override void OnDataSourceChanged();
        protected internal override void OnEndNewItemRow();
        protected override void OnItemChanged(ListChangedEventArgs e, DataControllerChangedItemCollection changedItems);
        protected internal override void OnItemDeleted(int listSourceRow);
        protected internal override void OnItemDeleting(int listSourceRow);
        protected override void OnPostRefresh(bool useRowsKeeper);
        protected override void OnRefreshed();
        protected internal override void OnStartNewItemRow();
        protected override void RaiseCurrentRowChanged(bool prevEditing);
        protected internal override void RaiseOnBindingListChanged(ListChangedEventArgs e);
        public override void RefreshData();
        protected override void SetBindingContextCore(System.Windows.Forms.BindingContext context);
        protected void SetCurrencyManagerPositionCore(int position);
        public override void SetDataSource(System.Windows.Forms.BindingContext context, object dataSource, string dataMember);
        protected virtual void SetTempDetachedListSourceRow(int listSourceRow);
        protected override void StopCurrentRowEditCore();
        public override void SyncCurrentRow();
        private void SyncWithCurrencyManager();
        protected void UnlockCurrencyResetEvent();
        protected void UnlockPositionChange();

        protected override System.Windows.Forms.BindingContext BindingContext { get; set; }

        protected System.Windows.Forms.CurrencyManager CurrencyManager { get; set; }

        protected int CurrencyManagerPosition { get; set; }

        protected bool IsPositionNotificationLocked { get; }

        private bool IsDeletingRow { get; }

        [Obsolete("The multithreading issue detection is disabled. You may encounter inconsistent or corrupted data.")]
        public static bool DisableThreadingProblemsDetection { get; set; }

        protected bool IsCurrencyResetEventLocked { get; }

        private enum CurrencyManagerSyncReason
        {
            public const CurrencyDataController.CurrencyManagerSyncReason ItemMoved = CurrencyDataController.CurrencyManagerSyncReason.ItemMoved;,
            public const CurrencyDataController.CurrencyManagerSyncReason ItemDeleted = CurrencyDataController.CurrencyManagerSyncReason.ItemDeleted;,
            public const CurrencyDataController.CurrencyManagerSyncReason ItemAdded = CurrencyDataController.CurrencyManagerSyncReason.ItemAdded;,
            public const CurrencyDataController.CurrencyManagerSyncReason General = CurrencyDataController.CurrencyManagerSyncReason.General;
        }

        private delegate void ListChangedDelegate(ListChangedEventArgs e);
    }
}

