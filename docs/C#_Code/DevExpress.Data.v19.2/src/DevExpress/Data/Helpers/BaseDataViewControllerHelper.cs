namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;

    public abstract class BaseDataViewControllerHelper : ListDataControllerHelper, IRelationListEx, IRelationList
    {
        private DataRow detachedRow;

        public BaseDataViewControllerHelper(DataControllerBase controller);
        protected override object AddNewRowCore();
        private bool CanIgnoreItemChanged();
        private bool CanIgnoreMoveEvent();
        IList IRelationList.GetDetailList(int listSourceRow, int relationIndex);
        string IRelationList.GetRelationName(int listSourceRow, int relationIndex);
        bool IRelationList.IsMasterRowEmpty(int listSourceRow, int relationIndex);
        int IRelationListEx.GetRelationCount(int listSourceRow);
        private DataRelation GetDataRelation(int relationIndex);
        protected virtual DataRow GetDataRow(int listSourceRow);
        private DataRow GetDataRowCore(int listSourceRow);
        private DataColumnInfo GetDetailInfo(int relationIndex);
        public override IDataErrorInfo GetRowErrorInfo(int listSourceRow);
        public override object GetRowKey(int listSourceRow);
        private bool IsValidRelation(int relationIndex);
        protected internal override void OnBindingListChanged(ListChangedEventArgs e);
        protected override void PopulateColumn(PropertyDescriptor descriptor);
        protected internal override void RaiseOnEndNewItemRow();
        protected internal override void RaiseOnStartNewItemRow();
        protected internal override void SetDetachedListSourceRow(int listSourceRow);
        public override void SetRowValue(int listSourceRow, int column, object val);
        protected override int TryIndexOf(object rowKey);

        public override bool CaseSensitive { get; }

        public abstract DataView View { get; }
    }
}

