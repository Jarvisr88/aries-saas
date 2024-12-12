namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class ScaleModeToBooleanConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture) => 
            ((ScaleMode) value) == ((ScaleMode) Enum.Parse(typeof(ScaleMode), (string) parameter, false));

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = (bool) value;
            ScaleMode mode = (ScaleMode) Enum.Parse(typeof(ScaleMode), (string) parameter, false);
            if (mode == ScaleMode.FitToPageWidth)
            {
                return (flag ? ScaleMode.FitToPageWidth : ScaleMode.AdjustToPercent);
            }
            if (mode != ScaleMode.AdjustToPercent)
            {
                throw new NotSupportedException();
            }
            return (flag ? ScaleMode.AdjustToPercent : ScaleMode.FitToPageWidth);
        }
    }
}

