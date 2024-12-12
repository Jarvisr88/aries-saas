namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfPercent50HatchPatternConstructor : PdfPercentHatchPatternConstructor
    {
        private static int[] elementsCount = new int[] { 4, 4, 4, 4, 4, 4, 4, 4 };
        private static int[] offsets = new int[] { 0, 1, 0, 1, 0, 1, 0, 1 };

        public PdfPercent50HatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush, elementsCount, offsets)
        {
        }
    }
}

