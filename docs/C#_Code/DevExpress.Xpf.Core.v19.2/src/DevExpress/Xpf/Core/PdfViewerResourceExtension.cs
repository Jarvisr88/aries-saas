namespace DevExpress.Xpf.Core
{
    using System;

    public class PdfViewerResourceExtension : ResourceExtensionBase
    {
        public PdfViewerResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Xpf.PdfViewer";
    }
}

