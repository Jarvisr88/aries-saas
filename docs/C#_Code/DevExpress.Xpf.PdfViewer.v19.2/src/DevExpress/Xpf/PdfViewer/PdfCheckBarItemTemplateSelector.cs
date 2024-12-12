namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.DocumentViewer.Themes;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.PdfViewer.Internal;
    using DevExpress.Xpf.PdfViewer.Themes;
    using System;
    using System.Windows;

    public class PdfCheckBarItemTemplateSelector : CheckBarItemTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container) => 
            !(item is CommandSetPageLayout) ? base.SelectTemplate(item, container) : this.SelectTemplateForPageLayoutItems(item, container);

        private DataTemplate SelectTemplateForPageLayoutItems(object item, DependencyObject container)
        {
            CommandSetPageLayout layout = item as CommandSetPageLayout;
            FrameworkContentElement element = container as FrameworkContentElement;
            if ((layout == null) || (element == null))
            {
                return base.SelectTemplate(item, container);
            }
            if (layout.IsSeparator)
            {
                DocumentViewerThemeKeyExtension extension1 = new DocumentViewerThemeKeyExtension();
                extension1.ThemeName = ThemeHelper.GetEditorThemeName(container);
                extension1.ResourceKey = DocumentViewerThemeKeys.SetZoomFactorAndModeItemSeparatorTemplate;
                return (DataTemplate) element.FindResource(extension1);
            }
            PdfViewerThemeKeyExtension resourceKey = new PdfViewerThemeKeyExtension();
            resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(container);
            resourceKey.ResourceKey = PdfViewerThemeKeys.SetPageLayoutItemTemplate;
            return (DataTemplate) element.FindResource(resourceKey);
        }
    }
}

