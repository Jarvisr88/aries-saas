namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class WindowControlBoxButtonsToVisibilityMultiValuesConverterExtension : MarkupExtension, IMultiValueConverter
    {
        private static WindowControlBoxButtonsToVisibilityMultiValuesConverterExtension converter;

        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value[0] == DependencyProperty.UnsetValue) || (value[1] == DependencyProperty.UnsetValue))
            {
                return Visibility.Collapsed;
            }
            Visibility visibility = (Visibility) value[1];
            return ((!((ControlBoxButtons) value[0]).HasFlag((ControlBoxButtons) parameter) || (visibility != Visibility.Collapsed)) ? Visibility.Collapsed : Visibility.Visible);
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            converter ??= new WindowControlBoxButtonsToVisibilityMultiValuesConverterExtension();
    }
}

