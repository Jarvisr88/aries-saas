namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class NavigationPaneTabTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            NavigationPaneTabType? nullable = item as NavigationPaneTabType?;
            if (nullable == null)
            {
                return null;
            }
            switch (nullable.Value)
            {
                case NavigationPaneTabType.DocumentMap:
                    return this.DocumentMapTemplate;

                case NavigationPaneTabType.Pages:
                    return this.PagesTemplate;

                case NavigationPaneTabType.SearchResults:
                    return this.SearchResultTemplate;
            }
            throw new InvalidOperationException();
        }

        public DataTemplate DocumentMapTemplate { get; set; }

        public DataTemplate PagesTemplate { get; set; }

        public DataTemplate SearchResultTemplate { get; set; }
    }
}

