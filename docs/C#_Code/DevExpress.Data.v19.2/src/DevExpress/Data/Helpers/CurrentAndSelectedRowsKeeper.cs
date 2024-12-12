namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public class CurrentAndSelectedRowsKeeper : SelectedRowsKeeper
    {
        private int groupCount;
        private int foundCurrentRow;

        public CurrentAndSelectedRowsKeeper(DataController controller, bool allowKeepSelection);
        public override void OnRestoreEnd();
        protected internal override void RestoreCore(object row, int level, object value);
        protected virtual void RestoreCurrentRow();
        public override void Save();
        protected virtual void SaveCurrentRow();

        protected BaseGridController Controller { get; }

        protected virtual bool IsAllowSaveCurrentControllerRow { get; }

        protected class CurrentRowData
        {
            public object SelectedObject;
            public int Index;

            public CurrentRowData(object selectedObject);
            public CurrentRowData(object selectedObject, int index);
        }
    }
}

