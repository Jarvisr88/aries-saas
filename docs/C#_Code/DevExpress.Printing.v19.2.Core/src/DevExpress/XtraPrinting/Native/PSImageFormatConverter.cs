namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;

    public class PSImageFormatConverter : ImageFormatConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType);
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context);
    }
}

