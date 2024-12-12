namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfCrossHatchPatternConstructor : PdfHatchPatternConstructor
    {
        public PdfCrossHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
        }

        protected override void FillCommands(PdfCommandConstructor constructor)
        {
            base.FillCommands(constructor);
            double lineStep = this.LineStep;
            PdfPoint[] points = new PdfPoint[] { new PdfPoint(0.0, lineStep), new PdfPoint(0.0, 0.0), new PdfPoint(lineStep, 0.0) };
            constructor.DrawLines(points);
        }
    }
}

