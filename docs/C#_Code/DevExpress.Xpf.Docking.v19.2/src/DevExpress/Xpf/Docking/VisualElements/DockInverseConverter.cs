namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Data;

    [ValueConversion(typeof(Dock), typeof(Dock))]
    public class DockInverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (((Dock) value))
            {
                case Dock.Left:
                    return Dock.Right;

                case Dock.Top:
                    return Dock.Bottom;

                case Dock.Bottom:
                    return Dock.Top;
            }
            return Dock.Left;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

