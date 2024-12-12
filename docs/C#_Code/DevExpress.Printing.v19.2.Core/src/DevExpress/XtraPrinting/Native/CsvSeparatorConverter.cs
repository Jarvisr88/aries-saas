namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class CsvSeparatorConverter : SeparatorConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType);
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType);
    }
}

