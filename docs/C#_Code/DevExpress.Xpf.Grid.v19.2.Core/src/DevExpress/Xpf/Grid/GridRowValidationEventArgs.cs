namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;

    public class GridRowValidationEventArgs : ValidationEventArgs, IDataRowEventArgs
    {
        protected readonly DataViewBase view;
        protected readonly int fRowHandle;

        public GridRowValidationEventArgs(object source, object value, int rowHandle, DataViewBase view) : base(BaseEdit.ValidateEvent, source, value, CultureInfo.CurrentCulture)
        {
            this.view = view;
            this.fRowHandle = rowHandle;
        }

        protected DataControlBase DataControl =>
            this.view.DataControl;

        public virtual int RowHandle =>
            this.fRowHandle;

        public virtual object Row =>
            this.DataControl.GetRow(this.RowHandle);
    }
}

