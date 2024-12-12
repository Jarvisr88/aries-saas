namespace DevExpress.Xpf.Utils.Themes
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class ThemePartLoaderExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            string path = this.Path;
            string themeName = ThemeNameHelper.GetThemeName(serviceProvider);
            return (!string.IsNullOrEmpty(themeName) ? new Uri(string.Format(ThemeNameHelper.ThemeAssemblyPrefix + "{0}.v19.2;component{1}", themeName, path), UriKind.RelativeOrAbsolute) : new Uri(string.Format(ThemeNameHelper.GetAssemblyName(serviceProvider) + ";component{0}", this.PathCore), UriKind.RelativeOrAbsolute));
        }

        public string AssemblyName { get; set; }

        public string Path { get; set; }

        public string PathCore { get; set; }
    }
}

