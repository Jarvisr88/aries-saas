namespace DevExpress.Pdf
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfVertex
    {
        private readonly PdfPoint point;
        private readonly PdfColor color;
        public PdfPoint Point =>
            this.point;
        public PdfColor Color =>
            this.color;
        internal PdfVertex(PdfPoint point, PdfColor color)
        {
            this.point = point;
            this.color = color;
        }
    }
}

