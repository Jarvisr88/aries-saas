namespace DevExpress.Xpf.Core
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class ThemeTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            sourceType == typeof(string);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string name = value as string;
            if (name == null)
            {
                return base.ConvertFrom(context, culture, value);
            }
            Theme theme = Theme.FindTheme(name);
            if (theme == null)
            {
                throw new ArgumentOutOfRangeException("theme");
            }
            return theme;
        }
    }
}

