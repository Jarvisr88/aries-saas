namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;
    using System.Windows;

    public interface IQueryContinueDragEventArgs : IRoutedEventArgs
    {
        bool EscapePressed { get; }

        DragDropKeyStates KeyStates { get; }

        DragAction Action { get; set; }
    }
}

