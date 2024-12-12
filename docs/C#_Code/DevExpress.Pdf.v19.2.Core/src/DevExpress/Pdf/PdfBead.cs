namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfBead : PdfObject
    {
        private const string dictionaryTypeName = "Bead";
        private const string threadDictionaryKey = "T";
        private const string nextDictionaryKey = "N";
        private const string previousDictionaryKey = "V";
        private const string pageDictionaryKey = "P";
        private const string locationDictionaryKey = "R";
        private readonly int nextNumber;
        private readonly int prevNumber;
        private readonly PdfArticleThread thread;
        private readonly PdfPage page;
        private readonly PdfRectangle location;
        private PdfBead next;
        private PdfBead previous;

        internal PdfBead(PdfArticleThread thread, PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            if (dictionary == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.thread = thread;
            PdfObjectReference objectReference = dictionary.GetObjectReference("T");
            PdfObjectReference reference2 = dictionary.GetObjectReference("N");
            PdfObjectReference reference3 = dictionary.GetObjectReference("V");
            PdfObjectReference reference4 = dictionary.GetObjectReference("P");
            this.location = dictionary.GetRectangle("R");
            if ((((objectReference != null) && (objectReference.Number != thread.ObjectNumber)) || (reference2 == null)) || ((reference3 == null) || (this.location == null)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.nextNumber = reference2.Number;
            this.prevNumber = reference3.Number;
            if (reference4 != null)
            {
                this.page = dictionary.Objects.GetPage(reference4.Number);
            }
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.Add("Type", new PdfName("Bead"));
            dictionary.Add("N", this.next);
            dictionary.Add("V", this.previous);
            dictionary.Add("P", this.page);
            dictionary.Add("R", this.location);
            dictionary.Add("T", this.thread);
            return dictionary;
        }

        internal int NextNumber =>
            this.nextNumber;

        internal int PrevNumber =>
            this.prevNumber;

        public PdfArticleThread Thread =>
            this.thread;

        public PdfPage Page =>
            this.page;

        public PdfRectangle Location =>
            this.location;

        public PdfBead Next
        {
            get => 
                this.next;
            internal set => 
                this.next = value;
        }

        public PdfBead Previous
        {
            get => 
                this.previous;
            internal set => 
                this.previous = value;
        }
    }
}

