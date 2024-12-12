namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class HideBorderSideToThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double left = 1.0;
            if (parameter != null)
            {
                left = double.Parse(parameter.ToString());
            }
            switch (((HideBorderSide) value))
            {
                case HideBorderSide.Bottom:
                    return new Thickness(left, left, left, 0.0);

                case HideBorderSide.Top:
                    return new Thickness(left, 0.0, left, left);

                case (HideBorderSide.All | HideBorderSide.Right):
                    return new Thickness(left, 0.0, left, 0.0);
            }
            return new Thickness(left);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

