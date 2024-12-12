namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfCaret
    {
        private readonly PdfTextPosition position;
        private readonly PdfCaretViewData viewData;
        private readonly PdfPoint startCoordinates;
        private PdfRectangle boundingBox;

        public PdfCaret(PdfTextPosition position, PdfCaretViewData viewData, PdfPoint startCoordinates)
        {
            this.position = position;
            this.viewData = viewData;
            this.startCoordinates = startCoordinates;
        }

        public PdfTextPosition Position =>
            this.position;

        public PdfCaretViewData ViewData =>
            this.viewData;

        public PdfPoint StartCoordinates =>
            this.startCoordinates;

        public PdfRectangle BoundingBox
        {
            get
            {
                if (this.boundingBox == null)
                {
                    PdfPoint topLeft = this.viewData.TopLeft;
                    double height = this.viewData.Height;
                    double angle = this.viewData.Angle;
                    double num3 = topLeft.Y - (Math.Cos(angle) * height);
                    double num4 = topLeft.X + (Math.Sin(angle) * height);
                    double left = Math.Min(topLeft.X, num4);
                    double bottom = Math.Min(topLeft.Y, num3);
                    double num7 = Math.Max(topLeft.X, num4);
                    double num8 = Math.Max(topLeft.Y, num3);
                    this.boundingBox = new PdfRectangle(left, bottom, (left == num7) ? (left + 1.0) : num7, (bottom == num8) ? (bottom + 1.0) : num8);
                }
                return this.boundingBox;
            }
        }
    }
}

