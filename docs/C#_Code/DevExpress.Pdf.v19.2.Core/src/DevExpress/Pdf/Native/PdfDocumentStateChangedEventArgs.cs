namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfDocumentStateChangedEventArgs
    {
        private readonly PdfDocumentStateChangedFlags flags;

        public PdfDocumentStateChangedEventArgs(PdfDocumentStateChangedFlags flags)
        {
            this.flags = flags;
        }

        public PdfDocumentStateChangedFlags Flags =>
            this.flags;
    }
}

