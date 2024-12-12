namespace DevExpress.Mvvm.Native
{
    using DevExpress.Mvvm.DataAnnotations;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ToolBarItemAttributeBuilderProvider : CustomAttributeBuilderProviderBase<ToolBarItemAttribute>
    {
        internal override Expression<Func<ToolBarItemAttribute>> GetConstructorExpression() => 
            Expression.Lambda<Func<ToolBarItemAttribute>>(Expression.New(typeof(ToolBarItemAttribute)), new ParameterExpression[0]);

        [IteratorStateMachine(typeof(<GetPropertyValuePairs>d__1))]
        internal override IEnumerable<Tuple<PropertyInfo, object>> GetPropertyValuePairs(ToolBarItemAttribute attribute)
        {
            ParameterExpression expression;
            if (attribute.GetOrder() != null)
            {
                expression = Expression.Parameter(typeof(ToolBarItemAttribute), "x");
                ParameterExpression[] expressionArray1 = new ParameterExpression[] { expression };
                yield return this.GetPropertyValuePair<ToolBarItemAttribute, int>(attribute, Expression.Lambda<Func<ToolBarItemAttribute, int>>(Expression.Property(expression, (MethodInfo) methodof(OrderAttribute.get_Order)), expressionArray1));
            }
            expression = Expression.Parameter(typeof(ToolBarItemAttribute), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            yield return this.GetPropertyValuePair<ToolBarItemAttribute, string>(attribute, Expression.Lambda<Func<ToolBarItemAttribute, string>>(Expression.Property(expression, (MethodInfo) methodof(ToolBarItemAttribute.get_Page)), parameters));
            expression = Expression.Parameter(typeof(ToolBarItemAttribute), "x");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            yield return this.GetPropertyValuePair<ToolBarItemAttribute, string>(attribute, Expression.Lambda<Func<ToolBarItemAttribute, string>>(Expression.Property(expression, (MethodInfo) methodof(ToolBarItemAttribute.get_PageGroup)), expressionArray3));
        }

    }
}

