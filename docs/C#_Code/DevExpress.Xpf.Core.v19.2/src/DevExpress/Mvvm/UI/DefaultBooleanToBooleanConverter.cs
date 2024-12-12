namespace DevExpress.Mvvm.UI
{
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class DefaultBooleanToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? nullableBooleanValue = ConverterHelper.GetNullableBooleanValue(value);
            if (!(targetType == typeof(bool)))
            {
                return (!(targetType == typeof(DefaultBoolean)) ? ((object) nullableBooleanValue) : ((object) ConverterHelper.ToDefaultBoolean(nullableBooleanValue)));
            }
            bool? nullable2 = nullableBooleanValue;
            return ((nullable2 != null) ? ((object) nullable2.GetValueOrDefault()) : ((object) 0));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? nullableBooleanValue = ConverterHelper.GetNullableBooleanValue(value);
            if (!(targetType == typeof(bool)))
            {
                return (!(targetType == typeof(bool?)) ? ((object) ConverterHelper.ToDefaultBoolean(nullableBooleanValue)) : ((object) nullableBooleanValue));
            }
            bool? nullable2 = nullableBooleanValue;
            return ((nullable2 != null) ? ((object) nullable2.GetValueOrDefault()) : ((object) 0));
        }
    }
}

