namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public class ServerModeCurrentAndSelectedRowsKeeper : CurrentAndSelectedRowsKeeper
    {
        public ServerModeCurrentAndSelectedRowsKeeper(ServerModeDataController controller, bool allowKeepSelection);

        protected override bool IsAllowSaveCurrentControllerRow { get; }
    }
}

