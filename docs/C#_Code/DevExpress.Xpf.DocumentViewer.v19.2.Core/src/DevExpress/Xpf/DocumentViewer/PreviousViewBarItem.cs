namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class PreviousViewBarItem : DocumentViewerBarButtonItem
    {
        static PreviousViewBarItem()
        {
            BarItem.KeyGestureProperty.OverrideMetadata(typeof(PreviousViewBarItem), new FrameworkPropertyMetadata(new KeyGesture(Key.Left, ModifierKeys.Alt)));
        }
    }
}

