namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Utils.Themes;
    using System;
    using System.Windows.Interop;

    public class DashboardResourceExtension : ThemeResourceExtensionBase
    {
        public DashboardResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string GetResourcePath(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(ThemeNameHelper.GetThemeName(serviceProvider)))
            {
                return ("Themes/" + base.ResourcePath);
            }
            string str2 = (!base.DenyXBAP || !BrowserInteropHelper.IsBrowserHosted) ? base.ResourcePath : "Themes/Fake.xaml";
            return $"/{this.Namespace}{".v19.2"};component/{str2}";
        }

        protected override string Namespace =>
            "DevExpress.Xpf.Dashboard";
    }
}

