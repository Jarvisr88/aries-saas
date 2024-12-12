namespace DevExpress.Xpf.DocumentViewer
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class DocumentViewerResourceExtension : MarkupExtension
    {
        private readonly string dllName;

        public DocumentViewerResourceExtension(string resourcePath)
        {
            this.ResourcePath = resourcePath;
            this.dllName = Assembly.GetExecutingAssembly().GetName().Name;
        }

        public sealed override object ProvideValue(IServiceProvider serviceProvider) => 
            DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, this.ResourcePath);

        public string ResourcePath { get; set; }
    }
}

