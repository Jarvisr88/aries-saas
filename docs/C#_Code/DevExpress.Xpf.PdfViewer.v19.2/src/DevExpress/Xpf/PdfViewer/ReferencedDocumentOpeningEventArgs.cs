namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ReferencedDocumentOpeningEventArgs : RoutedEventArgs
    {
        public ReferencedDocumentOpeningEventArgs(string documentSource, bool openInNewWindow) : base(PdfViewerControl.ReferencedDocumentOpeningEvent)
        {
            this.DocumentSource = documentSource;
            this.OpenInNewWindow = openInNewWindow;
        }

        public string DocumentSource { get; private set; }

        public bool OpenInNewWindow { get; private set; }

        public bool Cancel { get; set; }
    }
}

