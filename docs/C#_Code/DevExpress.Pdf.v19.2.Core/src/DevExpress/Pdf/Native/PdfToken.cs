namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfToken : IPdfWritableObject
    {
        private readonly string name;

        public PdfToken(string name)
        {
            this.name = name;
        }

        void IPdfWritableObject.Write(PdfDocumentStream stream, int number)
        {
            stream.WriteString(this.name);
        }

        public string Name =>
            this.name;
    }
}

