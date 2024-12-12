namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Utils.Themes;
    using System;
    using System.Reflection;

    public class DockingDynamicThemeResourceExtension : DynamicThemeResourceExtension
    {
        public DockingDynamicThemeResourceExtension(string resourceName) : base(resourceName)
        {
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            string themeName = ThemeNameHelper.GetThemeName(serviceProvider);
            if (string.IsNullOrEmpty(themeName))
            {
                Assembly assembly = (base.TypeInTargetAssembly != null) ? base.TypeInTargetAssembly.Assembly : Assembly.GetExecutingAssembly();
                string str2 = base.UseGeneric ? "IndependentParts" : Theme.DeepBlue.Name;
                return new Uri($"/{assembly.GetName().Name};component/{base.ResourcePath}/{str2}/{base.ResourceName}".Replace("//", "/"), UriKind.RelativeOrAbsolute);
            }
            string str3 = base.UseGeneric ? "Generic" : themeName;
            return new Uri($"/DevExpress.Xpf.Themes.{themeName}{".v19.2"};component/{base.ResourcePathInTheme}/{str3}/{base.ResourceName}".Replace("//", "/"), UriKind.RelativeOrAbsolute);
        }
    }
}

