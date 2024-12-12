namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class XamlEnumTypeConverter<T> : TypeConverter where T: struct
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            !(sourceType == typeof(string)) ? base.CanConvertFrom(context, sourceType) : true;

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) => 
            Enum.Parse(typeof(T), (string) value);
    }
}

