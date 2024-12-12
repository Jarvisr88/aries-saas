namespace DevExpress.Utils.Svg
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class SvgViewBoxTypeConverter : TypeConverter
    {
        public static readonly SvgViewBoxTypeConverter Instance = new SvgViewBoxTypeConverter();

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            !(sourceType == typeof(string)) ? base.CanConvertFrom(context, sourceType) : true;

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            !(destinationType == typeof(SvgViewBox)) ? base.CanConvertTo(context, destinationType) : true;

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string))
            {
                return base.ConvertFrom(context, culture, value);
            }
            char[] separator = new char[] { ',', ' ' };
            string[] strArray = (value as string).Split(separator, StringSplitOptions.RemoveEmptyEntries);
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
            return $"{box.MinX} {box.MinY} {box.Width} {box.Height}";
        }

        private double GetDouble(string value, CultureInfo culture) => 
            Math.Round(double.Parse(value, culture), 4);
    }
}

