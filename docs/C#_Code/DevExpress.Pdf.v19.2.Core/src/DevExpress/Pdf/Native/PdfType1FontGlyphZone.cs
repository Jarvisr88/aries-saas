namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfType1FontGlyphZone
    {
        private readonly double bottom;
        private readonly double top;
        public double Bottom =>
            this.bottom;
        public double Top =>
            this.top;
        public PdfType1FontGlyphZone(double bottom, double top)
        {
            this.bottom = bottom;
            this.top = top;
        }
    }
}

