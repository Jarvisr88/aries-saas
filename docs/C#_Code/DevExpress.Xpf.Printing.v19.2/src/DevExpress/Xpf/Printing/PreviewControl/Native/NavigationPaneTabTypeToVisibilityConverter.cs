namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class NavigationPaneTabTypeToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            NavigationPaneTabType? nullable = value as NavigationPaneTabType?;
            return ((nullable != null) ? ((((NavigationPaneTabType) nullable.Value) == this.ExpectedView) ? Visibility.Visible : Visibility.Collapsed) : ((this.ExpectedView == NavigationPaneTabType.SearchResults) ? Visibility.Visible : Visibility.Collapsed));
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public NavigationPaneTabType ExpectedView { get; set; }
    }
}

