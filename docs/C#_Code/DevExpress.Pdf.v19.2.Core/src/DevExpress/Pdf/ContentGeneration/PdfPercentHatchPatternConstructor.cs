namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfPercentHatchPatternConstructor : PdfHatchPatternConstructor
    {
        private readonly int[] elementsCount;
        private readonly int[] offsets;

        public PdfPercentHatchPatternConstructor(DXHatchBrush hatchBrush, int[] elementsCount, int[] offsets) : base(hatchBrush)
        {
            this.elementsCount = elementsCount;
            this.offsets = offsets;
        }

        protected override void FillCommands(PdfCommandConstructor constructor)
        {
            base.FillCommands(constructor);
            double rectangleSize = this.RectangleSize;
            int index = 0;
            while (index < this.elementsCount.Length)
            {
                int num3 = this.elementsCount[index];
                double left = (index * this.LineStep) / ((double) this.elementsCount.Length);
                int num5 = 0;
                while (true)
                {
                    if (num5 >= num3)
                    {
                        index++;
                        break;
                    }
                    double bottom = ((num5 * this.LineStep) / ((double) num3)) + (this.offsets[index] * rectangleSize);
                    constructor.FillRectangle(new PdfRectangle(left, bottom, left + rectangleSize, bottom + rectangleSize));
                    num5++;
                }
            }
        }

        protected double RectangleSize =>
            this.LineStep / 8.0;
    }
}

