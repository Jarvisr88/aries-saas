namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfDashedDownwardDiagonalHatchPatternConstructor : PdfDashedHorizontalHatchPatternConstructor
    {
        public PdfDashedDownwardDiagonalHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
            base.RotateTransform(45f);
        }
    }
}

