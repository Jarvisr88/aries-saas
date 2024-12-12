namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfNarrowVerticalHatchPatternConstructor : PdfVerticalHatchPatternConstructor
    {
        public PdfNarrowVerticalHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
        }

        protected override double LineStep =>
            base.LineStep / 4.0;
    }
}

