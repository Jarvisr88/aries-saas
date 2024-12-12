namespace DevExpress.Xpf.Core.ServerMode
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class GetEnumerableEventArgs : RoutedEventArgs
    {
        public IEnumerable ItemsSource { get; set; }

        public object Tag { get; set; }
    }
}

