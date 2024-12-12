namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public sealed class GiveRecordDragFeedbackEventArgs : RoutedEventArgs
    {
        public GiveRecordDragFeedbackEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }

        public DragDropEffects Effects { get; internal set; }

        public IDataObject Data { get; internal set; }

        public bool UseDefaultCursors { get; set; }
    }
}

