namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfTarget
    {
        private readonly PdfTargetMode mode;
        private readonly int pageIndex;
        private readonly double? x;
        private readonly double? y;
        private readonly double width;
        private readonly double height;
        private readonly double? zoom;

        public PdfTarget(PdfTargetMode mode, int pageIndex) : this(mode, pageIndex, nullable, nullable, 0.0, 0.0, nullable)
        {
            double? nullable = null;
            nullable = null;
            nullable = null;
        }

        public PdfTarget(PdfTargetMode mode, int pageIndex, PdfRectangle rectangle) : this(mode, pageIndex, new double?(rectangle.Left), new double?(rectangle.Top), rectangle.Width, rectangle.Height, nullable)
        {
        }

        public PdfTarget(PdfTargetMode mode, int pageIndex, double? x, double? y) : this(mode, pageIndex, x, y, 0.0, 0.0, nullable)
        {
        }

        public PdfTarget(int pageIndex, double? x, double? y, double? zoom) : this(PdfTargetMode.XYZ, pageIndex, x, y, 0.0, 0.0, zoom)
        {
        }

        private PdfTarget(PdfTargetMode mode, int pageIndex, double? x, double? y, double width, double height, double? zoom)
        {
            this.mode = mode;
            this.pageIndex = pageIndex;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.zoom = zoom;
        }

        public PdfTargetMode Mode =>
            this.mode;

        public int PageIndex =>
            this.pageIndex;

        public double? X =>
            this.x;

        public double? Y =>
            this.y;

        public double Width =>
            this.width;

        public double Height =>
            this.height;

        public double? Zoom =>
            this.zoom;
    }
}

