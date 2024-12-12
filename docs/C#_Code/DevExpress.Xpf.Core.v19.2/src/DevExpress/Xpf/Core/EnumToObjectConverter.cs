namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class EnumToObjectConverter : IValueConverter
    {
        public EnumToObjectConverter()
        {
            this.Values = new Dictionary<object, object>();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object defaultValue;
            if (!this.Values.TryGetValue(System.Convert.ToString(value), out defaultValue))
            {
                defaultValue = this.DefaultValue;
            }
            else if (defaultValue is EnumObjectProvider)
            {
                defaultValue = ((EnumObjectProvider) defaultValue).Value;
            }
            if ((this.AdditionalConverter != null) && (defaultValue != null))
            {
                defaultValue = this.AdditionalConverter.Convert(defaultValue, targetType, parameter, culture);
            }
            return ConverterHelper.Convert(defaultValue, targetType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object DefaultValue { get; set; }

        public IValueConverter AdditionalConverter { get; set; }

        public Dictionary<object, object> Values { get; set; }
    }
}

