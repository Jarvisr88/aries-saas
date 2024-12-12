namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfDivotHatchPatternConstructor : PdfRectangleBasedHatchPatternConstructor
    {
        private static PdfHatchPatternRect[] rectangles = new PdfHatchPatternRect[] { new PdfHatchPatternRect(0, 5), new PdfHatchPatternRect(0, 7), new PdfHatchPatternRect(3, 1), new PdfHatchPatternRect(3, 3), new PdfHatchPatternRect(4, 2), new PdfHatchPatternRect(7, 6) };

        public PdfDivotHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush, rectangles)
        {
        }
    }
}

