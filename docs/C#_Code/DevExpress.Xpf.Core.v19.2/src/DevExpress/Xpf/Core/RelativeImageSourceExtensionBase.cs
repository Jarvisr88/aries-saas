namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Interop;
    using System.Windows.Markup;
    using System.Windows.Media.Imaging;

    public abstract class RelativeImageSourceExtensionBase : MarkupExtension
    {
        protected RelativeImageSourceExtensionBase(string relativePath)
        {
            this.RelativePath = relativePath;
        }

        protected virtual Uri GetUri(IServiceProvider serviceProvider) => 
            DevExpress.Xpf.Core.UriHelper.GetUri(this.Namespace, (!this.DenyXBAP || !BrowserInteropHelper.IsBrowserHosted) ? this.RelativePath : "Themes/Fake.xaml", null);

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            new BitmapImage(this.GetUri(serviceProvider));

        public string RelativePath { get; set; }

        protected abstract string Namespace { get; }

        public bool DenyXBAP { get; set; }
    }
}

