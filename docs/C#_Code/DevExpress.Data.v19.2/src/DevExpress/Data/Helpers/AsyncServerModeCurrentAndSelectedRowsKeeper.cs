namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public class AsyncServerModeCurrentAndSelectedRowsKeeper : CurrentAndSelectedRowsKeeper
    {
        public AsyncServerModeCurrentAndSelectedRowsKeeper(AsyncServerModeDataController controller);
        protected override void RestoreCurrentRow();
        protected override void SaveCurrentRow();

        protected AsyncServerModeDataController Controller { get; }

        protected override bool IsAllowSaveCurrentControllerRow { get; }
    }
}

