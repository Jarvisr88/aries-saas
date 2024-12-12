namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class SeparatorConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType);
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType);
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value);
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType);

        private string TabAlias { get; }
    }
}

