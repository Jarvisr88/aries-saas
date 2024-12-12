namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Access;
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class BaseDataControllerHelper : IRelationListEx, IRelationList, IDisposable
    {
        private int detachedListSourceRow;
        private DataControllerBase controller;
        private Dictionary<string, DataColumnInfo> savedColumns;
        private PropertyDescriptorCollection descriptorCollection;
        private bool newItemRowAdding;
        private bool addNewRowProcess;
        private INotificationProvider notificationProvider;
        private bool allowIndexOf;
        internal int lastPropertyDescriptorCount;
        private bool isSubscribedToEvents;

        public BaseDataControllerHelper(DataControllerBase controller);
        public object AddNewRow();
        protected virtual object AddNewRowCore();
        protected internal virtual void CancelNewItemRow();
        protected virtual bool CanPopulate(PropertyDescriptor descriptor);
        protected virtual bool CanPopulateDetailDescriptor(PropertyDescriptor descriptor);
        protected virtual PropertyDescriptor CreateComplexDescriptor(ComplexColumnInfo complexColumn);
        protected virtual DataColumnInfo CreateDataColumn(PropertyDescriptor descriptor);
        protected virtual UnboundPropertyDescriptor CreateUnboundPropertyDescriptor(UnboundColumnInfo info);
        public void DeleteRow(int listSourceRow);
        IList IRelationList.GetDetailList(int listSourceRow, int relationIndex);
        string IRelationList.GetRelationName(int listSourceRow, int relationIndex);
        bool IRelationList.IsMasterRowEmpty(int listSourceRow, int relationIndex);
        int IRelationListEx.GetRelationCount(int listSourceRow);
        string IRelationListEx.GetRelationDisplayName(int listSourceRow, int relationIndex);
        public virtual void Dispose();
        public virtual int FindRowByKey(object rowKey);
        public virtual ComplexColumnInfoCollection GetComplexColumns();
        public Delegate GetGetRowValue(DataColumnInfo columnInfo, Type expectedReturnType);
        protected virtual Delegate GetGetRowValueCore(DataColumnInfo columnInfo, Type expectedReturnType);
        public int GetNewItemRowIndex();
        public object GetNewRow();
        public object GetNewRowDetailValue(DataColumnInfo info);
        public object GetNewRowValue(DataColumnInfo columnInfo);
        protected virtual PropertyDescriptorCollection GetPropertyDescriptorCollection();
        public object GetRow(int listSourceRow);
        public virtual object GetRow(int listSourceRow, OperationCompleted completed);
        public virtual IDXDataErrorInfo GetRowDXErrorInfo(int listSourceRow);
        public virtual IDataErrorInfo GetRowErrorInfo(int listSourceRow);
        public virtual object GetRowKey(int listSourceRow);
        public object GetRowValue(int listSourceRow, DataColumnInfo columnInfo);
        public virtual object GetRowValue(object row, DataColumnInfo columnInfo);
        public virtual object GetRowValue(int listSourceRow, DataColumnInfo columnInfo, OperationCompleted completed);
        public virtual object GetRowValueDetail(int listSourceRow, DataColumnInfo detailColumn);
        public virtual UnboundColumnInfoCollection GetUnboundColumns();
        protected virtual bool IsDetailDescriptor(PropertyDescriptor descriptor);
        protected internal virtual void OnBindingListChanged(ListChangedEventArgs e);
        protected void OnBindingListChanged(object sender, ListChangedEventArgs e);
        protected virtual void OnEndNewItemRow();
        protected virtual void PopulateColumn(PropertyDescriptor descriptor);
        public virtual void PopulateColumns();
        protected internal virtual void RaiseOnEndNewItemRow();
        protected internal virtual void RaiseOnStartNewItemRow();
        public virtual IList<DataColumnInfo> RePopulateColumns();
        protected internal virtual void SetDetachedListSourceRow(int listSourceRow);
        public void SetNewRowValue(int column, object val);
        protected void SetNewRowValue(object rowObject, DataColumnInfo columnInfo, object val);
        public virtual void SetRowValue(int listSourceRow, int column, object val);
        public virtual void SubscribeEvents();
        protected void SubscribeEventsCore();
        protected virtual int TryIndexOf(object rowKey);
        public virtual void UnsubscribeEvents();
        protected void UnsubscribeEventsCore();
        protected internal virtual void UpdateDetachedIndex(object addedRow);

        protected internal virtual int DetachedListSourceRow { get; set; }

        public bool IsAddNewRowProcess { get; }

        public virtual bool CaseSensitive { get; }

        public int DetachedCount { get; }

        public virtual int Count { get; }

        public DataControllerBase Controller { get; }

        public IList List { get; }

        public INotificationProvider NotificationProvider { get; }

        public ITypedList TypedList { get; }

        public ICancelAddNew CancelAddNew { get; }

        public IBindingList BindingList { get; }

        public IEditableCollectionView EditableView { get; }

        public DataColumnInfoCollection Columns { get; }

        public DataColumnInfoCollection DetailColumns { get; }

        public bool AllowNew { get; }

        public bool AllowEdit { get; }

        public bool AllowRemove { get; }

        public PropertyDescriptorCollection DescriptorCollection { get; }

        protected bool IsSubscribedToEvents { get; set; }

        public bool SupportsNotification { get; }

        public virtual IRelationList RelationList { get; }

        public virtual IRelationListEx RelationListEx { get; }

        int IRelationList.RelationCount { get; }
    }
}

