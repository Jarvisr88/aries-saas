namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class BaseFilterRangeAttribute : FilterAttributeLocalizable
    {
        protected BaseFilterRangeAttribute() : this(null, null)
        {
        }

        protected BaseFilterRangeAttribute(object minOrMinMember = null, object maxOrMaxMember = null)
        {
            if ((minOrMinMember is string) && !this.TryParse((string) minOrMinMember, out minOrMinMember))
            {
                this.MinimumMember = (string) minOrMinMember;
            }
            else
            {
                this.Minimum = minOrMinMember;
            }
            if ((maxOrMaxMember is string) && !this.TryParse((string) maxOrMaxMember, out maxOrMaxMember))
            {
                this.MaximumMember = (string) maxOrMaxMember;
            }
            else
            {
                this.Maximum = maxOrMaxMember;
            }
        }

        public string GetFromName() => 
            base.GetLocalizableValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseFilterRangeAttribute)), (MethodInfo) methodof(BaseFilterRangeAttribute.get_FromName)), new ParameterExpression[0]));

        protected override IEnumerable<Expression<Func<string>>> GetLocalizableProperties() => 
            new Expression<Func<string>>[] { Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseFilterRangeAttribute)), (MethodInfo) methodof(BaseFilterRangeAttribute.get_FromName)), new ParameterExpression[0]), Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseFilterRangeAttribute)), (MethodInfo) methodof(BaseFilterRangeAttribute.get_ToName)), new ParameterExpression[0]), Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseFilterRangeAttribute)), (MethodInfo) methodof(BaseFilterRangeAttribute.get_NullName)), new ParameterExpression[0]) };

        protected internal override string[] GetMembers()
        {
            string[] textArray1 = new string[3];
            textArray1[0] = this.MinimumMember;
            textArray1[1] = this.MaximumMember;
            return textArray1;
        }

        public string GetNullName() => 
            base.GetLocalizableValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseFilterRangeAttribute)), (MethodInfo) methodof(BaseFilterRangeAttribute.get_NullName)), new ParameterExpression[0]));

        public string GetToName() => 
            base.GetLocalizableValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseFilterRangeAttribute)), (MethodInfo) methodof(BaseFilterRangeAttribute.get_ToName)), new ParameterExpression[0]));

        protected virtual bool TryParse(string str, out object value)
        {
            value = str;
            return false;
        }

        public string FromName
        {
            get => 
                base.GetLocalizablePropertyValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseFilterRangeAttribute)), (MethodInfo) methodof(BaseFilterRangeAttribute.get_FromName)), new ParameterExpression[0]));
            set => 
                base.SetLocalizablePropertyValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseFilterRangeAttribute)), (MethodInfo) methodof(BaseFilterRangeAttribute.get_FromName)), new ParameterExpression[0]), value);
        }

        public string ToName
        {
            get => 
                base.GetLocalizablePropertyValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseFilterRangeAttribute)), (MethodInfo) methodof(BaseFilterRangeAttribute.get_ToName)), new ParameterExpression[0]));
            set => 
                base.SetLocalizablePropertyValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseFilterRangeAttribute)), (MethodInfo) methodof(BaseFilterRangeAttribute.get_ToName)), new ParameterExpression[0]), value);
        }

        public string NullName
        {
            get => 
                base.GetLocalizablePropertyValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseFilterRangeAttribute)), (MethodInfo) methodof(BaseFilterRangeAttribute.get_NullName)), new ParameterExpression[0]));
            set => 
                base.SetLocalizablePropertyValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseFilterRangeAttribute)), (MethodInfo) methodof(BaseFilterRangeAttribute.get_NullName)), new ParameterExpression[0]), value);
        }

        public object Minimum { get; protected set; }

        public object Maximum { get; protected set; }

        public string MinimumMember { get; set; }

        public string MaximumMember { get; set; }
    }
}

