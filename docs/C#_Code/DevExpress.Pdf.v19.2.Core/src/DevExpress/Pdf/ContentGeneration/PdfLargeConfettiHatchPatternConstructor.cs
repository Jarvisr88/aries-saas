namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfLargeConfettiHatchPatternConstructor : PdfRectangleBasedHatchPatternConstructor
    {
        private static PdfHatchPatternRect[] rectangles;

        static PdfLargeConfettiHatchPatternConstructor()
        {
            PdfHatchPatternRect[] rectArray1 = new PdfHatchPatternRect[9];
            rectArray1[0] = new PdfHatchPatternRect(0, 0, 1, 1);
            rectArray1[1] = new PdfHatchPatternRect(2, 0, 2, 2);
            rectArray1[2] = new PdfHatchPatternRect(7, 0, 1, 1);
            rectArray1[3] = new PdfHatchPatternRect(6, 2, 2, 2);
            rectArray1[4] = new PdfHatchPatternRect(3, 3, 2, 2);
            rectArray1[5] = new PdfHatchPatternRect(0, 7, 1, 1);
            rectArray1[6] = new PdfHatchPatternRect(0, 4, 2, 2);
            rectArray1[7] = new PdfHatchPatternRect(4, 6, 2, 2);
            rectArray1[8] = new PdfHatchPatternRect(7, 7, 1, 1);
            rectangles = rectArray1;
        }

        public PdfLargeConfettiHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush, rectangles)
        {
        }
    }
}

