namespace DevExpress.Xpf.Core
{
    using System;

    public class ValueChangedEventArgs<T> : EventArgs
    {
        private T _NewValue;
        private T _OldValue;

        public ValueChangedEventArgs(T oldValue, T newValue)
        {
            this._OldValue = oldValue;
            this._NewValue = newValue;
        }

        public T NewValue =>
            this._NewValue;

        public T OldValue =>
            this._OldValue;
    }
}

