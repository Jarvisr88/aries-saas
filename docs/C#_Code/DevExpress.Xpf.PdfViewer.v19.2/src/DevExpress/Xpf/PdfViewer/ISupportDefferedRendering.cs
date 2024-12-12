namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.PdfViewer.Internal;
    using System;

    public interface ISupportDefferedRendering : IPage
    {
        bool NeedsInvalidate { get; set; }

        bool ForceInvalidate { get; set; }

        DevExpress.Xpf.PdfViewer.Internal.TextureKey TextureKey { get; set; }

        double DpiMultiplier { get; }
    }
}

