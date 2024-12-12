namespace DevExpress.Xpf.Office.UI
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class GalleryStyleItemForeColorConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            !(targetType != typeof(Brush)) ? new SolidColorBrush((Color) value) : value;

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((SolidColorBrush) value).Color;
    }
}

