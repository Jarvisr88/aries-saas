namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System.Windows.Input;

    public interface IMouseButtonEventArgs : IMouseEventArgs, IRoutedEventArgs
    {
        MouseButton ChangedButton { get; }
    }
}

