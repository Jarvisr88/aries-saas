namespace DevExpress.Xpf.Editors
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class StringToObjectConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) => 
            value;
    }
}

