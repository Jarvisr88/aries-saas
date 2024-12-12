namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public sealed class FilterBooleanChoiceAttribute : FilterAttributeLocalizable
    {
        public FilterBooleanChoiceAttribute()
        {
        }

        public FilterBooleanChoiceAttribute(bool defaultValue)
        {
            this.DefaultValue = new bool?(defaultValue);
        }

        public FilterBooleanChoiceAttribute(string defaultValueOrDefaultValueMember)
        {
            bool flag;
            if (!bool.TryParse(defaultValueOrDefaultValueMember, out flag))
            {
                this.DefaultValueMember = defaultValueOrDefaultValueMember;
            }
            else
            {
                this.DefaultValue = new bool?(flag);
            }
        }

        public string GetDefaultName() => 
            base.GetLocalizableValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterBooleanChoiceAttribute)), (MethodInfo) methodof(FilterBooleanChoiceAttribute.get_DefaultName)), new ParameterExpression[0]));

        public string GetFalseName() => 
            base.GetLocalizableValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterBooleanChoiceAttribute)), (MethodInfo) methodof(FilterBooleanChoiceAttribute.get_FalseName)), new ParameterExpression[0]));

        protected override IEnumerable<Expression<Func<string>>> GetLocalizableProperties() => 
            new Expression<Func<string>>[] { Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterBooleanChoiceAttribute)), (MethodInfo) methodof(FilterBooleanChoiceAttribute.get_TrueName)), new ParameterExpression[0]), Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterBooleanChoiceAttribute)), (MethodInfo) methodof(FilterBooleanChoiceAttribute.get_FalseName)), new ParameterExpression[0]), Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterBooleanChoiceAttribute)), (MethodInfo) methodof(FilterBooleanChoiceAttribute.get_DefaultName)), new ParameterExpression[0]) };

        protected internal override string[] GetMembers() => 
            new string[] { this.DefaultValueMember };

        public string GetTrueName() => 
            base.GetLocalizableValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterBooleanChoiceAttribute)), (MethodInfo) methodof(FilterBooleanChoiceAttribute.get_TrueName)), new ParameterExpression[0]));

        public BooleanUIEditorType EditorType { get; set; }

        public bool? DefaultValue { get; private set; }

        public string DefaultValueMember { get; set; }

        public string TrueName
        {
            get => 
                base.GetLocalizablePropertyValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterBooleanChoiceAttribute)), (MethodInfo) methodof(FilterBooleanChoiceAttribute.get_TrueName)), new ParameterExpression[0]));
            set => 
                base.SetLocalizablePropertyValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterBooleanChoiceAttribute)), (MethodInfo) methodof(FilterBooleanChoiceAttribute.get_TrueName)), new ParameterExpression[0]), value);
        }

        public string FalseName
        {
            get => 
                base.GetLocalizablePropertyValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterBooleanChoiceAttribute)), (MethodInfo) methodof(FilterBooleanChoiceAttribute.get_FalseName)), new ParameterExpression[0]));
            set => 
                base.SetLocalizablePropertyValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterBooleanChoiceAttribute)), (MethodInfo) methodof(FilterBooleanChoiceAttribute.get_FalseName)), new ParameterExpression[0]), value);
        }

        public string DefaultName
        {
            get => 
                base.GetLocalizablePropertyValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterBooleanChoiceAttribute)), (MethodInfo) methodof(FilterBooleanChoiceAttribute.get_DefaultName)), new ParameterExpression[0]));
            set => 
                base.SetLocalizablePropertyValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterBooleanChoiceAttribute)), (MethodInfo) methodof(FilterBooleanChoiceAttribute.get_DefaultName)), new ParameterExpression[0]), value);
        }
    }
}

