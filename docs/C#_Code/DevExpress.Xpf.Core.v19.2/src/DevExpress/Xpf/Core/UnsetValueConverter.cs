namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class UnsetValueConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (value == null) ? DependencyProperty.UnsetValue : value;

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            value;
    }
}

