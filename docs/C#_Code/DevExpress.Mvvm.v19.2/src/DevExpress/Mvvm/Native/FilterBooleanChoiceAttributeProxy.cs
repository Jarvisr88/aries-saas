namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class FilterBooleanChoiceAttributeProxy : FilterAttributeProxy
    {
        protected override Attribute CreateRealAttribute()
        {
            Attribute attr = (this.DefaultValue != null) ? FilterAttributeProxy.BooleanChoiceWithDefaultValueInitializer(this.DefaultValue.Value) : FilterAttributeProxy.BooleanChoiceInitializer();
            base.SetProperty<FilterBooleanUIEditorType>(attr, Expression.Lambda<Func<FilterBooleanUIEditorType>>(Expression.Property(Expression.Constant(this, typeof(FilterBooleanChoiceAttributeProxy)), (MethodInfo) methodof(FilterBooleanChoiceAttributeProxy.get_EditorType)), new ParameterExpression[0]), this.EditorType);
            base.SetProperty<string>(attr, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterBooleanChoiceAttributeProxy)), (MethodInfo) methodof(FilterBooleanChoiceAttributeProxy.get_TrueName)), new ParameterExpression[0]), this.TrueName);
            base.SetProperty<string>(attr, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterBooleanChoiceAttributeProxy)), (MethodInfo) methodof(FilterBooleanChoiceAttributeProxy.get_FalseName)), new ParameterExpression[0]), this.FalseName);
            base.SetProperty<string>(attr, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterBooleanChoiceAttributeProxy)), (MethodInfo) methodof(FilterBooleanChoiceAttributeProxy.get_DefaultName)), new ParameterExpression[0]), this.DefaultName);
            base.SetProperty<string>(attr, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterBooleanChoiceAttributeProxy)), (MethodInfo) methodof(FilterBooleanChoiceAttributeProxy.get_DefaultValueMember)), new ParameterExpression[0]), this.DefaultValueMember);
            return attr;
        }

        public FilterBooleanUIEditorType EditorType { get; set; }

        public string TrueName { get; set; }

        public string FalseName { get; set; }

        public string DefaultName { get; set; }

        public bool? DefaultValue { get; set; }

        public string DefaultValueMember { get; set; }
    }
}

