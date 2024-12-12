namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Utils.Themes;
    using System;

    public class SchedulingResourceExtension : ThemeResourceExtensionBase
    {
        private const string coreAssemblyName = "DevExpress.Xpf.Scheduling";
        private const string themesPrefix = "DevExpress.Xpf.Themes";
        private string themeName;

        public SchedulingResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string GetResourcePath(IServiceProvider serviceProvider)
        {
            this.themeName = ThemeNameHelper.GetThemeName(serviceProvider);
            if (!string.IsNullOrEmpty(this.themeName))
            {
                return (!base.ShortPaths ? ("DevExpress.Xpf.Scheduling/" + base.ResourcePath) : ("Scheduling/" + base.ResourcePath));
            }
            this.themeName = null;
            return ("Themes/" + base.ResourcePath);
        }

        protected sealed override string Namespace =>
            (this.themeName != null) ? $"{"DevExpress.Xpf.Themes"}.{this.themeName}" : "DevExpress.Xpf.Scheduling";
    }
}

