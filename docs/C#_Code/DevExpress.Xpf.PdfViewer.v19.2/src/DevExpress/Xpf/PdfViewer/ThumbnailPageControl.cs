namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Windows;

    public class ThumbnailPageControl : PageControl
    {
        public ThumbnailPageControl()
        {
            base.RequestBringIntoView += new RequestBringIntoViewEventHandler(this.PdfPageControl_RequestBringIntoView);
        }

        protected override DependencyObject GetContainerForItemOverride() => 
            new PdfThumbnailPageControlItem();

        protected override bool IsItemItsOwnContainerOverride(object item) => 
            item is PdfThumbnailPageControlItem;

        private void PdfPageControl_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }
    }
}

