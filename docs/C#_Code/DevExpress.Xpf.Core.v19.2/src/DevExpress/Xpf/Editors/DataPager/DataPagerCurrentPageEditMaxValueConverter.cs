namespace DevExpress.Xpf.Editors.DataPager
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class DataPagerCurrentPageEditMaxValueConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (decimal) ((int) value);

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

