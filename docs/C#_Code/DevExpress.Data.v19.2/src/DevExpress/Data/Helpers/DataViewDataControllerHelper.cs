namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Runtime.CompilerServices;

    public class DataViewDataControllerHelper : BaseDataViewControllerHelper
    {
        private DataViewDataControllerHelper.FakeGetRow getRowDelegate;

        public DataViewDataControllerHelper(DataControllerBase controller);
        protected internal override void CancelNewItemRow();
        public override void Dispose();
        private DataRow dummy(int row);
        protected override DataRow GetDataRow(int listSourceRow);
        protected override Delegate GetGetRowValueCore(DataColumnInfo columnInfo, Type expectedReturnType);
        protected override PropertyDescriptorCollection GetPropertyDescriptorCollection();
        public override object GetRowValue(int listSourceRow, DataColumnInfo columnInfo, OperationCompleted completed);
        private void InitFastGetRow();

        public override DataView View { get; }

        private delegate DataRow FakeGetRow(DataView view, int row);
    }
}

