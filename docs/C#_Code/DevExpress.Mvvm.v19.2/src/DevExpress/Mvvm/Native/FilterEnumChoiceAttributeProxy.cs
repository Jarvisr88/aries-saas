namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class FilterEnumChoiceAttributeProxy : BaseFilterLookupAttributeProxy
    {
        protected override Attribute CreateRealAttribute()
        {
            Attribute attr = (base.UseFlags == null) ? FilterAttributeProxy.EnumChoiceEmptyInitializer() : FilterAttributeProxy.EnumChoiceInitializer(base.UseFlags.Value);
            base.SetProperty<FilterLookupUIEditorType>(attr, Expression.Lambda<Func<FilterLookupUIEditorType>>(Expression.Property(Expression.Constant(this, typeof(FilterEnumChoiceAttributeProxy)), (MethodInfo) methodof(BaseFilterLookupAttributeProxy.get_EditorType)), new ParameterExpression[0]), base.EditorType);
            base.SetProperty<string>(attr, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterEnumChoiceAttributeProxy)), (MethodInfo) methodof(BaseFilterLookupAttributeProxy.get_SelectAllName)), new ParameterExpression[0]), base.SelectAllName);
            if (base.UseSelectAll != null)
            {
                base.SetProperty<bool?>(attr, Expression.Lambda<Func<bool?>>(Expression.Property(Expression.Constant(this, typeof(FilterEnumChoiceAttributeProxy)), (MethodInfo) methodof(BaseFilterLookupAttributeProxy.get_UseSelectAll)), new ParameterExpression[0]), new bool?(base.UseSelectAll.Value));
            }
            return attr;
        }
    }
}

