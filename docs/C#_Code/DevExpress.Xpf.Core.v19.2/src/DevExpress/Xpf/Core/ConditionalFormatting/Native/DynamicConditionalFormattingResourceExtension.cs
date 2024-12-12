namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Utils.Themes;
    using System;
    using System.Runtime.CompilerServices;

    public class DynamicConditionalFormattingResourceExtension : ThemeResourceExtensionBase
    {
        private const string coreAssemblyName = "DevExpress.Xpf.Core";
        private const string inThemePath = "DevExpress.Xpf.Core/DevExpress.Xpf.Core/";
        private const string inThemeShortPath = "Core/Core/";
        private const string defaultThemeName = "DeepBlue";
        private const string themesPrefix = "DevExpress.Xpf.Themes";

        public DynamicConditionalFormattingResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        public static string CorrectThemeName(IServiceProvider serviceProvider)
        {
            string themeName = ThemeNameHelper.GetThemeName(serviceProvider);
            return ((string.IsNullOrEmpty(themeName) || (themeName == "MetropolisLight")) ? null : themeName);
        }

        protected override string GetResourcePath(IServiceProvider serviceProvider)
        {
            this.ThemeName = CorrectThemeName(serviceProvider);
            return $"{((this.ThemeName == null) ? "" : (base.ShortPaths ? "Core/Core/" : "DevExpress.Xpf.Core/DevExpress.Xpf.Core/"))}Themes/{(this.ThemeName ?? "DeepBlue")}/{base.ResourcePath}";
        }

        protected string ThemeName { get; set; }

        protected sealed override string Namespace =>
            (this.ThemeName != null) ? $"{"DevExpress.Xpf.Themes"}.{this.ThemeName}" : "DevExpress.Xpf.Core";
    }
}

