namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfListLogicalStructureElementAttribute : PdfLogicalStructureElementAttribute
    {
        internal const string Owner = "List";
        private const string listNumberingKey = "ListNumbering";
        private readonly PdfListLogicalStructureElementAttributeNumbering listNumbering;

        internal PdfListLogicalStructureElementAttribute(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.listNumbering = PdfEnumToStringConverter.Parse<PdfListLogicalStructureElementAttributeNumbering>(dictionary.GetName("ListNumbering"), true);
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.AddName("O", "List");
            dictionary.AddName("ListNumbering", PdfEnumToStringConverter.Convert<PdfListLogicalStructureElementAttributeNumbering>(this.listNumbering, true));
            return dictionary;
        }

        public PdfListLogicalStructureElementAttributeNumbering ListNumbering =>
            this.listNumbering;
    }
}

