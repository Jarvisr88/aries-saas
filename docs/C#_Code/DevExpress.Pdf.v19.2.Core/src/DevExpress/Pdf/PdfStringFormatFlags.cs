namespace DevExpress.Pdf
{
    using System;

    [Flags]
    public enum PdfStringFormatFlags
    {
        MeasureTrailingSpaces = 1,
        NoWrap = 2,
        LineLimit = 4,
        NoClip = 8
    }
}

