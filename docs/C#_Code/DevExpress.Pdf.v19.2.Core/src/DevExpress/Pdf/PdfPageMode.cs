namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfPageMode
    {
        [PdfFieldName("UseNone", "None")]
        UseNone = 0,
        UseOutlines = 1,
        UseThumbs = 2,
        FullScreen = 3,
        [PdfFieldName("UseOC", "UseOCG")]
        UseOC = 4,
        [PdfFieldName("UseAttachments", "UseAtachments")]
        UseAttachments = 5
    }
}

