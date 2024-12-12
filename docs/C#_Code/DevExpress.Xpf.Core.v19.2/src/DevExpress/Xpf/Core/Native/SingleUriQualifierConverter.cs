namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    internal class SingleUriQualifierConverter : IValueConverter
    {
        private Uri uri;

        public SingleUriQualifierConverter(Uri uri);
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}

