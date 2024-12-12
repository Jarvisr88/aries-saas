namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ScopeEventArgs : RoutedEventArgs
    {
        public ScopeEventArgs(double delta)
        {
            base.RoutedEvent = UIElement.PreviewMouseWheelEvent;
            this.Delta = delta;
        }

        public double Delta { get; private set; }
    }
}

