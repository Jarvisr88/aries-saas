namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfPageActions
    {
        internal const string OpenedKey = "O";
        internal const string ClosedKey = "C";
        private readonly PdfAction opened;
        private readonly PdfAction closed;

        internal PdfPageActions(PdfReaderDictionary dictionary)
        {
            this.opened = dictionary.GetAction("O");
            this.closed = dictionary.GetAction("C");
        }

        public PdfAction Opened =>
            this.opened;

        public PdfAction Closed =>
            this.closed;
    }
}

