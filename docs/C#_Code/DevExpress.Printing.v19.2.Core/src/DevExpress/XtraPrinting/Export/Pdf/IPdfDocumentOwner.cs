namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.XtraPrinting.Native;
    using System;

    public interface IPdfDocumentOwner
    {
        PdfNeverEmbeddedFonts NeverEmbeddedFonts { get; }

        PdfImageCache ImageCache { get; }

        DevExpress.XtraPrinting.Native.Measurer Measurer { get; }

        DevExpress.XtraPrinting.Native.Measurer MetafileMeasurer { get; }

        bool ScaleStrings { get; }
    }
}

