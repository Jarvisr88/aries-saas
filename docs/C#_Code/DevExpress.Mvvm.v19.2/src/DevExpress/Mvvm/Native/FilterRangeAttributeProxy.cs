namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class FilterRangeAttributeProxy : BaseFilterRangeAttributeProxy
    {
        protected override Attribute CreateRealAttribute()
        {
            Attribute attr = FilterAttributeProxy.RangeInitializer(base.MinOrMinMember, base.MaxOrMaxMember, null);
            base.SetProperty<string>(attr, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterRangeAttributeProxy)), (MethodInfo) methodof(BaseFilterRangeAttributeProxy.get_FromName)), new ParameterExpression[0]), base.FromName);
            base.SetProperty<string>(attr, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterRangeAttributeProxy)), (MethodInfo) methodof(BaseFilterRangeAttributeProxy.get_ToName)), new ParameterExpression[0]), base.ToName);
            base.SetProperty<FilterRangeUIEditorType>(attr, Expression.Lambda<Func<FilterRangeUIEditorType>>(Expression.Property(Expression.Constant(this, typeof(FilterRangeAttributeProxy)), (MethodInfo) methodof(FilterRangeAttributeProxy.get_EditorType)), new ParameterExpression[0]), this.EditorType);
            return attr;
        }

        public FilterRangeUIEditorType EditorType { get; set; }
    }
}

