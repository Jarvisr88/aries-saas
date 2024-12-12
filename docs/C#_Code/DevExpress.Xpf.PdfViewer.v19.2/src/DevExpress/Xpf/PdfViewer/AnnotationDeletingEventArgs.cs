namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Pdf;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class AnnotationDeletingEventArgs : RoutedEventArgs
    {
        public AnnotationDeletingEventArgs(int pageNumber, PdfRectangle rect, string name) : base(PdfViewerControl.AnnotationDeletingEvent)
        {
            this.PageNumber = pageNumber;
            this.Rectangle = rect;
            this.Name = name;
        }

        public int PageNumber { get; private set; }

        public PdfRectangle Rectangle { get; private set; }

        public string Name { get; private set; }

        public bool Cancel { get; set; }
    }
}

