namespace DevExpress.Utils.Editors
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class ObjectEditorTypeConverter : TypeConverter
    {
        public static string NullString;

        static ObjectEditorTypeConverter();
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType);
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value);
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType);
    }
}

