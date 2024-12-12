namespace DevExpress.Pdf.Native
{
    using System;

    [PdfDefaultField(0)]
    public enum Pdf3dOrthographicBinding
    {
        [PdfFieldName("Absolute")]
        Absolute = 0,
        [PdfFieldName("W")]
        Width = 1,
        [PdfFieldName("H")]
        Height = 2,
        [PdfFieldName("Min")]
        Min = 3,
        [PdfFieldName("Max")]
        Max = 4
    }
}

