namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Pdf;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class SelectionEventArgs : RoutedEventArgs
    {
        public SelectionEventArgs(PdfDocumentPosition position)
        {
            this.DocumentPosition = position;
        }

        public PdfDocumentPosition DocumentPosition { get; private set; }
    }
}

