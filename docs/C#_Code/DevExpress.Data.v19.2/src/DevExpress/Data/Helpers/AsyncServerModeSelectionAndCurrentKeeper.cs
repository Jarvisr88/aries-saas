namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public class AsyncServerModeSelectionAndCurrentKeeper : ServerModeSelectionAndCurrentKeeper
    {
        private bool allowResetControllerRow;

        public AsyncServerModeSelectionAndCurrentKeeper(AsyncServerModeDataController controller);
        protected override void RestoreCurrentDataRow(ref SelectedRowInfo currentRow, bool allowIndexOf);
        protected override bool RestoreDataRow(ref SelectedRowInfo info, bool allowIndexOf);
        protected override void SaveDataRow(ref SelectedRowInfo info, int listIndex, object selectedObject);

        public AsyncServerModeDataController Controller { get; }

        protected override bool IsAllowIndexOfSearch { get; }

        protected override bool AllowResetControllerRow { get; }
    }
}

