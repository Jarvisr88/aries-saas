namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfIndirectObject : PdfObjectPointer
    {
        private readonly PdfDataStream stream;

        public PdfIndirectObject(int number, int generation, long offset, PdfDataStream stream) : base(number, generation, offset)
        {
            this.stream = stream;
        }

        public PdfDataStream Stream =>
            this.stream;
    }
}

