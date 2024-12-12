namespace DevExpress.XtraPrinting.Native.Presentation
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class Base64StringImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}

