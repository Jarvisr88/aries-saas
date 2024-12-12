namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class NegationConverterExtension : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ConvertCore(value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            ConvertCore(value);

        private static object ConvertCore(object value) => 
            (value as bool) ? !((bool) value) : DependencyProperty.UnsetValue;

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;
    }
}

