namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfTextRange
    {
        private readonly PdfTextPosition startPosition;
        private readonly PdfTextPosition endPosition;

        public PdfTextRange(PdfTextPosition startPosition, PdfTextPosition endPosition)
        {
            this.startPosition = startPosition;
            this.endPosition = endPosition;
        }

        public PdfTextRange(int startPageIndex, int startWordNumber, int startOffset, int endPageIndex, int endWordNumber, int endOffset) : this(new PdfTextPosition(startPageIndex, new PdfPageTextPosition(startWordNumber, startOffset)), new PdfTextPosition(endPageIndex, new PdfPageTextPosition(endWordNumber, endOffset)))
        {
        }

        public PdfTextPosition StartPosition =>
            this.startPosition;

        public PdfTextPosition EndPosition =>
            this.endPosition;
    }
}

