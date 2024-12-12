namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(1), PdfSupportUndefinedValue]
    public enum PdfRenderingIntent
    {
        AbsoluteColorimetric,
        RelativeColorimetric,
        Saturation,
        Perceptual
    }
}

