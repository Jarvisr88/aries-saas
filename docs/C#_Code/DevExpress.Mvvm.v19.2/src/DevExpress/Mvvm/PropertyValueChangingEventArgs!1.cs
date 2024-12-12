namespace DevExpress.Mvvm
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class PropertyValueChangingEventArgs<T> : CancelEventArgs
    {
        public PropertyValueChangingEventArgs(T oldValue, T newValue)
        {
            this.NewValue = newValue;
            this.OldValue = oldValue;
        }

        public T NewValue { get; private set; }

        public T OldValue { get; private set; }
    }
}

