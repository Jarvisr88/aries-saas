namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfSmallConfettiHatchPatternConstructor : PdfRectangleBasedHatchPatternConstructor
    {
        private static PdfHatchPatternRect[] rectangles = new PdfHatchPatternRect[] { new PdfHatchPatternRect(0, 0), new PdfHatchPatternRect(4, 1), new PdfHatchPatternRect(1, 2), new PdfHatchPatternRect(6, 3), new PdfHatchPatternRect(3, 4), new PdfHatchPatternRect(2, 6), new PdfHatchPatternRect(7, 5), new PdfHatchPatternRect(5, 7) };

        public PdfSmallConfettiHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush, rectangles)
        {
        }
    }
}

