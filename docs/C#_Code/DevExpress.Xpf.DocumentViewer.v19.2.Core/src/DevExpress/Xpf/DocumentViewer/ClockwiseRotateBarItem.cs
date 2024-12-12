namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class ClockwiseRotateBarItem : DocumentViewerBarButtonItem
    {
        static ClockwiseRotateBarItem()
        {
            BarItem.KeyGestureProperty.OverrideMetadata(typeof(ClockwiseRotateBarItem), new FrameworkPropertyMetadata(new KeyGesture(Key.OemPlus, ModifierKeys.Shift | ModifierKeys.Control)));
        }
    }
}

