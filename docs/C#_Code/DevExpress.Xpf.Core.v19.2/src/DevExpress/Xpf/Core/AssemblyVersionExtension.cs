namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class AssemblyVersionExtension : MarkupExtension
    {
        public AssemblyVersionExtension()
        {
        }

        public AssemblyVersionExtension(bool showShortVersion)
        {
            this.ShowShortVersion = showShortVersion;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            !this.ShowShortVersion ? ("version " + "19.2.9.0") : "19.2.9.0";

        public bool ShowShortVersion { get; set; }
    }
}

