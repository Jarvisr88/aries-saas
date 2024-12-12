namespace DevExpress.Mvvm.UI
{
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class BooleanNegationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? nullableBooleanValue = ConverterHelper.GetNullableBooleanValue(value);
            if (nullableBooleanValue != null)
            {
                nullableBooleanValue = new bool?(!nullableBooleanValue.Value);
            }
            if (!(targetType == typeof(bool)))
            {
                return (!(targetType == typeof(DefaultBoolean)) ? ((object) nullableBooleanValue) : ((object) ConverterHelper.ToDefaultBoolean(nullableBooleanValue)));
            }
            bool? nullable2 = nullableBooleanValue;
            return ((nullable2 != null) ? ((object) nullable2.GetValueOrDefault()) : ((object) 1));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            this.Convert(value, targetType, parameter, culture);
    }
}

