namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfTextPosition : PdfPageTextPosition
    {
        private readonly int pageIndex;

        public PdfTextPosition(int pageIndex, PdfPageTextPosition position) : base(position.WordNumber, position.Offset)
        {
            this.pageIndex = pageIndex;
        }

        public PdfTextPosition(int pageIndex, int wordNumber, int offset) : base(wordNumber, offset)
        {
            this.pageIndex = pageIndex;
        }

        public int PageIndex =>
            this.pageIndex;
    }
}

