namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfToolTipSettings
    {
        private readonly string title;
        private readonly string text;
        private readonly PdfDocumentArea documentArea;

        public PdfToolTipSettings(string title, string text, PdfDocumentArea documentArea)
        {
            this.title = title;
            this.text = text;
            this.documentArea = documentArea;
        }

        public string Title =>
            this.title;

        public string Text =>
            this.text;

        public PdfDocumentArea DocumentArea =>
            this.documentArea;
    }
}

