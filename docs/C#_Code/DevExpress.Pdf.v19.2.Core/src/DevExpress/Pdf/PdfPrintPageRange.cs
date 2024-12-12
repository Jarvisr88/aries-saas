namespace DevExpress.Pdf
{
    using System;

    public class PdfPrintPageRange
    {
        private readonly int start;
        private readonly int end;

        internal PdfPrintPageRange(int start, int end)
        {
            this.start = start;
            this.end = end;
        }

        public int Start =>
            this.start;

        public int End =>
            this.end;
    }
}

