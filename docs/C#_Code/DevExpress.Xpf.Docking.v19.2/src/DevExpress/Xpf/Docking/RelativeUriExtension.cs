namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    [MarkupExtensionReturnType(typeof(Uri))]
    public class RelativeUriExtension : MarkupExtension
    {
        public RelativeUriExtension()
        {
        }

        public RelativeUriExtension(string uriString)
        {
            this.UriString = uriString;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            new Uri(this.UriString, UriKind.Relative);

        [ConstructorArgument("uriString")]
        public string UriString { get; set; }
    }
}

