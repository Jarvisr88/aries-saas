namespace DevExpress.Xpf.Printing.PreviewControl.Bars
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class SaveBarItem : DocumentViewerBarButtonItem
    {
        static SaveBarItem()
        {
            BarItem.KeyGestureProperty.OverrideMetadata(typeof(SaveBarItem), new FrameworkPropertyMetadata(new KeyGesture(Key.S, ModifierKeys.Control)));
        }
    }
}

