namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfPercent20HatchPatternConstructor : PdfPercentHatchPatternConstructor
    {
        private static int[] elementsCount = new int[] { 2, 2, 2, 2 };
        private static int[] offsets;

        static PdfPercent20HatchPatternConstructor()
        {
            int[] numArray2 = new int[4];
            numArray2[1] = 2;
            numArray2[3] = 2;
            offsets = numArray2;
        }

        public PdfPercent20HatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush, elementsCount, offsets)
        {
        }
    }
}

