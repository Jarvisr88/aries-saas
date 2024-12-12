namespace DevExpress.Mvvm.UI
{
    using DevExpress.Data.Helpers;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Data;

    public class FormatStringConverter : IValueConverter
    {
        private bool allowSimpleFormatString = true;

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object obj2 = GetFormattedValue(this.FormatString, value, CultureInfo.CurrentUICulture, this.OutStringCaseFormat, this.AllowSimpleFormatString);
            return ((!(obj2 is string) || !this.SplitPascalCase) ? obj2 : SplitStringHelper.SplitPascalCaseString((string) obj2));
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public static string GetDisplayFormat(string displayFormat, bool allowSimpleFormatString = true)
        {
            if (string.IsNullOrEmpty(displayFormat))
            {
                return string.Empty;
            }
            if (!allowSimpleFormatString)
            {
                return displayFormat;
            }
            string str = displayFormat;
            return (!str.Contains("{") ? $"{{0:{str}}}" : str);
        }

        public static object GetFormattedValue(string formatString, object value, CultureInfo culture, bool allowSimpleFormatString = true)
        {
            string displayFormat = GetDisplayFormat(formatString, allowSimpleFormatString);
            if (string.IsNullOrEmpty(displayFormat))
            {
                return value;
            }
            object[] args = new object[] { value };
            return string.Format(culture, displayFormat, args);
        }

        public static object GetFormattedValue(string formatString, object value, CultureInfo culture, TextCaseFormat outStringCaseFormat, bool allowSimpleFormatString = true)
        {
            object obj2 = GetFormattedValue(formatString, value, culture, allowSimpleFormatString);
            return ((obj2 != null) ? ((outStringCaseFormat == TextCaseFormat.Lower) ? obj2.ToString().ToLower() : ((outStringCaseFormat == TextCaseFormat.Upper) ? obj2.ToString().ToUpper() : obj2.ToString())) : null);
        }

        public string FormatString { get; set; }

        public bool AllowSimpleFormatString
        {
            get => 
                this.allowSimpleFormatString;
            set => 
                this.allowSimpleFormatString = value;
        }

        public TextCaseFormat OutStringCaseFormat { get; set; }

        public bool SplitPascalCase { get; set; }

        public enum TextCaseFormat
        {
            Default,
            Lower,
            Upper
        }
    }
}

