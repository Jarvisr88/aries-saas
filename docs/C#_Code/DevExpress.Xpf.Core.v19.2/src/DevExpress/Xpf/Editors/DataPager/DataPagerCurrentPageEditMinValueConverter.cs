namespace DevExpress.Xpf.Editors.DataPager
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class DataPagerCurrentPageEditMinValueConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (decimal) Math.Min((int) value, 1);

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

