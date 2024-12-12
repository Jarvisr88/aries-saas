namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class UriOpeningEventArgs : RoutedEventArgs
    {
        public UriOpeningEventArgs(System.Uri uri) : base(PdfViewerControl.UriOpeningEvent)
        {
            this.Uri = uri;
        }

        public System.Uri Uri { get; private set; }

        public bool Cancel { get; set; }
    }
}

