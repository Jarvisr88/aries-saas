namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfComment
    {
        private readonly string text;

        public PdfComment(string text)
        {
            this.text = text;
        }

        public string Text =>
            this.text;
    }
}

