namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class ZoomInBarItem : DocumentViewerBarButtonItem
    {
        static ZoomInBarItem()
        {
            BarItem.KeyGestureProperty.OverrideMetadata(typeof(ZoomInBarItem), new FrameworkPropertyMetadata(new KeyGesture(Key.OemPlus, ModifierKeys.Control)));
        }
    }
}

