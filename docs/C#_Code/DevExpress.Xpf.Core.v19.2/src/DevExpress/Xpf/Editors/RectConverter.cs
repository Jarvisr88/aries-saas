namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class RectConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) => 
            ((values[0] == DependencyProperty.UnsetValue) || (values[1] == DependencyProperty.UnsetValue)) ? DependencyProperty.UnsetValue : new Rect(0.0, 0.0, (double) values[0], (double) values[1]);

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

