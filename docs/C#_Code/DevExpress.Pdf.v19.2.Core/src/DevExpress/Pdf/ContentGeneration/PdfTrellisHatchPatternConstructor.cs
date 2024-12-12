namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using System;

    public class PdfTrellisHatchPatternConstructor : PdfRectangleBasedHatchPatternConstructor
    {
        private static PdfHatchPatternRect[] rectangles = new PdfHatchPatternRect[0x24];

        static PdfTrellisHatchPatternConstructor()
        {
            int num = 0;
            int num2 = 0;
            while (num2 < 4)
            {
                rectangles[num++] = new PdfHatchPatternRect(0, num2 * 2, 8, 1);
                int num3 = 0;
                while (true)
                {
                    if (num3 >= 4)
                    {
                        num2++;
                        break;
                    }
                    int num4 = num3 * 4;
                    int num5 = num2 * 4;
                    rectangles[num++] = new PdfHatchPatternRect(num4 + 1, num5 + 1, 2, 1);
                    rectangles[num++] = new PdfHatchPatternRect(num4 - 1, num5 + 3, 2, 1);
                    num3++;
                }
            }
        }

        public PdfTrellisHatchPatternConstructor(DXHatchBrush hatchBrush) : base(hatchBrush, rectangles)
        {
        }

        protected override PdfLineCapStyle LineCapStyle =>
            PdfLineCapStyle.Butt;
    }
}

