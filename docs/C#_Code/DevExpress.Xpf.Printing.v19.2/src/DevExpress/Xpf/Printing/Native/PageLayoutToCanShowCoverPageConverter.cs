namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class PageLayoutToCanShowCoverPageConverter : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, System.Type targetType, object parameter, CultureInfo culture)
        {
            int num = Guard.ArgumentMatchType<int>(values[1], "values[1]");
            return ((((PageDisplayMode) Guard.ArgumentMatchType<PageDisplayMode>(values[0], "value[0]")) != PageDisplayMode.Columns) ? ((object) 0) : ((object) (num == 2)));
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;
    }
}

