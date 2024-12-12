namespace DevExpress.Mvvm
{
    using System;
    using System.Runtime.CompilerServices;

    public class PropertyValueChangedEventArgs<T> : EventArgs
    {
        public PropertyValueChangedEventArgs(T oldValue, T newValue)
        {
            this.NewValue = newValue;
            this.OldValue = oldValue;
        }

        public T NewValue { get; private set; }

        public T OldValue { get; private set; }
    }
}

