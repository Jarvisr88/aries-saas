namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public class AsyncServerModeGroupedRowsKeeperEx : GroupedRowsKeeperEx
    {
        public AsyncServerModeGroupedRowsKeeperEx(DataController controller);
        protected override bool GetAllRecordsSelected();

        public override bool AllExpanded { get; }
    }
}

