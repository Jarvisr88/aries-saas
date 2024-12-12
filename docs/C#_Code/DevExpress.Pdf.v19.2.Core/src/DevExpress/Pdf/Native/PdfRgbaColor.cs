namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfRgbaColor
    {
        private readonly double a;
        private readonly double r;
        private readonly double g;
        private readonly double b;

        private PdfRgbaColor(PdfRGBColorData data)
        {
            this.r = data.R;
            this.g = data.G;
            this.b = data.B;
            this.a = 1.0;
        }

        public PdfRgbaColor(double r, double g, double b, double a)
        {
            this.a = a;
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public static PdfRgbaColor Create(PdfColor color) => 
            (color != null) ? new PdfRgbaColor(new PdfRGBColorData(color)) : null;

        public PdfColor ToPdfColor()
        {
            double[] components = new double[] { this.r, this.g, this.b };
            return new PdfColor(components);
        }

        public double A =>
            this.a;

        public double R =>
            this.r;

        public double G =>
            this.g;

        public double B =>
            this.b;
    }
}

