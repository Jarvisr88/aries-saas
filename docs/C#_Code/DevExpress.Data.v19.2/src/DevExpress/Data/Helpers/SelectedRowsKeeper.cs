namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public class SelectedRowsKeeper : BaseRowsKeeper
    {
        private bool allowKeepSelection;

        public SelectedRowsKeeper(DataController controller, bool allowKeepSelection);
        public virtual void OnRestoreEnd();
        protected internal override void RestoreCore(object row, int level, object value);
        public override void Save();
        protected virtual void SaveDataRow(int controllerRow, object selectedObject);
        protected virtual void SaveGroupInfo(GroupRowInfo group, object selectedObject);
        protected virtual void SaveRowCore(int selectedHandle, object selectedObject);

        public bool AllowKeepSelection { get; }
    }
}

