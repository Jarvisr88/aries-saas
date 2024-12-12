namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class CheckObjectNotNullConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture) => 
            value != null;

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

