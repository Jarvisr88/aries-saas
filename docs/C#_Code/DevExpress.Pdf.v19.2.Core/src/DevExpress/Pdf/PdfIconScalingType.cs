namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(1)]
    public enum PdfIconScalingType
    {
        [PdfFieldName("A")]
        Anamorphic = 0,
        [PdfFieldName("P")]
        Proportional = 1
    }
}

