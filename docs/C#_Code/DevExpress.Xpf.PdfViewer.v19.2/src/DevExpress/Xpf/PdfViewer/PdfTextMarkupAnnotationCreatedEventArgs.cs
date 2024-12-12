namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PdfTextMarkupAnnotationCreatedEventArgs : RoutedEventArgs
    {
        internal PdfTextMarkupAnnotationCreatedEventArgs(string name) : base(PdfViewerControl.TextMarkupAnnotationCreatedEvent)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}

