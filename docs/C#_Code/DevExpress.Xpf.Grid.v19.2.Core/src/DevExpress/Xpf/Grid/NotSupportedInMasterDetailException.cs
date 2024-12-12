namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class NotSupportedInMasterDetailException : NotSupportedException
    {
        private const string HelpLinkString = "http://go.devexpress.com/XPF-MasterDetail-Limitations.aspx";
        private const string LearnMoreMessage = " For a complete list of limitations, please see http://go.devexpress.com/XPF-MasterDetail-Limitations.aspx";
        internal const string HitTestInfoCanBeCalculatedOnlyOnTheMasterViewLevel = "Hit testing is supported only by a master view. Hit information cannot be calculated for detail views.";
        internal const string ServerAndInstantFeedbackModeNotSupported = "Server and Instant Feedback UI modes are not supported in master-detail mode. For a complete list of limitations, please see http://go.devexpress.com/XPF-MasterDetail-Limitations.aspx";
        internal const string ICollectionViewNotSupported = "ICollectionView is not supported in master-detail mode. For a complete list of limitations, please see http://go.devexpress.com/XPF-MasterDetail-Limitations.aspx";
        internal const string AutoFilterRowNotSupported = "The Auto-Filter Row feature is not supported by detail grids. For a complete list of limitations, please see http://go.devexpress.com/XPF-MasterDetail-Limitations.aspx";
        internal const string NewItemRowNotSupported = "The New Item Row feature is not supported by detail grids. For a complete list of limitations, please see http://go.devexpress.com/XPF-MasterDetail-Limitations.aspx";
        internal const string CheckBoxSelectionNotSupported = "Selection via the checkbox column is not supported in master-detail mode. For a complete list of limitations, please see http://go.devexpress.com/XPF-MasterDetail-Limitations.aspx";
        internal const string OnlyTableViewSupported = "CardView and TreeListView are not supported in master-detail mode. For a complete list of limitations, please see http://go.devexpress.com/XPF-MasterDetail-Limitations.aspx";
        internal const string OnlyGridControlSupported = "TreeListControl cannot be used to represent detail data. For a complete list of limitations, please see http://go.devexpress.com/XPF-MasterDetail-Limitations.aspx";
        internal const string BandsNotSupported = "Banded layout is not supported in Master-Detail mode. For a complete list of limitations, please see http://go.devexpress.com/XPF-MasterDetail-Limitations.aspx";
        internal const string DragDropNotSupported = "Set the AllowDragDrop property only in the master grid to enable drag-and-drop functionality within detail grids.";
        internal const string VirtualSourceNotSupported = "The GridControl bound to the Virtual data source does not support master-detail data presentation. For a complete list of limitations, please see http://go.devexpress.com/XPF-MasterDetail-Limitations.aspx";
        internal const string KeepViewportOnDataUpdateNotSupported = "Keeping viewport on data update is not supported in Master-Detail mode. For a complete list of limitations, please see http://go.devexpress.com/XPF-MasterDetail-Limitations.aspx";
        internal const string PagingModeNotSupported = "The paged GridControl does not support master-detail data presentation. For a complete list of limitations, please see http://go.devexpress.com/XPF-MasterDetail-Limitations.aspx";

        public NotSupportedInMasterDetailException(string message) : base(message)
        {
            this.HelpLink = "http://go.devexpress.com/XPF-MasterDetail-Limitations.aspx";
        }

        public override string HelpLink { get; set; }
    }
}

