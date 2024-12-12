namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public class DependencyPropertyChangedEventArgs<T>
    {
        private T oldValue;
        private T newValue;

        public DependencyPropertyChangedEventArgs(DependencyPropertyChangedEventArgs e);

        public T OldValue { get; }

        public T NewValue { get; }
    }
}

