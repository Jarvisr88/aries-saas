namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class VisibilityToBooleanConverter : MarkupExtension, IValueConverter
    {
        static VisibilityToBooleanConverter()
        {
            Instance = new VisibilityToBooleanConverter();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((Visibility) value) == Visibility.Visible;

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static VisibilityToBooleanConverter Instance { get; private set; }
    }
}

