namespace DevExpress.Utils.Svg
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class SvgUnitOffsetConverter : SvgUnitConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
            {
                return null;
            }
            string str = ((string) value).Trim();
            string str2 = str;
            string str3 = string.Empty;
            SvgUnitType px = SvgUnitType.Px;
            if (char.IsLetter(str[str.Length - 1]))
            {
                str3 = str.Substring(str.Length - 2).Trim();
                str2 = str.Substring(0, str.Length - 2).Trim();
            }
            if (str[str.Length - 1] == '%')
            {
                px = SvgUnitType.Percentage;
                str2 = str.Substring(0, str.Length - 1).Trim();
            }
            if (string.IsNullOrEmpty(str2))
            {
                return null;
            }
            double unitValue = CoordinateParser.TransformFloat(str2);
            return ((px != SvgUnitType.Percentage) ? ((string.IsNullOrEmpty(str3) || !Enum.TryParse<SvgUnitType>(str3, true, out px)) ? new SvgUnit(SvgUnitType.Percentage, unitValue * 100.0) : new SvgUnit(px, unitValue)) : new SvgUnit(px, unitValue));
        }
    }
}

