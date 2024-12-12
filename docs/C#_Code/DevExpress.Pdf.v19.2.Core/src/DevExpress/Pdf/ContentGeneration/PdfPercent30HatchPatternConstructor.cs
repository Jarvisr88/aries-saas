namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfPercent30HatchPatternConstructor : PdfPercentHatchPatternConstructor
    {
        private static int[] elementsCount = new int[] { 4, 2, 4, 2, 4, 2, 4, 2 };
        private static int[] offsets = new int[] { 0, 1, 0, 3, 0, 1, 0, 3 };

        public PdfPercent30HatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush, elementsCount, offsets)
        {
        }
    }
}

