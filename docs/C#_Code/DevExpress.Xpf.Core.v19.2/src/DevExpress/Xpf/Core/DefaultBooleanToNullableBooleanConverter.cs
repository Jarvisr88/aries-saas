namespace DevExpress.Xpf.Core
{
    using DevExpress.Core;
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class DefaultBooleanToNullableBooleanConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((DefaultBoolean) value).ToBool();

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? nullable = (bool?) value;
            return ((nullable != null) ? (nullable.Value ? DefaultBoolean.True : DefaultBoolean.False) : DefaultBoolean.Default);
        }
    }
}

