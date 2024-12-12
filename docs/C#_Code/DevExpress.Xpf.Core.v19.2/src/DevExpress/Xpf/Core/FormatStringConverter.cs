namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class FormatStringConverter : MarkupExtension, IValueConverter
    {
        public FormatStringConverter()
        {
        }

        public FormatStringConverter(string formatString)
        {
            this.FormatString = formatString;
        }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.Converter != null)
            {
                value = this.Converter.Convert(value, targetType, parameter, culture);
            }
            return GetFormattedValue(this.FormatString ?? (parameter as string), value, culture);
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static string GetDisplayFormat(string displayFormat)
        {
            if (string.IsNullOrEmpty(displayFormat))
            {
                return string.Empty;
            }
            string str = displayFormat;
            return (!str.Contains("{") ? $"{{0:{str}}}" : str);
        }

        public static object GetFormattedValue(string formatString, object value, CultureInfo culture)
        {
            string displayFormat = GetDisplayFormat(formatString);
            if (string.IsNullOrEmpty(displayFormat))
            {
                return value;
            }
            object[] args = new object[] { value };
            return string.Format(culture, displayFormat, args);
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        public IValueConverter Converter { get; set; }

        public string FormatString { get; set; }
    }
}

