namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class PrintDocumentBarItem : DocumentViewerBarButtonItem
    {
        static PrintDocumentBarItem()
        {
            BarItem.KeyGestureProperty.OverrideMetadata(typeof(PrintDocumentBarItem), new FrameworkPropertyMetadata(new KeyGesture(Key.P, ModifierKeys.Control)));
        }
    }
}

