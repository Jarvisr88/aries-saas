namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfIconScalingCircumstances
    {
        [PdfFieldName("A")]
        Always = 0,
        [PdfFieldName("B")]
        BiggerThanAnnotationRectangle = 1,
        [PdfFieldName("S")]
        SmallerThanAnnotationRectangle = 2,
        [PdfFieldName("N")]
        Never = 3
    }
}

