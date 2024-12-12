namespace DevExpress.Utils.Design
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class SingleTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string text = value as string;
            if (text == null)
            {
                return base.ConvertFrom(context, culture, value);
            }
            if (text.Trim().Length == 0)
            {
                return null;
            }
            culture ??= CultureInfo.CurrentCulture;
            return (float) TypeDescriptor.GetConverter(typeof(float)).ConvertFromString(context, culture, text);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (!(value is float))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            float ef = (float) value;
            culture ??= CultureInfo.CurrentCulture;
            return ToString(context, culture, ef);
        }

        public static string ToString(ITypeDescriptorContext context, CultureInfo culture, float ef) => 
            TypeDescriptor.GetConverter(typeof(float)).ConvertToString(context, culture, Math.Round((double) ef, 2));
    }
}

