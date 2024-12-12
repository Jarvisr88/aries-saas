namespace DevExpress.Mvvm.UI
{
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class BooleanToObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DefaultBoolean)
            {
                DefaultBoolean flag2 = (DefaultBoolean) value;
                value = (flag2 != DefaultBoolean.True) ? ((flag2 != DefaultBoolean.False) ? null : ((object) false)) : true;
            }
            if (value is bool?)
            {
                bool? nullable = (bool?) value;
                bool flag3 = true;
                value = (nullable.GetValueOrDefault() == flag3) ? ((object) (nullable != null)) : ((object) 0);
            }
            return ((value != null) ? ((value as bool) ? (((bool) value) ? this.TrueValue : this.FalseValue) : null) : this.NullValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object TrueValue { get; set; }

        public object FalseValue { get; set; }

        public object NullValue { get; set; }
    }
}

