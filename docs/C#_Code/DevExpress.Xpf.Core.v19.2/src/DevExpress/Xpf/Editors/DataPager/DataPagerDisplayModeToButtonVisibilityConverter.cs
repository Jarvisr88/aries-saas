namespace DevExpress.Xpf.Editors.DataPager
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;

    public class DataPagerDisplayModeToButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            char[] separator = new char[] { '_' };
            return ((((string) parameter).Split(separator).ToList<string>().IndexOf(value.ToString()) != -1) ? Visibility.Visible : Visibility.Collapsed);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

