namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class FindTextBarItem : DocumentViewerBarButtonItem
    {
        static FindTextBarItem()
        {
            BarItem.KeyGestureProperty.OverrideMetadata(typeof(FindTextBarItem), new FrameworkPropertyMetadata(new KeyGesture(Key.F, ModifierKeys.Control)));
        }

        protected override void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
        {
            base.OnCommandChanged(oldCommand, newCommand);
        }
    }
}

