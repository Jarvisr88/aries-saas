namespace DevExpress.Pdf.Native
{
    using System;

    [PdfDefaultField(1)]
    public enum PdfDocumentAccessPermissions
    {
        [PdfFieldValue(1)]
        NoChanges = 0,
        [PdfFieldValue(2)]
        FormFillingAndSignatures = 1,
        [PdfFieldValue(3)]
        AnnotationsFormFillingAndSignatures = 2
    }
}

