namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class FloatingContainerEventArgs : RoutedEventArgs
    {
        public FloatingContainerEventArgs(FloatingContainer container)
        {
            this.Container = container;
        }

        public FloatingContainer Container { get; private set; }
    }
}

