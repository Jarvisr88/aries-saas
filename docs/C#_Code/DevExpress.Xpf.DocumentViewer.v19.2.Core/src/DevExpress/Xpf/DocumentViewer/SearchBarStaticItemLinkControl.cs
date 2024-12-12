namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using System;

    public class SearchBarStaticItemLinkControl : BarStaticItemLinkControl
    {
        protected override Func<BarItemLayoutPanelThemeKeys, BarItemLayoutPanelThemeKeyExtension> GetThemeKeyExtensionFunc =>
            delegate (BarItemLayoutPanelThemeKeys key) {
                SearchBarStaticItemLayoutPanelThemeKeyExtension extension1 = new SearchBarStaticItemLayoutPanelThemeKeyExtension();
                extension1.ResourceKey = key;
                extension1.ThemeName = base.ThemeName;
                return extension1;
            };
    }
}

