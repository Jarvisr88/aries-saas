namespace DevExpress.Pdf.Native
{
    using System;

    [Flags]
    public enum PdfDocumentStateChangedFlags
    {
        AllContent = 1,
        Selection = 2,
        Annotation = 4
    }
}

