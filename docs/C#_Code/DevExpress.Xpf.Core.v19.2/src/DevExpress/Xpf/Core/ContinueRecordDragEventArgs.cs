namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public sealed class ContinueRecordDragEventArgs : RoutedEventArgs
    {
        public ContinueRecordDragEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }

        public bool EscapePressed { get; internal set; }

        public DragDropKeyStates KeyStates { get; internal set; }

        public IDataObject Data { get; internal set; }

        public DragAction Action { get; set; }
    }
}

