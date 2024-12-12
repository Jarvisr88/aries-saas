namespace DevExpress.XtraReports.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class ExpressionBindingCollectionBase<T> : Collection<T> where T: BasicExpressionBinding
    {
        internal const StringComparison Comparison = StringComparison.OrdinalIgnoreCase;

        public void AddRange(IEnumerable<T> items)
        {
            foreach (T local in items)
            {
                base.Add(local);
            }
        }

        public void AddRange(T[] items)
        {
            foreach (T local in items)
            {
                base.Add(local);
            }
        }
    }
}

