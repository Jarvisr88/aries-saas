namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public class ServerModeSelectionAndCurrentKeeper : SelectionAndCurrentKeeper
    {
        public ServerModeSelectionAndCurrentKeeper(ServerModeDataControllerBase controller);
        protected override bool IsAllowRestoreCurrentRow();
        protected override void RestoreDataRowsAlt();

        public ServerModeDataControllerBase Controller { get; }

        protected override bool IsAllowIndexOfSearch { get; }
    }
}

