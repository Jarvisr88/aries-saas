namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;

    public class SearchBarButtonItemLinkControl : BarButtonItemLinkControl
    {
        protected override void UpdateLayoutPanelElementContentProperties()
        {
            base.UpdateLayoutPanelElementContentProperties();
            if (base.LayoutPanel != null)
            {
                base.LayoutPanel.ContentHorizontalAlignment = HorizontalAlignment.Center;
            }
        }

        protected override Func<BarItemLayoutPanelThemeKeys, BarItemLayoutPanelThemeKeyExtension> GetThemeKeyExtensionFunc =>
            delegate (BarItemLayoutPanelThemeKeys key) {
                SearchBarButtonItemLayoutPanelThemeKeyExtension extension1 = new SearchBarButtonItemLayoutPanelThemeKeyExtension();
                extension1.ResourceKey = key;
                extension1.ThemeName = base.ThemeName;
                return extension1;
            };
    }
}

