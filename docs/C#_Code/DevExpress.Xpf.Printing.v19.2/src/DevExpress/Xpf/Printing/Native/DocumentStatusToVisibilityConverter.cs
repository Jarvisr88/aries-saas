namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class DocumentStatusToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture) => 
            (((DocumentStatus) value) == DocumentStatus.None) ? Visibility.Collapsed : Visibility.Visible;

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;
    }
}

