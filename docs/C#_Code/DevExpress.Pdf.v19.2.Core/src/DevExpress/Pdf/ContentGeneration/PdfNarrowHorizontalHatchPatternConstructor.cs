namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfNarrowHorizontalHatchPatternConstructor : PdfHorizontalHatchPatternConstructor
    {
        public PdfNarrowHorizontalHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
        }

        protected override double LineStep =>
            base.LineStep / 4.0;
    }
}

