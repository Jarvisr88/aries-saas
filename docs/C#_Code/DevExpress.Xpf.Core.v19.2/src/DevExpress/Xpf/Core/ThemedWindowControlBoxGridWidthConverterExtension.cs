namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class ThemedWindowControlBoxGridWidthConverterExtension : MarkupExtension, IMultiValueConverter
    {
        private static ThemedWindowControlBoxGridWidthConverterExtension converter;

        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            double num = (double) value[0];
            return ((((Visibility) value[1]) == Visibility.Visible) ? num : 0.0);
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            converter ??= new ThemedWindowControlBoxGridWidthConverterExtension();
    }
}

