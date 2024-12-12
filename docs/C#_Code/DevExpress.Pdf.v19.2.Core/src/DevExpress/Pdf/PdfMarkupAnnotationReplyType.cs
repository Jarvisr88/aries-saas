namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfMarkupAnnotationReplyType
    {
        [PdfFieldName("R")]
        Reply = 0,
        Group = 1
    }
}

