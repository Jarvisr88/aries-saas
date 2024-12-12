namespace DevExpress.Pdf.Native
{
    using System;

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class PdfFieldNameAttribute : Attribute
    {
        private readonly string text;
        private readonly string alternateText;

        public PdfFieldNameAttribute(string text) : this(text, null)
        {
        }

        public PdfFieldNameAttribute(string text, string alternateText)
        {
            this.text = text;
            this.alternateText = alternateText;
        }

        public string Text =>
            this.text;

        public string AlternateText =>
            this.alternateText;
    }
}

