namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfDiagonalCrossHatchPatternConstructor : PdfCrossHatchPatternConstructor
    {
        public PdfDiagonalCrossHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
            base.RotateTransform(-45f);
        }
    }
}

