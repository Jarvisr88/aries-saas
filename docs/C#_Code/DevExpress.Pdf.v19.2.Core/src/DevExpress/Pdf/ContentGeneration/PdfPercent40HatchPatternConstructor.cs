namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfPercent40HatchPatternConstructor : PdfPercentHatchPatternConstructor
    {
        private static int[] elementsCount = new int[] { 4, 0, 4, 4, 4, 0, 4, 4 };
        private static int[] offsets = new int[] { 0, 0, 0, 1, 0, 4, 0, 1 };

        public PdfPercent40HatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush, elementsCount, offsets)
        {
        }

        protected override void FillCommands(PdfCommandConstructor constructor)
        {
            base.FillCommands(constructor);
            double rectangleSize = base.RectangleSize;
            for (int i = 1; i < 6; i += 2)
            {
                constructor.FillRectangle(new PdfRectangle(rectangleSize, i * rectangleSize, rectangleSize * 2.0, (i + 1) * rectangleSize));
                double left = (5.0 * this.LineStep) / 8.0;
                double bottom = ((i + 4) % 8) * rectangleSize;
                constructor.FillRectangle(new PdfRectangle(left, bottom, left + rectangleSize, bottom + rectangleSize));
            }
        }
    }
}

