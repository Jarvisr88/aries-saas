namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class SaveAsBarItem : DocumentViewerBarButtonItem
    {
        static SaveAsBarItem()
        {
            BarItem.KeyGestureProperty.OverrideMetadata(typeof(SaveAsBarItem), new FrameworkPropertyMetadata(new KeyGesture(Key.S, ModifierKeys.Control)));
        }
    }
}

