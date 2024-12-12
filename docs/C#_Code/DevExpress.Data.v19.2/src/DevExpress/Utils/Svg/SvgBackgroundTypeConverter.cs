namespace DevExpress.Utils.Svg
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class SvgBackgroundTypeConverter : SvgViewBoxTypeConverter
    {
        public static readonly SvgBackgroundTypeConverter Instance = new SvgBackgroundTypeConverter();

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string))
            {
                return base.ConvertFrom(context, culture, value);
            }
            char[] separator = new char[] { ',', ' ' };
            string[] strArray = (value as string).Replace("new", "").Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length == 0)
            {
                return SvgViewBox.Empty;
            }
            if (strArray.Length != 4)
            {
                throw new ArgumentException("value");
            }
            CultureInfo invariantCulture = CultureInfo.InvariantCulture;
            return new SvgViewBox(this.GetDouble(strArray[0], invariantCulture), this.GetDouble(strArray[1], invariantCulture), this.GetDouble(strArray[2], invariantCulture), this.GetDouble(strArray[3], invariantCulture));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (!(value is SvgViewBox))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            SvgViewBox box = value as SvgViewBox;
            if (box.IsEmpty)
            {
                return "new    ";
            }
            return $"new {box.MinX} {box.MinY} {box.Width} {box.Height}";
        }

        private double GetDouble(string value, CultureInfo culture) => 
            Math.Round(double.Parse(value, culture), 4);
    }
}

