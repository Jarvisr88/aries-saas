namespace DevExpress.Pdf.Native
{
    using System;

    [PdfDefaultField(0)]
    public enum Pdf3dPerspectiveScaling
    {
        [PdfFieldName("W")]
        Width = 0,
        [PdfFieldName("H")]
        Height = 1,
        [PdfFieldName("Min")]
        Min = 2,
        [PdfFieldName("Max")]
        Max = 3
    }
}

