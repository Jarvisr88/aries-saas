namespace DevExpress.Utils.Svg
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class SvgEnumConverter : EnumConverter
    {
        public SvgEnumConverter(Type type) : base(type)
        {
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            object obj2 = base.ConvertTo(context, culture, value, destinationType);
            return (!(destinationType == typeof(string)) ? obj2 : obj2.ToString().ToLower());
        }
    }
}

