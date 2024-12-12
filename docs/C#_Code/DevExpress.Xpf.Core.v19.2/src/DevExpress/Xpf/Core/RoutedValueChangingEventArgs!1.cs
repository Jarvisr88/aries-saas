namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class RoutedValueChangingEventArgs<T> : CancelRoutedEventArgs
    {
        public RoutedValueChangingEventArgs(T oldValue, T newValue) : this(null, oldValue, newValue)
        {
        }

        public RoutedValueChangingEventArgs(RoutedEvent routedEvent, T oldValue, T newValue) : base(routedEvent)
        {
            this.NewValue = newValue;
            this.OldValue = oldValue;
        }

        public T OldValue { get; private set; }

        public T NewValue { get; private set; }
    }
}

