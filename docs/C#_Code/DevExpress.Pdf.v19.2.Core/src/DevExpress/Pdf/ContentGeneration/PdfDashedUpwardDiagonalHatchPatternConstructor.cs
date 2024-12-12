namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfDashedUpwardDiagonalHatchPatternConstructor : PdfDashedDownwardDiagonalHatchPatternConstructor
    {
        public PdfDashedUpwardDiagonalHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
            base.RotateTransform(90f);
        }
    }
}

