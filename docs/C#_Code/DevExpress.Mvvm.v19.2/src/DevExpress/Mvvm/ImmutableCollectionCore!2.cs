namespace DevExpress.Mvvm
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public abstract class ImmutableCollectionCore<T, TCollection> : ReadOnlyCollection<T> where TCollection: ReadOnlyCollection<T>
    {
        protected ImmutableCollectionCore() : base(new List<T>())
        {
        }

        protected ImmutableCollectionCore(IEnumerable<T> values) : base(values.ToList<T>())
        {
        }

        protected abstract TCollection Create(IEnumerable<T> values);
        internal TCollection SelectCore(Func<T, T> selector) => 
            this.Create(this.Select<T, T>(selector));

        internal TCollection SelectCore(Func<T, int, T> selector) => 
            this.Create(this.Select<T, T>(selector));

        public TCollection SetElementAt(int index, T value)
        {
            T[] values = this.ToArray<T>();
            values[index] = value;
            return this.Create(values);
        }
    }
}

