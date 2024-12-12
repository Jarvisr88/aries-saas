namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class PredefinedNotificationViewModelBackgroundColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PredefinedToastNotificationVewModel model = value as PredefinedToastNotificationVewModel;
            SolidColorBrush brush1 = new SolidColorBrush();
            SolidColorBrush brush2 = new SolidColorBrush();
            brush2.Color = (model == null) ? Colors.Transparent : model.BackgroundColor;
            return brush2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

