namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfSupportUndefinedValue, PdfDefaultField(0)]
    public enum PdfPageLayout
    {
        SinglePage,
        OneColumn,
        TwoColumnLeft,
        TwoColumnRight,
        TwoPageLeft,
        TwoPageRight
    }
}

