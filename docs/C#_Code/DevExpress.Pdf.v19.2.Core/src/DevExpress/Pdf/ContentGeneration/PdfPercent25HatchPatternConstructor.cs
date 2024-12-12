namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfPercent25HatchPatternConstructor : PdfPercentHatchPatternConstructor
    {
        private static int[] elementsCount = new int[] { 4, 4, 4, 4 };
        private static int[] offsets;

        static PdfPercent25HatchPatternConstructor()
        {
            int[] numArray2 = new int[4];
            numArray2[1] = 1;
            numArray2[3] = 1;
            offsets = numArray2;
        }

        public PdfPercent25HatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush, elementsCount, offsets)
        {
        }
    }
}

