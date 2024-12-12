namespace DevExpress.Pdf.Native
{
    using System;
    using System.Globalization;

    public class PdfObjectReference : IPdfWritableObject
    {
        private readonly int number;
        private readonly int generation;

        public PdfObjectReference(int number) : this(number, 0)
        {
        }

        public PdfObjectReference(int number, int generation)
        {
            this.number = number;
            this.generation = generation;
        }

        void IPdfWritableObject.Write(PdfDocumentStream stream, int number)
        {
            object[] args = new object[] { this.number };
            stream.WriteString(string.Format(CultureInfo.InvariantCulture, "{0} 0 R", args));
        }

        public int Number =>
            this.number;

        public int Generation =>
            this.generation;
    }
}

