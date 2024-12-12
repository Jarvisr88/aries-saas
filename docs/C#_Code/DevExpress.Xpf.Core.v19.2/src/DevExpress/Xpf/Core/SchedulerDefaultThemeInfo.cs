namespace DevExpress.Xpf.Core
{
    using System;

    [Obsolete]
    public class SchedulerDefaultThemeInfo : DefaultThemeInfo
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            (Uri) new SchedulerResourceExtension("Themes/" + DefaultThemeInfo.DefaultThemeName + ".xaml").ProvideValue(null);
    }
}

