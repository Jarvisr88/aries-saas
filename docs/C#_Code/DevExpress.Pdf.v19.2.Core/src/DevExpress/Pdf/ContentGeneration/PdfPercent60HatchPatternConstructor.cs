namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using System;

    public class PdfPercent60HatchPatternConstructor : PdfPercent30HatchPatternConstructor
    {
        public PdfPercent60HatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
            base.MultipleTransform(new PdfTransformationMatrix(1.0, 0.0, 0.0, 1.0, 0.0, -base.RectangleSize));
        }
    }
}

