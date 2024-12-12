namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class Pdf3dData : PdfObject
    {
        protected Pdf3dData(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
        }

        protected virtual PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddEnumName<Pdf3dDataType>("Type", this.Type);
            return dictionary;
        }

        public abstract Pdf3dDataType Type { get; }
    }
}

