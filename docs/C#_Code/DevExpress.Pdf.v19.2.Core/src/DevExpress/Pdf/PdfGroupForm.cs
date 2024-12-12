namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfGroupForm : PdfForm
    {
        internal const string DictionaryKey = "Group";
        private readonly PdfTransparencyGroup group;

        internal PdfGroupForm(PdfDocumentCatalog catalog, PdfRectangle bBox) : base(catalog, bBox)
        {
            this.group = new PdfTransparencyGroup();
        }

        internal PdfGroupForm(PdfReaderStream stream, PdfReaderDictionary groupDictionary, PdfResources parentResources) : base(stream, parentResources)
        {
            this.group = new PdfTransparencyGroup(groupDictionary);
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            dictionary.Add("Group", this.group);
            return dictionary;
        }

        public PdfTransparencyGroup Group =>
            this.group;
    }
}

