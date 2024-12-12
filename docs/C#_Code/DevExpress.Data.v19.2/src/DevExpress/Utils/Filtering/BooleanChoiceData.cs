namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class BooleanChoiceData : MetricAttributesData
    {
        internal BooleanChoiceData(IDictionary<string, object> memberValues) : base(memberValues)
        {
        }

        public bool DefaultValue
        {
            get => 
                base.GetValue<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(BooleanChoiceData)), (MethodInfo) methodof(BooleanChoiceData.get_DefaultValue)), new ParameterExpression[0]));
            set => 
                base.SetValue<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(BooleanChoiceData)), (MethodInfo) methodof(BooleanChoiceData.get_DefaultValue)), new ParameterExpression[0]), value);
        }
    }
}

