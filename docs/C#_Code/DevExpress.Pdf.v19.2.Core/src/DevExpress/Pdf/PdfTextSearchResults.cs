namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfTextSearchResults
    {
        internal static PdfTextSearchResults NotFound = new PdfTextSearchResults(null, 0, null, null, PdfTextSearchStatus.NotFound);
        internal static PdfTextSearchResults Finished = new PdfTextSearchResults(null, 0, null, null, PdfTextSearchStatus.Finished);
        private readonly PdfPage page;
        private readonly int pageNumber;
        private readonly IList<PdfWord> words;
        private readonly IList<PdfOrientedRectangle> rectangles;
        private readonly PdfTextSearchStatus status;
        private PdfRectangle boundingRectangle;

        internal PdfTextSearchResults(PdfPage page, int pageNumber, IList<PdfWord> words, IList<PdfOrientedRectangle> rectangles, PdfTextSearchStatus status)
        {
            this.page = page;
            this.pageNumber = pageNumber;
            this.words = words;
            this.rectangles = rectangles;
            this.status = status;
        }

        public PdfPage Page =>
            this.page;

        [Obsolete("The PageIndex property is now obsolete. Use the PageNumber property instead.")]
        public int PageIndex =>
            this.pageNumber - 1;

        public int PageNumber =>
            this.pageNumber;

        public IList<PdfWord> Words =>
            this.words;

        public IList<PdfOrientedRectangle> Rectangles =>
            this.rectangles;

        public PdfTextSearchStatus Status =>
            this.status;

        internal PdfRectangle BoundingRectangle
        {
            get
            {
                if (this.boundingRectangle == null)
                {
                    if (this.rectangles.Count == 0)
                    {
                        return new PdfRectangle(0.0, 0.0, 1.0, 1.0);
                    }
                    double maxValue = double.MaxValue;
                    double num2 = double.MaxValue;
                    double minValue = double.MinValue;
                    double num4 = double.MinValue;
                    foreach (PdfOrientedRectangle rectangle in this.rectangles)
                    {
                        maxValue = PdfMathUtils.Min(maxValue, rectangle.Left);
                        num2 = PdfMathUtils.Min(num2, rectangle.Bottom);
                        minValue = PdfMathUtils.Max(minValue, rectangle.Right);
                        num4 = PdfMathUtils.Max(num4, rectangle.Top);
                    }
                    this.boundingRectangle = new PdfRectangle(PdfMathUtils.Min(maxValue, minValue), PdfMathUtils.Min(num2, num4), PdfMathUtils.Max(maxValue, minValue), PdfMathUtils.Max(num2, num4));
                }
                return this.boundingRectangle;
            }
        }

        internal PdfPoint Position
        {
            get
            {
                PdfPoint topLeft = this.BoundingRectangle.TopLeft;
                return new PdfPoint(topLeft.X + (this.boundingRectangle.Width / 2.0), topLeft.Y - (this.boundingRectangle.Height / 2.0));
            }
        }
    }
}

