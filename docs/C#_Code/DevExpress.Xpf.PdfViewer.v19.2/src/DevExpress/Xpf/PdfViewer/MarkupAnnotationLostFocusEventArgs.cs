namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Pdf;
    using System;
    using System.Windows;

    public class MarkupAnnotationLostFocusEventArgs : RoutedEventArgs
    {
        private readonly PdfMarkupAnnotationInfo info;

        internal MarkupAnnotationLostFocusEventArgs(PdfMarkupAnnotationInfo info) : base(PdfViewerControl.MarkupAnnotationLostFocusEvent)
        {
            this.info = info;
        }

        public PdfMarkupAnnotationInfo Info =>
            this.info;
    }
}

