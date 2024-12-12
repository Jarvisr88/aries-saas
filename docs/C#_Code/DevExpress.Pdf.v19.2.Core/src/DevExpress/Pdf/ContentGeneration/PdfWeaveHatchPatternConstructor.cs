namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfWeaveHatchPatternConstructor : PdfRectangleBasedHatchPatternConstructor
    {
        private static PdfHatchPatternRect[] rectangles = new PdfHatchPatternRect[0x13];

        static PdfWeaveHatchPatternConstructor()
        {
            int index = 0;
            for (int i = 0; i < 5; i++)
            {
                rectangles[index++] = new PdfHatchPatternRect(i, 4 - i);
                rectangles[index++] = new PdfHatchPatternRect(1 + i, 7 - i);
            }
            for (int j = 0; j < 3; j++)
            {
                rectangles[index++] = new PdfHatchPatternRect(5 + j, 1 + j);
                rectangles[index++] = new PdfHatchPatternRect(7 - j, 7 - j);
            }
            for (int k = 0; k < 2; k++)
            {
                rectangles[index++] = new PdfHatchPatternRect(k, k);
            }
            rectangles[index] = new PdfHatchPatternRect(3, 7);
        }

        public PdfWeaveHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush, rectangles)
        {
        }
    }
}

