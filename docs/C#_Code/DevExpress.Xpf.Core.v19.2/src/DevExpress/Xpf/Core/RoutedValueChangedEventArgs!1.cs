namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class RoutedValueChangedEventArgs<T> : RoutedEventArgs
    {
        public RoutedValueChangedEventArgs(T oldValue, T newValue) : this(null, oldValue, newValue)
        {
        }

        public RoutedValueChangedEventArgs(RoutedEvent routedEvent, T oldValue, T newValue) : base(routedEvent)
        {
            this.NewValue = newValue;
            this.OldValue = oldValue;
        }

        public T OldValue { get; private set; }

        public T NewValue { get; private set; }
    }
}

