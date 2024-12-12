namespace DevExpress.Data
{
    using System;
    using System.ComponentModel;

    public abstract class BaseListSourceDataController : DataController
    {
        public const int FilterRow = -2147483646;
        public const int NewItemRow = -2147483647;
        protected bool newItemRowEditing;

        protected BaseListSourceDataController();
        public abstract void AddNewRow();
        protected void BeginRowEdit(int controllerRow);
        protected void CancelRowEdit(int controllerRow);
        protected void EndRowEdit(int controllerRow);
        protected IEditableObject GetEditableObject(int controllerRow);
        public override object GetRow(int controllerRow, OperationCompleted completed);
        public override object GetRowValue(int controllerRow, int column, OperationCompleted completed);
        protected override object GetRowValueDetail(int controllerRow, DataColumnInfo column);
        public override bool IsGroupRowHandle(int controllerRowHandle);
        protected internal override void OnEndNewItemRow();
        protected internal override void OnStartNewItemRow();
        protected override void Reset();
        public virtual void SetDataSource(object dataSource);
        protected override void SetRowValueCore(int controllerRow, int column, object val);

        public bool IsNewItemRowEditing { get; }
    }
}

