namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Utils.Themes;
    using System;

    public class DynamicEditorsResourceExtension : ThemeResourceExtensionBase
    {
        private const string coreAssemblyName = "DevExpress.Xpf.Core";
        private const string editorsInThemePath = "DevExpress.Xpf.Core/DevExpress.Xpf.Core/";
        private const string editorsInThemeShortPath = "Core/Core/";
        private const string defaultThemeName = "DeepBlue";
        private const string themesPrefix = "DevExpress.Xpf.Themes";
        private string themeName;

        public DynamicEditorsResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string GetResourcePath(IServiceProvider serviceProvider)
        {
            this.themeName = ThemeNameHelper.GetThemeName(serviceProvider);
            if (string.IsNullOrEmpty(this.themeName))
            {
                this.themeName = null;
            }
            return $"{((this.themeName == null) ? "" : (base.ShortPaths ? "Core/Core/" : "DevExpress.Xpf.Core/DevExpress.Xpf.Core/"))}Editors/Themes/{(this.themeName ?? "DeepBlue")}/{base.ResourcePath}";
        }

        protected sealed override string Namespace =>
            (this.themeName != null) ? $"{"DevExpress.Xpf.Themes"}.{this.themeName}" : "DevExpress.Xpf.Core";
    }
}

