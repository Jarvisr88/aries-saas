namespace DevExpress.Xpf.Printing.PreviewControl.Native
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
            new ThumbnailPageControlItem();

        protected override bool IsItemItsOwnContainerOverride(object item) => 
            item is ThumbnailPageControlItem;

        private void PdfPageControl_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }
    }
}

