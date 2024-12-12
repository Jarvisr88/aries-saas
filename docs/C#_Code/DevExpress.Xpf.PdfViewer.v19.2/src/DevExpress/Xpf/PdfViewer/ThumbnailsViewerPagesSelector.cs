namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Windows;

    public class ThumbnailsViewerPagesSelector : DocumentViewerItemsControl
    {
        public ThumbnailsViewerPagesSelector()
        {
            this.SetDefaultStyleKey(typeof(ThumbnailsViewerPagesSelector));
        }

        protected override DependencyObject GetContainerForItemOverride() => 
            new ThumbnailPageControl();

        protected override bool IsItemItsOwnContainerOverride(object item) => 
            item is ThumbnailPageControl;
    }
}

