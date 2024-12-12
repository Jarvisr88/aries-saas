namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public class ThemedWindowBackButtonEventArgs : RoutedEventArgs
    {
        public ThemedWindowBackButtonEventArgs(RoutedEvent routedEvent, object source)
        {
            base.RoutedEvent = routedEvent;
            base.Source = source;
        }
    }
}

