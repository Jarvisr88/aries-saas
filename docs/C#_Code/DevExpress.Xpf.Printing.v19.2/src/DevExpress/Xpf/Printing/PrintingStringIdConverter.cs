namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class PrintingStringIdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                throw new ArgumentException("parameter");
            }
            return PrintingLocalizer.GetString((PrintingStringId) Enum.Parse(typeof(PrintingStringId), parameter.ToString(), false));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

