namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Windows;

    public class PastingFromClipboardEventArgs : GridEventArgs
    {
        public PastingFromClipboardEventArgs(DataControlBase source, RoutedEvent routedEvent) : base(source, routedEvent)
        {
        }
    }
}

