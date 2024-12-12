namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfVerticalHatchPatternConstructor : PdfHorizontalHatchPatternConstructor
    {
        public PdfVerticalHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
            base.RotateTransform(90f);
        }
    }
}

