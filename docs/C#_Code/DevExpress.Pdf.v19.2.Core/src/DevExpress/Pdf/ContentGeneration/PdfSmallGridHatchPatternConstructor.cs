namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfSmallGridHatchPatternConstructor : PdfCrossHatchPatternConstructor
    {
        public PdfSmallGridHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
        }

        protected override double LineStep =>
            base.LineStep / 2.0;
    }
}

