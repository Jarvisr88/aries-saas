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

    public class ContextMenuItemAttributeBuilderProvider : CustomAttributeBuilderProviderBase<ContextMenuItemAttribute>
    {
        internal override Expression<Func<ContextMenuItemAttribute>> GetConstructorExpression() => 
            Expression.Lambda<Func<ContextMenuItemAttribute>>(Expression.New(typeof(ContextMenuItemAttribute)), new ParameterExpression[0]);

        [IteratorStateMachine(typeof(<GetPropertyValuePairs>d__1))]
        internal override IEnumerable<Tuple<PropertyInfo, object>> GetPropertyValuePairs(ContextMenuItemAttribute attribute)
        {
            ParameterExpression expression;
            if (attribute.GetOrder() != null)
            {
                expression = Expression.Parameter(typeof(ContextMenuItemAttribute), "x");
                ParameterExpression[] expressionArray1 = new ParameterExpression[] { expression };
                yield return this.GetPropertyValuePair<ContextMenuItemAttribute, int>(attribute, Expression.Lambda<Func<ContextMenuItemAttribute, int>>(Expression.Property(expression, (MethodInfo) methodof(OrderAttribute.get_Order)), expressionArray1));
            }
            expression = Expression.Parameter(typeof(ContextMenuItemAttribute), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            yield return this.GetPropertyValuePair<ContextMenuItemAttribute, string>(attribute, Expression.Lambda<Func<ContextMenuItemAttribute, string>>(Expression.Property(expression, (MethodInfo) methodof(ContextMenuItemAttribute.get_Group)), parameters));
        }

    }
}

