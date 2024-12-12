namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Data;

    [ValueConversion(typeof(CaptionLocation), typeof(Orientation))]
    public class TabHeaderCaptionLocationToOrientationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CaptionLocation location = (CaptionLocation) value;
            return (((location == CaptionLocation.Left) || (location == CaptionLocation.Right)) ? Orientation.Vertical : Orientation.Horizontal);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            value;
    }
}

