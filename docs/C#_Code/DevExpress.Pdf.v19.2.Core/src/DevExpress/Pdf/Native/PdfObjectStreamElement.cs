namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfObjectStreamElement : PdfDocumentItem
    {
        private readonly int elementIndex;
        private readonly int objectStreamNumber;

        public PdfObjectStreamElement(int number, int objectStreamNumber, int elementIndex) : base(number, 0)
        {
            this.objectStreamNumber = objectStreamNumber;
            this.elementIndex = elementIndex;
        }

        public int ElementIndex =>
            this.elementIndex;

        public int ObjectStreamNumber =>
            this.objectStreamNumber;
    }
}

