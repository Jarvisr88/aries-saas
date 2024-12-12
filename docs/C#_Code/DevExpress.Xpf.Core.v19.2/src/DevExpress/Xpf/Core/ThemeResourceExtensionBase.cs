namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Interop;
    using System.Windows.Markup;

    public abstract class ThemeResourceExtensionBase : MarkupExtension
    {
        protected ThemeResourceExtensionBase(string resourcePath)
        {
            this.ResourcePath = resourcePath;
            this.ShortPaths = false;
        }

        protected abstract string GetResourcePath(IServiceProvider serviceProvider);
        public sealed override object ProvideValue(IServiceProvider serviceProvider)
        {
            string str = (!this.DenyXBAP || !BrowserInteropHelper.IsBrowserHosted) ? this.GetResourcePath(serviceProvider) : "Themes/Fake.xaml";
            return new Uri($"/{this.Namespace}{".v19.2"}{this.Postfix};component/{str}", UriKind.RelativeOrAbsolute);
        }

        public string ResourcePath { get; set; }

        public bool ShortPaths { get; set; }

        public bool DenyXBAP { get; set; }

        protected abstract string Namespace { get; }

        protected virtual string Postfix =>
            null;
    }
}

