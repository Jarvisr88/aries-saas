namespace DevExpress.Utils.Design
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class LocalizableObjectTypeConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            (destinationType == typeof(string)) || base.CanConvertTo(context, destinationType);

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => 
            !(destinationType == typeof(string)) ? base.ConvertTo(context, culture, value, destinationType) : ((value != null) ? $"({DisplayTypeNameHelper.GetDisplayTypeName(value.GetType())})" : string.Empty);
    }
}

