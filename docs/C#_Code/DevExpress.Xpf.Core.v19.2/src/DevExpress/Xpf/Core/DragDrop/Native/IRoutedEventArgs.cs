namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;

    public interface IRoutedEventArgs
    {
        bool Handled { get; set; }

        object OriginalSource { get; }
    }
}

