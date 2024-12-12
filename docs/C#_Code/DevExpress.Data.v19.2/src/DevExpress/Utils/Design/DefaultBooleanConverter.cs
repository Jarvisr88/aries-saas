namespace DevExpress.Utils.Design
{
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class DefaultBooleanConverter : EnumTypeConverter
    {
        public DefaultBooleanConverter() : base(typeof(DefaultBoolean))
        {
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            (sourceType == typeof(bool)) || base.CanConvertFrom(context, sourceType);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) => 
            !(value as bool) ? base.ConvertFrom(context, culture, value) : (((bool) value) ? DefaultBoolean.True : DefaultBoolean.False);
    }
}

