namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfPlaidHatchPatternConstructor : PdfRectangleBasedHatchPatternConstructor
    {
        private static PdfHatchPatternRect[] rectangles = new PdfHatchPatternRect[0x11];

        static PdfPlaidHatchPatternConstructor()
        {
            int index = 0;
            int x = 0;
            while (x < 8)
            {
                int y = x % 2;
                while (true)
                {
                    if (y >= 4)
                    {
                        x++;
                        break;
                    }
                    rectangles[index++] = new PdfHatchPatternRect(x, y);
                    y += 2;
                }
            }
            rectangles[index] = new PdfHatchPatternRect(0, 4, 4, 4);
        }

        public PdfPlaidHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush, rectangles)
        {
        }
    }
}

