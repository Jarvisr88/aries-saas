namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(1)]
    public enum PdfPrintFieldLogicalStructureElementAttributeRadioButtonState
    {
        [PdfFieldName("on")]
        On = 0,
        [PdfFieldName("off")]
        Off = 1,
        [PdfFieldName("neutral")]
        Neutral = 2
    }
}

