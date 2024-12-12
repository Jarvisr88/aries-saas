namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfPageLabel : PdfObject
    {
        private const string dictionaryName = "PageLabel";
        private const string styleKey = "S";
        private const string prefixKey = "P";
        private const string firstNumberKey = "St";
        private readonly PdfPageNumberingStyle numberingStyle;
        private readonly string prefix;
        private readonly int firstNumber = 1;

        internal PdfPageLabel(PdfObjectCollection objects, object value)
        {
            PdfReaderDictionary dictionary = objects.TryResolve(value, null) as PdfReaderDictionary;
            if (dictionary == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.numberingStyle = dictionary.GetEnumName<PdfPageNumberingStyle>("S");
            string text1 = dictionary.GetString("P");
            string text2 = text1;
            if (text1 == null)
            {
                string local1 = text1;
                text2 = string.Empty;
            }
            this.prefix = text2;
            int? integer = dictionary.GetInteger("St");
            this.firstNumber = (integer != null) ? integer.GetValueOrDefault() : 1;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("Type", new PdfName("PageLabel"));
            dictionary.AddEnumName<PdfPageNumberingStyle>("S", this.numberingStyle);
            dictionary.AddNotNullOrEmptyString("P", this.prefix);
            dictionary.Add("St", this.firstNumber, 1);
            return dictionary;
        }

        public PdfPageNumberingStyle NumberingStyle =>
            this.numberingStyle;

        public string Prefix =>
            this.prefix;

        public int FirstNumber =>
            this.firstNumber;
    }
}

