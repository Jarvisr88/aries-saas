namespace DevExpress.XtraReports.Expressions
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;

    internal class BasicExpressionBindingConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            (destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType);

        public sealed override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => 
            !(destinationType == typeof(InstanceDescriptor)) ? base.ConvertTo(context, culture, value, destinationType) : this.ConvertToInstanceDescriptor((BasicExpressionBinding) value);

        protected virtual InstanceDescriptor ConvertToInstanceDescriptor(BasicExpressionBinding value)
        {
            Type[] types = new Type[] { typeof(string), typeof(string) };
            return new InstanceDescriptor(typeof(BasicExpressionBinding).GetConstructor(types), GetConstructorParams(value), true);
        }

        private static object[] GetConstructorParams(BasicExpressionBinding value) => 
            new string[] { value.PropertyName, value.Expression };
    }
}

