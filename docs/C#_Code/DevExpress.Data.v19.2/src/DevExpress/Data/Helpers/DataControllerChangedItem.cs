namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public class DataControllerChangedItem
    {
        private int controllerRowHandle;
        private NotifyChangeType changedType;
        private bool visible;
        private int visibleIndex;
        private bool groupSimpleChange;

        public DataControllerChangedItem(int controllerRowHandle, NotifyChangeType changedType, GroupRowInfo parentGroupRow, bool groupSimpleChange);
        public bool IsEqual(int controllerRowHandle, NotifyChangeType changedType);

        public bool GroupSimpleChange { get; }

        public int ControllerRowHandle { get; }

        public NotifyChangeType ChangedType { get; }

        public bool Visible { get; }

        public int VisibleIndex { get; set; }
    }
}

