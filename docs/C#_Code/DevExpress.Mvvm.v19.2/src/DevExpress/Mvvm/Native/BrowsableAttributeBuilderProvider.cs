namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;

    internal class BrowsableAttributeBuilderProvider : CustomAttributeBuilderProviderBase<BrowsableAttribute>
    {
        internal override Expression<Func<BrowsableAttribute>> GetConstructorExpression()
        {
            Expression[] expressionArray1 = new Expression[] { Expression.Constant(true, typeof(bool)) };
            return Expression.Lambda<Func<BrowsableAttribute>>(Expression.New((ConstructorInfo) methodof(BrowsableAttribute..ctor), (IEnumerable<Expression>) expressionArray1), new ParameterExpression[0]);
        }

        internal override IEnumerable<object> GetConstructorParameters(BrowsableAttribute attribute)
        {
            List<object> list1 = new List<object>();
            list1.Add(attribute.Browsable);
            return list1;
        }
    }
}

