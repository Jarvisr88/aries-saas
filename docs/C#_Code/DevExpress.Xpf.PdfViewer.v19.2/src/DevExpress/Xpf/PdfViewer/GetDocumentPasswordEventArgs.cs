namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Windows;

    public class GetDocumentPasswordEventArgs : RoutedEventArgs
    {
        public GetDocumentPasswordEventArgs(string path) : base(PdfViewerControl.GetDocumentPasswordEvent)
        {
            this.Path = path;
        }

        public string Path { get; private set; }

        public SecureString Password { get; set; }
    }
}

