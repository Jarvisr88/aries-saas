namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(1)]
    public enum PdfAnnotationHighlightingMode
    {
        [PdfFieldName("N")]
        None = 0,
        [PdfFieldName("I")]
        Invert = 1,
        [PdfFieldName("O")]
        Outline = 2,
        [PdfFieldName("P")]
        Push = 3,
        [PdfFieldName("T")]
        Toggle = 4
    }
}

