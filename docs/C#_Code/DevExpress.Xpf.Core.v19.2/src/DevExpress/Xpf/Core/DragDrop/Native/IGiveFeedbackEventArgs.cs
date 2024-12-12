namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;
    using System.Windows;

    public interface IGiveFeedbackEventArgs : IRoutedEventArgs
    {
        bool UseDefaultCursors { get; set; }

        DragDropEffects Effects { get; }
    }
}

