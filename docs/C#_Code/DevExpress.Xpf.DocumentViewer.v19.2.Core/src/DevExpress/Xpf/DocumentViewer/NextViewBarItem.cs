namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class NextViewBarItem : DocumentViewerBarButtonItem
    {
        static NextViewBarItem()
        {
            BarItem.KeyGestureProperty.OverrideMetadata(typeof(NextViewBarItem), new FrameworkPropertyMetadata(new KeyGesture(Key.Right, ModifierKeys.Alt)));
        }
    }
}

