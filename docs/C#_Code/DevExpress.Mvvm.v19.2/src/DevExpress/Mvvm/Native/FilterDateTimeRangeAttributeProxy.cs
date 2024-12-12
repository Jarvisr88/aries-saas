namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class FilterDateTimeRangeAttributeProxy : BaseFilterRangeAttributeProxy
    {
        protected override Attribute CreateRealAttribute()
        {
            Attribute attribute;
            if ((base.MinOrMinMember is string) && (base.MaxOrMaxMember is string))
            {
                attribute = FilterAttributeProxy.DateTimeRangeInitializer((string) base.MinOrMinMember, (string) base.MaxOrMaxMember);
            }
            else
            {
                attribute = FilterAttributeProxy.DateTimeRangeEmptyInitializer();
                base.SetProperty<object>(attribute, "Minimum", base.MinOrMinMember);
                base.SetProperty<object>(attribute, "Maximum", base.MaxOrMaxMember);
            }
            base.SetProperty<string>(attribute, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterDateTimeRangeAttributeProxy)), (MethodInfo) methodof(BaseFilterRangeAttributeProxy.get_FromName)), new ParameterExpression[0]), base.FromName);
            base.SetProperty<string>(attribute, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterDateTimeRangeAttributeProxy)), (MethodInfo) methodof(BaseFilterRangeAttributeProxy.get_ToName)), new ParameterExpression[0]), base.ToName);
            base.SetProperty<FilterDateTimeRangeUIEditorType>(attribute, Expression.Lambda<Func<FilterDateTimeRangeUIEditorType>>(Expression.Property(Expression.Constant(this, typeof(FilterDateTimeRangeAttributeProxy)), (MethodInfo) methodof(FilterDateTimeRangeAttributeProxy.get_EditorType)), new ParameterExpression[0]), this.EditorType);
            return attribute;
        }

        public FilterDateTimeRangeUIEditorType EditorType { get; set; }
    }
}

