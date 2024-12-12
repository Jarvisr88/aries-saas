namespace DevExpress.Xpf.Core
{
    using System;

    [Obsolete]
    public class NavBarDefaultThemeInfo : DefaultThemeInfo
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            (Uri) new NavBarResourceExtension("Themes/" + DefaultThemeInfo.DefaultThemeName + ".xaml").ProvideValue(null);
    }
}

