namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPageTextPosition
    {
        private readonly int wordNumber;
        private readonly int offset;

        public PdfPageTextPosition(int wordNumber, int offset)
        {
            this.wordNumber = wordNumber;
            this.offset = offset;
        }

        public static bool AreEqual(PdfPageTextPosition p1, PdfPageTextPosition p2) => 
            (p1 != null) ? ((p2 != null) ? ((p1.wordNumber == p2.wordNumber) && (p1.offset == p2.offset)) : false) : ReferenceEquals(p2, null);

        public int WordNumber =>
            this.wordNumber;

        public int Offset =>
            this.offset;
    }
}

