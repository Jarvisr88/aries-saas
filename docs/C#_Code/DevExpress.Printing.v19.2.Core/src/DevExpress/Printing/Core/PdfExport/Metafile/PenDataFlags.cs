namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;

    [Flags]
    public enum PenDataFlags
    {
        PenDataTransform = 1,
        PenDataStartCap = 2,
        PenDataEndCap = 4,
        PenDataJoin = 8,
        PenDataMiterLimit = 0x10,
        PenDataLineStyle = 0x20,
        PenDataDashedLineCap = 0x40,
        PenDataDashedLineOffset = 0x80,
        PenDataDashedLine = 0x100,
        PenDataNonCenter = 0x200,
        PenDataCompoundLine = 0x400,
        PenDataCustomStartCap = 0x800,
        PenDataCustomEndCap = 0x1000
    }
}

