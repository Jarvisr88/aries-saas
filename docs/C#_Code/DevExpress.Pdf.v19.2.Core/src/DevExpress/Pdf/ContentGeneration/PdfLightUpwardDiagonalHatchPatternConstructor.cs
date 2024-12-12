namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfLightUpwardDiagonalHatchPatternConstructor : PdfBackwardDiagonalHatchPatternConstructor
    {
        public PdfLightUpwardDiagonalHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
        }

        protected override double LineStep =>
            base.LineStep / 2.0;
    }
}

