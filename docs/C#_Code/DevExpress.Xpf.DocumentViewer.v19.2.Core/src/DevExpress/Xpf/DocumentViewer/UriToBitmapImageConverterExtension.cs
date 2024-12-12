namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Utils;
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class UriToBitmapImageConverterExtension : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Uri uri = GetUri(value);
            return ((uri != null) ? new ImageSelectorExtension(GetPatchedUri(uri)).ProvideValue(null) : null);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            null;

        private static Uri GetPatchedUri(Uri uri)
        {
            Match match = Regex.Match(uri.OriginalString, @"(pack://application:,,,)?\/(?<AssemblyName>.*);component\/(?<RelativePath>.*)");
            string str = match.Groups["AssemblyName"].Value;
            char[] trimChars = new char[] { '/' };
            return new Uri($"pack://application:,,,/{str};component/{match.Groups["RelativePath"].Value.Replace('\\', '/').Trim(trimChars)}");
        }

        private static Uri GetUri(object value)
        {
            if (NetVersionDetector.IsNetCore3())
            {
                return (value as Uri);
            }
            UriTypeConverter converter = new UriTypeConverter();
            return (((value == null) || !converter.CanConvertFrom(value.GetType())) ? null : (converter.ConvertFrom(value) as Uri));
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;
    }
}

