namespace DevExpress.Xpf.Core
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class ObjectConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            !(sourceType == typeof(string)) ? base.CanConvertFrom(context, sourceType) : true;

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) => 
            ((value == null) || !(value is string)) ? base.ConvertFrom(context, culture, value) : value;
    }
}

