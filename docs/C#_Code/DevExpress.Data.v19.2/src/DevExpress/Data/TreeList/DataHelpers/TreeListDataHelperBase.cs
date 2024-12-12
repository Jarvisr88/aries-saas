namespace DevExpress.Data.TreeList.DataHelpers
{
    using DevExpress.Data;
    using DevExpress.Data.TreeList;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class TreeListDataHelperBase : IDisposable
    {
        private int loading;
        protected internal int NewNodeId;

        public TreeListDataHelperBase(TreeListDataControllerBase controller);
        public virtual TreeListNodeBase AddNewNode(TreeListNodeBase parentNode);
        public virtual bool AllowNew(TreeListNodeBase parentNode);
        protected virtual void BeginLoad();
        protected virtual void CalcNodeIds();
        protected internal virtual void CancelNewNode(TreeListNodeBase node);
        protected virtual bool CanPopulate(PropertyDescriptor descriptor);
        protected internal virtual void CheckNewNodePrimaryKeyValidity(TreeListNodeBase node);
        protected virtual DataColumnInfo CreateDataColumn(PropertyDescriptor descriptor);
        protected virtual TreeListUnboundPropertyDescriptor CreateUnboundPropertyDescriptor(UnboundColumnInfo info);
        public virtual void DeleteNode(TreeListNodeBase node, bool deleteChildren, bool modifySource);
        public virtual Action DeleteNodeWithChildrenAndSource(TreeListNodeBase node, bool allowRollback);
        public virtual void Dispose();
        protected virtual void EndLoad();
        protected internal virtual void EndNewNode(TreeListNodeBase node);
        public virtual TreeListNodeBase FindNode(object collection, int listIndex);
        protected virtual PropertyDescriptor GetActualComplexPropertyDescriptor(ComplexColumnInfo info);
        public abstract IEnumerable<IBindingList> GetBindingLists();
        public virtual object GetCellValueByListIndex(int listSourceRowIndex, string fieldName);
        public virtual object GetDataRowByListIndex(int listSourceRowIndex);
        public virtual int GetListIndexByDataRow(object row);
        protected int GetRootParentIndex(TreeListNodeBase node);
        public virtual object GetValue(TreeListNodeBase node, DataColumnInfo columnInfo);
        protected internal object GetValue(TreeListNodeBase node, PropertyDescriptor descriptor);
        public virtual object GetValue(TreeListNodeBase node, string fieldName);
        public virtual object GetValue(object item, string fieldName);
        protected virtual bool IsColumnVisible(DataColumnInfo column);
        protected internal virtual bool IsServicePropertyDescriptor(PropertyDescriptor descriptor);
        public abstract void LoadData();
        public virtual void NodeExpandingCollapsing(TreeListNodeBase node);
        protected virtual void PopulateColumn(PropertyDescriptor descriptor);
        public abstract void PopulateColumns();
        protected virtual void PopulateComplexColumns();
        protected virtual void PopulateUnboundColumns();
        public virtual void RecalcNodeIdsIfNeeded();
        public virtual void ReloadChildNodes(TreeListNodeBase node, IEnumerable children = null);
        protected void RemoveTreeListNode(TreeListNodeBase node);
        public virtual void SetCellValueByListIndex(int listSourceRowIndex, string fieldName, object value);
        public virtual void SetValue(TreeListNodeBase node, DataColumnInfo columnInfo, object value);
        protected void SetValue(TreeListNodeBase node, PropertyDescriptor descriptor, object value);
        public virtual void SetValue(TreeListNodeBase node, string fieldName, object value);
        public void SetValue(object item, string fieldName, object value);
        protected internal virtual void UpdateNodeId(TreeListNodeBase node);

        public abstract Type ItemType { get; }

        public virtual bool IsReady { get; }

        public virtual bool IsUnboundMode { get; }

        protected TreeListDataControllerBase Controller { get; private set; }

        protected IDataProvider DataProvider { get; }

        protected DataColumnInfoCollection Columns { get; }

        public virtual bool IsDeletingChildrenInProgress { get; }

        public abstract bool AllowEdit { get; }

        public abstract bool AllowRemove { get; }

        public abstract bool IsLoaded { get; }

        public bool IsLoading { get; }

        protected internal virtual bool SupportNotifications { get; }

        protected internal bool AddingNewNode { get; set; }
    }
}

