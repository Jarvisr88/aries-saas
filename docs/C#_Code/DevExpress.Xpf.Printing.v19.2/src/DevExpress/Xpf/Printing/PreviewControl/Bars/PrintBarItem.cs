namespace DevExpress.Xpf.Printing.PreviewControl.Bars
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class PrintBarItem : DocumentViewerBarButtonItem
    {
        static PrintBarItem()
        {
            BarItem.KeyGestureProperty.OverrideMetadata(typeof(PrintBarItem), new FrameworkPropertyMetadata(new KeyGesture(Key.P, ModifierKeys.Control)));
        }
    }
}

