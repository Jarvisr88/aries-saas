namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfSolidDiamondHatchPatternConstructor : PdfCheckerBoardHatchPatternConstructor
    {
        public PdfSolidDiamondHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
            base.RotateTransform(-45f);
        }
    }
}

