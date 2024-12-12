namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;

    public class PdfBarContainerControl : BarContainerControl
    {
        public PdfBarContainerControl()
        {
            base.DefaultStyleKey = typeof(PdfBarContainerControl);
        }

        protected override DependencyObject GetContainerForItemOverride() => 
            new PdfBarControl();
    }
}

