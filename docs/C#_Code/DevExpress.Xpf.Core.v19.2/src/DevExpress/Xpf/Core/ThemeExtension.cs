namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class ThemeExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            Theme theme = Theme.FindTheme(this.Name);
            if (theme != null)
            {
                return theme;
            }
            Theme theme1 = new Theme(this.Name);
            theme1.AssemblyName = this.AssemblyName;
            theme1.PublicKeyToken = this.PublicKeyToken;
            theme1.Version = this.Version;
            Theme theme2 = theme1;
            Theme.RegisterTheme(theme2);
            return theme2;
        }

        public string Name { get; set; }

        public string AssemblyName { get; set; }

        public string Version { get; set; }

        public string PublicKeyToken { get; set; }
    }
}

