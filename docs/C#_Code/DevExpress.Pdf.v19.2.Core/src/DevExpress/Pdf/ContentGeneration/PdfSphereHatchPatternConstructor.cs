namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfSphereHatchPatternConstructor : PdfRectangleBasedHatchPatternConstructor
    {
        private static PdfHatchPatternRect[] rectangles = new PdfHatchPatternRect[12];

        static PdfSphereHatchPatternConstructor()
        {
            int num = 0;
            int num2 = 0;
            while (num2 < 2)
            {
                int x = num2 * 4;
                int num4 = 0;
                while (true)
                {
                    if (num4 >= 2)
                    {
                        num2++;
                        break;
                    }
                    int y = 1 + (4 * num4);
                    rectangles[num++] = new PdfHatchPatternRect(x, y, 1, 3);
                    rectangles[num++] = new PdfHatchPatternRect(y, x, 3, 1);
                    num4++;
                }
            }
            rectangles[num++] = new PdfHatchPatternRect(1, 6, 2, 2);
            rectangles[num++] = new PdfHatchPatternRect(3, 5, 1, 3);
            rectangles[num++] = new PdfHatchPatternRect(4, 2, 3, 2);
            rectangles[num++] = new PdfHatchPatternRect(7, 1, 1, 3);
        }

        public PdfSphereHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush, rectangles)
        {
        }
    }
}

