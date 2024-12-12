namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfBackwardDiagonalHatchPatternConstructor : PdfForwardDiagonalHatchPatternConstructor
    {
        public PdfBackwardDiagonalHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
            base.RotateTransform(-90f);
        }
    }
}

