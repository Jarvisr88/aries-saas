namespace DevExpress.Xpf.Editors.Flyout.Native
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class IndicatorDirectionToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IndicatorDirection direction;
            string str = parameter as string;
            if ((str == null) || !Enum.TryParse<IndicatorDirection>(str, out direction))
            {
                throw new ArgumentException("ConverterParameter");
            }
            return direction.Equals(value).ToVisibility();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

