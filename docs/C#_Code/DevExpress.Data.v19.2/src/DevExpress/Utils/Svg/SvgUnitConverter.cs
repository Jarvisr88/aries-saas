namespace DevExpress.Utils.Svg
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class SvgUnitConverter : TypeConverter
    {
        public static readonly SvgUnitConverter Instance = new SvgUnitConverter();

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
            {
                return null;
            }
            string a = ((string) value).Trim();
            string str2 = a;
            string str3 = string.Empty;
            SvgUnitType px = SvgUnitType.Px;
            if (string.Equals(a, SvgUnit.None, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }
            if (char.IsLetter(a[a.Length - 1]))
            {
                str3 = a.Substring(a.Length - 2).Trim();
                str2 = a.Substring(0, a.Length - 2).Trim();
            }
            if (a[a.Length - 1] == '%')
            {
                px = SvgUnitType.Percentage;
                str2 = a.Substring(0, a.Length - 1).Trim();
            }
            if (string.IsNullOrEmpty(str2))
            {
                return null;
            }
            double unitValue = CoordinateParser.TransformFloat(str2);
            return ((px != SvgUnitType.Percentage) ? ((string.IsNullOrEmpty(str3) || !Enum.TryParse<SvgUnitType>(str3, true, out px)) ? new SvgUnit(unitValue) : new SvgUnit(px, unitValue)) : new SvgUnit(px, unitValue));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => 
            (value != null) ? ((SvgUnit) value).ToString() : base.ConvertTo(context, culture, value, destinationType);
    }
}

