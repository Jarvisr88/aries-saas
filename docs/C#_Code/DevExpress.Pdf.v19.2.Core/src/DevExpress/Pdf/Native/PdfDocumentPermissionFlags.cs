namespace DevExpress.Pdf.Native
{
    using System;

    internal enum PdfDocumentPermissionFlags : long
    {
        Printing = 4L,
        Modifying = 8L,
        DataExtraction = 0x10L,
        ModifyingFormFieldsAndAnnotations = 0x20L,
        FormFilling = 0x100L,
        Accessibility = 0x200L,
        DocumentAssembling = 0x400L,
        HighQualityPrinting = 0x800L
    }
}

