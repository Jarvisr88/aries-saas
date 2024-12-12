namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Utils.Themes;
    using System;
    using System.Runtime.CompilerServices;

    public class ThemePartKeyExtension : ThemePartLoaderExtension
    {
        public override bool Equals(object obj)
        {
            ThemePartKeyExtension extension = obj as ThemePartKeyExtension;
            return ((extension != null) ? string.Equals(base.AssemblyName, extension.AssemblyName, StringComparison.InvariantCultureIgnoreCase) : false);
        }

        public override int GetHashCode() => 
            (base.AssemblyName != null) ? base.AssemblyName.GetHashCode() : 0;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            this.Uri = base.ProvideValue(serviceProvider) as System.Uri;
            return this;
        }

        internal System.Uri Uri { get; set; }
    }
}

