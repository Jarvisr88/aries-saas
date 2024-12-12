namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfHorizontalBricksHatchPatternConstructor : PdfHatchPatternConstructor
    {
        public PdfHorizontalBricksHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
        }

        protected override void FillCommands(PdfCommandConstructor constructor)
        {
            base.FillCommands(constructor);
            double lineStep = this.LineStep;
            double y = this.LineWidth / 2.0;
            double x = lineStep / 2.0;
            double num4 = y + x;
            PdfPoint[] points = new PdfPoint[] { new PdfPoint(lineStep, y), new PdfPoint(y, y), new PdfPoint(y, num4), new PdfPoint(lineStep, num4) };
            constructor.DrawLines(points);
            constructor.DrawLine(new PdfPoint(x, x + (this.LineWidth * 1.5)), new PdfPoint(x, lineStep));
        }
    }
}

