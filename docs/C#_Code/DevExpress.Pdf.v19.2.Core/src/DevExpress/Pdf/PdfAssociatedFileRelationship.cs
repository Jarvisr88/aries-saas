namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfAssociatedFileRelationship
    {
        Source,
        Data,
        Alternative,
        Supplement,
        EncryptedPayload,
        Unspecified
    }
}

