namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public enum PdfPrintFieldLogicalStructureElementAttributeRole
    {
        [PdfFieldName("rb")]
        RadioButton = 0,
        [PdfFieldName("cb")]
        CheckBox = 1,
        [PdfFieldName("pb")]
        PushButton = 2,
        [PdfFieldName("tv")]
        TextValueField = 3
    }
}

