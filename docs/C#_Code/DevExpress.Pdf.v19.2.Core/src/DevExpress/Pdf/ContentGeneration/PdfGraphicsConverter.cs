namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using System;

    public static class PdfGraphicsConverter
    {
        public static PdfColor ConvertColor(ARGBColor color) => 
            new PdfColor(ConvertColorToColorComponents(color));

        public static double[] ConvertColorToColorComponents(ARGBColor color) => 
            new double[] { ((float) color.R) / 255f, ((float) color.G) / 255f, ((float) color.B) / 255f };

        public static PdfRectangle ConvertRectangle(DXRectangleF rect)
        {
            float x = rect.X;
            float y = rect.Y;
            return new PdfRectangle((double) x, (double) y, (double) (rect.Width + x), (double) (rect.Height + y));
        }
    }
}

