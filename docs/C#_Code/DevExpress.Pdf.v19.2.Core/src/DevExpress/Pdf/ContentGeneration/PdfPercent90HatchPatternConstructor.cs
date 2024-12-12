namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using System;

    public class PdfPercent90HatchPatternConstructor : PdfPercent05HatchPatternConstructor
    {
        public PdfPercent90HatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
            base.RotateTransform(180f);
            base.MultipleTransform(new PdfTransformationMatrix(-1.0, 0.0, 0.0, -1.0, 0.0, 0.0));
        }
    }
}

