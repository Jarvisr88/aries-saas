namespace ActiproSoftware.ComponentModel
{
    using System;
    using System.Runtime.CompilerServices;

    public class PropertyChangedEventArgs<T> : EventArgs
    {
        [CompilerGenerated]
        private T #5Sb;
        [CompilerGenerated]
        private T #6Sb;

        public PropertyChangedEventArgs(T oldValue, T newValue);

        public T NewValue { get; private set; }

        public T OldValue { get; private set; }
    }
}

