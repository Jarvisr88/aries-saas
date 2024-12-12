namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfAnnotationBorderEffectStyle
    {
        [PdfFieldName("S")]
        NoEffect = 0,
        [PdfFieldName("C")]
        CloudyEffect = 1
    }
}

