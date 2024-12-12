namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfCaretViewData
    {
        private readonly PdfPoint topLeft;
        private readonly double height;
        private readonly double angle;
        public PdfPoint TopLeft =>
            this.topLeft;
        public double Height =>
            this.height;
        public double Angle =>
            this.angle;
        public PdfCaretViewData(PdfPoint topLeft, double height, double angle)
        {
            this.topLeft = topLeft;
            this.height = height;
            this.angle = angle;
        }
    }
}

