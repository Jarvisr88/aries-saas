namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class UniqueValuesEnumChoiceData : EnumChoiceData
    {
        internal UniqueValuesEnumChoiceData(IDictionary<string, object> memberValues) : base(memberValues)
        {
        }

        public object UniqueValues
        {
            get => 
                base.GetValue<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(UniqueValuesEnumChoiceData)), (MethodInfo) methodof(UniqueValuesEnumChoiceData.get_UniqueValues)), new ParameterExpression[0]));
            set => 
                base.SetValue<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(UniqueValuesEnumChoiceData)), (MethodInfo) methodof(UniqueValuesEnumChoiceData.get_UniqueValues)), new ParameterExpression[0]), value);
        }
    }
}

