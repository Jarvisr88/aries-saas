namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfWidgetAnnotationTextPosition
    {
        [PdfFieldValue(0)]
        NoIcon = 0,
        [PdfFieldValue(1)]
        NoCaption = 1,
        [PdfFieldValue(2)]
        CaptionBelowTheIcon = 2,
        [PdfFieldValue(3)]
        CaptionAboveTheIcon = 3,
        [PdfFieldValue(4)]
        CaptionToTheRightOfTheIcon = 4,
        [PdfFieldValue(5)]
        CaptionToTheLeftOfTheIcon = 5,
        [PdfFieldValue(6)]
        CaptionOverlaidDirectlyOnTheIcon = 6
    }
}

