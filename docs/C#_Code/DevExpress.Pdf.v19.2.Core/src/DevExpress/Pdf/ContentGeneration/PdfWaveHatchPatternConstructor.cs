namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfWaveHatchPatternConstructor : PdfRectangleBasedHatchPatternConstructor
    {
        private static PdfHatchPatternRect[] rectangles = new PdfHatchPatternRect[10];

        static PdfWaveHatchPatternConstructor()
        {
            int num = 0;
            for (int i = 0; i < 2; i++)
            {
                int num3 = 4 * i;
                rectangles[num++] = new PdfHatchPatternRect(3, 1 + num3, 2, 1);
                rectangles[num++] = new PdfHatchPatternRect(0, 3 + num3, 2, 1);
                rectangles[num++] = new PdfHatchPatternRect(2, 2 + num3, 1, 1);
                rectangles[num++] = new PdfHatchPatternRect(5, 2 + num3, 1, 1);
                rectangles[num++] = new PdfHatchPatternRect(7, 2 + num3, 1, 1);
            }
        }

        public PdfWaveHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush, rectangles)
        {
        }
    }
}

