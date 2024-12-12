namespace DMEWorks.Forms
{
    using DMEWorks.Expressions;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public class ArrayGridSource<T> : ArrayViewBase<T>, IGridSource, IBindingList, IList, ICollection, IEnumerable
    {
        internal ArrayGridSource(IEnumerable<T> collection, params string[] properties) : base(collection, properties)
        {
        }

        void IGridSource.ApplyFilterText(string searchString)
        {
            Expression node = DMEWorks.Expressions.Utilities.BuildFilter(base.GetLambdaExpressions(), searchString);
            if (node == null)
            {
                base.ApplyFilterCore(null);
            }
            else
            {
                base.ApplyFilterCore(LambdaExpressionVisitor<T>.CreateExpression(node).Compile());
            }
        }
    }
}

