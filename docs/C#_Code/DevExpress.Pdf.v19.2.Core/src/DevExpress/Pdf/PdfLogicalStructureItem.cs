namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfLogicalStructureItem : PdfObject
    {
        protected PdfLogicalStructureItem(int objectNumber) : base(objectNumber)
        {
        }

        protected abstract PdfWriterDictionary CreateDictionary(PdfObjectCollection collection);
        internal static PdfLogicalStructureItem Parse(PdfLogicalStructure logicalStructure, PdfLogicalStructureEntry parent, object value)
        {
            PdfLogicalStructureElement element = parent as PdfLogicalStructureElement;
            PdfReaderDictionary dictionary = value as PdfReaderDictionary;
            if (dictionary == null)
            {
                if ((element == null) || !(value is int))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return new PdfLogicalStructureMcidContent((int) value);
            }
            string name = dictionary.GetName("Type");
            if (name == "MCR")
            {
                if (element == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return new PdfLogicalStructureMarkedContentReference(dictionary);
            }
            if (name != "OBJR")
            {
                return PdfLogicalStructureElement.Create(logicalStructure, parent, dictionary);
            }
            if (element == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfLogicalStructureContentItem item = new PdfLogicalStructureContentItem(element.Page, dictionary);
            return ((item.Content == null) ? null : item);
        }

        protected internal virtual void Resolve()
        {
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection) => 
            this.CreateDictionary(collection);

        protected internal virtual object Write(PdfObjectCollection collection) => 
            collection.AddObject((PdfObject) this);

        protected internal virtual PdfPage ContainingPage =>
            null;
    }
}

