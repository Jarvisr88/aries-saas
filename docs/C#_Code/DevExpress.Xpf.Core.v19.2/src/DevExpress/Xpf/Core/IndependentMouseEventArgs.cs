namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public abstract class IndependentMouseEventArgs : RoutedEventArgs
    {
        private readonly object originalSource;

        public IndependentMouseEventArgs(object originalSource)
        {
            this.originalSource = originalSource;
        }

        public abstract void CaptureMouse(UIElement element);
        public abstract Point GetPosition(UIElement relativeTo);
        public abstract void ReleaseMouseCapture(UIElement element);

        public object OriginalSource =>
            this.originalSource;

        public abstract MouseButtonState LeftButton { get; }
    }
}

