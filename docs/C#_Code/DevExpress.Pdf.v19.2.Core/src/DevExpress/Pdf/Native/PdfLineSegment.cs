namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfLineSegment
    {
        private readonly PdfPoint point1;
        private readonly PdfPoint point2;
        public double Length =>
            PdfPoint.Distance(this.point1, this.point2);
        public PdfRectangle BoundingBox
        {
            get
            {
                PdfPoint[] points = new PdfPoint[] { this.point1, this.point2 };
                return PdfRectangle.CreateBoundingBox(points);
            }
        }
        public PdfPoint Point1 =>
            this.point1;
        public PdfPoint Point2 =>
            this.point2;
        public PdfLineSegment(PdfPoint point1, PdfPoint point2)
        {
            this.point1 = point1;
            this.point2 = point2;
        }

        public PdfLineSegment Transform(PdfTransformationMatrix matrix) => 
            new PdfLineSegment(matrix.Transform(this.point1), matrix.Transform(this.point2));
    }
}

