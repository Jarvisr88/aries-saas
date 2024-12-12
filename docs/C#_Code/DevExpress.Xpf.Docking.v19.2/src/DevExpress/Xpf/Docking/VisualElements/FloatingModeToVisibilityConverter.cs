namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    [ValueConversion(typeof(FloatingMode), typeof(Visibility))]
    public class FloatingModeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (((FloatingMode) value) == FloatingMode.Desktop) ? Visibility.Collapsed : Visibility.Visible;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

