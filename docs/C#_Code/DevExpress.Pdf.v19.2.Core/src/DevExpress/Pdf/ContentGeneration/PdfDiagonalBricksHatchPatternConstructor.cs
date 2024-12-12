namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfDiagonalBricksHatchPatternConstructor : PdfHorizontalBricksHatchPatternConstructor
    {
        public PdfDiagonalBricksHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
            base.RotateTransform(-45f);
        }
    }
}

