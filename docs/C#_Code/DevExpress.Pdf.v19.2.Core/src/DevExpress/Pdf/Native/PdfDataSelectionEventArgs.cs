namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfDataSelectionEventArgs : EventArgs
    {
        private readonly PdfDocumentPosition position;

        public PdfDataSelectionEventArgs(PdfDocumentPosition position)
        {
            this.position = position;
        }

        public PdfDocumentPosition Position =>
            this.position;
    }
}

