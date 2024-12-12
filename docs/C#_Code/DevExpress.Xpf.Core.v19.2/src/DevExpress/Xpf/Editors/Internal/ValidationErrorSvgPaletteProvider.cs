namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors.Themes;
    using DevExpress.Xpf.Utils.Themes;
    using System;
    using System.Windows;
    using System.Windows.Markup;
    using System.Xaml;

    public class ValidationErrorSvgPaletteProvider : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            IRootObjectProvider service = (IRootObjectProvider) serviceProvider.GetService(typeof(IRootObjectProvider));
            if (service == null)
            {
                return null;
            }
            ResourceDictionary rootObject = service.RootObject as ResourceDictionary;
            if (rootObject == null)
            {
                return null;
            }
            string themeName = ThemeNameHelper.GetThemeName(serviceProvider);
            InplaceBaseEditThemeKeyExtension extension1 = new InplaceBaseEditThemeKeyExtension();
            extension1.ResourceKey = InplaceBaseEditThemeKeys.ValidationErrorSvgPalette;
            extension1.ThemeName = themeName;
            return rootObject[extension1];
        }
    }
}

