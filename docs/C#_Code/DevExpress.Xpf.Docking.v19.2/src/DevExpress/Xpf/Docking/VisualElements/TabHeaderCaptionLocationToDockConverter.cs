namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Data;

    [ValueConversion(typeof(CaptionLocation), typeof(Dock))]
    public class TabHeaderCaptionLocationToDockConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CaptionLocation location = (CaptionLocation) value;
            return (((location == CaptionLocation.Left) || (location == CaptionLocation.Right)) ? Dock.Top : Dock.Left);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            value;
    }
}

