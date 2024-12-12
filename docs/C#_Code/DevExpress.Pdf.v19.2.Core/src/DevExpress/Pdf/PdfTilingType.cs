namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public enum PdfTilingType
    {
        [PdfFieldValue(1)]
        ConstantSpacing = 0,
        [PdfFieldValue(2)]
        NoDistortion = 1,
        [PdfFieldValue(3)]
        FasterTiling = 2
    }
}

