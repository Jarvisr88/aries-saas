namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfLogicalStructureElementAttribute : PdfObject
    {
        protected const string OwnerKey = "O";

        protected PdfLogicalStructureElementAttribute(int objectNumber) : base(objectNumber)
        {
        }

        protected abstract PdfWriterDictionary CreateDictionary(PdfObjectCollection collection);
        internal static PdfLogicalStructureElementAttribute Parse(PdfReaderDictionary dictionary)
        {
            string name = dictionary.GetName("O");
            return ((name == "Layout") ? ((PdfLogicalStructureElementAttribute) PdfLayoutLogicalStructureElementAttribute.ParseAttribute(dictionary)) : ((name == "List") ? ((PdfLogicalStructureElementAttribute) new PdfListLogicalStructureElementAttribute(dictionary)) : ((name == "PrintField") ? ((PdfLogicalStructureElementAttribute) new PdfPrintFieldLogicalStructureElementAttribute(dictionary)) : ((name == "Table") ? ((PdfLogicalStructureElementAttribute) new PdfTableLogicalStructureElementAttribute(dictionary)) : ((PdfLogicalStructureElementAttribute) new PdfCustomLogicalStructureElementAttribute(name, dictionary))))));
        }

        internal static PdfLogicalStructureElementAttribute[] Parse(PdfObjectCollection objects, object value)
        {
            IList<object> list = objects.TryResolve(value, null) as IList<object>;
            if (list == null)
            {
                PdfLogicalStructureElementAttribute attribute = ParseAttribute(objects, value);
                if (attribute == null)
                {
                    return null;
                }
                return new PdfLogicalStructureElementAttribute[] { attribute };
            }
            int count = list.Count;
            PdfLogicalStructureElementAttribute[] attributeArray = new PdfLogicalStructureElementAttribute[count];
            for (int i = 0; i < count; i++)
            {
                PdfLogicalStructureElementAttribute attribute2 = ParseAttribute(objects, list[i]);
                if (attribute2 == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                attributeArray[i] = attribute2;
            }
            return attributeArray;
        }

        private static PdfLogicalStructureElementAttribute ParseAttribute(PdfObjectCollection objects, object value) => 
            objects.GetObject<PdfLogicalStructureElementAttribute>(value, new Func<PdfReaderDictionary, PdfLogicalStructureElementAttribute>(PdfLogicalStructureElementAttribute.Parse));

        protected internal override object ToWritableObject(PdfObjectCollection collection) => 
            this.CreateDictionary(collection);
    }
}

