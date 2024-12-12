namespace DevExpress.Pdf.Native
{
    using System;

    [PdfDefaultField(0)]
    public enum Pdf3dClippingStyle
    {
        [PdfFieldName("ANF")]
        Automatic = 0,
        [PdfFieldName("XNF")]
        Explicit = 1
    }
}

