namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPageTextRange
    {
        private readonly int pageIndex;
        private readonly PdfPageTextPosition startTextPosition;
        private readonly PdfPageTextPosition endTextPosition;
        private readonly bool wholePage;

        public PdfPageTextRange(int pageIndex) : this(pageIndex, new PdfPageTextPosition(0, 0), new PdfPageTextPosition(0, 0))
        {
            this.wholePage = true;
        }

        public PdfPageTextRange(int pageIndex, PdfPageTextPosition startTextPosition, PdfPageTextPosition endTextPosition)
        {
            this.pageIndex = pageIndex;
            this.startTextPosition = startTextPosition;
            this.endTextPosition = endTextPosition;
        }

        public PdfPageTextRange(int pageIndex, int startWordNumber, int startOffset, int endWordNumber, int endOffset) : this(pageIndex, new PdfPageTextPosition(startWordNumber, startOffset), new PdfPageTextPosition(endWordNumber, endOffset))
        {
        }

        public static bool AreEqual(PdfPageTextRange t1, PdfPageTextRange t2) => 
            (t1 != null) ? ((t2 != null) ? ((t1.pageIndex == t2.pageIndex) && ((t1.wholePage == t2.wholePage) && (PdfPageTextPosition.AreEqual(t1.startTextPosition, t2.startTextPosition) && PdfPageTextPosition.AreEqual(t1.endTextPosition, t2.endTextPosition)))) : false) : ReferenceEquals(t2, null);

        public int PageIndex =>
            this.pageIndex;

        public PdfPageTextPosition StartTextPosition =>
            this.startTextPosition;

        public PdfPageTextPosition EndTextPosition =>
            this.endTextPosition;

        public int StartWordNumber =>
            this.startTextPosition.WordNumber;

        public int EndWordNumber =>
            this.endTextPosition.WordNumber;

        public int StartOffset =>
            this.startTextPosition.Offset;

        public int EndOffset =>
            this.endTextPosition.Offset;

        public bool WholePage =>
            this.wholePage;
    }
}

