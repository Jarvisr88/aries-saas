namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Utils.Themes;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class DynamicThemeResourceExtension : MarkupExtension
    {
        public DynamicThemeResourceExtension(string resourceName)
        {
            this.ResourceName = resourceName;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            string themeName = ThemeNameHelper.GetThemeName(serviceProvider);
            if (string.IsNullOrEmpty(themeName))
            {
                string str2 = this.UseGeneric ? "Generic" : Theme.DeepBlue.Name;
                return new Uri($"/{this.TypeInTargetAssembly.Assembly.GetName().Name};component/{this.ResourcePath}/{str2}/{this.ResourceName}".Replace("//", "/"), UriKind.RelativeOrAbsolute);
            }
            string str3 = this.UseGeneric ? "Generic" : themeName;
            return new Uri($"/DevExpress.Xpf.Themes.{themeName}{".v19.2"};component/{this.ResourcePathInTheme}/{str3}/{this.ResourceName}".Replace("//", "/"), UriKind.RelativeOrAbsolute);
        }

        public string ResourceName { get; set; }

        public string ResourcePath { get; set; }

        public string ResourcePathInTheme { get; set; }

        public Type TypeInTargetAssembly { get; set; }

        public bool UseGeneric { get; set; }
    }
}

