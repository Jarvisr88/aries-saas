namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfJumpAction : PdfAction
    {
        private const string destinationDictionaryKey = "D";
        private readonly PdfDestinationObject destination;

        protected PdfJumpAction(PdfReaderDictionary dictionary) : base(dictionary)
        {
            this.destination = dictionary.GetDestination("D");
        }

        protected PdfJumpAction(PdfDocument document, PdfDestination destination) : base(document)
        {
            this.destination = new PdfDestinationObject(destination);
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            if (this.destination != null)
            {
                dictionary.Add("D", this.destination.ToWriteableObject(base.DocumentCatalog, objects, this.IsInternal));
            }
            return dictionary;
        }

        protected abstract bool IsInternal { get; }

        public PdfDestination Destination =>
            this.destination?.GetDestination(base.DocumentCatalog, this.IsInternal);
    }
}

