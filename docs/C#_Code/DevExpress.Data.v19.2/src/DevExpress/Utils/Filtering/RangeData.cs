namespace DevExpress.Utils.Filtering
{
    using DevExpress.Utils.Filtering.Internal;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;

    public class RangeData : MetricAttributesData
    {
        internal RangeData(IDictionary<string, object> memberValues) : base(memberValues)
        {
        }

        private void SetAggregates(Type type, object[] uniqueValues)
        {
            object[] objArray = UniqueValues.Aggregate(type, uniqueValues);
            base.SetValue<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(RangeData)), (MethodInfo) methodof(RangeData.get_Minimum)), new ParameterExpression[0]), objArray[0]);
            base.SetValue<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(RangeData)), (MethodInfo) methodof(RangeData.get_Maximum)), new ParameterExpression[0]), objArray[1]);
            base.SetValue<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(RangeData)), (MethodInfo) methodof(RangeData.get_Average)), new ParameterExpression[0]), objArray[2]);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void SetDates(object[] uniqueDates)
        {
            base.SetValue<object>("UniqueValues", uniqueDates);
            this.SetAggregates(typeof(DateTime), uniqueDates);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void SetTimeSpans(object[] uniqueSpans)
        {
            base.SetValue<object>("UniqueValues", uniqueSpans);
            this.SetAggregates(typeof(TimeSpan), uniqueSpans);
        }

        public object Minimum
        {
            get => 
                base.GetValue<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(RangeData)), (MethodInfo) methodof(RangeData.get_Minimum)), new ParameterExpression[0]));
            set => 
                base.SetValue<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(RangeData)), (MethodInfo) methodof(RangeData.get_Minimum)), new ParameterExpression[0]), value);
        }

        public object Maximum
        {
            get => 
                base.GetValue<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(RangeData)), (MethodInfo) methodof(RangeData.get_Maximum)), new ParameterExpression[0]));
            set => 
                base.SetValue<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(RangeData)), (MethodInfo) methodof(RangeData.get_Maximum)), new ParameterExpression[0]), value);
        }

        public object Average
        {
            get => 
                base.GetValue<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(RangeData)), (MethodInfo) methodof(RangeData.get_Average)), new ParameterExpression[0]));
            set => 
                base.SetValue<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(RangeData)), (MethodInfo) methodof(RangeData.get_Average)), new ParameterExpression[0]), value);
        }
    }
}

