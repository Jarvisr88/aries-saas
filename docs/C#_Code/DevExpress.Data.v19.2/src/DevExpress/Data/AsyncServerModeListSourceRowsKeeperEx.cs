namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using System;

    public class AsyncServerModeListSourceRowsKeeperEx : ListSourceRowsKeeper
    {
        public AsyncServerModeListSourceRowsKeeperEx(AsyncServerModeDataController controller, SelectedRowsKeeper rowsKeeper);
        protected override GroupedRowsKeeperEx CreateGroupRowsKeeper();
        protected override object ExGetGroupRowKeyCore(GroupRowInfo group);
        protected virtual void RestoreRegularRowsSelection();
        protected override void RestoreSelectionCore(int count);

        protected AsyncServerModeDataController Controller { get; }
    }
}

