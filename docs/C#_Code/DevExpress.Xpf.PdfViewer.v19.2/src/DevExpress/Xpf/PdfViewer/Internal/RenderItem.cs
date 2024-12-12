namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.PdfViewer;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class RenderItem
    {
        public System.Windows.Rect Rect { get; set; }

        public DevExpress.Xpf.DocumentViewer.PageWrapper PageWrapper { get; set; }

        public ISupportDefferedRendering Page { get; set; }

        public bool NeedsInvalidate { get; set; }

        public bool ForceInvalidate { get; set; }

        public DevExpress.Xpf.PdfViewer.Internal.TextureType TextureType { get; set; }
    }
}

