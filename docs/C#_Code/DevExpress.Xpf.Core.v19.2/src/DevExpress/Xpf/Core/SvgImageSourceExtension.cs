namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public class SvgImageSourceExtension : BaseSvgImageSourceExtension
    {
        protected override System.Uri CreateSvgUri(IServiceProvider provider)
        {
            System.Uri uri;
            System.Uri uri2;
            return (UriQualifierHelper.MakeAbsoluteUri(provider, this.Uri, out uri2, out uri) ? uri2 : null);
        }

        public System.Uri Uri { get; set; }
    }
}

