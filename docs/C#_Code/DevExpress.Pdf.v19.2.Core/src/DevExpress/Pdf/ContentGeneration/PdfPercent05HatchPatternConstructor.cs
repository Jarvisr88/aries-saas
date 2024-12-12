namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfPercent05HatchPatternConstructor : PdfPercentHatchPatternConstructor
    {
        private static int[] elementsCount = new int[] { 1, 1 };
        private static int[] offsets;

        static PdfPercent05HatchPatternConstructor()
        {
            int[] numArray2 = new int[2];
            numArray2[1] = 4;
            offsets = numArray2;
        }

        public PdfPercent05HatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush, elementsCount, offsets)
        {
        }
    }
}

