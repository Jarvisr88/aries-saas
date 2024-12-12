namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfOutlinedDiamondHatchPatternConstructor : PdfCrossHatchPatternConstructor
    {
        public PdfOutlinedDiamondHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
            base.RotateTransform(45f);
        }
    }
}

