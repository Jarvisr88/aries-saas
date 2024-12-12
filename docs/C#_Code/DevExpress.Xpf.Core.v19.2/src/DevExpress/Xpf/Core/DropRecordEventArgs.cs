namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public sealed class DropRecordEventArgs : DragEventArgsBase
    {
        public DropRecordEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }
    }
}

