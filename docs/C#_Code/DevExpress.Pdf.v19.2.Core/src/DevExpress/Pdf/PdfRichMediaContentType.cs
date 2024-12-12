namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public enum PdfRichMediaContentType
    {
        [PdfFieldName("3D")]
        Media3D = 0,
        Flash = 1,
        Sound = 2,
        Video = 3,
        [PdfFieldName("html5")]
        HTML5 = 4
    }
}

