namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Utils;
    using System;
    using System.Windows.Input;

    internal class IndependentMouseButtonEventArgs : IndependentMouseEventArgs, IMouseButtonEventArgs, IMouseEventArgs, IRoutedEventArgs
    {
        private readonly MouseButtonEventArgs component;

        public IndependentMouseButtonEventArgs(MouseButtonEventArgs arg) : base(arg)
        {
            Guard.ArgumentNotNull(arg, "arg");
            this.component = arg;
        }

        MouseButton IMouseButtonEventArgs.ChangedButton =>
            this.component.ChangedButton;
    }
}

