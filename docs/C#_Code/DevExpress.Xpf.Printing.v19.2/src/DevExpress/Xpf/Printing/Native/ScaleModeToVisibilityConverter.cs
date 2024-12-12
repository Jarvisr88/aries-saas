namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class ScaleModeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture) => 
            (((ScaleMode) value) == ((ScaleMode) Enum.Parse(typeof(ScaleMode), (string) parameter, false))) ? Visibility.Visible : Visibility.Collapsed;

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility) value;
            ScaleMode mode = (ScaleMode) Enum.Parse(typeof(ScaleMode), (string) parameter, false);
            if (mode == ScaleMode.AdjustToPercent)
            {
                if (visibility == Visibility.Visible)
                {
                    return ScaleMode.AdjustToPercent;
                }
                if (visibility != Visibility.Collapsed)
                {
                    throw new NotSupportedException();
                }
                return ScaleMode.FitToPageWidth;
            }
            if (mode != ScaleMode.FitToPageWidth)
            {
                throw new NotSupportedException();
            }
            if (visibility == Visibility.Visible)
            {
                return ScaleMode.FitToPageWidth;
            }
            if (visibility != Visibility.Collapsed)
            {
                throw new NotSupportedException();
            }
            return ScaleMode.AdjustToPercent;
        }
    }
}

