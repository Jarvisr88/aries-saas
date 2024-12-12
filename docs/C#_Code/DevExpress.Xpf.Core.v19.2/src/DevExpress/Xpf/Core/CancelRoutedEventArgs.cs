namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CancelRoutedEventArgs : RoutedEventArgs
    {
        public CancelRoutedEventArgs()
        {
        }

        public CancelRoutedEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }

        public CancelRoutedEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }

        public bool Cancel { get; set; }
    }
}

