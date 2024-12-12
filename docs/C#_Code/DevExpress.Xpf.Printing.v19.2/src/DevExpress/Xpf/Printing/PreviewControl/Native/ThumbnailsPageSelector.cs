namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Windows;

    public class ThumbnailsPageSelector : DocumentViewerItemsControl
    {
        public ThumbnailsPageSelector()
        {
            this.SetDefaultStyleKey(typeof(ThumbnailsPageSelector));
        }

        protected override DependencyObject GetContainerForItemOverride() => 
            new ThumbnailPageControl();

        protected override bool IsItemItsOwnContainerOverride(object item) => 
            item is ThumbnailPageControl;
    }
}

