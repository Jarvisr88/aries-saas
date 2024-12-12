namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfWideDownwardDiagonalHatchPatternConstructor : PdfForwardDiagonalHatchPatternConstructor
    {
        public PdfWideDownwardDiagonalHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
        }

        protected override double LineWidth =>
            base.LineWidth * 3.0;
    }
}

