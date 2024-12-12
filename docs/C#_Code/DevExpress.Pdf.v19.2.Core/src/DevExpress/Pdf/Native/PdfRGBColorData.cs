namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfRGBColorData
    {
        private readonly double red;
        private readonly double green;
        private readonly double blue;
        public double R =>
            this.red;
        public double G =>
            this.green;
        public double B =>
            this.blue;
        public PdfRGBColorData(PdfColor color)
        {
            double[] components = PdfDeviceColorSpace.TransformToRGB(color).Components;
            this.red = components[0];
            this.green = components[1];
            this.blue = components[2];
        }
    }
}

