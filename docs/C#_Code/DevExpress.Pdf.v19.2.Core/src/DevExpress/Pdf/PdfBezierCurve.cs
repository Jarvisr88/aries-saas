namespace DevExpress.Pdf
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfBezierCurve
    {
        private readonly PdfVertex vertex1;
        private readonly PdfPoint controlPoint1;
        private readonly PdfPoint controlPoint2;
        private readonly PdfVertex vertex2;
        public PdfVertex Vertex1 =>
            this.vertex1;
        public PdfPoint ControlPoint1 =>
            this.controlPoint1;
        public PdfPoint ControlPoint2 =>
            this.controlPoint2;
        public PdfVertex Vertex2 =>
            this.vertex2;
        internal PdfBezierCurve(PdfVertex vertex1, PdfPoint controlPoint1, PdfPoint controlPoint2, PdfVertex vertex2)
        {
            this.vertex1 = vertex1;
            this.controlPoint1 = controlPoint1;
            this.controlPoint2 = controlPoint2;
            this.vertex2 = vertex2;
        }
    }
}

