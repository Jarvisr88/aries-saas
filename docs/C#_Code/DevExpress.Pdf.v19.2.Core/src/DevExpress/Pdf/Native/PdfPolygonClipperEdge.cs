namespace DevExpress.Pdf.Native
{
    using System;

    [Flags]
    internal enum PdfPolygonClipperEdge
    {
        None = 0,
        Left = 1,
        Right = 2,
        Bottom = 4,
        Top = 8
    }
}

