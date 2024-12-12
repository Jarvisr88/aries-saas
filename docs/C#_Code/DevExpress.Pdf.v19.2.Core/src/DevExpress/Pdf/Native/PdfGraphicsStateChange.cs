namespace DevExpress.Pdf.Native
{
    using System;

    [Flags]
    public enum PdfGraphicsStateChange
    {
        None = 0,
        Pen = 1,
        RenderingIntent = 2,
        Overprint = 4,
        Font = 8,
        BlackGenerationFunction = 0x10,
        UndercolorRemovalFunction = 0x20,
        TransferFunction = 0x40,
        Halftone = 0x80,
        FlatnessTolerance = 0x100,
        SmoothnessTolerance = 0x200,
        StrokeAdjustment = 0x400,
        BlendMode = 0x800,
        SoftMask = 0x1000,
        Alpha = 0x2000,
        TextKnockout = 0x4000
    }
}

