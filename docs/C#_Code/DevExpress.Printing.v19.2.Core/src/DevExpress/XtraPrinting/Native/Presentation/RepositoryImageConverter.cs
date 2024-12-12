namespace DevExpress.XtraPrinting.Native.Presentation
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class RepositoryImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}

