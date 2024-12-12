namespace DevExpress.Pdf.Native
{
    using System;

    [PdfDefaultField(1)]
    public enum Pdf3dDeactivationCircumstances
    {
        [PdfFieldName("PC")]
        PageClosed = 0,
        [PdfFieldName("PI")]
        PageInvisible = 1,
        [PdfFieldName("XD")]
        ExplicitlyDeactivated = 2
    }
}

