namespace DevExpress.Xpf.Core
{
    using System;

    [Obsolete]
    public class DockingDefaultThemeInfo : DefaultThemeInfo
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            (Uri) new DockingResourceExtension("Themes/" + DefaultThemeInfo.DefaultThemeName + ".xaml").ProvideValue(null);
    }
}

