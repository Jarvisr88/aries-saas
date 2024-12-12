namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class UniqueValuesBooleanChoiceData : BooleanChoiceData
    {
        internal UniqueValuesBooleanChoiceData(IDictionary<string, object> memberValues) : base(memberValues)
        {
        }

        public object UniqueValues
        {
            get => 
                base.GetValue<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(UniqueValuesBooleanChoiceData)), (MethodInfo) methodof(UniqueValuesBooleanChoiceData.get_UniqueValues)), new ParameterExpression[0]));
            set => 
                base.SetValue<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(UniqueValuesBooleanChoiceData)), (MethodInfo) methodof(UniqueValuesBooleanChoiceData.get_UniqueValues)), new ParameterExpression[0]), value);
        }
    }
}

