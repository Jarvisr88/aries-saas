namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public abstract class BaseFilterLookupAttribute : FilterAttributeLocalizable
    {
        internal bool? useSelectAll;

        protected BaseFilterLookupAttribute()
        {
        }

        protected override IEnumerable<Expression<Func<string>>> GetLocalizableProperties() => 
            new Expression<Func<string>>[] { Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseFilterLookupAttribute)), (MethodInfo) methodof(BaseFilterLookupAttribute.get_SelectAllName)), new ParameterExpression[0]), Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseFilterLookupAttribute)), (MethodInfo) methodof(BaseFilterLookupAttribute.get_NullName)), new ParameterExpression[0]) };

        public string GetNullName() => 
            base.GetLocalizableValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseFilterLookupAttribute)), (MethodInfo) methodof(BaseFilterLookupAttribute.get_NullName)), new ParameterExpression[0]));

        public string GetSelectAllName() => 
            base.GetLocalizableValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseFilterLookupAttribute)), (MethodInfo) methodof(BaseFilterLookupAttribute.get_SelectAllName)), new ParameterExpression[0]));

        public bool UseSelectAll
        {
            get => 
                this.useSelectAll.GetValueOrDefault(true);
            set => 
                this.useSelectAll = new bool?(value);
        }

        public ValueSelectionMode SelectionMode { get; set; }

        public string SelectAllName
        {
            get => 
                base.GetLocalizablePropertyValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseFilterLookupAttribute)), (MethodInfo) methodof(BaseFilterLookupAttribute.get_SelectAllName)), new ParameterExpression[0]));
            set => 
                base.SetLocalizablePropertyValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseFilterLookupAttribute)), (MethodInfo) methodof(BaseFilterLookupAttribute.get_SelectAllName)), new ParameterExpression[0]), value);
        }

        public string NullName
        {
            get => 
                base.GetLocalizablePropertyValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseFilterLookupAttribute)), (MethodInfo) methodof(BaseFilterLookupAttribute.get_NullName)), new ParameterExpression[0]));
            set => 
                base.SetLocalizablePropertyValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseFilterLookupAttribute)), (MethodInfo) methodof(BaseFilterLookupAttribute.get_NullName)), new ParameterExpression[0]), value);
        }
    }
}

