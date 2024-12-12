namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class CounterClockwiseRotateBarItem : DocumentViewerBarButtonItem
    {
        static CounterClockwiseRotateBarItem()
        {
            BarItem.KeyGestureProperty.OverrideMetadata(typeof(CounterClockwiseRotateBarItem), new FrameworkPropertyMetadata(new KeyGesture(Key.OemMinus, ModifierKeys.Shift | ModifierKeys.Control)));
        }
    }
}

