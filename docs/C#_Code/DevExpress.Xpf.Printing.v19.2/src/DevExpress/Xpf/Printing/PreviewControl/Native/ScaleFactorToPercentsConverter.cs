namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class ScaleFactorToPercentsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((float) value) * 100f;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float num = 0f;
            try
            {
                num = System.Convert.ToSingle(value);
            }
            catch
            {
            }
            return (num / 100f);
        }
    }
}

