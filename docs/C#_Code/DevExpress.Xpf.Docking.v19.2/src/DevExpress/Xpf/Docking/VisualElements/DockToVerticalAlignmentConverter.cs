namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    [ValueConversion(typeof(Dock), typeof(VerticalAlignment))]
    public class DockToVerticalAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Dock dock = (Dock) value;
            return ((dock == Dock.Top) ? VerticalAlignment.Bottom : ((dock == Dock.Bottom) ? VerticalAlignment.Top : VerticalAlignment.Stretch));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

