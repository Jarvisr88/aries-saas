namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using System;

    public class ServerModeGroupedRowsKeeperEx : GroupedRowsKeeperEx
    {
        public ServerModeGroupedRowsKeeperEx(DataController controller);
        protected override bool GetAllRecordsSelected();

        public override bool AllExpanded { get; }
    }
}

