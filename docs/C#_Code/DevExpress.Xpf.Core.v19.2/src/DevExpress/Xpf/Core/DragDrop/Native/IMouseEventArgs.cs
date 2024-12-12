namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System.Windows;
    using System.Windows.Input;

    public interface IMouseEventArgs : IRoutedEventArgs
    {
        Point GetPosition(IInputElement relativeTo);

        MouseButtonState LeftButton { get; }
    }
}

