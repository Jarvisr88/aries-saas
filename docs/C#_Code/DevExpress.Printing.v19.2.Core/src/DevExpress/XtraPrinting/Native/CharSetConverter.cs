namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Globalization;

    public class CharSetConverter : TypeConverter
    {
        private static SortedList charSetList;

        static CharSetConverter();
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType);
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType);
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value);
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType);
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context);
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context);
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context);
    }
}

