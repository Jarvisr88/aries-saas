namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfZigZagHatchPatternConstructor : PdfHatchPatternConstructor
    {
        public PdfZigZagHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush)
        {
        }

        protected override void FillCommands(PdfCommandConstructor constructor)
        {
            base.FillCommands(constructor);
            double num = this.LineWidth / 2.0;
            double x = this.LineStep / 2.0;
            for (int i = 0; i < 2; i++)
            {
                double y = num + (x * i);
                PdfPoint[] points = new PdfPoint[] { new PdfPoint(0.0, y), new PdfPoint(x, (x * (i + 1)) - num), new PdfPoint(this.LineStep, y) };
                constructor.DrawLines(points);
            }
        }
    }
}

