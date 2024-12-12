namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfLayoutLogicalStructureElementAttributeWritingMode
    {
        [PdfFieldName("LrTb")]
        LeftToRight = 0,
        [PdfFieldName("RlTb")]
        RightToLeft = 1,
        [PdfFieldName("TbRl")]
        TopToBottom = 2
    }
}

