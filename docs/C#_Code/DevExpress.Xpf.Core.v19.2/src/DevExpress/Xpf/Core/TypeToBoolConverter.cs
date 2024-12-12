namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class TypeToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (value == null) ? ((object) 0) : ((value.GetType().Name == ((string) parameter)) ? ((object) 1) : ((object) (value.GetType().FullName == ((string) parameter))));

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("TypeToBoolConverter.ConvertBack");
        }
    }
}

