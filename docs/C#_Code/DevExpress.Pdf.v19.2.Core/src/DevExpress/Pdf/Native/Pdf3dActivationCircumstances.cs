namespace DevExpress.Pdf.Native
{
    using System;

    [PdfDefaultField(2)]
    public enum Pdf3dActivationCircumstances
    {
        [PdfFieldName("PO")]
        PageOpening = 0,
        [PdfFieldName("PV")]
        PageVisible = 1,
        [PdfFieldName("XA")]
        ExplicitlyActivated = 2
    }
}

