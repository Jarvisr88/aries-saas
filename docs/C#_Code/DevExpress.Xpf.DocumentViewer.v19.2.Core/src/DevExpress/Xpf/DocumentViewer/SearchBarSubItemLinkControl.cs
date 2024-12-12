namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using System;

    public class SearchBarSubItemLinkControl : BarSubItemLinkControl
    {
        protected override Func<BarItemLayoutPanelThemeKeys, BarItemLayoutPanelThemeKeyExtension> GetThemeKeyExtensionFunc =>
            delegate (BarItemLayoutPanelThemeKeys key) {
                SearchBarSubItemLayoutPanelThemeKeyExtension extension1 = new SearchBarSubItemLayoutPanelThemeKeyExtension();
                extension1.ResourceKey = key;
                extension1.ThemeName = base.ThemeName;
                return extension1;
            };
    }
}

