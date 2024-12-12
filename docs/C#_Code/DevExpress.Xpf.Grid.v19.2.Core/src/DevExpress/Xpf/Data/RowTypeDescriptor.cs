namespace DevExpress.Xpf.Data
{
    using DevExpress.Data;
    using System;

    public class RowTypeDescriptor : RowTypeDescriptorBase
    {
        private DevExpress.Xpf.Data.RowHandle rowHandle;
        private int listSourceRowIndex;

        public RowTypeDescriptor(WeakReference ownerReference, DevExpress.Xpf.Data.RowHandle rowHandle, int listSourceRowIndex) : base(ownerReference)
        {
            this.rowHandle = rowHandle;
            this.listSourceRowIndex = listSourceRowIndex;
        }

        protected internal override object GetValue(DataColumnInfo info) => 
            this.GetValueCore(info.Name);

        internal override object GetValue(string fieldName) => 
            this.GetValueCore(fieldName);

        private object GetValueCore(string fieldName) => 
            (this.RowHandle.Value == -2147483648) ? ((this.listSourceRowIndex != -1) ? base.Owner.GetCellValueByListIndex(this.listSourceRowIndex, fieldName) : null) : base.Owner.GetRowValue(this.RowHandle.Value, fieldName);

        protected internal override void SetValue(DataColumnInfo info, object value)
        {
            base.Owner.SetRowValue(this.RowHandle, info, value);
        }

        public DevExpress.Xpf.Data.RowHandle RowHandle =>
            this.rowHandle;
    }
}

