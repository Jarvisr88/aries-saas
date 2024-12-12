namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using System;

    public class ServerModeListSourceRowsKeeper : ListSourceRowsKeeper
    {
        public ServerModeListSourceRowsKeeper(ServerModeDataController controller, SelectedRowsKeeper rowsKeeper);
        protected override GroupedRowsKeeperEx CreateGroupRowsKeeper();
        protected override object ExGetGroupRowKeyCore(GroupRowInfo group);
        protected virtual void RestoreRegularRowsSelection();
        protected override void RestoreSelectionCore(int count);

        protected ServerModeDataController Controller { get; }
    }
}

