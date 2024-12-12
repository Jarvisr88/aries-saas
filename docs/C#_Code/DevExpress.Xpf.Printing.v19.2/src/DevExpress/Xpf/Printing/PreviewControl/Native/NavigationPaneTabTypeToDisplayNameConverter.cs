namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class NavigationPaneTabTypeToDisplayNameConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            NavigationPaneTabType? nullable = value as NavigationPaneTabType?;
            if (nullable == null)
            {
                return null;
            }
            switch (nullable.Value)
            {
                case NavigationPaneTabType.DocumentMap:
                    return PrintingLocalizer.GetString(PrintingStringId.NavigationPane_DocumentMapTabCaption);

                case NavigationPaneTabType.Pages:
                    return PrintingLocalizer.GetString(PrintingStringId.NavigationPane_PagesTabCaption);

                case NavigationPaneTabType.SearchResults:
                    return PrintingLocalizer.GetString(PrintingStringId.NavigationPane_SearchResultsTabCaption);
            }
            throw new InvalidOperationException();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

