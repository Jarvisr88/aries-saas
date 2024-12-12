namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class PdfResourceExtension : MarkupExtension
    {
        private readonly string dllName;

        public PdfResourceExtension(string resourcePath)
        {
            this.ResourcePath = resourcePath;
            this.dllName = Assembly.GetExecutingAssembly().GetName().Name;
        }

        public sealed override object ProvideValue(IServiceProvider serviceProvider) => 
            ((IValueConverter) new PdfUriToBitmapImageConverterExtension().ProvideValue(serviceProvider)).Convert(DevExpress.Xpf.DocumentViewer.UriHelper.GetAbsoluteUri(this.dllName, this.ResourcePath), typeof(Uri), null, CultureInfo.InvariantCulture);

        public string ResourcePath { get; set; }
    }
}

