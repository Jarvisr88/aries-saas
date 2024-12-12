namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfShingleHatchPatternConstructor : PdfRectangleBasedHatchPatternConstructor
    {
        private static PdfHatchPatternRect[] rectangles = new PdfHatchPatternRect[10];

        static PdfShingleHatchPatternConstructor()
        {
            int index = 0;
            for (int i = 0; i < 3; i++)
            {
                rectangles[index++] = new PdfHatchPatternRect(5 - i, i + 1);
                rectangles[index++] = new PdfHatchPatternRect(i, i + 1);
            }
            rectangles[index++] = new PdfHatchPatternRect(6, 0, 2, 1);
            rectangles[index++] = new PdfHatchPatternRect(4, 4, 2, 1);
            rectangles[index++] = new PdfHatchPatternRect(6, 5);
            rectangles[index] = new PdfHatchPatternRect(7, 6, 1, 2);
        }

        public PdfShingleHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush, rectangles)
        {
        }
    }
}

