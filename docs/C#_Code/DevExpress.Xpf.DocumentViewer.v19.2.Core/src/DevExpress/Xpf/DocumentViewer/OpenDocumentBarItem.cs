namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class OpenDocumentBarItem : DocumentViewerBarButtonItem
    {
        static OpenDocumentBarItem()
        {
            BarItem.KeyGestureProperty.OverrideMetadata(typeof(OpenDocumentBarItem), new FrameworkPropertyMetadata(new KeyGesture(Key.O, ModifierKeys.Control)));
        }
    }
}

