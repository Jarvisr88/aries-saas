namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Pdf;
    using System;
    using System.Windows;

    public class MarkupAnnotationGotFocusEventArgs : RoutedEventArgs
    {
        private readonly PdfMarkupAnnotationInfo info;

        internal MarkupAnnotationGotFocusEventArgs(PdfMarkupAnnotationInfo info) : base(PdfViewerControl.MarkupAnnotationGotFocusEvent)
        {
            this.info = info;
        }

        public PdfMarkupAnnotationInfo Info =>
            this.info;
    }
}

