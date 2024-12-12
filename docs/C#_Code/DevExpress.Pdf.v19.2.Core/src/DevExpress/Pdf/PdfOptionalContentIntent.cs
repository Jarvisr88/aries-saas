namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [Flags, PdfDefaultField(1)]
    public enum PdfOptionalContentIntent
    {
        View = 1,
        Design = 2,
        All = 3
    }
}

