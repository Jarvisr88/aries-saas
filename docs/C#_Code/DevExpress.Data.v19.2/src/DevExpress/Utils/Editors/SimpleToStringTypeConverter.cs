namespace DevExpress.Utils.Editors
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class SimpleToStringTypeConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType);
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType);
    }
}

