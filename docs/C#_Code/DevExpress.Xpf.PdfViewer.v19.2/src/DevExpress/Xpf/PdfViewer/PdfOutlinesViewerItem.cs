namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Pdf.Native;
    using DevExpress.Xpf.DocumentViewer;
    using System;

    public class PdfOutlinesViewerItem : DocumentMapItem
    {
        private readonly PdfOutlineTreeListItem item;
        private readonly Action<PdfOutlineTreeListItem> navigateCallback;

        public PdfOutlinesViewerItem(Action<PdfOutlineTreeListItem> navigateCallback, PdfOutlineTreeListItem item) : base(item.Id, item.ParentId, item.Title)
        {
            this.item = item;
            this.navigateCallback = navigateCallback;
            base.IsExpanded = item.Expanded;
        }

        protected override void CommandExecuted()
        {
            base.CommandExecuted();
            this.navigateCallback(this.item);
        }

        protected override void IsExpandedChanged()
        {
            base.IsExpandedChanged();
            this.item.Expanded = base.IsExpanded;
        }
    }
}

