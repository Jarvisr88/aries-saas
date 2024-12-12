namespace DevExpress.Data.Utils
{
    using System;

    public class ValueHolder
    {
        private object value;
        private IValueLoader loader;

        public ValueHolder(IValueLoader loader);

        public virtual object Value { get; }
    }
}

