namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Interop;
    using System.Windows.Markup;

    [Obsolete("for testing purposes only DO not use in production code!")]
    public class TestThemeResourceExtension : MarkupExtension
    {
        public TestThemeResourceExtension(string resourcePath)
        {
            this.ResourcePath = resourcePath;
        }

        public sealed override object ProvideValue(IServiceProvider serviceProvider) => 
            new Uri($"/{this.Namespace};component/{(!this.DenyXBAP || !BrowserInteropHelper.IsBrowserHosted) ? this.ResourcePath : "Themes/Fake.xaml"}", UriKind.RelativeOrAbsolute);

        public string ResourcePath { get; set; }

        public bool DenyXBAP { get; set; }

        protected string Namespace =>
            "TestTheme";
    }
}

