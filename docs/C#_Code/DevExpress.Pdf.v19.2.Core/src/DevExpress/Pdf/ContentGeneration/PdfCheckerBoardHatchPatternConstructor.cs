namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfCheckerBoardHatchPatternConstructor : PdfHatchPatternConstructor
    {
        public PdfCheckerBoardHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
            base.RotateTransform(90f);
        }

        protected override void FillCommands(PdfCommandConstructor constructor)
        {
            base.FillCommands(constructor);
            double lineStep = this.LineStep;
            double num2 = lineStep / 2.0;
            for (double i = 0.0; i < 1.0; i += 0.5)
            {
                double x = lineStep * i;
                double y = x + num2;
                PdfPoint[] points = new PdfPoint[] { new PdfPoint(x, x), new PdfPoint(x, y), new PdfPoint(y, y), new PdfPoint(y, x) };
                constructor.FillPolygon(points, true);
            }
        }
    }
}

