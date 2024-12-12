namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfRichMediaInstance : PdfObject
    {
        private const string assetDictionaryKey = "Asset";
        private const string parametersDictionaryKey = "Params";
        private readonly PdfFileSpecification asset;
        private readonly PdfRichMediaContentType contentType;
        private readonly PdfRichMediaParams parameters;

        internal PdfRichMediaInstance(IEnumerable<KeyValuePair<string, PdfFileSpecification>> assets, PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            PdfObjectReference objectReference = dictionary.GetObjectReference("Asset");
            if ((assets == null) || (objectReference == null))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int number = objectReference.Number;
            foreach (KeyValuePair<string, PdfFileSpecification> pair in assets)
            {
                PdfFileSpecification specification = pair.Value;
                if (specification.ObjectNumber == number)
                {
                    this.asset = specification;
                    break;
                }
            }
            if (this.asset == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfRichMediaContentType? richMediaContentType = dictionary.GetRichMediaContentType();
            this.contentType = (richMediaContentType != null) ? richMediaContentType.GetValueOrDefault() : PdfRichMediaContentType.Flash;
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("Params");
            if (dictionary2 != null)
            {
                if (this.contentType != PdfRichMediaContentType.Flash)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.parameters = new PdfRichMediaParams(dictionary2);
            }
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("Asset", this.asset);
            if (this.contentType != PdfRichMediaContentType.Flash)
            {
                dictionary.AddEnumName<PdfRichMediaContentType>("Subtype", this.contentType);
            }
            dictionary.Add("Params", this.parameters);
            return dictionary;
        }

        public PdfFileSpecification Asset =>
            this.asset;

        public PdfRichMediaContentType ContentType =>
            this.contentType;

        public PdfRichMediaParams Parameters =>
            this.parameters;
    }
}

