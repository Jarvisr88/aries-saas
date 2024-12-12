namespace DevExpress.Pdf
{
    using System;

    public class PdfDocumentPosition
    {
        private const int nearDistance = 3;
        private readonly int pageNumber;
        private readonly PdfPoint point;

        public PdfDocumentPosition(int pageNumber, PdfPoint point)
        {
            this.pageNumber = pageNumber;
            this.point = point;
        }

        internal bool NearTo(PdfDocumentPosition position)
        {
            PdfPoint point = position.point;
            return ((this.pageNumber == position.pageNumber) && ((Math.Abs((double) (this.point.X - point.X)) <= 3.0) && (Math.Abs((double) (this.point.Y - point.Y)) <= 3.0)));
        }

        internal int PageIndex =>
            this.pageNumber - 1;

        public int PageNumber =>
            this.pageNumber;

        public PdfPoint Point =>
            this.point;
    }
}

