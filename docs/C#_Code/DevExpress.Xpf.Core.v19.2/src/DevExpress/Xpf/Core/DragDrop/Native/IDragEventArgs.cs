namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;
    using System.Windows;

    public interface IDragEventArgs : IRoutedEventArgs
    {
        Point GetPosition(IInputElement relativeTo);

        IDataObject Data { get; }

        DragDropKeyStates KeyStates { get; }

        DragDropEffects AllowedEffects { get; }

        DragDropEffects Effects { get; set; }
    }
}

