namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Data;

    [ValueConversion(typeof(CaptionLocation), typeof(Dock))]
    public class GroupCaptionLocationToDockConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (((CaptionLocation) value))
            {
                case CaptionLocation.Left:
                    return Dock.Left;

                case CaptionLocation.Right:
                    return Dock.Right;

                case CaptionLocation.Bottom:
                    return Dock.Bottom;
            }
            return Dock.Top;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            value;
    }
}

