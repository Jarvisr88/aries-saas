namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.DocumentViewer.Themes;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class CheckBarItemTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is CommandSetZoomFactorAndModeItem)
            {
                return this.SelectTemplateForZoomItems(item, container);
            }
            if (!(item is CommandToggleButton))
            {
                return base.SelectTemplate(item, container);
            }
            DocumentViewerThemeKeyExtension resourceKey = new DocumentViewerThemeKeyExtension();
            resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(container);
            resourceKey.ResourceKey = DocumentViewerThemeKeys.BarToggleButtonTemplate;
            return (DataTemplate) (container as FrameworkContentElement).FindResource(resourceKey);
        }

        private DataTemplate SelectTemplateForZoomItems(object item, DependencyObject container)
        {
            CommandSetZoomFactorAndModeItem item2 = item as CommandSetZoomFactorAndModeItem;
            FrameworkContentElement element = container as FrameworkContentElement;
            if ((item2 == null) || (element == null))
            {
                return base.SelectTemplate(item, container);
            }
            if (item2.IsSeparator)
            {
                DocumentViewerThemeKeyExtension extension1 = new DocumentViewerThemeKeyExtension();
                extension1.ThemeName = ThemeHelper.GetEditorThemeName(container);
                extension1.ResourceKey = DocumentViewerThemeKeys.SetZoomFactorAndModeItemSeparatorTemplate;
                return (DataTemplate) element.FindResource(extension1);
            }
            if (item2.ZoomMode == ZoomMode.Custom)
            {
                DocumentViewerThemeKeyExtension extension2 = new DocumentViewerThemeKeyExtension();
                extension2.ThemeName = ThemeHelper.GetEditorThemeName(container);
                extension2.ResourceKey = DocumentViewerThemeKeys.SetZoomFactorItemTemplate;
                return (DataTemplate) element.FindResource(extension2);
            }
            DocumentViewerThemeKeyExtension resourceKey = new DocumentViewerThemeKeyExtension();
            resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(container);
            resourceKey.ResourceKey = DocumentViewerThemeKeys.SetZoomModeItemTemplate;
            return (DataTemplate) element.FindResource(resourceKey);
        }
    }
}

