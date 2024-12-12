namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfPercent10HatchPatternConstructor : PdfPercentHatchPatternConstructor
    {
        private static int[] elementsCount = new int[] { 2, 2 };
        private static int[] offsets;

        static PdfPercent10HatchPatternConstructor()
        {
            int[] numArray2 = new int[2];
            numArray2[1] = 2;
            offsets = numArray2;
        }

        public PdfPercent10HatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush, elementsCount, offsets)
        {
        }
    }
}

