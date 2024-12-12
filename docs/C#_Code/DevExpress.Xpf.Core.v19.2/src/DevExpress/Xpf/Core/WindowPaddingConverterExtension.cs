namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class WindowPaddingConverterExtension : MarkupExtension, IValueConverter
    {
        private static WindowPaddingConverterExtension converter;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (value != null) ? ((object) new Thickness(((Thickness) value).Left)) : null;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            converter ??= new WindowPaddingConverterExtension();
    }
}

