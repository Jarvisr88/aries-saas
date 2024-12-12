namespace DevExpress.DataAccess
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    [Serializable, TypeConverter(typeof(Expression.ExpressionTypeConverter))]
    public class Expression : IFormattable
    {
        [NonSerialized]
        private Type resultType;

        public Expression()
        {
        }

        public Expression(string expressionString) : this(expressionString, null)
        {
        }

        public Expression(string expressionString, Type resultType)
        {
            this.ExpressionString = expressionString;
            this.ResultType = resultType;
        }

        internal static Expression ConvertFromString(string value)
        {
            Match match = new Regex("\\A\\(([\\w\\\\`\\[\\]\\.+*&,]+(?:, [\\w\\.]+(?:, Version=\\d{1,5}\\.\\d{1,5}\\.\\d{1,5}\\.\\d{1,5}, Culture=[\\w-\"]+, PublicKeyToken=(?:(?:(?:[\\da-f]{2}){8,})|(?:null)))?)?)\\)\\((.*)\\)\\Z", RegexOptions.Singleline).Match(value);
            if (match.Success)
            {
                Expression expression1 = new Expression(match.Groups[2].Value);
                expression1.ResultType = Type.GetType(match.Groups[1].Value, false);
                return expression1;
            }
            if (!new Regex(@"\A(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{4}|[A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)\Z").IsMatch(value))
            {
                return new Expression(value);
            }
            object component = SafeBinaryFormatter.Deserialize(value);
            try
            {
                return (Expression) component;
            }
            catch (InvalidCastException)
            {
                Type componentType = component.GetType();
                if (!string.Equals(componentType.FullName, typeof(Expression).FullName, StringComparison.Ordinal))
                {
                    throw;
                }
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(componentType);
                string expressionString = (string) properties["ExpressionString"].GetValue(component);
                PropertyDescriptor descriptor = properties["ResultType"];
                return new Expression(expressionString, (descriptor == null) ? null : ((Type) descriptor.GetValue(component)));
            }
        }

        internal static string ConvertToString(Expression value) => 
            (value != null) ? $"({GetResultTypeString(value.ResultType)})({value.ExpressionString})" : null;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if ((obj == null) || (base.GetType() != obj.GetType()))
            {
                return false;
            }
            Expression expression = (Expression) obj;
            return (string.Equals(this.ExpressionString, expression.ExpressionString, StringComparison.Ordinal) && (this.ResultType == expression.ResultType));
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        private static string GetResultTypeString(Type resultType) => 
            (resultType == null) ? "null" : (XtraPropertyInfo.IsPrimitiveType(resultType) ? resultType.FullName : resultType.AssemblyQualifiedName);

        string IFormattable.ToString(string format, IFormatProvider formatProvider) => 
            this.ExpressionString;

        public override string ToString() => 
            this.ExpressionString;

        public string ExpressionString { get; set; }

        public Type ResultType
        {
            get => 
                this.resultType;
            set => 
                this.resultType = value;
        }

        private class ExpressionTypeConverter : TypeConverter
        {
            private static readonly ConstructorInfo constructorInfo;

            static ExpressionTypeConverter()
            {
                Type[] types = new Type[] { typeof(string), typeof(Type) };
                constructorInfo = typeof(Expression).GetConstructor(types);
            }

            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
                !(sourceType == typeof(string)) ? base.CanConvertFrom(context, sourceType) : true;

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
                !(destinationType == typeof(InstanceDescriptor)) ? base.CanConvertTo(context, destinationType) : true;

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                string expressionString = value as string;
                return ((expressionString == null) ? base.ConvertFrom(context, culture, value) : new Expression(expressionString));
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                Expression expression = value as Expression;
                if ((expression == null) || !(destinationType == typeof(InstanceDescriptor)))
                {
                    return base.ConvertTo(context, culture, value, destinationType);
                }
                object[] arguments = new object[] { expression.ExpressionString, expression.ResultType };
                return new InstanceDescriptor(constructorInfo, arguments);
            }
        }
    }
}

