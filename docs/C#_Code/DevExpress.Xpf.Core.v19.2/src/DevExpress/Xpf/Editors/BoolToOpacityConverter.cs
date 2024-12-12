namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    internal class BoolToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((bool) value) ? 1.0 : 0.0;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

