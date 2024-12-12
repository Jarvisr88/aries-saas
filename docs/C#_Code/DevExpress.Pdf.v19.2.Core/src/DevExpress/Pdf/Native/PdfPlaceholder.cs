namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPlaceholder : IPdfWritableObject
    {
        private readonly int length;
        private int offset;

        public PdfPlaceholder(int length)
        {
            this.length = length;
        }

        void IPdfWritableObject.Write(PdfDocumentStream stream, int number)
        {
            this.offset = (int) stream.Position;
            stream.WriteBytes(new byte[this.length]);
        }

        public int Length =>
            this.length;

        public int Offset =>
            this.offset;
    }
}

