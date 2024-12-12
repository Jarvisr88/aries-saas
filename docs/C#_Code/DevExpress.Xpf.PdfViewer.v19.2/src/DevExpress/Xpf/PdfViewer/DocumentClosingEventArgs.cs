namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DocumentClosingEventArgs : RoutedEventArgs
    {
        public DocumentClosingEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }

        public bool Cancel { get; set; }

        public MessageBoxResult? SaveDialogResult { get; set; }
    }
}

