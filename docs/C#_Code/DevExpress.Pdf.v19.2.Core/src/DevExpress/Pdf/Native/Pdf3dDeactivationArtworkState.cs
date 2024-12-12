namespace DevExpress.Pdf.Native
{
    using System;

    [PdfDefaultField(0)]
    public enum Pdf3dDeactivationArtworkState
    {
        [PdfFieldName("U")]
        Uninstantiated = 0,
        [PdfFieldName("I")]
        Instantiated = 1,
        [PdfFieldName("L")]
        Live = 2
    }
}

