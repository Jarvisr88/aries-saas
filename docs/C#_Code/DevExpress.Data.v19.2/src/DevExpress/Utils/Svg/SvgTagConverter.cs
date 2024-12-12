namespace DevExpress.Utils.Svg
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class SvgTagConverter : TypeConverter
    {
        public static readonly SvgTagConverter Instance = new SvgTagConverter();

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) => 
            value;

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => 
            value.ToString();
    }
}

