namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class WindowControlBoxButtonsToVisibilityConverterExtension : MarkupExtension, IValueConverter
    {
        private static WindowControlBoxButtonsToVisibilityConverterExtension converter;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            ControlBoxButtons buttons = (ControlBoxButtons) value;
            return (((parameter == null) || !buttons.HasFlag((ControlBoxButtons) parameter)) ? Visibility.Collapsed : Visibility.Visible);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            converter ??= new WindowControlBoxButtonsToVisibilityConverterExtension();
    }
}

