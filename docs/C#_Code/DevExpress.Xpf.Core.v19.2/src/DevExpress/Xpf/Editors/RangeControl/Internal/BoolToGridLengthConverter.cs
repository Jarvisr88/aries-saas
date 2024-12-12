namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class BoolToGridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GridLength length;
            double result = 0.0;
            if (parameter.ToString() == "Auto")
            {
                length = new GridLength(1.0, GridUnitType.Auto);
            }
            else
            {
                double.TryParse(parameter.ToString(), out result);
                length = new GridLength(result);
            }
            return (((bool) value) ? length : new GridLength(0.0));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

