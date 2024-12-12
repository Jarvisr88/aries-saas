namespace DevExpress.Xpf.Printing.Parameters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class ParameterDescriptionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
            {
                throw new NotSupportedException();
            }
            return (string.IsNullOrEmpty(values[0].ToString()) ? values[1] : values[0]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

