namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Text;

    public class EncodingConverter : TypeConverter
    {
        private static Encoding DefaultEncoding;

        static EncodingConverter();
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType);
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType);
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value);
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType);
        private static List<Encoding> GetStandardEncodings();
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context);
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context);
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context);
    }
}

