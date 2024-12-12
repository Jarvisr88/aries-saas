namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Utils;
    using System;
    using System.Windows;
    using System.Windows.Input;

    internal class IndependentMouseEventArgs : IndependentRoutedEventArgs, IMouseEventArgs, IRoutedEventArgs
    {
        private readonly MouseEventArgs component;

        public IndependentMouseEventArgs(MouseEventArgs arg) : base(arg)
        {
            Guard.ArgumentNotNull(arg, "arg");
            this.component = arg;
        }

        Point IMouseEventArgs.GetPosition(IInputElement relativeTo) => 
            this.component.GetPosition(relativeTo);

        MouseButtonState IMouseEventArgs.LeftButton =>
            this.component.LeftButton;
    }
}

