namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfDashedVerticalHatchPatternConstructor : PdfDashedHorizontalHatchPatternConstructor
    {
        public PdfDashedVerticalHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
            base.RotateTransform(90f);
        }
    }
}

