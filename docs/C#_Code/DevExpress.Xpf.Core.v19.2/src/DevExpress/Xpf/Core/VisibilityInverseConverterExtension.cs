namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class VisibilityInverseConverterExtension : MarkupExtension, IValueConverter
    {
        private static VisibilityInverseConverterExtension converter;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((value == null) || (((Visibility) value) != Visibility.Visible)) ? Visibility.Visible : Visibility.Collapsed;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("VisibilityInverseConverter.ConvertBack");
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            converter ??= new VisibilityInverseConverterExtension();
    }
}

