namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfDottedGridHatchPatternConstructor : PdfCrossHatchPatternConstructor
    {
        public PdfDottedGridHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
        }

        protected override void FillCommands(PdfCommandConstructor constructor)
        {
            double[] dashPattern = new double[] { this.LineWidth };
            constructor.SetLineStyle(PdfLineStyle.CreateDashed(dashPattern, 0.0));
            base.FillCommands(constructor);
        }

        protected override PdfLineCapStyle LineCapStyle =>
            PdfLineCapStyle.Butt;
    }
}

