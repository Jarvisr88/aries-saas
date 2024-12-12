namespace DevExpress.Xpf.Printing.PreviewControl
{
    using DevExpress.Xpf.Printing.PreviewControl.Native.Rendering;
    using DevExpress.XtraPrinting;
    using System;

    public interface IDocumentPage
    {
        bool IsSelected { get; }

        DevExpress.XtraPrinting.Page Page { get; }

        double ScaleMultiplier { get; }

        bool NeedsInvalidate { get; set; }

        bool ForceInvalidate { get; set; }

        int PageIndex { get; }

        DevExpress.Xpf.Printing.PreviewControl.Native.Rendering.TextureKey TextureKey { get; set; }
    }
}

