namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class BooleanToVisibilityConverter : MarkupExtension, IValueConverter
    {
        static BooleanToVisibilityConverter()
        {
            Instance = new BooleanToVisibilityConverter();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            !((bool) value) ? Visibility.Collapsed : Visibility.Visible;

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static BooleanToVisibilityConverter Instance { get; private set; }
    }
}

