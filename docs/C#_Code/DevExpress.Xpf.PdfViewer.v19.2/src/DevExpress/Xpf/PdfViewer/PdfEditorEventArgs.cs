namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Windows;

    public class PdfEditorEventArgs : PdfEditorEventArgsBase
    {
        internal PdfEditorEventArgs(RoutedEvent e, string fieldName) : base(e, fieldName)
        {
        }
    }
}

