namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class BoolToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double trueValue;
            if (this.InnerConverter != null)
            {
                value = this.InnerConverter.Convert(value, targetType, parameter, culture);
            }
            if ((bool) value)
            {
                trueValue = this.TrueValue;
            }
            else
            {
                double? falseValue = this.FalseValue;
                trueValue = (falseValue != null) ? falseValue.GetValueOrDefault() : double.NaN;
            }
            return trueValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public IValueConverter InnerConverter { get; set; }

        public double TrueValue { get; set; }

        public double? FalseValue { get; set; }
    }
}

