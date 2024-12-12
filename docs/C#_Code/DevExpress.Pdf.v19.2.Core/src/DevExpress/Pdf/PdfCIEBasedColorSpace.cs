namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfCIEBasedColorSpace : PdfCustomColorSpace
    {
        protected const string GammaDictionaryKey = "Gamma";
        private const string writePointDictionaryKey = "WhitePoint";
        private const string blackPointDictionaryKey = "BlackPoint";
        private readonly PdfCIEColor whitePoint;
        private readonly PdfCIEColor blackPoint;

        protected PdfCIEBasedColorSpace(PdfReaderDictionary dictionary)
        {
            IList<object> array = dictionary.GetArray("WhitePoint");
            if (array == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.whitePoint = new PdfCIEColor(array);
            if ((this.whitePoint.X <= 0.0) || ((this.whitePoint.Y != 1.0) || (this.whitePoint.Z <= 0.0)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            IList<object> list2 = dictionary.GetArray("BlackPoint");
            this.blackPoint = (list2 == null) ? new PdfCIEColor() : new PdfCIEColor(list2);
        }

        protected virtual PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.Add("WhitePoint", this.whitePoint.ToArray());
            if (!this.blackPoint.IsEmpty)
            {
                dictionary.Add("BlackPoint", this.blackPoint.ToArray());
            }
            return dictionary;
        }

        protected static PdfReaderDictionary ResolveColorSpaceDictionary(PdfObjectCollection collection, IList<object> array)
        {
            if (array.Count != 2)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfReaderDictionary dictionary = ((collection == null) ? ((PdfReaderDictionary) array[1]) : ((PdfReaderDictionary) collection.TryResolve(array[1], null))) as PdfReaderDictionary;
            if (dictionary == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return dictionary;
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection) => 
            new object[] { new PdfName(this.Name), this.CreateDictionary(collection) };

        public PdfCIEColor WhitePoint =>
            this.whitePoint;

        public PdfCIEColor BlackPoint =>
            this.blackPoint;

        protected abstract string Name { get; }
    }
}

