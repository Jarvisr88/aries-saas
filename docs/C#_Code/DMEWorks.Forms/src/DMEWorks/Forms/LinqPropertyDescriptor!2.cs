namespace DMEWorks.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;

    internal sealed class LinqPropertyDescriptor<T, TOutput> : LinqPropertyDescriptor<T>
    {
        private readonly Func<T, TOutput> getter;
        private readonly Expression<Func<T, TOutput>> expression;

        public LinqPropertyDescriptor(string name, Expression<Func<T, TOutput>> expression) : base(name)
        {
            if (expression == null)
            {
                Expression<Func<T, TOutput>> local1 = expression;
                throw new ArgumentNullException("expression");
            }
            this.expression = expression;
            this.getter = expression.Compile();
        }

        public override bool Equals(object other)
        {
            LinqPropertyDescriptor<T, TOutput> descriptor = other as LinqPropertyDescriptor<T, TOutput>;
            return ((descriptor != null) && ((descriptor.getter == this.getter) && base.Equals(descriptor)));
        }

        public override IComparer<T> GetComparer(ListSortDirection sortDirection) => 
            (sortDirection != ListSortDirection.Ascending) ? ((IComparer<T>) new PropertyComparerDesc<T, TOutput>(this.getter)) : ((IComparer<T>) new PropertyComparerAsc<T, TOutput>(this.getter));

        public override int GetHashCode() => 
            this.NameHashCode;

        public override object GetValue(object component) => 
            this.getter((T) component);

        public override LambdaExpression ToExpression() => 
            this.expression;

        public override Type PropertyType =>
            typeof(TOutput);

        private class PropertyComparerAsc : IComparer<T>
        {
            private readonly IComparer<TOutput> comparer;
            private readonly Func<T, TOutput> getter;

            public PropertyComparerAsc(Func<T, TOutput> getter)
            {
                this.comparer = Comparer<TOutput>.Default;
                if (getter == null)
                {
                    Func<T, TOutput> local1 = getter;
                    throw new ArgumentNullException("getter");
                }
                this.getter = getter;
            }

            public int Compare(T x, T y) => 
                this.comparer.Compare(this.getter(x), this.getter(y));
        }

        private class PropertyComparerDesc : IComparer<T>
        {
            private readonly IComparer<TOutput> comparer;
            private readonly Func<T, TOutput> getter;

            public PropertyComparerDesc(Func<T, TOutput> getter)
            {
                this.comparer = Comparer<TOutput>.Default;
                if (getter == null)
                {
                    Func<T, TOutput> local1 = getter;
                    throw new ArgumentNullException("getter");
                }
                this.getter = getter;
            }

            public int Compare(T x, T y) => 
                this.comparer.Compare(this.getter(y), this.getter(x));
        }
    }
}

