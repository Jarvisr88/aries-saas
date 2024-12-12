namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class ZoomOutBarItem : DocumentViewerBarButtonItem
    {
        static ZoomOutBarItem()
        {
            BarItem.KeyGestureProperty.OverrideMetadata(typeof(ZoomOutBarItem), new FrameworkPropertyMetadata(new KeyGesture(Key.OemMinus, ModifierKeys.Control)));
        }
    }
}

