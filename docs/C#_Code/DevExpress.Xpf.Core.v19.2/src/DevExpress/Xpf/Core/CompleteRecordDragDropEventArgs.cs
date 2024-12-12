namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public sealed class CompleteRecordDragDropEventArgs : RoutedEventArgs
    {
        public CompleteRecordDragDropEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }

        public DragDropEffects Effects { get; set; }

        public object[] Records { get; internal set; }

        public bool Canceled { get; internal set; }
    }
}

