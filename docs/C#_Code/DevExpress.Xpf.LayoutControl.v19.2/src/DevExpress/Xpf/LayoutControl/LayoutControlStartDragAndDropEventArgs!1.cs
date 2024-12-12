namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class LayoutControlStartDragAndDropEventArgs<T> : EventArgs
    {
        public LayoutControlStartDragAndDropEventArgs(T data, System.Windows.Input.MouseEventArgs mouseEventArgs, FrameworkElement source)
        {
            this.Data = data;
            this.MouseEventArgs = mouseEventArgs;
            this.Source = source;
        }

        public T Data { get; private set; }

        public System.Windows.Input.MouseEventArgs MouseEventArgs { get; private set; }

        public FrameworkElement Source { get; private set; }
    }
}

