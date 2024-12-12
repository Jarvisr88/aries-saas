namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public static class RowKeeperFactory
    {
        public static IClassicRowKeeper CreateAsyncRowsKeeperKeeper(AsyncServerModeDataController controller);
        public static IClassicRowKeeper CreateCurrentRowsKeeper(BaseGridController controller);
        public static IClassicRowKeeper CreateListSourceRowsKeeper(DataController controller);
        public static IClassicRowKeeper CreateRowKeeper<T>(T controller, ExpandedGroupKeeper groupsKeeper, SelectionKeeper selectionKeeper) where T: DataController;
        public static IClassicRowKeeper CreateServerModeRowsKeeper(ServerModeDataControllerBase controller);
    }
}

