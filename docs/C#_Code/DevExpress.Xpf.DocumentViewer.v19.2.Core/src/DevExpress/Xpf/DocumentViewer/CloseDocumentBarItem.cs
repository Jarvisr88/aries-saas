namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class CloseDocumentBarItem : DocumentViewerBarButtonItem
    {
        static CloseDocumentBarItem()
        {
            BarItem.KeyGestureProperty.OverrideMetadata(typeof(CloseDocumentBarItem), new FrameworkPropertyMetadata(new KeyGesture(Key.W, ModifierKeys.Control)));
        }
    }
}

