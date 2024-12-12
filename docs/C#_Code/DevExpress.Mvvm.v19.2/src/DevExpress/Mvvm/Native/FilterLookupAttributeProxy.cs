namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class FilterLookupAttributeProxy : BaseFilterLookupAttributeProxy
    {
        protected override Attribute CreateRealAttribute()
        {
            Attribute attribute;
            if ((this.TopOrTopMember is int) && (this.MaxCountOrMaxCountMember is int))
            {
                attribute = FilterAttributeProxy.LookupInitializer(this.DataSourceOrDataSourceMember, (int) this.TopOrTopMember, (int) this.MaxCountOrMaxCountMember);
            }
            else
            {
                attribute = FilterAttributeProxy.LookupInitializer(this.DataSourceOrDataSourceMember, 0, 0);
                if (this.TopOrTopMember is string)
                {
                    base.SetProperty<object>(attribute, "TopMember", this.TopOrTopMember);
                }
                if (this.MaxCountOrMaxCountMember is string)
                {
                    base.SetProperty<object>(attribute, "MaxCountMember", this.MaxCountOrMaxCountMember);
                }
            }
            base.SetProperty<FilterLookupUIEditorType>(attribute, Expression.Lambda<Func<FilterLookupUIEditorType>>(Expression.Property(Expression.Constant(this, typeof(FilterLookupAttributeProxy)), (MethodInfo) methodof(BaseFilterLookupAttributeProxy.get_EditorType)), new ParameterExpression[0]), base.EditorType);
            base.SetProperty<string>(attribute, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterLookupAttributeProxy)), (MethodInfo) methodof(FilterLookupAttributeProxy.get_ValueMember)), new ParameterExpression[0]), this.ValueMember);
            base.SetProperty<string>(attribute, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterLookupAttributeProxy)), (MethodInfo) methodof(FilterLookupAttributeProxy.get_DisplayMember)), new ParameterExpression[0]), this.DisplayMember);
            base.SetProperty<string>(attribute, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterLookupAttributeProxy)), (MethodInfo) methodof(BaseFilterLookupAttributeProxy.get_SelectAllName)), new ParameterExpression[0]), base.SelectAllName);
            if (base.UseFlags != null)
            {
                base.SetProperty<bool?>(attribute, Expression.Lambda<Func<bool?>>(Expression.Property(Expression.Constant(this, typeof(FilterLookupAttributeProxy)), (MethodInfo) methodof(BaseFilterLookupAttributeProxy.get_UseFlags)), new ParameterExpression[0]), new bool?(base.UseFlags.Value));
            }
            if (base.UseSelectAll != null)
            {
                base.SetProperty<bool?>(attribute, Expression.Lambda<Func<bool?>>(Expression.Property(Expression.Constant(this, typeof(FilterLookupAttributeProxy)), (MethodInfo) methodof(BaseFilterLookupAttributeProxy.get_UseSelectAll)), new ParameterExpression[0]), new bool?(base.UseSelectAll.Value));
            }
            return attribute;
        }

        public object DataSourceOrDataSourceMember { get; set; }

        public string ValueMember { get; set; }

        public string DisplayMember { get; set; }

        public object TopOrTopMember { get; set; }

        public object MaxCountOrMaxCountMember { get; set; }
    }
}

