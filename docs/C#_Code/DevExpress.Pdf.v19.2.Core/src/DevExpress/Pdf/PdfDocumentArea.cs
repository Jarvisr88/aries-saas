namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfDocumentArea
    {
        private readonly int pageNumber;
        private readonly PdfRectangle area;

        public PdfDocumentArea(int pageNumber, PdfRectangle area)
        {
            this.pageNumber = pageNumber;
            this.area = area;
        }

        public static PdfDocumentArea Create(PdfDocumentPosition position1, PdfDocumentPosition position2)
        {
            PdfDocumentArea area;
            try
            {
                int pageNumber = position1.PageNumber;
                if (pageNumber != position2.PageNumber)
                {
                    area = null;
                }
                else
                {
                    PdfPoint point = position1.Point;
                    PdfPoint point2 = position2.Point;
                    area = new PdfDocumentArea(pageNumber, new PdfRectangle(PdfMathUtils.Min(point.X, point2.X), PdfMathUtils.Min(point.Y, point2.Y), PdfMathUtils.Max(point.X, point2.X), PdfMathUtils.Max(point.Y, point2.Y)));
                }
            }
            catch
            {
                area = null;
            }
            return area;
        }

        internal int PageIndex =>
            this.pageNumber - 1;

        public int PageNumber =>
            this.pageNumber;

        public PdfRectangle Area =>
            this.area;
    }
}

