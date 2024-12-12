namespace DevExpress.Xpf.Printing.PreviewControl.Bars
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class CopyBarItem : DocumentViewerBarButtonItem
    {
        static CopyBarItem()
        {
            BarItem.KeyGestureProperty.OverrideMetadata(typeof(CopyBarItem), new FrameworkPropertyMetadata(new KeyGesture(Key.C, ModifierKeys.Control)));
        }
    }
}

