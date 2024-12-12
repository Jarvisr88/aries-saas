namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfFreeTextAnnotationIntent
    {
        FreeText = 0,
        FreeTextCallout = 1,
        [PdfFieldName("FreeTextTypewriter", "FreeTextTypeWriter")]
        FreeTextTypewriter = 2
    }
}

