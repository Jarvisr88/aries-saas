namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfNonFullScreenPageMode
    {
        [PdfFieldName("UseNone", "None")]
        UseNone = 0,
        [PdfFieldName("UseOutlines", "Outlines")]
        UseOutlines = 1,
        [PdfFieldName("UseThumbs", "Thumbs")]
        UseThumbs = 2,
        [PdfFieldName("UseOC", "OC")]
        UseOC = 3
    }
}

