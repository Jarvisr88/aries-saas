namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;

    public class ThemeNameTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            !(sourceType == typeof(string)) ? base.CanConvertFrom(context, sourceType) : true;

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) => 
            !(value is string) ? base.ConvertFrom(context, culture, value) : ((((string) value) != "Default") ? value : Theme.Default.Name);

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<string> values = new List<string>(Theme.Themes.Count);
            foreach (Theme theme in Theme.Themes)
            {
                if (theme.ShowInThemeSelector)
                {
                    values.Add(theme.Name);
                }
            }
            values.Add("None");
            values.Add("Default");
            return new TypeConverter.StandardValuesCollection(values);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => 
            true;
    }
}

