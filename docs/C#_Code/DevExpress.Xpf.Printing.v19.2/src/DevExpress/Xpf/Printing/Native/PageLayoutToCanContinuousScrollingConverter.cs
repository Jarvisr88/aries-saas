namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class PageLayoutToCanContinuousScrollingConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            PageDisplayMode mode = Guard.ArgumentMatchType<PageDisplayMode>(value, "value");
            return ((mode == PageDisplayMode.Single) ? ((object) 1) : ((object) (mode == PageDisplayMode.Columns)));
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;
    }
}

