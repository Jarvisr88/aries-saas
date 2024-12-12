namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class SelectAllBarItem : DocumentViewerBarButtonItem
    {
        static SelectAllBarItem()
        {
            BarItem.KeyGestureProperty.OverrideMetadata(typeof(SelectAllBarItem), new FrameworkPropertyMetadata(new KeyGesture(Key.A, ModifierKeys.Control)));
        }
    }
}

