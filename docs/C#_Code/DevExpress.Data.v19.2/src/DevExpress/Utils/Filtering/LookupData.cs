namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class LookupData : MetricAttributesData
    {
        internal LookupData(IDictionary<string, object> memberValues) : base(memberValues)
        {
        }

        public object DataSource
        {
            get => 
                base.GetValue<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(LookupData)), (MethodInfo) methodof(LookupData.get_DataSource)), new ParameterExpression[0]));
            set => 
                base.SetValue<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(LookupData)), (MethodInfo) methodof(LookupData.get_DataSource)), new ParameterExpression[0]), value);
        }

        public int Top
        {
            get => 
                base.GetValue<int>(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(LookupData)), (MethodInfo) methodof(LookupData.get_Top)), new ParameterExpression[0]));
            set => 
                base.SetValue<int>(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(LookupData)), (MethodInfo) methodof(LookupData.get_Top)), new ParameterExpression[0]), value);
        }

        public int MaxCount
        {
            get => 
                base.GetValue<int>(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(LookupData)), (MethodInfo) methodof(LookupData.get_MaxCount)), new ParameterExpression[0]));
            set => 
                base.SetValue<int>(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(LookupData)), (MethodInfo) methodof(LookupData.get_MaxCount)), new ParameterExpression[0]), value);
        }
    }
}

